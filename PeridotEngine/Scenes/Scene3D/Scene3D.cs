using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Game.ECS;
using PeridotEngine.Game.ECS.Components;
using PeridotEngine.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; } = new Ecs();
        public SceneResources Resources { get; } = new();

        private Query? meshes;

        public override void Initialize()
        {
            
        }

        public override void Update(GameTime gameTime)
        {

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

            gd.Clear(Color.CornflowerBlue);

            meshes!.ForEach((StaticMeshComponent meshC, StaticPositionRotationScaleComponent posC, EffectComponent effectC) =>
            {
                if (meshC.VertexBuffer == null)
                {
                    meshC.VertexBuffer = new VertexBuffer(gd, typeof(VertexPositionColorTexture),
                        meshC.Mesh.Vertices.Length, BufferUsage.WriteOnly);
                }

                gd.SetVertexBuffer(meshC.VertexBuffer);
                
                foreach (EffectPass pass in effectC.Effect.Techniques[0].Passes)
                {
                    pass.Apply();
                    gd.DrawPrimitives(PrimitiveType.TriangleList, 0, meshC.VertexBuffer.VertexCount / 3);
                }
            });
        }

        public override void Deinitialize()
        {
        }
    }
}
