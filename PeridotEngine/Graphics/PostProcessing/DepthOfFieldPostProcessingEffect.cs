using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.PostProcessing
{
    public class DepthOfFieldPostProcessingEffect : PostProcessingEffectBase
    {
        private Direction blurDirection;
        public Direction BlurDirection
        {
            get => blurDirection;
            set
            {
                blurDirection = value;
                Technique = null;
            }
        }

        public DepthOfFieldPostProcessingEffect() : base(Globals.Content.Load<Effect>("Effects/PostProcessing/DepthOfFieldEffect"))
        {
        }

        protected override void ChooseTechnique()
        {
            Technique = BlurDirection switch
            {
                Direction.HORIZONTAL => Effect.Techniques[0],
                Direction.VERTICAL => Effect.Techniques[1],
                _ => throw new Exception("Switch case not covered!")
            };
        }

        public enum Direction
        {
            HORIZONTAL,
            VERTICAL
        }
    }
}
