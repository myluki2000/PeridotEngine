using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Scenes.Scene3D;
using SharpDX.Direct3D9;
using Color = Microsoft.Xna.Framework.Color;
using Effect = Microsoft.Xna.Framework.Graphics.Effect;
using Mesh = PeridotEngine.Graphics.Geometry.Mesh;
using VertexDeclaration = Microsoft.Xna.Framework.Graphics.VertexDeclaration;
using VertexElement = Microsoft.Xna.Framework.Graphics.VertexElement;

namespace PeridotEngine.Graphics.Effects
{
    public partial class SimpleEffect : EffectBase, IEffectTexture
    {
        private readonly EffectParameter textureParam;
        private readonly EffectParameter mixColorParam;
        private readonly EffectParameter texturePositionParam;
        private readonly EffectParameter textureSizeParam;
        private TextureResources? textureResources;

        static SimpleEffect()
        {
            EffectPool.RegisterEffectType<SimpleEffect>();
        }

        public SimpleEffect() : base(Globals.Content.Load<Effect>("Effects/SimpleEffect"))
        {
            textureParam = Parameters["Texture"];
            mixColorParam = Parameters["MixColor"];
            texturePositionParam = Parameters["TexturePosition"];
            textureSizeParam = Parameters["TextureSize"];
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
                    textureResources.TextureAtlasChanged += TextureAtlasChanged;
            }
        }

        private void TextureAtlasChanged(object? sender, IEnumerable<TextureResources.TextureInfo> textureInfos)
        {
            textureParam.SetValue(textureResources?.TextureAtlas);
        }

        public override EffectProperties CreatePropertiesBase()
        {
            return CreateProperties();
        }

        public SimpleEffectProperties CreateProperties()
        {
            return new SimpleEffectProperties(this);
        }

        public partial class SimpleEffectProperties : EffectProperties
        {
            public Color MixColor { get; set; } = Color.White;

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
                this.effect = effect;
            }

            public override void Apply(Mesh mesh)
            {
                base.Apply(mesh);

                effect.mixColorParam.SetValue(MixColor.ToVector4());

                if (TextureEnabled)
                {
                    RectangleF bounds = effect.TextureResources.GetTextureBoundsInAtlas(TextureId);
                    effect.texturePositionParam.SetValue(bounds.Location.ToVector2());
                    effect.textureSizeParam.SetValue(bounds.Size.ToVector2());
                }

                if (Technique == null)
                {
                    ChooseTechnique(mesh.GetVertexDeclaration());
                }
            }

            public override EffectBase Effect => effect;

            private readonly SimpleEffect effect;
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

                Technique = effect.Techniques[techniqueIndex];
            }
        }
    }
}
