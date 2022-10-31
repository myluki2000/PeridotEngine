using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components
{
    internal sealed partial class StaticMeshGroupComponent : IComponent
    {
        public Mesh[] Meshes { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
        public Scene3D Scene { get; }

        public StaticMeshGroupComponent(Mesh[] meshes, Scene3D scene)
        {
            Meshes = meshes;
            Scene = scene;
        }
    }
}
