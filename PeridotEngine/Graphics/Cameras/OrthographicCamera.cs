using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PeridotEngine.Graphics.Cameras
{
    public class OrthographicCamera : Camera
    {
        public override float AspectRatio { get; set; } = 1;

        public OrthographicCamera()
        {
            UpdateProjectionMatrix();
        }

        protected sealed override void UpdateProjectionMatrix()
        {
            ProjectionMatrix = Matrix.CreateOrthographic(20 * AspectRatio, 20, NearPlane, FarPlane);
        }
    }
}
