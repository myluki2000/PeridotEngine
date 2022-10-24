using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Effects;

namespace PeridotEngine.Game.ECS.Components
{
    public class EffectComponent : IComponent
    {
        public EffectBase Effect { get; set; }

        public EffectComponent(EffectBase effect)
        {
            Effect = effect;
        }
    }
}
