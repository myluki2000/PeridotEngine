using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Effects
{
    public class DepthEffect : EffectBase
    {
        public DepthEffect() : base(Globals.Content.Load<Effect>("Effects/DepthEffect"))
        {
        }

        public override EffectProperties CreatePropertiesBase()
        {
            throw new NotImplementedException();
        }

        public override Type GetPropertiesType()
        {
            throw new NotImplementedException();
        }
    }
}
