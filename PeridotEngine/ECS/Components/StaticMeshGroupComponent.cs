using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Geometry;

namespace PeridotWindows.ECS.Components
{
    internal sealed class StaticMeshGroupComponent : IComponent
    {
        public Mesh[] Meshes { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
    }
}
