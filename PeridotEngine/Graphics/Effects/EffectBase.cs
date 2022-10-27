using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics.Effects
{
    public abstract class EffectBase : Effect
    {
        public Matrix World { get; set; } = Matrix.Identity;
        public Matrix ViewProjection { get; set; }
        public abstract Color MixColor { get; set; }

        protected readonly EffectParameter WorldViewProjParam;

        protected EffectBase(Effect cloneSource) : base(cloneSource)
        {
            WorldViewProjParam = Parameters["WorldViewProjection"];
        }

        protected override void OnApply()
        {
            Matrix worldViewProjection = World * ViewProjection;
            WorldViewProjParam.SetValue(worldViewProjection);
        }
    }
}
