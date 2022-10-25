using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine.Game.ECS;
using PeridotEngine.Game.ECS.Components;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Camera;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
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

            if (keyState.IsKeyDown(Keys.Space))
            {
                Camera.MoveUp((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.LeftShift))
            {
                Camera.MoveDown((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        private bool firstDraw = true;
        private void BeforeFirstDraw()
        {
            Globals.Graphics.GraphicsDevice.RasterizerState = RasterizerState.CullClockwise;

            Resources.MeshResources.CreateQuad("quad");

            IComponent[] components = new IComponent[]
            {
                new StaticMeshComponent(Resources.MeshResources.GetAllMeshes().First())
                {
                    Appearance = StaticMeshComponent.MeshAppearance.DIFFUSE_TEXTURE,
                },
                new StaticPositionRotationScaleComponent(),
                new EffectComponent(EffectPool.Effect<SimpleEffect>())
            };

            Ecs.Archetype(typeof(StaticMeshComponent), typeof(StaticPositionRotationScaleComponent), typeof(EffectComponent))
                .CreateEntity(components);

            meshes = Ecs.Query()
                .Has<StaticPositionRotationScaleComponent>()
                .Has<StaticMeshComponent>()
                .Has<EffectComponent>();
        }

        public override void Draw(GameTime gameTime)
        {
            if (firstDraw)
            {
                firstDraw = false;
                BeforeFirstDraw();
            }

            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            gd.Clear(Color.CornflowerBlue);

            EffectPool.UpdateEffectMatrices(Matrix.Identity, Camera.GetViewMatrix(), Camera.GetProjectionMatrix());

            // TODO: Only update effect textures if texture atlas changes
            if (Resources.TextureResources.TextureAtlas != null)
            {
                EffectPool.UpdateEffectTextures(Resources.TextureResources.TextureAtlas);
            }

            meshes!.ForEach((StaticMeshComponent meshC, StaticPositionRotationScaleComponent posC, EffectComponent effectC) =>
            {
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

                if (meshC.Mesh.GetVertexType() == typeof(VertexPosition))
                {
                    effectC.Effect.CurrentTechnique = effectC.Effect.Techniques[0];
                }
                else if (meshC.Mesh.GetVertexType() == typeof(VertexPositionColor))
                {
                    effectC.Effect.CurrentTechnique = effectC.Effect.Techniques[1];
                }
                else if (meshC.Mesh.GetVertexType() == typeof(VertexPositionTexture))
                {
                    if (meshC.Appearance.HasFlag(StaticMeshComponent.MeshAppearance.DIFFUSE_TEXTURE))
                    {
                        effectC.Effect.CurrentTechnique = effectC.Effect.Techniques[2];
                        
                    }
                    else
                    {
                        effectC.Effect.CurrentTechnique = effectC.Effect.Techniques[0];
                    }
                }

                if (meshC.Appearance.HasFlag(StaticMeshComponent.MeshAppearance.MIX_COLOR))
                {
                    effectC.Effect.MixColor = meshC.Color;
                }
                else
                {
                    effectC.Effect.MixColor = Color.White;
                }

                foreach (EffectPass pass in effectC.Effect.CurrentTechnique.Passes)
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
