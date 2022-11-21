using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Effects
{
    public interface IEffectShadows
    {
        public Texture2D ShadowMap { get; set; }
        public Matrix LightViewProjection { get; set; }
        public Vector3 LightPosition { get; set; }
    }
}
