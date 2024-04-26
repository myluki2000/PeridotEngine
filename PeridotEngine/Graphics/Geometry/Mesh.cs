using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public override BoundingBox Bounds { get; protected set; }

        public override int GetVertexCount()
        {
            return vertices.Length;
        }

        public override uint[] GetIndices()
        {
            return indices;
        }

        protected override void CreateVertexBuffer()
        {
            VertexBuffer = new VertexBuffer(Globals.Graphics.GraphicsDevice, vertexDeclaration, GetVertexCount(), BufferUsage.WriteOnly);
            typeof(VertexBuffer).GetMethods().First(x => x.Name == "SetData" && x.GetParameters().Length == 1)
                .MakeGenericMethod(GetVertexType()).Invoke(VertexBuffer, new[] { vertices });
        }

        protected override void CreateIndexBuffer()
        {
            IndexBuffer = new IndexBuffer(Globals.Graphics.GraphicsDevice, IndexElementSize.ThirtyTwoBits, indices.Length, BufferUsage.WriteOnly);
            IndexBuffer.SetData(GetIndices());
        }

        public Mesh(T[] vertices, uint[] indices, BoundingBox bounds)
        {
            this.vertices = vertices;
            this.indices = indices;
            vertexDeclaration = new T().VertexDeclaration;
            this.Bounds = bounds;
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

            PopulateBounds();   
        }

        public override BoundingBox Bounds { get; protected set; }

        private unsafe void PopulateBounds()
        {
            byte[] data = new byte[VertexBuffer.VertexCount * VertexBuffer.VertexDeclaration.VertexStride];
            VertexBuffer.GetData(data, 0, data.Length);

            VertexElement positionElement = VertexBuffer.VertexDeclaration.GetVertexElements()
                .First(x => x is { VertexElementUsage: VertexElementUsage.Position, UsageIndex: 0 });

            BoundingBox newBounds = new BoundingBox(new Vector3(float.MaxValue, float.MaxValue, float.MaxValue),
                new Vector3(float.MinValue, float.MinValue, float.MinValue));

            fixed (byte* dataPtr = data)
            {
                for (int i = 0; i < data.Length; i += VertexBuffer.VertexDeclaration.VertexStride)
                {
                    void* elePtr = &dataPtr[i + positionElement.Offset];

                    if (positionElement.VertexElementFormat != VertexElementFormat.Vector3)
                    {
                        throw new Exception("Position elements with format other than Vector3 are not supported.");
                    }

                    Vector3 pos = *((Vector3*)elePtr);
                    Debug.WriteLine(pos);

                    if (pos.X < newBounds.Min.X)
                        newBounds.Min.X = pos.X;

                    if (pos.X > newBounds.Max.X)
                        newBounds.Max.X = pos.X;

                    if (pos.Y < newBounds.Min.Y)
                        newBounds.Min.Y = pos.Y;

                    if (pos.Y > newBounds.Max.Y)
                        newBounds.Max.Y = pos.Y;

                    if (pos.Z < newBounds.Min.Z)
                        newBounds.Min.Z = pos.Z;

                    if (pos.Z > newBounds.Max.Z)
                        newBounds.Max.Z = pos.Z;
                }
            }

            Bounds = newBounds;
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

        protected override void CreateVertexBuffer()
        {
            throw new NotImplementedException("CreateVertexBuffer should never need to be called on a ModelMesh as the vertex buffer is set at object initialization.");
        }

        protected override void CreateIndexBuffer()
        {
            throw new NotImplementedException("CreateIndexBuffer should never need to be called on a ModelMesh as the index buffer is set at object initialization.");
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

        [JsonIgnore]
        public abstract BoundingBox Bounds { get; protected set; }

        public abstract int GetVertexCount();
        public abstract uint[] GetIndices();

        protected abstract void CreateVertexBuffer();
        protected abstract void CreateIndexBuffer();

        public abstract Type GetVertexType();

        public abstract VertexDeclaration GetVertexDeclaration();
    }
}
