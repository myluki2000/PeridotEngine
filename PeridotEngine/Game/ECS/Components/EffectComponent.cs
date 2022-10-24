using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Game.ECS.Components
{
    public class EffectComponent : IComponent
    {
        public Effect Effect { get; set; }

        public EffectComponent(Effect effect)
        {
            Effect = effect;
        }
    }
}
