using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;

namespace PeridotEngine.ECS.Systems
{
    public class MeshRenderingSystem(Scene3D scene)
    {
        public readonly ComponentQuery<StaticMeshComponent, PositionRotationScaleComponent> Meshes = scene.Ecs.Query()
            .Has<PositionRotationScaleComponent>()
            .Has<StaticMeshComponent>()
            .OnComponents<StaticMeshComponent, PositionRotationScaleComponent>();

        public void RenderMeshes(EffectBase? effectOverride = null)
        {
            GraphicsDevice gd = Globals.GraphicsDevice;

            Meshes!.ForEach((uint entityId, StaticMeshComponent meshC, PositionRotationScaleComponent posC) =>
            {
                RenderMesh(entityId, meshC, posC);
            });
        }

        public void RenderMesh(uint objectId, StaticMeshComponent meshC, PositionRotationScaleComponent posC, EffectBase? effectOverride = null)
        {
            if (meshC.EffectProperties == null || meshC.Mesh == null) return;

            GraphicsDevice gd = Globals.GraphicsDevice;

            if (effectOverride == null)
            {
                meshC.EffectProperties.Effect.World = posC.Transformation;
                meshC.EffectProperties.Effect.ObjectId = objectId;
            }
            else
            {
                effectOverride.World = posC.Transformation;
                effectOverride.Apply();
            }

            if (meshC.Mesh.Mesh.VertexBuffer == null)
            {
                meshC.Mesh.Mesh.VertexBuffer = new VertexBuffer(gd, meshC.Mesh.Mesh.GetVertexDeclaration(),
                    meshC.Mesh.Mesh.GetVertexCount(), BufferUsage.WriteOnly);

                object vertsObj = meshC.Mesh.Mesh.GetType().GetMethod("GetVertices")
                    .Invoke(meshC.Mesh.Mesh, new object[] { });

                typeof(VertexBuffer).GetMethods().First(x => x.Name == "SetData" && x.GetParameters().Length == 1)
                    .MakeGenericMethod(meshC.Mesh.Mesh.GetVertexType()).Invoke(meshC.Mesh.Mesh.VertexBuffer, new[] { vertsObj });
            }

            if (meshC.Mesh.Mesh.IndexBuffer == null)
            {
                uint[] indices = meshC.Mesh.Mesh.GetIndices();
                meshC.Mesh.Mesh.IndexBuffer = new IndexBuffer(gd, IndexElementSize.ThirtyTwoBits, indices.Length,
                    BufferUsage.WriteOnly);
                meshC.Mesh.Mesh.IndexBuffer.SetData(indices);
            }

            gd.SetVertexBuffer(meshC.Mesh.Mesh.VertexBuffer);
            gd.Indices = meshC.Mesh.Mesh.IndexBuffer;

            if (effectOverride == null)
            {
                meshC.EffectProperties.Apply(meshC.Mesh.Mesh);
            }

            EffectTechnique technique = effectOverride == null
                                            ? meshC.EffectProperties.Technique!
                                            : effectOverride.CurrentTechnique;

            foreach (EffectPass pass in technique.Passes)
            {
                pass.Apply();
                gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, meshC.Mesh.Mesh.IndexBuffer.IndexCount / 3);
            }
        }
    }
}
