using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.PostProcessing
{
    public abstract partial class PostProcessingEffectBase
    {
        protected readonly Effect Effect;

        private readonly EffectParameter? colorTextureParam;
        private readonly EffectParameter? depthTextureParam;
        private readonly EffectParameter? projectionParam;
        private readonly EffectParameter? inverseProjectionParam;

        protected PostProcessingEffectBase(Effect cloneSource)
        {
            Effect = cloneSource;
            colorTextureParam = Effect.Parameters["ColorTexture"];
            depthTextureParam = Effect.Parameters["DepthTexture"];
            projectionParam = Effect.Parameters["Projection"];
            inverseProjectionParam = Effect.Parameters["InverseProjection"];
        }

        public void UpdateParameters(Texture2D colorTexture, Texture2D depthTexture, Matrix projection)
        {
            colorTextureParam?.SetValue(colorTexture);
            depthTextureParam?.SetValue(depthTexture);
            projectionParam?.SetValue(projection);
            inverseProjectionParam?.SetValue(Matrix.Invert(projection));
        }

        protected abstract void ChooseTechnique();

        private EffectTechnique? technique;
        public EffectTechnique Technique
        {
            get
            {
                if(technique == null)
                    ChooseTechnique();

                return technique!;
            }
            protected set => technique = value;
        }
    }
}
