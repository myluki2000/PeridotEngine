using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Effects
{
    public class SimpleEffect : EffectBase
    {
        private readonly EffectParameter worldViewProjParam;

        public SimpleEffect() : base(Globals.Content.Load<Effect>("Effects/SimpleEffect"))
        {
            worldViewProjParam = Parameters["WorldViewProjection"];
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
    }
}
