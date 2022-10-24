using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Effects
{
    public abstract class EffectBase : Effect
    {
        public abstract Matrix WorldViewProjectionMatrix { get; set; }

        protected EffectBase(Effect cloneSource) : base(cloneSource)
        {
        }
    }
}
