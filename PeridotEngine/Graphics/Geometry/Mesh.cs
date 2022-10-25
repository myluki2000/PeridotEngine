using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Geometry
{
    public class Mesh
    {
        public VertexPositionColorNormalTexture[] Vertices { get; set; }

        public Mesh(Vector3[] vertices)
        {
            this.Vertices = vertices;
        }
    }
}
