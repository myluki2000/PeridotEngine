using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PeridotEngine.Misc
{
    static class ExtensionMethods
    {
        public static Vector3 Transform(this Vector3 v, Matrix matrix)
        {
            return Vector3.Transform(v, matrix);
        }
    }
}
