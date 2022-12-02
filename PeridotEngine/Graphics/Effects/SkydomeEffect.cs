using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Geometry;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics.Effects
{
    public class SkydomeEffect : EffectBase, IEffectFog
    {
        private readonly EffectParameter fogStartParam;
        private readonly EffectParameter fogEndParam;
        private readonly EffectParameter fogColorParam;
        private readonly EffectParameter cameraPositionParam;

        public SkydomeEffect() : base(Globals.Content.Load<Effect>("Effects/SkydomeEffect"))
        {
            fogStartParam = Parameters["FogStart"];
            fogEndParam = Parameters["FogEnd"];
            fogColorParam = Parameters["FogColor"];
            cameraPositionParam = Parameters["CameraPosition"];
        }

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

        private Color fogColor;
        public Color FogColor
        {
            get => fogColor;
            set
            {
                fogColor = value;
                fogColorParam.SetValue(value.ToVector4());
            }
        }

        public float FogStart
        {
            get => fogStartParam.GetValueSingle();
            set => fogStartParam.SetValue(value);
        }

        public float FogEnd
        {
            get => fogEndParam.GetValueSingle();
            set => fogEndParam.SetValue(value);
        }

        public Vector3 CameraPosition
        {
            get => cameraPositionParam?.GetValueVector3() ?? Vector3.Zero;
            set => cameraPositionParam?.SetValue(value);
        }

        public partial class SkydomeEffectProperties : EffectProperties
        {
            public override UserControl PropertiesControl { get; }

            public SkydomeEffectProperties(EffectBase effect) : base(effect)
            {
                Technique = Effect.Techniques[0];
            }
        }
    }
}
