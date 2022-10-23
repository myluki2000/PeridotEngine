using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics
{
    internal class Mesh
    {
        public VertexPositionColorTexture[] Vertices { get; set; }
        public uint TextureId { get; set; }
    }
}
