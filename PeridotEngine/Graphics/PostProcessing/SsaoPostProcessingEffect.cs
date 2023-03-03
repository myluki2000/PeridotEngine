using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.PostProcessing
{
    public class SsaoPostProcessingEffect : PostProcessingEffectBase
    {
        public SsaoPostProcessingEffect() : base(Globals.Content.Load<Effect>("Effects/PostProcessing/SsaoEffect"))
        {
        }

        protected override void ChooseTechnique()
        {
            Technique = Effect.Techniques[0];
        }
    }
}
