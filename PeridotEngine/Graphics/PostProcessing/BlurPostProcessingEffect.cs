using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.PostProcessing
{
    public class BlurPostProcessingEffect : PostProcessingEffectBase
    {
        private Direction blurDirection;
        public Direction BlurDirection
        {
            get => blurDirection;
            set
            {
                blurDirection = value;
                Technique = null!;
            }
        }

        public float KernelSize
        {
            get => kernelSizeParam.GetValueSingle();
            set => kernelSizeParam.SetValue(value);
        }

        private readonly EffectParameter kernelSizeParam;

        public BlurPostProcessingEffect() : base(Globals.Content.Load<Effect>("Effects/PostProcessing/BlurEffect"))
        {
            kernelSizeParam = Effect.Parameters["KernelSize"];
            KernelSize = 0.03f;
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

