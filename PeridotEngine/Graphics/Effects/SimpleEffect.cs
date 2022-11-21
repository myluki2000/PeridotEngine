using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;
using SharpDX.Direct3D9;
using Color = Microsoft.Xna.Framework.Color;
using Effect = Microsoft.Xna.Framework.Graphics.Effect;
using Mesh = PeridotEngine.Graphics.Geometry.Mesh;
using VertexDeclaration = Microsoft.Xna.Framework.Graphics.VertexDeclaration;
using VertexElement = Microsoft.Xna.Framework.Graphics.VertexElement;

namespace PeridotEngine.Graphics.Effects
{
    public partial class SimpleEffect : EffectBase, IEffectTexture, IEffectShadows, IEffectFog
    {
        private readonly EffectParameter textureParam;
        private readonly EffectParameter mixColorParam;
        private readonly EffectParameter texturePositionParam;
        private readonly EffectParameter textureSizeParam;
        private readonly EffectParameter textureRepeatParam;

        private readonly EffectParameter shadowMapParam;
        private readonly EffectParameter lightWorldViewProjParam;
        private readonly EffectParameter lightPositionParam;

        private readonly EffectParameter fogStartParam;
        private readonly EffectParameter fogEndParam;
        private readonly EffectParameter fogColorParam;

        private TextureResources? textureResources;

        public SimpleEffect() : base(Globals.Content.Load<Effect>("Effects/SimpleEffect"))
        {
            textureParam = Parameters["Texture"];
            shadowMapParam = Parameters["ShadowMap"];
            mixColorParam = Parameters["MixColor"];
            texturePositionParam = Parameters["TexturePosition"];
            textureSizeParam = Parameters["TextureSize"];
            lightWorldViewProjParam = Parameters["LightWorldViewProjection"];
            textureRepeatParam = Parameters["TextureRepeat"];
            fogStartParam = Parameters["FogStart"];
            fogEndParam = Parameters["FogEnd"];
            fogColorParam = Parameters["FogColor"];
            lightPositionParam = Parameters["lightPosition"];
        }

        public TextureResources? TextureResources
        {
            get => textureResources;
            set
            {
                if (textureResources != null)
                    textureResources.TextureAtlasChanged -= TextureAtlasChanged;
                
                textureResources = value;

                if (textureResources != null)
                {
                    textureResources.TextureAtlasChanged += TextureAtlasChanged;
                    TextureAtlasChanged(null, textureResources.GetAllTextures());
                }
            }
        }

        public Texture2D ShadowMap
        {
            get => shadowMapParam.GetValueTexture2D();
            set => shadowMapParam.SetValue(value);
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

        public Matrix LightViewProjection { get; set; }

        public Vector3 LightPosition
        {
            get => lightPositionParam.GetValueVector3();
            set => lightPositionParam.SetValue(value);
        }

        private void TextureAtlasChanged(object? sender, IEnumerable<TextureResources.TextureInfo> textureInfos)
        {
            textureParam.SetValue(textureResources?.TextureAtlas);
        }

        public override EffectProperties CreatePropertiesBase()
        {
            return CreateProperties();
        }

        public override Type GetPropertiesType()
        {
            return typeof(SimpleEffectProperties);
        }

        public SimpleEffectProperties CreateProperties()
        {
            return new SimpleEffectProperties(this);
        }

        public override void UpdateMatrices()
        {
            base.UpdateMatrices();

            Matrix lightWorldViewProj = World * LightViewProjection;

            lightWorldViewProjParam.SetValue(lightWorldViewProj);
        }

        public partial class SimpleEffectProperties : EffectProperties
        {
            public Color MixColor { get; set; } = Color.White;

            public bool ShadowsEnabled
            {
                get => shadowsEnabled;
                set
                {
                    shadowsEnabled = value;
                    Technique = null;
                }
            }

            public bool FogEnabled
            {
                get => fogEnabled;
                set
                {
                    fogEnabled = value;
                    Technique = null;
                }
            }

            public bool VertexColorEnabled
            {
                get => vertexColorEnabled;
                set
                {
                    vertexColorEnabled = value;
                    Technique = null;
                }
            }

            public bool TextureEnabled
            {
                get => textureEnabled;
                set
                {
                    textureEnabled = value;
                    Technique = null;
                }
            }

            public bool DiffuseShadingEnabled
            {
                get => diffuseShadingEnabled;
                set
                {
                    diffuseShadingEnabled = value;
                    Technique = null;
                }
            }

            public uint TextureId { get; set; }

            public int TextureRepeatX { get; set; } = 1;
            public int TextureRepeatY { get; set; } = 1;

            public SimpleEffectProperties(SimpleEffect effect)
            {
                Effect = effect;
            }

            public override void Apply(Mesh mesh)
            {
                base.Apply(mesh);

                SimpleEffect.mixColorParam.SetValue(MixColor.ToVector4());

                if (TextureEnabled)
                {
                    RectangleF bounds = SimpleEffect.TextureResources.GetTextureBoundsInAtlas(TextureId);
                    SimpleEffect.texturePositionParam.SetValue(bounds.Location.ToVector2());
                    SimpleEffect.textureSizeParam.SetValue(bounds.Size.ToVector2());
                    SimpleEffect.textureRepeatParam.SetValue(new Vector2(TextureRepeatX, TextureRepeatY));
                }

                if (Technique == null)
                {
                    ChooseTechnique(mesh.GetVertexDeclaration());
                }
            }

            private SimpleEffect SimpleEffect => (SimpleEffect)Effect;

            private bool textureEnabled;
            private bool vertexColorEnabled;
            private bool shadowsEnabled = true;
            private bool fogEnabled = true;
            private bool diffuseShadingEnabled = true;

            private void ChooseTechnique(VertexDeclaration vertexDeclaration)
            {
                int techniqueIndex = 0;

                VertexElement[] vertexElements = vertexDeclaration.GetVertexElements();

                if (vertexElements.All(x => x.VertexElementUsage != VertexElementUsage.Position))
                    throw new Exception("Vertex declaration needs to contain position element!");

                if (VertexColorEnabled)
                {
                    if (vertexElements.All(x => x.VertexElementUsage != VertexElementUsage.Color))
                        throw new Exception(
                            "VertexColorEnabled is set to true but mesh does not contain vertex color data.");

                    techniqueIndex |= 0b00001;
                }

                if (TextureEnabled)
                {
                    if (vertexElements.All(x => x.VertexElementUsage != VertexElementUsage.TextureCoordinate))
                        throw new Exception("TextureEnabled is set to true but mesh does not contain UV data.");

                    techniqueIndex |= 0b00010;
                }

                if (ShadowsEnabled)
                {
                    techniqueIndex |= 0b00100;
                }

                if (FogEnabled)
                {
                    techniqueIndex |= 0b01000;
                }

                if (DiffuseShadingEnabled)
                {
                    if (vertexElements.All(x => x.VertexElementUsage != VertexElementUsage.Normal))
                        throw new Exception("DiffuseShadingEnabled is set to true but mesh does not contain normal data.");

                    techniqueIndex |= 0b10000;
                }

                Technique = Effect.Techniques[techniqueIndex];
            }
        }
    }
}
