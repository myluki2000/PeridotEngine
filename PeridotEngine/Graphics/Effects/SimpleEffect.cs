using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Effects
{
    public class SimpleEffect : EffectBase, IEffectTexture
    {
        private readonly EffectParameter worldViewProjParam;
        private readonly EffectParameter textureParam;

        public SimpleEffect() : base(Globals.Content.Load<Effect>("Effects/SimpleEffect"))
        {
            worldViewProjParam = Parameters["WorldViewProjection"];
            textureParam = Parameters["Texture"];
        }

        private Matrix worldViewProj;
        public override Matrix WorldViewProjectionMatrix
        {
            get => worldViewProj;
            set
            {
                worldViewProj = value;
                worldViewProjParam.SetValue(value);
            }
        }

        public Texture2D Texture
        {
            get => textureParam.GetValueTexture2D();
            set => textureParam.SetValue(value);
        }
    }
}
