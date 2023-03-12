using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.ECS.Systems;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.Graphics.PostProcessing;
using static PeridotEngine.Graphics.Effects.SkydomeEffect;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Scenes.Scene3D
{
    public class SceneRenderPipeline : IDisposable
    {
        private readonly Scene3D scene;

        private readonly MeshRenderingSystem meshRenderingSystem;
        private readonly SunShadowMapSystem sunShadowMapSystem;

        private RenderTarget2D colorRt;
        private RenderTarget2D colorRt2;
        private RenderTarget2D depthRt;
        private RenderTarget2D normalRt;
        private RenderTarget2D ssaoRt;

        private SimplePostProcessingEffect postProcessingEffect;
        private SsaoPostProcessingEffect ssaoPostProcessingEffect;
        private DepthOfFieldPostProcessingEffect dofPostProcessingEffect;

        public SceneRenderPipeline(Scene3D scene)
        {
            this.scene = scene;

            meshRenderingSystem = new MeshRenderingSystem(scene);
            sunShadowMapSystem = new SunShadowMapSystem(scene);

            void InitRts()
            {
                colorRt?.Dispose();
                colorRt2?.Dispose();
                depthRt?.Dispose();
                normalRt?.Dispose();
                ssaoRt?.Dispose();
                colorRt = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth,
                    Globals.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
                colorRt2 = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth,
                    Globals.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
                depthRt = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth,
                    Globals.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.Single, DepthFormat.Depth24);
                normalRt = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth,
                    Globals.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.HalfVector4, DepthFormat.Depth24);
                ssaoRt = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth / 2,
                    Globals.Graphics.PreferredBackBufferHeight / 2, false, SurfaceFormat.Color, DepthFormat.Depth24);
            }

            InitRts();
            Globals.GameMain.Window.ClientSizeChanged += (_, _) => InitRts();

            postProcessingEffect = new SimplePostProcessingEffect()
            {
                FogEnabled = true,
                FogStart = 20f,
                FogEnd = 50f,
                FogColor = Color.CornflowerBlue,
                ScreenSpaceAmbientOcclusionEnabled = true,
            };
            ssaoPostProcessingEffect = new SsaoPostProcessingEffect();
            dofPostProcessingEffect = new DepthOfFieldPostProcessingEffect();
        }

        public void Render(RenderTarget2D? target)
        {
            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            scene.Resources.EffectPool.UpdateEffectCameraData(scene.Camera);

            // TODO: It's only necessary to re-render the shadow map if scene geometry changes
            Texture2D? shadowMap = sunShadowMapSystem.GenerateShadowMap(meshRenderingSystem, out Vector3 lightPosition, out Matrix lightViewProj);

            scene.Resources.EffectPool.UpdateEffectShadows(shadowMap, lightPosition, lightViewProj);

            // render scene meshes to color, normal and depth buffers
            gd.SetRenderTargets(colorRt, depthRt, normalRt);
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
            scene.Skydome.EffectProperties.Effect.UpdateMatrices();
            foreach (EffectPass pass in scene.Skydome.EffectProperties.Technique.Passes)
            {
                pass.Apply();
                gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, scene.Skydome.IndexBuffer.IndexCount / 3);
            }

            if (AmbientOcclusionEnabled)
            {
                gd.SetRenderTarget(ssaoRt);
                ssaoPostProcessingEffect.UpdateParameters(colorRt, depthRt, normalRt, projection, scene.Camera.NearPlane,
                    scene.Camera.FarPlane);
                RenderTargetRenderer.RenderRenderTarget(ssaoPostProcessingEffect);
            }

            // combine
            gd.SetRenderTarget(colorRt2);
            postProcessingEffect.UpdateParameters(colorRt, depthRt, null, projection, scene.Camera.NearPlane, scene.Camera.FarPlane);
            postProcessingEffect.AmbientOcclusionTexture = ssaoRt;
            RenderTargetRenderer.RenderRenderTarget(postProcessingEffect);

            // depth of field
            gd.SetRenderTarget(colorRt);
            dofPostProcessingEffect.BlurDirection = DepthOfFieldPostProcessingEffect.Direction.HORIZONTAL;
            dofPostProcessingEffect.UpdateParameters(colorRt2, depthRt, null, projection, scene.Camera.NearPlane, scene.Camera.FarPlane);
            RenderTargetRenderer.RenderRenderTarget(dofPostProcessingEffect);

            gd.SetRenderTarget(null);
            dofPostProcessingEffect.BlurDirection = DepthOfFieldPostProcessingEffect.Direction.VERTICAL;
            dofPostProcessingEffect.UpdateParameters(colorRt, depthRt, null, projection, scene.Camera.NearPlane, scene.Camera.FarPlane);
            RenderTargetRenderer.RenderRenderTarget(dofPostProcessingEffect);
        }

        private bool ambientOcclusionEnabled;
        public bool AmbientOcclusionEnabled
        {
            get => ambientOcclusionEnabled;
            set
            {
                ambientOcclusionEnabled = value;
                postProcessingEffect.ScreenSpaceAmbientOcclusionEnabled = value;
            }
        }

        ~SceneRenderPipeline()
        {
            Dispose();
        }

        public void Dispose()
        {
            colorRt?.Dispose();
            colorRt2?.Dispose();
            depthRt?.Dispose();
            normalRt?.Dispose();
            ssaoRt?.Dispose();
        }
    }
}
