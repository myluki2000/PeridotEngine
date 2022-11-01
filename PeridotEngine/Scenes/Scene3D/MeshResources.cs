﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Geometry;
using Color = Microsoft.Xna.Framework.Color;
using ModelMesh = PeridotEngine.Graphics.Geometry.ModelMesh;

namespace PeridotEngine.Scenes.Scene3D
{
    public class MeshResources
    {
        public event EventHandler<IEnumerable<MeshInfo>>? MeshListChanged; 

        private readonly List<MeshInfo> meshes = new();

        public MeshResources()
        {
            CreateQuad("quad");
            CreateTriangle("tri");
        }

        public IEnumerable<MeshInfo> GetAllMeshes()
        {
            return meshes;
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
                    0, 1, 2,
                    2, 3, 0,
                });

            AddMesh(new MeshInfo(name, mesh));
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
                    0, 1, 2
                });

            AddMesh(new MeshInfo(name, mesh));
        }

        public void LoadModel(string contentPath)
        {
            Model model = Globals.Content.Load<Model>(contentPath);

            ModelMesh mesh = new(model.Meshes[0].MeshParts[0].VertexBuffer,
                                 model.Meshes[0].MeshParts[0].IndexBuffer);

            AddMesh(new MeshInfo(contentPath, mesh));

            Globals.Content.UnloadAsset(contentPath);
        }

        private void AddMesh(MeshInfo mesh)
        {
            meshes.Add(mesh);
            MeshListChanged?.Invoke(this, meshes);
        }

        public class MeshInfo
        {
            public MeshInfo(string name, Mesh mesh)
            {
                Name = name;
                Mesh = mesh;
            }

            public readonly Mesh Mesh;
            public readonly string Name;

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
