using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Text;
using Accessibility;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PeridotEngine.ECS.Systems;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Graphics.PostProcessing;
using PeridotEngine.IO.JsonConverters;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; }
        public SceneResources Resources { get; }

        public Camera Camera { get; set; }

        public MeshRenderingSystem MeshRenderingSystem;
        public SunShadowMapSystem SunShadowMapSystem;

        private RenderTarget2D colorRT;
        private RenderTarget2D depthRT;
        private RenderTarget2D normalRT;
        private RenderTarget2D ssaoRT;

        private Skydome skydome;
        private SkydomeEffect.SkydomeEffectProperties skydomeEffectProperties;

        private SimplePostProcessingEffect postProcessingEffect;
        private SsaoPostProcessingEffect ssaoPostProcessingEffect;
        private DepthOfFieldPostProcessingEffect dofPostProcessingEffect;

        public Scene3D()
        {
            Resources = new SceneResources(this);
            Ecs = new Ecs();
            Camera = new PerspectiveCamera();
        }

        public Scene3D(string json)
        {
            JToken root = JToken.Parse(json);
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                Converters =
                {
                    new StaticMeshComponentJsonConverter(this),
                    new EffectPropertiesJsonConverter(this),
                    new EcsJsonConverter(this),
                    new SceneResourcesJsonConverter(this)
                }
            });

            Resources = (SceneResources)root["Resources"].ToObject(typeof(SceneResources), serializer);
            Camera = root["Camera"].ToObject<PerspectiveCamera>();
            Ecs = (Ecs)root["Ecs"].ToObject(typeof(Ecs), serializer);
        }

        public override void Initialize()
        {
            MeshRenderingSystem = new(this);
            SunShadowMapSystem = new(this);

            skydome = new();
            skydomeEffectProperties = Resources.EffectPool.Effect<SkydomeEffect>().CreateProperties();

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


            void InitRTs()
            {
                colorRT?.Dispose();
                depthRT?.Dispose();
                normalRT?.Dispose();
                ssaoRT?.Dispose();
                colorRT = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth,
                    Globals.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
                depthRT = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth,
                    Globals.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.Single, DepthFormat.Depth24);
                normalRT = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth,
                    Globals.Graphics.PreferredBackBufferHeight, false, SurfaceFormat.HalfVector4, DepthFormat.Depth24);
                ssaoRT = new(Globals.Graphics.GraphicsDevice, Globals.Graphics.PreferredBackBufferWidth / 2,
                    Globals.Graphics.PreferredBackBufferHeight / 2, false, SurfaceFormat.Color, DepthFormat.Depth24);
            }

            InitRTs();
            Globals.GameMain.Window.ClientSizeChanged += (_, _) => InitRTs();

            Globals.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            Camera.Position = new Vector3(0, 1, 1);

            void UpdateCameraAspectRatio()
            {
                if (Camera is PerspectiveCamera perspectiveCamera)
                    perspectiveCamera.AspectRatio = (float)Globals.Graphics.PreferredBackBufferWidth /
                                                    Globals.Graphics.PreferredBackBufferHeight;
            }

            UpdateCameraAspectRatio();
            Globals.GameMain.Window.ClientSizeChanged += (_, _) => UpdateCameraAspectRatio();
        }

        private KeyboardState lastKeyboardState;
        public override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.G) && lastKeyboardState.IsKeyUp(Keys.G))
            {
                postProcessingEffect.ScreenSpaceAmbientOcclusionEnabled =
                    !postProcessingEffect.ScreenSpaceAmbientOcclusionEnabled;
            }

            lastKeyboardState = Keyboard.GetState();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            Resources.EffectPool.UpdateEffectCameraData(Camera);

            // TODO: It's only necessary to re-render the shadow map if scene geometry changes
            Texture2D? shadowMap = SunShadowMapSystem.GenerateShadowMap(out Vector3 lightPosition, out Matrix lightViewProj);

            Resources.EffectPool.UpdateEffectShadows(shadowMap, lightPosition, lightViewProj);

            // render color and depth buffers
            gd.SetRenderTargets(colorRT, depthRT, normalRT);
            gd.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.White,
                float.MaxValue, 0);

            MeshRenderingSystem.RenderMeshes();

            gd.SetVertexBuffer(skydome.VertexBuffer);
            gd.Indices = skydome.IndexBuffer;

            Matrix projection = Camera.GetProjectionMatrix();

            skydomeEffectProperties.Effect.World = Matrix.Identity;
            skydomeEffectProperties.Effect.View = Camera.GetRotationMatrix();
            skydomeEffectProperties.Effect.Projection = projection;
            skydomeEffectProperties.Effect.UpdateMatrices();
            foreach (EffectPass pass in skydomeEffectProperties.Technique.Passes)
            {
                pass.Apply();
                gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, skydome.IndexBuffer.IndexCount / 3);
            }

            if (postProcessingEffect.ScreenSpaceAmbientOcclusionEnabled)
            {
                gd.SetRenderTarget(ssaoRT);
                ssaoPostProcessingEffect.UpdateParameters(colorRT, depthRT, normalRT, projection, Camera.NearPlane,
                    Camera.FarPlane);
                RenderTargetRenderer.RenderRenderTarget(ssaoPostProcessingEffect);
            }

            // combine
            gd.SetRenderTarget(normalRT);
            postProcessingEffect.UpdateParameters(colorRT, depthRT, null, projection, Camera.NearPlane, Camera.FarPlane);
            postProcessingEffect.AmbientOcclusionTexture = ssaoRT;
            RenderTargetRenderer.RenderRenderTarget(postProcessingEffect);

            // depth of field
            gd.SetRenderTarget(null);
            dofPostProcessingEffect.UpdateParameters(colorRT, depthRT, null, projection, Camera.NearPlane, Camera.FarPlane);
            RenderTargetRenderer.RenderRenderTarget(dofPostProcessingEffect);
        }

        public override void Deinitialize()
        {
            colorRT?.Dispose();
            depthRT?.Dispose();
            normalRT?.Dispose();
            ssaoRT?.Dispose();
        }
    }
}
