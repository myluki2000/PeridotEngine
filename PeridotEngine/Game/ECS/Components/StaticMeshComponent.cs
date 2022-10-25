using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Geometry;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Game.ECS.Components
{
    internal sealed class StaticMeshComponent : IComponent
    {
        public Mesh Mesh { get; set; }

        public MeshAppearance Appearance { get; set; } = MeshAppearance.NONE;
        public Color Color { get; set; }
        public RectangleF DiffuseTexture { get; set; }

        public StaticMeshComponent(Mesh mesh)
        {
            Mesh = mesh;
        }

        [Flags]
        public enum MeshAppearance
        {
            NONE = 0,
            MIX_COLOR = 1,
            DIFFUSE_TEXTURE = 2,
        }
    }
}
