using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.IO.JsonConverters;
using PeridotWindows.ECS;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using System.Reflection;
using PeridotEngine.ECS.Systems;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; }
        public SceneResources Resources { get; }

        public Camera Camera { get; set; }

        [JsonIgnore]
        public Skydome Skydome { get; private set; }

        private PhysicsSystem? _physicsSystem;

        public Scene3D()
        {
            Resources = new SceneResources(this);
            Ecs = new Ecs();
            Camera = new PerspectiveCamera() { AllowAutomaticAspectRatioAdjustment = true };
        }

        protected Scene3D(JToken root)
        {
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                Converters =
                {
                    new MeshInfoJsonConverter(this),
                    new EffectPropertiesJsonConverter(this),
                    new EcsJsonConverter(this),
                    new SceneResourcesJsonConverter(this)
                }
            });

            Resources = (SceneResources)root["Resources"].ToObject(typeof(SceneResources), serializer);
            Camera = (PerspectiveCamera)root["Camera"].ToObject(typeof(PerspectiveCamera), serializer);
            Ecs = (Ecs)root["Ecs"].ToObject(typeof(Ecs), serializer);
        }

        public override void Initialize()
        {
            Skydome = new(Resources.EffectPool.Effect<SkydomeEffect>().CreateProperties());
            _physicsSystem = new PhysicsSystem(this);
        }

        private KeyboardState lastKeyboardState;
        public override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                _physicsSystem!.Update(gameTime);
            }

            lastKeyboardState = Keyboard.GetState();
        }

        public override void Deinitialize()
        {
            _physicsSystem?.Dispose();
        }

        public static Scene3D FromJson(JToken root)
        {
            string typeString = root["type"]?.Value<string>() 
                          ?? throw new Exception("Error while parsing scene json. Missing scene type.");

            Type sceneType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ass => ass.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Scene3D)) || t == typeof(Scene3D))
                    .Where(t => typeString == t.FullName))
                .First();

            Scene3D instance = (Scene3D?)Activator.CreateInstance(
                                   sceneType, 
                                   BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, 
                                   null, 
                                   [root], 
                                   null, 
                                   null)
                ?? throw new Exception("Error while instantiating new scene.");

            return instance;
        }

        public JToken ToJson()
        {
            JToken root = JToken.FromObject(this, new JsonSerializer()
            {
                Converters =
                {
                    new MeshInfoJsonConverter(this),
                    new EffectPropertiesJsonConverter(this),
                    new EcsJsonConverter(this),
                    new SceneResourcesJsonConverter(this)
                }
            });

            root["type"] = GetType().FullName;

            return root;
        }
    }
}
