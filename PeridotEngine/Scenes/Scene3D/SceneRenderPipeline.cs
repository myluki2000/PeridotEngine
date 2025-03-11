using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.ECS.Systems;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.Graphics.PostProcessing;
using PeridotEngine.Misc;
using static PeridotEngine.Graphics.Effects.SkydomeEffect;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace PeridotEngine.Scenes.Scene3D
{
    public class SceneRenderPipeline : IDisposable
    {
        /// <summary>
        /// Enables or disables screen space ambient occlusion for this render pipeline.
        /// </summary>
        public bool AmbientOcclusionEnabled { get; set; } = true;
        /// <summary>
        /// Enables or disables post processing depth of field for this render pipeline.
        /// </summary>
        public bool DepthOfFieldEnabled { get; set; } = false;
        /// <summary>
        /// Enables or disables post processing fog for this render pipeline.
        /// </summary>
        public bool FogEnabled { get; set; } = true;

        /// <summary>
        /// Set to 0 to disable MSAA. Set to higher values to set the preferred sample count
        /// for MSAA (a different sample) count may be chosen if the device does not support
        /// the preferred sample count set using this property.
        /// Defaults to 0.
        /// </summary>
        public int PreferredMultiSampleCount
        {
            get => preferredMultiSampleCount;
            set
            {
                preferredMultiSampleCount = value;
                // re-init render targets using the new sample count setting.
                InitRts();
            }
        }

        /// <summary>
        /// True during the process of the render pipeline refreshing/replacing rendertargets.
        /// </summary>
        public bool IsRefreshingRts { get; private set; }

        private int preferredMultiSampleCount = 0;

        private readonly Scene3D scene;

        private readonly MeshRenderingSystem meshRenderingSystem;
        private readonly SunShadowMapSystem sunShadowMapSystem;

        
        private RenderTarget2D colorRt;
        private RenderTarget2D depthRt;
        private RenderTarget2D normalRt;
        private RenderTarget2D objectPickingRt;

        // post processing render targets
        private RenderTarget2D colorRtIn;
        private RenderTarget2D colorRtOut;

        private readonly SimplePostProcessingEffect postProcessingEffect;
        private readonly SsaoPostProcessingEffect ssaoPostProcessingEffect;
        private readonly DepthOfFieldPostProcessingEffect dofPostProcessingEffect;
        

        public SceneRenderPipeline(Scene3D scene)
        {
            this.scene = scene;

            meshRenderingSystem = new MeshRenderingSystem(scene);
            sunShadowMapSystem = new SunShadowMapSystem(scene);

            Globals.BackBufferSizeChanged += (sender, args) => InitRts();
            InitRts();

            postProcessingEffect = new SimplePostProcessingEffect()
            {
                FogEnabled = true,
                FogStart = 20f,
                FogEnd = 50f,
                FogColor = Color.CornflowerBlue,
                ScreenSpaceAmbientOcclusionEnabled = false, // set later during render according to property
            };
            ssaoPostProcessingEffect = new SsaoPostProcessingEffect();
            dofPostProcessingEffect = new DepthOfFieldPostProcessingEffect();
        }

        public void Render(RenderTarget2D? target)
        {
            if (IsRefreshingRts)
                return;

            GraphicsDevice gd = Globals.GraphicsDevice;
            gd.SamplerStates[0] = SamplerState.PointWrap;

            if(scene.Camera.AllowAutomaticAspectRatioAdjustment)
                scene.Camera.AspectRatio = (float)Globals.GraphicsDevice.PresentationParameters.BackBufferWidth /
                                           Globals.GraphicsDevice.PresentationParameters.BackBufferHeight;

            scene.Resources.EffectPool.UpdateEffectCameraData(scene.Camera);

            // TODO: It's only necessary to re-render the shadow map if scene geometry changes
            Texture2D? shadowMap = sunShadowMapSystem.GenerateShadowMap(meshRenderingSystem, out Vector3 lightPosition, out Matrix lightViewProj);

            scene.Resources.EffectPool.UpdateEffectShadows(shadowMap, lightPosition, lightViewProj);

            // TODO: Might need different blend states for the different render targets
            // see https://stackoverflow.com/questions/53565844/only-do-alpha-blending-on-certain-output-colors
            gd.BlendState = BlendState.Opaque;

            // render scene meshes to color, normal and depth buffers
            gd.SetRenderTargets(colorRt, depthRt, normalRt, objectPickingRt);
            gd.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.White,
                float.MaxValue, 0);

            meshRenderingSystem.RenderMeshes();

            // render skydome
            gd.SetVertexBuffer(scene.Skydome.VertexBuffer);
            gd.Indices = scene.Skydome.IndexBuffer;

            Matrix projection = scene.Camera.GetProjectionMatrix();

            scene.Skydome.EffectProperties.Effect.World = Matrix.Identity;
            scene.Skydome.EffectProperties.Effect.View = scene.Camera.GetRotationMatrix();
            scene.Skydome.EffectProperties.Effect.Projection = projection;
            scene.Skydome.EffectProperties.Effect.Apply();
            foreach (EffectPass pass in scene.Skydome.EffectProperties.Technique.Passes)
            {
                pass.Apply();
                gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, scene.Skydome.IndexBuffer.IndexCount / 3);
            }

            if (AmbientOcclusionEnabled)
            {
                gd.SetRenderTarget(colorRtIn);
                ssaoPostProcessingEffect.UpdateParameters(colorRt, depthRt, normalRt, projection, scene.Camera.NearPlane,
                    scene.Camera.FarPlane);
                RenderTargetRenderer.RenderRenderTarget(ssaoPostProcessingEffect);
            }

            // combine
            gd.SetRenderTarget(colorRtOut);
            postProcessingEffect.UpdateParameters(colorRt, depthRt, null, projection, scene.Camera.NearPlane, scene.Camera.FarPlane);
            postProcessingEffect.ScreenSpaceAmbientOcclusionEnabled = AmbientOcclusionEnabled;
            postProcessingEffect.FogEnabled = FogEnabled;
            postProcessingEffect.AmbientOcclusionTexture = colorRtIn;
            RenderTargetRenderer.RenderRenderTarget(postProcessingEffect);

            (colorRtIn, colorRtOut) = (colorRtOut, colorRtIn);

            if (DepthOfFieldEnabled)
            {
                // depth of field horizontal blur pass
                gd.SetRenderTarget(colorRtOut);
                dofPostProcessingEffect.BlurDirection = DepthOfFieldPostProcessingEffect.Direction.HORIZONTAL;
                dofPostProcessingEffect.UpdateParameters(colorRtIn, depthRt, null, projection, scene.Camera.NearPlane,
                    scene.Camera.FarPlane);
                RenderTargetRenderer.RenderRenderTarget(dofPostProcessingEffect);

                (colorRtIn, colorRtOut) = (colorRtOut, colorRtIn);

                // depth of field vertical blur pass
                gd.SetRenderTarget(colorRtOut);
                dofPostProcessingEffect.BlurDirection = DepthOfFieldPostProcessingEffect.Direction.VERTICAL;
                dofPostProcessingEffect.UpdateParameters(colorRtIn, depthRt, null, projection, scene.Camera.NearPlane, scene.Camera.FarPlane);
                RenderTargetRenderer.RenderRenderTarget(dofPostProcessingEffect);

                (colorRtIn, colorRtOut) = (colorRtOut, colorRtIn);
            }

            // final render to screen
            var r = gd.GetRenderTargets();
            gd.SetRenderTarget(target);
            postProcessingEffect.ScreenSpaceAmbientOcclusionEnabled = false;
            postProcessingEffect.FogEnabled = false;
            postProcessingEffect.UpdateParameters(colorRtIn, null, null, projection, scene.Camera.NearPlane, scene.Camera.FarPlane);
            RenderTargetRenderer.RenderRenderTarget(postProcessingEffect);

        }

        public uint? GetObjectIdAtScreenPos(Point screenPos)
        {
            uint[] objectIds = new uint[1];

            Vector2 relativePos = screenPos.ToVector2() / Globals.GraphicsDevice.PresentationParameters.BackBufferSize().ToVector2();

            if (relativePos.X < 0 || relativePos.Y < 0 || relativePos.X >= 1 || relativePos.Y >= 1)
                return null;

            Vector2 rtPos = relativePos * objectPickingRt.Bounds.Size.ToVector2();

            objectPickingRt.GetData(0, new Rectangle(rtPos.ToPoint(), new(1, 1)), objectIds, 0, 1);
            return objectIds[0];
        }

        private void InitRts()
        {
            IsRefreshingRts = true;
            colorRtIn?.Dispose();
            colorRtOut?.Dispose();
            depthRt?.Dispose();
            normalRt?.Dispose();

            // multisampling sample count must be the same on all render targets bound at the same time!
            colorRt = new(Globals.GraphicsDevice, Globals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24,
                preferredMultiSampleCount, RenderTargetUsage.DiscardContents);
            depthRt = new(Globals.GraphicsDevice, Globals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Single, DepthFormat.Depth24,
                preferredMultiSampleCount, RenderTargetUsage.DiscardContents);
            normalRt = new(Globals.GraphicsDevice, Globals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.HalfVector4, DepthFormat.Depth24,
                preferredMultiSampleCount, RenderTargetUsage.DiscardContents);
            objectPickingRt = new(Globals.GraphicsDevice, Globals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Single, DepthFormat.Depth24,
                preferredMultiSampleCount, RenderTargetUsage.DiscardContents);

            colorRtIn = new(Globals.GraphicsDevice, Globals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
            colorRtOut = new(Globals.GraphicsDevice, Globals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Globals.GraphicsDevice.PresentationParameters.BackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
            IsRefreshingRts = false;
        }

        ~SceneRenderPipeline()
        {
            Dispose();
        }

        public void Dispose()
        {
            colorRtIn?.Dispose();
            colorRtOut?.Dispose();
            depthRt?.Dispose();
            normalRt?.Dispose();
            objectPickingRt?.Dispose();
            sunShadowMapSystem?.Dispose();
        }
    }
}
