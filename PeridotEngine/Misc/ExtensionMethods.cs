using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Misc
{
    static class ExtensionMethods
    {
        public static Vector3 Transform(this Vector3 v, Matrix matrix)
        {
            return Vector3.Transform(v, matrix);
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
    }
}
