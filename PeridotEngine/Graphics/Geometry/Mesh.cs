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
        private VertexDeclaration vertexDeclaration;

        public override IVertexType[] GetVerticesBase()
        {
            return Array.ConvertAll(vertices, x => (IVertexType)x);
        }

        public T[] GetVertices()
        {
            return vertices;
        }


        public Mesh(T[] vertices)
        {
            this.vertices = vertices;
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

        public abstract IVertexType[] GetVerticesBase();

        public abstract Type GetVertexType();

        public abstract VertexDeclaration GetVertexDeclaration();
    }
}
