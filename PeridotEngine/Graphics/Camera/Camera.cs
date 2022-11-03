using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public virtual void Update(GameTime gameTime) {}

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(-Position) * Matrix.CreateRotationY(Yaw) * Matrix.CreateRotationX(-Pitch) * Matrix.CreateRotationZ(Roll);
        }

        public void UpdateProjectionMatrix()
        {
            Debug.WriteLine(Globals.Graphics.GraphicsDevice.DisplayMode.AspectRatio);
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(80),
                (float)Globals.Graphics.PreferredBackBufferWidth / Globals.Graphics.PreferredBackBufferHeight,
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
            Position += GetLookDirectionPlanar().Transform(Matrix.CreateRotationY(+MathHelper.PiOver2)) * distance;
        }

        public void MoveRight(float distance = 1f)
        {
            Position += GetLookDirectionPlanar().Transform(Matrix.CreateRotationY(-MathHelper.PiOver2)) * distance;
        }

        public void MoveUp(float distance = 1f)
        {
            Position += new Vector3(0, distance, 0);
        }

        public void MoveDown(float distance = 1f)
        {
            Position += new Vector3(0, -distance, 0);
        }

        public Vector3 GetLookDirection()
        {
            Vector3 direction = new Vector3(
                (float)Math.Sin(Yaw),
                0,
                -(float)Math.Cos(Yaw)
            );
            direction.Normalize();
            direction *= (float)Math.Cos(Pitch);
            direction.Y = (float)Math.Sin(Pitch);
            direction.Normalize();
            return direction;
        }

        public Vector3 GetLookDirectionPlanar()
        {
            Vector3 direction = new Vector3(
                (float)Math.Sin(Yaw),
                0,
                -(float)Math.Cos(Yaw)
            );
            direction.Normalize();
            return direction;
        }
    }
}
