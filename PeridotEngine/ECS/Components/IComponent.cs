using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components
{
    public partial interface IComponent
    {
        public Scene3D Scene { get; }
    }
}
