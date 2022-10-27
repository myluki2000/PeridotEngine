using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine.Misc;

namespace PeridotEngine.Graphics.Camera
{
    public class Camera
    {
        public Vector3 Position { get; set; }

        public float Roll
        {
            get => _roll;
            set => _roll = (float)(value % (Math.PI * 2));
        }

        public float Pitch
        {
            get => _pitch;
            set
            {
                if (value > MathHelper.PiOver2) value = MathHelper.PiOver2;
                if (value < -MathHelper.PiOver2) value = -MathHelper.PiOver2;
                _pitch = value;
            }
        }

        public float Yaw
        {
            get => _yaw;
            set => _yaw = (float)(value % (Math.PI * 2));
        }

        private float _roll;
        private float _pitch;
        private float _yaw;

        private Matrix projectionMatrix;

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(-Position) * Matrix.CreateRotationY(Yaw) * Matrix.CreateRotationX(-Pitch) * Matrix.CreateRotationZ(Roll);
        }

        public void UpdateProjectionMatrix()
        {
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2,
                Globals.Graphics.GraphicsDevice.DisplayMode.AspectRatio,
                0.1f,
                100
            );
        }

        public Matrix GetProjectionMatrix()
        {
            return projectionMatrix;
        }

        public Matrix GetLookAtMatrix(Vector3 target)
        {
            return Matrix.CreateLookAt(Position, target, new Vector3(0, 1, 0));
        }

        public void MoveForward(float distance = 1f)
        {
            Position += GetLookDirection() * distance;
        }

        public void MoveBackward(float distance = 1f)
        {
            Position += -GetLookDirection() * distance;
        }

        public void MoveLeft(float distance = 1f)
        {
            Position += GetLookDirection().Transform(Matrix.CreateRotationY(+MathHelper.PiOver2)) * distance;
        }

        public void MoveRight(float distance = 1f)
        {
            Position += GetLookDirection().Transform(Matrix.CreateRotationY(-MathHelper.PiOver2)) * distance;
        }

        public void MoveUp(float distance = 1f)
        {
            Position += new Vector3(0, distance, 0);
        }

        public void MoveDown(float distance = 1f)
        {
            Position += new Vector3(0, -distance, 0);
        }

        private Vector3 GetLookDirection()
        {
            // TODO: Make Y axis work
            Vector3 direction = new Vector3(
                (float)Math.Sin(Yaw) / 1,
                0,
                -(float)Math.Cos(Yaw) / 1
            );
            direction.Normalize();
            return direction;
        }
    }
}
