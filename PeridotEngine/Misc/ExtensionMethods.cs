using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace PeridotEngine.Misc
{
    public static class ExtensionMethods
    {
        public static Vector3 Transform(this Vector3 v, Matrix matrix)
        {
            return Vector3.Transform(v, matrix);
        }

        public static Vector4 Transform(this Vector4 v, Matrix matrix)
        {
            return Vector4.Transform(v, matrix);
        }

        public static System.Drawing.Color ToSystemColor(this Color value)
        {
            System.Drawing.Color result = System.Drawing.Color.FromArgb(
                value.A,
                value.R,
                value.G,
                value.B
            );
            return result;
        }

        public static Color ToXnaColor(this System.Drawing.Color value)
        {
            return new Color(value.R, value.G, value.B, value.A);
        }

        public static Vector3 ToEulerAngles(this Quaternion q)
        {
            float yaw = (float)Math.Atan2(2.0 * (q.Y * q.Z + q.W * q.X), q.W * q.W - q.X * q.X - q.Y * q.Y + q.Z * q.Z);
            float pitch = (float)Math.Asin(-2.0 * (q.X * q.Z - q.W * q.Y));
            float roll = (float)Math.Atan2(2.0 * (q.X * q.Y + q.W * q.Z), q.W * q.W + q.X * q.X - q.Y * q.Y - q.Z * q.Z);

            return new Vector3(pitch, yaw, roll);
        }

        public static Vector2 ToXnaVector2(this SharpDX.Vector2 value)
        {
            // TODO: Can probably be optimized with unsafe code
            return new Vector2(value.X, value.Y);
        }

        public static Vector2 ToXnaVector2(this SharpDX.Size2F value)
        {
            // TODO: Can probably be optimized with unsafe code
            return new Vector2(value.Width, value.Height);
        }

        public static System.Numerics.Vector3 ToNumericsVector3(this Vector3 value)
        {
            // TODO: Can probably be optimized with unsafe code
            return new System.Numerics.Vector3(value.X, value.Y, value.Z);
        }

        public static Vector3 ToXnaVector3(this System.Numerics.Vector3 value)
        {
            // TODO: Can probably be optimized with unsafe code
            return new Vector3(value.X, value.Y, value.Z);
        }

        public static Point BackBufferSize(this PresentationParameters pp)
        {
            return new Point(pp.BackBufferWidth, pp.BackBufferHeight);
        }
    }
}
