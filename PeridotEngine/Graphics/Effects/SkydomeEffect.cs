﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Geometry;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics.Effects
{
    public partial class SkydomeEffect() : EffectBase(Globals.Content.Load<Effect>("Effects/SkydomeEffect"))
    {
        public override EffectProperties CreatePropertiesBase()
        {
            return CreateProperties();
        }

        public SkydomeEffectProperties CreateProperties()
        {
            return new SkydomeEffectProperties(this);
        }

        public override Type GetPropertiesType()
        {
            return typeof(SkydomeEffectProperties);
        }

        public partial class SkydomeEffectProperties : EffectProperties
        {
            public override event EventHandler<EffectProperties>? ValuesChanged;

            public SkydomeEffectProperties(EffectBase effect) : base(effect)
            {
                Technique = Effect.Techniques[0];
            }
        }
    }
}
