using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Camera;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.IO.JsonConverters;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; }
        public SceneResources Resources { get; }

        public Camera Camera { get; set; }

        private Query? meshes;

        public Scene3D()
        {
            Resources = new SceneResources(this);
            Ecs = new Ecs();
            Camera = new Camera();
        }

        public Scene3D(string json)
        {
            JToken root = JToken.Parse(json);
            JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                Converters =
                {
                    new StaticMeshComponentJsonConverter(this),
                    new EffectPropertiesJsonConverter(this),
                    new ArchetypeJsonConverter(),
                    new SceneResourcesJsonConverter(this)
                }
            });

            Resources = (SceneResources)root["Resources"].ToObject(typeof(SceneResources), serializer);
            Camera = root["Camera"].ToObject<Camera>();
            Ecs = (Ecs)root["Ecs"].ToObject(typeof(Ecs), serializer);
        }

        public Scene3D(SceneResources resources, Ecs ecs, Camera camera)
        {
            Resources = resources;
            Ecs = ecs;
            Camera = camera;
        }

        public override void Initialize()
        {
            Globals.Graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;

            Camera.Position = new Vector3(0, 1, 1);

            Globals.GameMain.Window.ClientSizeChanged += (_, _) => Camera.UpdateProjectionMatrix();
            Camera.UpdateProjectionMatrix();

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

                if (meshC.Mesh.Mesh.VertexBuffer == null)
                {
                    meshC.Mesh.Mesh.VertexBuffer = new VertexBuffer(gd, meshC.Mesh.Mesh.GetVertexDeclaration(),
                        meshC.Mesh.Mesh.GetVertexCount(), BufferUsage.WriteOnly);

                    object vertsObj = meshC.Mesh.Mesh.GetType().GetMethod("GetVertices")
                        .Invoke(meshC.Mesh.Mesh, new object[] { });
                    
                    typeof(VertexBuffer).GetMethods().First(x => x.Name == "SetData" && x.GetParameters().Length == 1)
                        .MakeGenericMethod(meshC.Mesh.Mesh.GetVertexType()).Invoke(meshC.Mesh.Mesh.VertexBuffer, new[] {vertsObj});
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

                meshC.EffectProperties.Apply(meshC.Mesh.Mesh);
                foreach (EffectPass pass in meshC.EffectProperties.Technique!.Passes)
                {
                    pass.Apply();
                    gd.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, meshC.Mesh.Mesh.IndexBuffer.IndexCount / 3);
                }
            });
        }

        public override void Deinitialize()
        {
        }
    }
}
