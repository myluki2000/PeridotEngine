using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine.Misc;
using Point = Microsoft.Xna.Framework.Point;

namespace PeridotEngine.Graphics.Cameras
{
    public abstract class Camera
    {
        public abstract float AspectRatio { get; set; }
        public bool AllowAutomaticAspectRatioAdjustment { get; set; } = false;

        public Vector3 Position { get; set; }

        public float NearPlane
        {
            get => nearPlane;
            set
            {
                nearPlane = value;
                UpdateProjectionMatrix();
            }
        }

        public float FarPlane
        {
            get => farPlane;
            set
            {
                farPlane = value;
                UpdateProjectionMatrix();
            }
        }

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

        protected Matrix ProjectionMatrix;
        private float nearPlane = 0.1f;
        private float farPlane = 100f;

        public virtual void Update(GameTime gameTime) { }

        public Matrix GetViewMatrix()
        {
            return GetTranslationMatrix() * GetRotationMatrix();
        }

        public Matrix GetTranslationMatrix()
        {
            return Matrix.CreateTranslation(-Position);
        }

        public Matrix GetRotationMatrix()
        {
            return Matrix.CreateRotationY(Yaw) * Matrix.CreateRotationX(-Pitch) * Matrix.CreateRotationZ(Roll);
        }

        protected abstract void UpdateProjectionMatrix();

        public Matrix GetProjectionMatrix()
        {
            return ProjectionMatrix;
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

        public Vector2 WorldPosToScreenPos(Vector3 worldPos)
        {
            Vector4 res = new Vector4(worldPos, 1).Transform(GetViewMatrix() * GetProjectionMatrix());
            res /= res.W;
            return new Vector2((res.X + 1) / 2 * Globals.GraphicsDevice.PresentationParameters.BackBufferWidth,
                               (-res.Y + 1) / 2 * Globals.GraphicsDevice.PresentationParameters.BackBufferHeight);
        }
    }
}
