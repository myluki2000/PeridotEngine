using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Geometry
{
    /// <summary>
    /// Static helper class to generate basic mesh shapes.
    /// </summary>
    public static class MeshGenerator
    {
        public static (VertexPositionNormalTexture[] vertices, uint[] indices) GenerateTriangle()
        {
            return (new VertexPositionNormalTexture[]
            {
                new(new Vector3(0, 0, 0.5f), new Vector3(0, 1, 0), new Vector2(0.5f, 1)),
                new(new Vector3(0.5f, 0, -0.5f), new Vector3(0, 1, 0), new Vector2(1, 0)),
                new(new Vector3(-0.5f, 0, -0.5f), new Vector3(0, 1, 0), new Vector2(0, 0)),
            },
            new uint[]
            {
                2, 1, 0
            });
        }

        public static (VertexPositionNormalTexture[] vertices, uint[] indices) GenerateQuad()
        {
            return (new VertexPositionNormalTexture[]
            {
                new(new Vector3(-0.5f, 0,  0.5f), new Vector3(0, 1, 0), new Vector2(0, 1)),
                new(new Vector3( 0.5f, 0,  0.5f), new Vector3(0, 1, 0), new Vector2(1, 1)),
                new(new Vector3( 0.5f, 0, -0.5f), new Vector3(0, 1, 0), new Vector2(1, 0)),
                new(new Vector3(-0.5f, 0, -0.5f), new Vector3(0, 1, 0), new Vector2(0, 0)),
            },
            new uint[]
            {
                2, 1, 0,
                0, 3, 2
            });
        }

        public static (VertexPositionNormalTexture[] vertices, uint[] indices) GenerateSphere(int resolution = 100)
        {
            int vertexCount = resolution * resolution;

            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[vertexCount];
            int vertexIndex = 0;

            for (int i = 0; i < resolution; i++)
            {
                float y = -1f + (float)i * 2 / (resolution - 1);
                // calculate radius of sphere at a specific height
                float radius = MathF.Sqrt(1 * 1 - y * y);

                float stepSize = (2 * MathF.PI) / (resolution - 1);
                for (int j = 0; j < resolution; j++)
                {
                    float angle = j * stepSize;
                    float x = MathF.Cos(angle) * radius;
                    float z = MathF.Sin(angle) * radius;
                    vertices[vertexIndex++] = new VertexPositionNormalTexture(new Vector3(x, y, z),
                                                                              new Vector3(x, y, z),
                                                                              new Vector2((float)j / resolution, y + 0.5f));
                }
            }

            uint[] indices = new uint[resolution * resolution * 6];
            int indexIndex = 0;

            for (int i = resolution + 1; i < resolution * resolution; i++)
            {
                indices[indexIndex++] = (uint)i;
                indices[indexIndex++] = (uint)(i - 1);
                indices[indexIndex++] = (uint)(i - resolution - 1);

                indices[indexIndex++] = (uint)i;
                indices[indexIndex++] = (uint)(i - resolution - 1);
                indices[indexIndex++] = (uint)(i - resolution);
            }

            return (vertices, indices);
        }
    }
}
