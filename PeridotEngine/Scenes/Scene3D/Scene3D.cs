using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PeridotEngine.ECS.Systems;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.IO.JsonConverters;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using IEffectFog = PeridotEngine.Graphics.Effects.IEffectFog;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; }
        public SceneResources Resources { get; }

        public Camera Camera { get; set; }

        public MeshRenderingSystem MeshRenderingSystem;
        public SunShadowMapSystem SunShadowMapSystem;

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
                    new ArchetypeJsonConverter(),
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

            Globals.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            Camera.Position = new Vector3(0, 1, 1);

            Globals.GameMain.Window.ClientSizeChanged += (_, _) =>
            {
                if(Camera is PerspectiveCamera perspectiveCamera)
                    perspectiveCamera.AspectRatio = (float)Globals.Graphics.PreferredBackBufferWidth /
                                                    Globals.Graphics.PreferredBackBufferHeight;
            };
            if (Camera is PerspectiveCamera perspectiveCamera)
                perspectiveCamera.AspectRatio = (float)Globals.Graphics.PreferredBackBufferWidth /
                                            Globals.Graphics.PreferredBackBufferHeight;


            // set fog params for effects
            Resources.EffectPool.EffectInstantiated += (_, effect) =>
            {
                if (effect is IEffectFog fog)
                {
                    fog.FogStart = 20f;
                    fog.FogEnd = 50f;
                    fog.FogColor = Color.CornflowerBlue;
                }
            };
            foreach (WeakReference<EffectBase> effectRef in Resources.EffectPool.Effects.Values)
            {
                if (!effectRef.TryGetTarget(out EffectBase? effect)) continue;

                if (effect is IEffectFog fog)
                {
                    fog.FogStart = 20f;
                    fog.FogEnd = 50f;
                    fog.FogColor = Color.CornflowerBlue;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            Resources.EffectPool.UpdateEffectCameraData(Camera);

            // TODO: It's only necessary to re-render the shadow map if scene geometry changes
            Texture2D? shadowMap = SunShadowMapSystem.GenerateShadowMap(out Vector3 lightPosition, out Matrix lightViewProj);

            gd.Clear(Color.CornflowerBlue);
            Resources.EffectPool.UpdateEffectShadows(shadowMap, lightPosition, lightViewProj);
            MeshRenderingSystem.RenderMeshes();
        }

        public override void Deinitialize()
        {
        }
    }
}
