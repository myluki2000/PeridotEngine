using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Geometry
{
    public class Mesh<T> : Mesh where T : IVertexType, new()
    {
        private readonly T[] vertices;
        private readonly uint[] indices;

        private VertexDeclaration vertexDeclaration;

        public override IVertexType[] GetVerticesBase()
        {
            return Array.ConvertAll(vertices, x => (IVertexType)x);
        }

        public T[] GetVertices()
        {
            return vertices;
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

    public abstract class Mesh
    {
        public VertexBuffer? VertexBuffer { get; set; }
        public IndexBuffer? IndexBuffer { get; set; }

        public abstract IVertexType[] GetVerticesBase();
        public abstract uint[] GetIndices();

        public abstract Type GetVertexType();

        public abstract VertexDeclaration GetVertexDeclaration();
    }
}
