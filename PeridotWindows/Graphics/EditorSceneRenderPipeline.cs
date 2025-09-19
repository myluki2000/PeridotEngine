using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Systems;

namespace PeridotWindows.Graphics
{
    public class EditorSceneRenderPipeline : SceneRenderPipeline
    {
        private readonly EditorLightVisualizationRenderingSystem lightVisRenderingSystem;

        public EditorSceneRenderPipeline(Scene3D scene) : base(scene)
        {
            lightVisRenderingSystem = new EditorLightVisualizationRenderingSystem(scene);
        }

        public Archetype.Entity? SelectedEntity { get; set; }

        protected override void RenderMeshes(GraphicsDevice gd)
        {
            base.RenderMeshes(gd);

            lightVisRenderingSystem.DrawLightVisualization(gd);
            DrawSelectedObjectBox(gd);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        /// <summary>
        /// Helper method to draw a box around the currently selected object.
        /// </summary>
        private void DrawSelectedObjectBox(GraphicsDevice gd)
        {
            if (SelectedEntity == null)
                return;

            if (SelectedEntity.Archetype.HasComponent<StaticMeshComponent>() && SelectedEntity.Archetype.HasComponent<PositionRotationScaleComponent>())
            {
                StaticMeshComponent meshC = SelectedEntity.GetComponent<StaticMeshComponent>();
                PositionRotationScaleComponent posC = SelectedEntity.GetComponent<PositionRotationScaleComponent>();

                SimpleEffect effect = new();
                effect.World = posC.Transformation;
                effect.View = Scene.Camera.GetViewMatrix();
                effect.Projection = Scene.Camera.GetProjectionMatrix();
                effect.Apply();

                if (meshC.Mesh == null || meshC.Mesh.Mesh == null)
                    return;

                BoundingBox bounds = meshC.Mesh.Mesh.Bounds;

                VertexPosition[] verts =
                [
                    // bottom layer
                    new(bounds.Min), // front bottom left
                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Min.Z)), // front bottom right

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Min.Z)), // front bottom right
                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Max.Z)), // back bottom right

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Max.Z)), // back bottom right
                    new(new(bounds.Min.X, bounds.Min.Y, bounds.Max.Z)), // back bottom left
                    
                    new(new(bounds.Min.X, bounds.Min.Y, bounds.Max.Z)), // back bottom left
                    new(bounds.Min), // front bottom left

                    // edges between bottom and top layer
                    new(bounds.Min), // front bottom left
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Min.Z)), // front top left

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Min.Z)), // front bottom right
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Min.Z)), // front top right

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Max.Z)), // back bottom right
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Max.Z)), // back top right

                    new(new(bounds.Min.X, bounds.Min.Y, bounds.Max.Z)), // back bottom left
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Max.Z)), // back top left

                    // top layer
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Min.Z)), // front top left
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Min.Z)), // front top right

                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Min.Z)), // front top right
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Max.Z)), // back top right

                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Max.Z)), // back top right
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Max.Z)), // back top left
                    
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Max.Z)), // back top left
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Min.Z)) // front top left
                ];

                foreach (EffectPass pass in effect.Techniques[0].Passes)
                {
                    pass.Apply();
                    gd.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, verts.Length / 2);
                }
            }
        }
    }
}
