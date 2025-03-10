using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.ECS.Components;
using PeridotEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Systems
{
    public class EditorLightVisualizationRenderingSystem(Scene3D scene)
    {
        private readonly Query sunlightQuery = scene.Ecs.Query().Has<SunLightComponent>().Has<PositionRotationScaleComponent>();

        public void DrawLightVisualization(GraphicsDevice gd)
        {
            DrawSunlightVisualization(gd);
        }

        /// <summary>
        /// Helper method to draw a representation of sunlight objects in the editor.
        /// </summary>
        private void DrawSunlightVisualization(GraphicsDevice gd)
        {
            sunlightQuery.ForEach(
                (uint _, PositionRotationScaleComponent posC) =>
                {
                    SimpleEffect effect = new();
                    effect.World = Matrix.Identity;
                    effect.View = scene.Camera.GetViewMatrix();
                    effect.Projection = scene.Camera.GetProjectionMatrix();
                    effect.Apply();

                    // direction the sun is "facing"
                    Vector3 direction = new Vector3(
                        (float)Math.Sin(posC.Rotation.Y),
                        0,
                        -(float)Math.Cos(posC.Rotation.Y)
                    );
                    direction.Normalize();
                    direction *= (float)Math.Cos(posC.Rotation.X);
                    direction.Y = (float)Math.Sin(posC.Rotation.X);
                    direction.Normalize();

                    VertexPosition[] verts = new[]
                    {
                        new VertexPosition(posC.Position),
                        new VertexPosition(posC.Position + direction)
                    };

                    foreach (EffectPass pass in effect.Techniques[0].Passes)
                    {
                        pass.Apply();
                        gd.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, 1);
                    }
                });
        }
    }
}
