using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine.Game.ECS;
using PeridotEngine.Game.ECS.Components;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Camera;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; } = new Ecs();
        public SceneResources Resources { get; } = new();

        public Camera Camera { get; } = new();

        private Query? meshes;

        public override void Initialize()
        {
            Camera.Position = new Vector3(0, 1, 1);
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

            if (keyState.IsKeyDown(Keys.LeftShift))
            {
                Camera.MoveUp((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.LeftControl))
            {
                Camera.MoveDown((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        private bool firstDraw = true;
        private void BeforeFirstDraw()
        {
            Resources.MeshResources.CreateQuad("quad");

            IComponent[] components = new IComponent[]
            {
                new StaticMeshComponent(Resources.MeshResources.GetAllMeshes().First()),
                new StaticPositionRotationScaleComponent(),
                new EffectComponent(new BasicEffect(Globals.Graphics.GraphicsDevice))
            };

            Ecs.Archetype(typeof(StaticMeshComponent), typeof(StaticPositionRotationScaleComponent), typeof(EffectComponent))
                .CreateEntity(components);

            meshes = Ecs.Query().Has<StaticMeshComponent>().Has<StaticPositionRotationScaleComponent>().Has<EffectComponent>();
        }

        public override void Draw(GameTime gameTime)
        {
            if (firstDraw)
            {
                firstDraw = false;
                BeforeFirstDraw();
            }

            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;
            gd.RasterizerState = RasterizerState.CullNone;
            gd.Clear(Color.CornflowerBlue);

            meshes!.ForEach((StaticMeshComponent meshC, StaticPositionRotationScaleComponent posC, EffectComponent effectC) =>
            {
                BasicEffect be = (BasicEffect)effectC.Effect;
                be.World = Matrix.Identity;
                be.View = Camera.GetViewMatrix();
                be.Projection = Camera.GetProjectionMatrix();
                be.Alpha = 1;
                be.VertexColorEnabled = true;

                if (meshC.VertexBuffer == null)
                {
                    meshC.VertexBuffer = new VertexBuffer(gd, typeof(VertexPositionColorTexture),
                        meshC.Mesh.Vertices.Length, BufferUsage.WriteOnly);
                    meshC.VertexBuffer.SetData(meshC.Mesh.Vertices);
                }

                gd.SetVertexBuffer(meshC.VertexBuffer);

                foreach (EffectPass pass in be.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    gd.DrawUserPrimitives(PrimitiveType.TriangleList, meshC.Mesh.Vertices, 0, meshC.Mesh.Vertices.Length / 3, VertexPositionColorTexture.VertexDeclaration);
                    //gd.DrawPrimitives(PrimitiveType.TriangleList, 0, meshC.VertexBuffer.VertexCount / 3);
                }
            });
        }

        public override void Deinitialize()
        {
        }
    }
}
