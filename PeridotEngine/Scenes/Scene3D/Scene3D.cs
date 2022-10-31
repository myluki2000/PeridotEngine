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

        public Camera Camera { get; } = new();

        private Query? meshes;

        public Scene3D()
        {
            Resources = new SceneResources(this);
        }

        public override void Initialize()
        {
            Camera.Position = new Vector3(0, 1, 1);

            Globals.GameMain.Window.ClientSizeChanged += (_, _) => Camera.UpdateProjectionMatrix();
            Camera.UpdateProjectionMatrix();

            Globals.Graphics.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;

            Resources.MeshResources.CreateQuad("quad");
            Resources.MeshResources.CreateTriangle("tri");

            IComponent[] components = new IComponent[]
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
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
            {
                Camera.MoveForward((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                Camera.MoveBackward((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                Camera.MoveLeft((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                Camera.MoveRight((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                Camera.MoveUp((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.LeftShift))
            {
                Camera.MoveDown((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
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
                    IVertexType[] verts = meshC.Mesh.GetVerticesBase();
                    meshC.Mesh.VertexBuffer = new VertexBuffer(gd, meshC.Mesh.GetVertexType(),
                        verts.Length, BufferUsage.WriteOnly);

                    object vertsObj = meshC.Mesh.GetType().GetMethod("GetVertices")
                        .Invoke(meshC.Mesh, new object[] { });

                    typeof(VertexBuffer).GetMethods().First(x => x.Name == "SetData" && x.GetParameters().Length == 1)
                        .MakeGenericMethod(meshC.Mesh.GetVertexType()).Invoke(meshC.Mesh.VertexBuffer, new[] {vertsObj});
                }

                gd.SetVertexBuffer(meshC.Mesh.VertexBuffer);

                meshC.EffectProperties.Apply(meshC.Mesh);
                foreach (EffectPass pass in meshC.EffectProperties.Technique!.Passes)
                {
                    pass.Apply();
                    gd.DrawPrimitives(PrimitiveType.TriangleList, 0, meshC.Mesh.VertexBuffer.VertexCount / 3);
                }
            });
        }

        public override void Deinitialize()
        {
        }
    }
}
