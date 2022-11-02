using PeridotEngine.Scenes.Scene3D;
using System.Text.Json.Serialization;

namespace PeridotWindows.ECS.Components
{
    public abstract partial class ComponentBase
    {
        [JsonIgnore]
        public Scene3D Scene { get; protected set; }
    }
}
