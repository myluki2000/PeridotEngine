using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Camera;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; } = new Ecs();
        public SceneResources Resources { get; }

        public Camera Camera { get; set; } = new();

        private Query? meshes;

        public Scene3D()
        {
            Resources = new SceneResources(this);
        }

        public override void Initialize()
        {
            Globals.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            Camera.Position = new Vector3(0, 1, 1);

            Globals.GameMain.Window.ClientSizeChanged += (_, _) => Camera.UpdateProjectionMatrix();
            Camera.UpdateProjectionMatrix();

            ComponentBase[] components = new ComponentBase[]
            {
                new StaticMeshComponent(this, Resources.MeshResources.GetAllMeshes().First().Mesh, Resources.EffectPool.Effect<SimpleEffect>().CreateProperties())
                {
                    
                },
                new PositionRotationScaleComponent(this)
                {
                    Position = new Vector3(0, 0, 0),
                    Rotation = new Vector3(0, 0, 0)
                }
            };

            Ecs.Archetype(typeof(StaticMeshComponent), typeof(PositionRotationScaleComponent))
                .CreateEntity(components);

            meshes = Ecs.Query()
                .Has<PositionRotationScaleComponent>()
                .Has<StaticMeshComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
        }

 

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            gd.Clear(Color.CornflowerBlue);

            Resources.EffectPool.UpdateEffectViewProjection(Camera.GetViewMatrix() * Camera.GetProjectionMatrix());

            meshes!.ForEach((StaticMeshComponent meshC, PositionRotationScaleComponent posC) =>
            {
                meshC.EffectProperties.Effect.World = posC.Transformation;

                if (meshC.Mesh.VertexBuffer == null)
                {
                    meshC.Mesh.VertexBuffer = new VertexBuffer(gd, meshC.Mesh.GetVertexDeclaration(),
                        meshC.Mesh.GetVertexCount(), BufferUsage.WriteOnly);

                    object vertsObj = meshC.Mesh.GetType().GetMethod("GetVertices")
                        .Invoke(meshC.Mesh, new object[] { });
                    
                    typeof(VertexBuffer).GetMethods().First(x => x.Name == "SetData" && x.GetParameters().Length == 1)
                        .MakeGenericMethod(meshC.Mesh.GetVertexType()).Invoke(meshC.Mesh.VertexBuffer, new[] {vertsObj});
                }

                if (meshC.Mesh.IndexBuffer == null)
                {
                    uint[] indices = meshC.Mesh.GetIndices();
                    meshC.Mesh.IndexBuffer = new IndexBuffer(gd, IndexElementSize.ThirtyTwoBits, indices.Length,
                        BufferUsage.WriteOnly);
                    meshC.Mesh.IndexBuffer.SetData(indices);
                }

                gd.SetVertexBuffer(meshC.Mesh.VertexBuffer);
                gd.Indices = meshC.Mesh.IndexBuffer;

                meshC.EffectProperties.Apply(meshC.Mesh);
                foreach (EffectPass pass in meshC.EffectProperties.Technique!.Passes)
                {
                    pass.Apply();
                    gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, meshC.Mesh.IndexBuffer.IndexCount / 3);
                }
            });
        }

        public override void Deinitialize()
        {
        }
    }
}
