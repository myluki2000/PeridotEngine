using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Scenes.Scene3D
{
    public class MeshResources
    {
        private readonly List<MeshInfo> meshes = new();

        public IEnumerable<Mesh> GetAllMeshes()
        {
            return meshes.Select(x => x.Mesh);
        }

        public void CreateQuad(string name)
        {
            Mesh mesh = new()
            {
                Vertices = new VertexPositionColorTexture[6]
                {
                    new (new Vector3(-0.5f, 0, 0), Color.White, new Vector2(0, 0)),
                    new (new Vector3(0.5f, 0, 0), Color.White, new Vector2(0, 0)),
                    new (new Vector3(0.5f, 0, 1), Color.White, new Vector2(0, 0)),

                    new (new Vector3(0.5f, 0, 1), Color.White, new Vector2(0, 0)),
                    new (new Vector3(-0.5f, 0, 1), Color.White, new Vector2(0, 0)),
                    new (new Vector3(-0.5f, 0, 0), Color.White, new Vector2(0, 0)),
                }
            };

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
