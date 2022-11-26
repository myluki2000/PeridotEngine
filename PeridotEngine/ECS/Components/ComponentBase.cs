using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.ECS.Components
{
    public abstract partial class ComponentBase
    {
        [JsonIgnore]
        public Scene3D Scene { get; protected set; }

        protected ComponentBase(Scene3D scene)
        {
            Scene = scene;
        }
    }
}
