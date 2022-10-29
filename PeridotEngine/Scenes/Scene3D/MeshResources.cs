using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Geometry;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Scenes.Scene3D
{
    public class MeshResources
    {
        private readonly List<MeshInfo> meshes = new();

        public IEnumerable<MeshInfo> GetAllMeshes()
        {
            return meshes;
        }

        public void CreateQuad(string name)
        {
            Mesh mesh = new Mesh<VertexPositionTexture>(new VertexPositionTexture[6] {
                    new(new Vector3(-0.5f, 0,  0.5f), new Vector2(0, 1)),
                    new(new Vector3( 0.5f, 0,  0.5f), new Vector2(1, 1)),
                    new(new Vector3( 0.5f, 0, -0.5f), new Vector2(1, 0)),

                    new(new Vector3( 0.5f, 0, -0.5f), new Vector2(1, 0)),
                    new(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
                    new(new Vector3(-0.5f, 0,  0.5f), new Vector2(0, 1)),
                });

            meshes.Add(new MeshInfo(name, mesh));
        }

        public class MeshInfo
        {
            public MeshInfo(string name, Mesh mesh)
            {
                Name = name;
                Mesh = mesh;
            }

            public Mesh Mesh;
            public string Name;
        }
    }
}
