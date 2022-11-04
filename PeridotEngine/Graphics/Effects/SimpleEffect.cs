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
    public partial class SimpleEffect : EffectBase, IEffectTexture, IEffectShadows
    {
        private readonly EffectParameter textureParam;
        private readonly EffectParameter mixColorParam;
        private readonly EffectParameter texturePositionParam;
        private readonly EffectParameter textureSizeParam;


        private readonly EffectParameter enableShadowsParam;
        private readonly EffectParameter shadowMapParam;
        private readonly EffectParameter lightWorldViewProjParam;
        private TextureResources? textureResources;

        public SimpleEffect() : base(Globals.Content.Load<Effect>("Effects/SimpleEffect"))
        {
            textureParam = Parameters["Texture"];
            shadowMapParam = Parameters["ShadowMap"];
            mixColorParam = Parameters["MixColor"];
            texturePositionParam = Parameters["TexturePosition"];
            textureSizeParam = Parameters["TextureSize"];
            enableShadowsParam = Parameters["EnableShadows"];
            lightWorldViewProjParam = Parameters["LightWorldViewProjection"];
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

        public Matrix LightViewProjection { get; set; }

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
            public bool EnableShadows { get; set; }

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

            public uint TextureId { get; set; }

            public SimpleEffectProperties(SimpleEffect effect)
            {
                Effect = effect;
            }

            public override void Apply(Mesh mesh)
            {
                base.Apply(mesh);

                SimpleEffect.mixColorParam.SetValue(MixColor.ToVector4());
                SimpleEffect.enableShadowsParam.SetValue(EnableShadows);

                if (TextureEnabled)
                {
                    RectangleF bounds = SimpleEffect.TextureResources.GetTextureBoundsInAtlas(TextureId);
                    SimpleEffect.texturePositionParam.SetValue(bounds.Location.ToVector2());
                    SimpleEffect.textureSizeParam.SetValue(bounds.Size.ToVector2());
                }

                if (Technique == null)
                {
                    ChooseTechnique(mesh.GetVertexDeclaration());
                }
            }

            private SimpleEffect SimpleEffect => (SimpleEffect)Effect;

            private bool textureEnabled;
            private bool vertexColorEnabled;

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

                    techniqueIndex += 1;
                }


                if (TextureEnabled)
                {
                    if (vertexElements.All(x => x.VertexElementUsage != VertexElementUsage.TextureCoordinate))
                        throw new Exception("TextureEnabled is set to true but mesh does not contain UV data.");

                    techniqueIndex += 2;
                }

                Technique = Effect.Techniques[techniqueIndex];
            }
        }
    }
}
