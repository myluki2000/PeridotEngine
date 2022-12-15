using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Misc;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics
{
    public class Skydome
    {
        public VertexBuffer VertexBuffer { get; private set; }
        public IndexBuffer IndexBuffer { get; private set; }

        private int resolution = 100;
        public int Resolution
        {
            get => resolution;
            set
            {
                resolution = value;
                GenerateGeometry();
            }
        }

        public Skydome()
        {
            GenerateGeometry();
        }

        private void GenerateGeometry()
        {
            (VertexPositionNormalTexture[] verts, uint[] indices) = MeshGenerator.GenerateSphere(Resolution);
            Array.Reverse(indices);

            // scale the sphere
            Matrix m = Matrix.CreateScale(100, 20, 100);
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i].Position = verts[i].Position.Transform(m);
            }
            
            VertexBuffer?.Dispose();
            IndexBuffer?.Dispose();

            VertexBuffer = new(Globals.Graphics.GraphicsDevice, typeof(VertexPositionNormalTexture), verts.Length,
                BufferUsage.WriteOnly);
            IndexBuffer = new(Globals.Graphics.GraphicsDevice, IndexElementSize.ThirtyTwoBits, indices.Length,
                BufferUsage.WriteOnly);

            VertexBuffer.SetData(verts);
            IndexBuffer.SetData(indices);
        }
    }
}
