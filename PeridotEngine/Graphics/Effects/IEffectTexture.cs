using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Effects
{
    internal interface IEffectTexture
    {
        public Texture2D Texture { get; set; }
        public Vector2 TexturePosition { get; set; }
        public Vector2 TextureSize { get; set; }
    }
}
