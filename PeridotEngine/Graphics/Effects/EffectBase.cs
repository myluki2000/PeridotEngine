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
        public abstract Matrix WorldViewProjectionMatrix { get; set; }
        public abstract Color MixColor { get; set; }

        protected EffectBase(Effect cloneSource) : base(cloneSource)
        {
        }
    }
}
