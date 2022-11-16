using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PeridotEngine.Graphics.Camera
{
    public class OrthographicCamera : Camera
    {
        public OrthographicCamera()
        {
            UpdateProjectionMatrix();
        }

        protected sealed override void UpdateProjectionMatrix()
        {
            ProjectionMatrix = Matrix.CreateOrthographic(20, 20, 0.1f, 100);
        }
    }
}
