using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using RectangleF = SharpDX.RectangleF;

namespace PeridotEngine.Scenes.Scene3D
{
    public class SceneResources
    {
        public readonly TextureResources TextureResources = new();
        public readonly MeshResources MeshResources = new();
    }
}
