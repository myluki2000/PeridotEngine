using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Geometry;

namespace PeridotEngine.Game.ECS.Components
{
    internal sealed class StaticMeshGroupComponent : IComponent
    {
        public Mesh[] Meshes { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
    }
}
