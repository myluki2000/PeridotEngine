using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics.Effects
{
    public class SimpleEffect : EffectBase, IEffectTexture
    {
        private readonly EffectParameter textureParam;
        private readonly EffectParameter mixColorParam;
        private readonly EffectParameter texturePositionParam;
        private readonly EffectParameter textureSizeParam;

        public SimpleEffect() : base(Globals.Content.Load<Effect>("Effects/SimpleEffect"))
        {
            textureParam = Parameters["Texture"];
            mixColorParam = Parameters["MixColor"];
            texturePositionParam = Parameters["TexturePosition"];
            textureSizeParam = Parameters["TextureSize"];
        }

        public override Color MixColor
        {
            get => mixColor;
            set
            {
                if (value == mixColor) return;
                mixColor = value;
                mixColorParam.SetValue(value.ToVector4());
            }
        }

        public Texture2D Texture
        {
            get => textureParam.GetValueTexture2D();
            set => textureParam.SetValue(value);
        }

        public Vector2 TexturePosition
        {
            get => texturePosition;
            set
            {
                if (texturePosition == value) return;
                texturePosition = value;
                texturePositionParam.SetValue(value);
            }
        }

        public Vector2 TextureSize
        {
            get => textureSize;
            set
            {
                if (textureSize == value) return;
                textureSize = value;
                textureSizeParam.SetValue(value);
            }
        }

        private Color mixColor;
        private Vector2 texturePosition;
        private Vector2 textureSize;
    }
}
