using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.PostProcessing
{
    public class DepthOfFieldPostProcessingEffect : PostProcessingEffectBase
    {
        public DepthOfFieldPostProcessingEffect() : base(Globals.Content.Load<Effect>("Effects/PostProcessing/DepthOfFieldEffect"))
        {
        }

        protected override void ChooseTechnique()
        {
            Technique = Effect.Techniques[0];
        }
    }
}
