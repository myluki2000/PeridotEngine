using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Geometry;
using static PeridotEngine.Scenes.Scene3D.MeshResources;
using Color = Microsoft.Xna.Framework.Color;
using ModelMesh = PeridotEngine.Graphics.Geometry.ModelMesh;

namespace PeridotEngine.Scenes.Scene3D
{
    public class MeshResources
    {
        public event EventHandler<IEnumerable<MeshInfo>>? MeshListChanged;

        private readonly List<MeshInfo> defaultMeshes = new();
        [JsonProperty]
        private readonly List<MeshInfo> customMeshes = new();

        public MeshResources()
        {
            CreateQuad("quad");
            CreateTriangle("tri");
        }

        public IEnumerable<MeshInfo> GetAllMeshes()
        {
            return defaultMeshes.Concat(customMeshes);
        }

        public void CreateQuad(string name)
        {
            Mesh mesh = new Mesh<VertexPositionTexture>(
                new VertexPositionTexture[]
                {
                    new(new Vector3(-0.5f, 0,  0.5f), new Vector2(0, 1)),
                    new(new Vector3( 0.5f, 0,  0.5f), new Vector2(1, 1)),
                    new(new Vector3( 0.5f, 0, -0.5f), new Vector2(1, 0)),
                    new(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
                },
                new uint[]
                {
                    2, 1, 0,
                    0, 3, 2
                });

            AddDefaultMesh(new MeshInfo(name, null, mesh));
        }

        public void CreateTriangle(string name)
        {
            Mesh mesh = new Mesh<VertexPositionTexture>(
                new VertexPositionTexture[]
                {
                    new(new Vector3(0, 0, 0.5f), new Vector2(0.5f, 1)),
                    new(new Vector3(0.5f, 0, -0.5f), new Vector2(1, 0)),
                    new(new Vector3(-0.5f, 0, -0.5f), new Vector2(0, 0)),
                },
                new uint[]
                {
                    2, 1, 0
                });

            AddDefaultMesh(new MeshInfo(name, null, mesh));
        }

        public void LoadModel(string contentPath)
        {
            MeshInfo meshInfo = new MeshInfo(contentPath, contentPath, null);
            LoadModel(meshInfo);
            AddCustomMesh(meshInfo);
        }

        public void LoadModel(MeshInfo meshInfo)
        {
            Model model = Globals.Content.Load<Model>(meshInfo.FilePath);

            meshInfo.Mesh = new ModelMesh(model.Meshes[0].MeshParts[0].VertexBuffer,
                model.Meshes[0].MeshParts[0].IndexBuffer);

            Globals.Content.UnloadAsset(meshInfo.FilePath);
        } 

        private void AddCustomMesh(MeshInfo mesh)
        {
            customMeshes.Add(mesh);
            MeshListChanged?.Invoke(this, GetAllMeshes());
        }

        private void AddDefaultMesh(MeshInfo mesh)
        {
            defaultMeshes.Add(mesh);
            MeshListChanged?.Invoke(this, GetAllMeshes());
        }

        public class MeshInfo
        {
            public MeshInfo(string name, string? filePath, Mesh? mesh)
            {
                Name = name;
                FilePath = filePath;
                Mesh = mesh;
            }

            [JsonIgnore]
            public Mesh? Mesh { get; set; }
            public string Name { get; set; }
            public string? FilePath { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
