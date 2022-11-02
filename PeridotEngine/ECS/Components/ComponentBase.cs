using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components
{
    public abstract partial class ComponentBase
    {
        public Scene3D Scene { get; protected set; }
    }
}
