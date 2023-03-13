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
        private readonly EffectParameter? normalTextureParam;
        private readonly EffectParameter? projectionParam;
        private readonly EffectParameter? inverseProjectionParam;
        private readonly EffectParameter? cameraNearPlaneParam;
        private readonly EffectParameter? cameraFarPlaneParam;
        private readonly EffectParameter? aspectRatioParam;

        protected PostProcessingEffectBase(Effect cloneSource)
        {
            Effect = cloneSource;
            colorTextureParam = Effect.Parameters["ColorTexture"];
            depthTextureParam = Effect.Parameters["DepthTexture"];
            normalTextureParam = Effect.Parameters["NormalTexture"];
            projectionParam = Effect.Parameters["Projection"];
            inverseProjectionParam = Effect.Parameters["InverseProjection"];
            cameraNearPlaneParam = Effect.Parameters["NearPlane"];
            cameraFarPlaneParam = Effect.Parameters["FarPlane"];
            aspectRatioParam = Effect.Parameters["AspectRatio"];
        }

        public void UpdateParameters(Texture2D colorTexture, Texture2D depthTexture, Texture2D normalTexture, Matrix projection, float nearPlane, float farPlane)
        {
            colorTextureParam?.SetValue(colorTexture);
            depthTextureParam?.SetValue(depthTexture);
            normalTextureParam?.SetValue(normalTexture);
            projectionParam?.SetValue(projection);
            inverseProjectionParam?.SetValue(Matrix.Invert(projection));
            cameraNearPlaneParam?.SetValue(nearPlane);
            cameraFarPlaneParam?.SetValue(farPlane);
            aspectRatioParam?.SetValue(colorTexture.Width / colorTexture.Height);
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
