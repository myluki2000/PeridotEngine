using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics.PostProcessing
{
    public class SimplePostProcessingEffect : PostProcessingEffectBase
    {
        private readonly EffectParameter? fogColorParam;
        private readonly EffectParameter? fogStartParam;
        private readonly EffectParameter? fogEndParam;

        public SimplePostProcessingEffect() : base(Globals.Content.Load<Effect>("Effects/PostProcessing/SimplePostProcessingEffect"))
        {
            fogColorParam = Effect.Parameters["FogColor"];
            fogStartParam = Effect.Parameters["FogStart"];
            fogEndParam = Effect.Parameters["FogEnd"];
        }

        protected override void ChooseTechnique()
        {
            Technique = Effect.Techniques[0];
        }

        private Color fogColor;
        public Color FogColor
        {
            get => fogColor;
            set
            {
                fogColor = value;
                fogColorParam?.SetValue(value.ToVector4());
            }
        }

        public float FogStart
        {
            get => fogStartParam?.GetValueSingle() ?? 0;
            set => fogStartParam?.SetValue(value);
        }

        public float FogEnd
        {
            get => fogEndParam?.GetValueSingle() ?? 0;
            set => fogEndParam?.SetValue(value);
        }
    }
}
