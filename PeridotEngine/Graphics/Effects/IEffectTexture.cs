using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.Graphics.Effects
{
    internal interface IEffectTexture
    {
        public TextureResources? TextureResources { get; set; }
    }
}
