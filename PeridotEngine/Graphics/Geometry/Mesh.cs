using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Geometry
{
    public class Mesh<T> : Mesh where T : IVertexType, new()
    {
        private readonly T[] vertices;
        private readonly uint[] indices;

        private VertexDeclaration vertexDeclaration;

        public T[] GetVertices()
        {
            return vertices;
        }

        public override int GetVertexCount()
        {
            return vertices.Length;
        }

        public override uint[] GetIndices()
        {
            return indices;
        }

        public Mesh(T[] vertices, uint[] indices)
        {
            this.vertices = vertices;
            this.indices = indices;
            vertexDeclaration = new T().VertexDeclaration;
        }

        public override Type GetVertexType()
        {
            return typeof(T);
        }

        public override VertexDeclaration GetVertexDeclaration()
        {
            return vertexDeclaration;
        }
    }

    public class ModelMesh : Mesh
    {
        public ModelMesh(VertexBuffer vertexBuffer, IndexBuffer indexBuffer)
        {
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
        }

        public override int GetVertexCount()
        {
            if (VertexBuffer == null)
                throw new Exception("VertexBuffer should never be NULL for a ModelMesh.");

            return VertexBuffer.VertexCount;
        }

        public override uint[] GetIndices()
        {
            throw new NotImplementedException("You should never need to get the indices of ModelMesh. Instead use the provided IndexBuffer directly.");
        }

        public override Type GetVertexType()
        {
            throw new NotImplementedException("You should never need to get the VertexType of ModelMesh. Instead use the provided VertexBuffer directly.");
        }

        public override VertexDeclaration GetVertexDeclaration()
        {
            if (VertexBuffer == null)
                throw new Exception("VertexBuffer should never be NULL for a ModelMesh.");

            return VertexBuffer.VertexDeclaration;
        }
    }

    public abstract class Mesh
    {
        [JsonIgnore]
        public VertexBuffer? VertexBuffer { get; set; }
        [JsonIgnore]
        public IndexBuffer? IndexBuffer { get; set; }

        public abstract int GetVertexCount();
        public abstract uint[] GetIndices();

        public abstract Type GetVertexType();

        public abstract VertexDeclaration GetVertexDeclaration();
    }
}
