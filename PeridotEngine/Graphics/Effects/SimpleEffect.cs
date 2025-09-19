using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Misc;
using PeridotEngine.Scenes.Scene3D;
using Color = Microsoft.Xna.Framework.Color;
using Effect = Microsoft.Xna.Framework.Graphics.Effect;
using Mesh = PeridotEngine.Graphics.Geometry.Mesh;
using RectangleF = SharpDX.RectangleF;
using VertexDeclaration = Microsoft.Xna.Framework.Graphics.VertexDeclaration;
using VertexElement = Microsoft.Xna.Framework.Graphics.VertexElement;

namespace PeridotEngine.Graphics.Effects
{
    public partial class SimpleEffect : EffectBase, IEffectTexture, IEffectShadows, IEffectCameraData
    {
        private readonly EffectParameter textureParam;
        private readonly EffectParameter mixColorParam;
        private readonly EffectParameter texturePositionParam;
        private readonly EffectParameter textureSizeParam;
        private readonly EffectParameter textureRepeatParam;

        private readonly EffectParameter shadowMapParam;
        private readonly EffectParameter lightWorldViewProjParam;
        private readonly EffectParameter lightPositionParam;

        private readonly EffectParameter? cameraPositionParam;

        public SimpleEffect() : base(Globals.Content.Load<Effect>("Effects/SimpleEffect"))
        {
            textureParam = Parameters["Texture"];
            shadowMapParam = Parameters["ShadowMap"];
            mixColorParam = Parameters["MixColor"];
            texturePositionParam = Parameters["TexturePosition"];
            textureSizeParam = Parameters["TextureSize"];
            lightWorldViewProjParam = Parameters["LightWorldViewProjection"];
            textureRepeatParam = Parameters["TextureRepeat"];
            lightPositionParam = Parameters["LightPosition"];
            cameraPositionParam = Parameters["CameraPosition"];
        }

        public Vector3 CameraPosition
        {
            get => cameraPositionParam?.GetValueVector3() ?? Vector3.Zero;
            set => cameraPositionParam?.SetValue(value);
        }

        public TextureResources? TextureResources
        {
            get;
            set
            {
                if (value == field)
                    return;

                if (field != null)
                    field.TextureAtlasChanged.RemoveHandler(TextureAtlasChanged);

                field = value;

                if (field != null)
                {
                    field.TextureAtlasChanged.AddWeakHandler(TextureAtlasChanged);
                    TextureAtlasChanged(null, field.GetAllTextures());
                }
            }
        }

        public Texture2D ShadowMap
        {
            get => shadowMapParam.GetValueTexture2D();
            set => shadowMapParam.SetValue(value);
        }

        public Matrix LightViewProjection { get; set; }

        public Vector3 LightPosition
        {
            get => lightPositionParam.GetValueVector3();
            set => lightPositionParam.SetValue(value);
        }

        private void TextureAtlasChanged(object? sender, IEnumerable<TextureResources.ITextureInfo> textureInfos)
        {
            textureParam?.SetValue(TextureResources?.TextureAtlas);
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

        public override void Apply()
        {
            base.Apply();

            Matrix lightWorldViewProj = World * LightViewProjection;

            lightWorldViewProjParam.SetValue(lightWorldViewProj);
        }

        public partial class SimpleEffectProperties(SimpleEffect effect) : EffectProperties(effect)
        {
            public Color MixColor
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    ValuesChanged.Invoke(this, this);
                }
            } = Color.White;

            public bool ShadowsEnabled
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    Technique = null;
                    ValuesChanged.Invoke(this, this);
                }
            } = true;

            public bool VertexColorEnabled
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    Technique = null;
                    ValuesChanged.Invoke(this, this);
                }
            }

            public bool TextureEnabled
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    Technique = null;
                    ValuesChanged.Invoke(this, this);
                }
            }

            public bool DiffuseShadingEnabled
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    Technique = null;
                    ValuesChanged.Invoke(this, this);
                }
            } = true;

            public bool RandomTextureRotationEnabled
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    Technique = null;
                    ValuesChanged.Invoke(this, this);
                }
            } = false;

            public bool ObjectPickingEnabled
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    Technique = null;
                    ValuesChanged.Invoke(this, this);
                }
            } = true;

            public int TextureId
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    ValuesChanged.Invoke(this, this);
                }
            }

            public int TextureRepeatX
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    ValuesChanged.Invoke(this, this);
                }
            } = 1;

            public int TextureRepeatY
            {
                get;
                set
                {
                    if (field == value)
                        return;

                    field = value;
                    ValuesChanged.Invoke(this, this);
                }
            } = 1;

            public override void Apply(Mesh mesh)
            {
                base.Apply(mesh);

                SimpleEffect.mixColorParam.SetValue(MixColor.ToVector4());

                if (TextureEnabled)
                {
                    RectangleF bounds = SimpleEffect.TextureResources.GetTextureBoundsInAtlas(TextureId);
                    SimpleEffect.texturePositionParam.SetValue(bounds.Location.ToXnaVector2());
                    SimpleEffect.textureSizeParam.SetValue(bounds.Size.ToXnaVector2());
                    SimpleEffect.textureRepeatParam.SetValue(new Vector2(TextureRepeatX, TextureRepeatY));
                }

                // TODO: I think this doesn't take into account whether the passed mesh has a different vertex declaration
                // than the one originally used for ChooseTechnique(). However this usually doesn't matter because
                // EffectProperties are kept by the MeshComponent, so an EffectProperty has some Mesh associated with it
                // that doesn't change
                if (Technique == null)
                {
                    ChooseTechnique(mesh.GetVertexDeclaration());
                }
            }

            private SimpleEffect SimpleEffect => (SimpleEffect)Effect;

            private static readonly Dictionary<int, EffectTechnique> techniquesDict = new();

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

                if (DiffuseShadingEnabled)
                {
                    if (vertexElements.All(x => x.VertexElementUsage != VertexElementUsage.Normal))
                        throw new Exception("DiffuseShadingEnabled is set to true but mesh does not contain normal data.");

                    techniqueIndex |= 0b01000;
                }

                if (RandomTextureRotationEnabled)
                {
                    techniqueIndex |= 0b10000;
                }

                if (ObjectPickingEnabled)
                {
                    techniqueIndex |= 0b100000;
                }

                if (techniquesDict.TryGetValue(techniqueIndex, out EffectTechnique? technique))
                {
                    Technique = technique;
                }
                else
                {
                    Technique = Effect.Techniques[UfxHelper.GenerateTechniqueId("SimpleEffect",
                        VertexColorEnabled, TextureEnabled, ShadowsEnabled,
                        DiffuseShadingEnabled, RandomTextureRotationEnabled, ObjectPickingEnabled)];
                    techniquesDict.Add(techniqueIndex, Technique);
                }
            }
        }
    }
}
