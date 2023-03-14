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

        public Camera Camera
        {
            get => camera;
            set
            {
                camera = value;
                UpdateCameraAspectRatio();
            }
        }

        public Skydome Skydome { get; private set; }


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
            Skydome = new(Resources.EffectPool.Effect<SkydomeEffect>().CreateProperties());

            Camera.Position = new Vector3(0, 1, 1);
            UpdateCameraAspectRatio();

            Globals.GameMain.Window.ClientSizeChanged += (_, _) => UpdateCameraAspectRatio();
        }

        private KeyboardState lastKeyboardState;
        private Camera camera;

        public override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.G) && lastKeyboardState.IsKeyUp(Keys.G))
            {
                
            }

            lastKeyboardState = Keyboard.GetState();
        }

        public override void Deinitialize()
        {
            
        }

        private void UpdateCameraAspectRatio()
        {
            if (Camera is PerspectiveCamera perspectiveCamera)
            {
                perspectiveCamera.AspectRatio = (float)Globals.Graphics.PreferredBackBufferWidth /
                                                Globals.Graphics.PreferredBackBufferHeight;
            }
        }
    }
}
