using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeridotEngine.Graphics.Cameras
{
    public class PerspectiveCamera : Camera
    {
        private float aspectRatio = 1;
        public float AspectRatio
        {
            get => aspectRatio;
            set
            {
                aspectRatio = value;
                UpdateProjectionMatrix();
            }
        }

        public PerspectiveCamera()
        {
            UpdateProjectionMatrix();
        }

        protected sealed override void UpdateProjectionMatrix()
        {
            ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(80),
                aspectRatio,
                0.1f,
                100
            );
        }
    }
}
