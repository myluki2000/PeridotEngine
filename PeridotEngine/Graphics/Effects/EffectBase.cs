using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Geometry;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics.Effects
{
    public abstract partial class EffectBase : Effect
    {
        public Matrix World { get; set; } = Matrix.Identity;
        public Matrix ViewProjection { get; set; }

        [JsonIgnore]
        protected readonly EffectParameter WorldParam;
        [JsonIgnore]
        protected readonly EffectParameter? TransposedInverseWorldParam;
        [JsonIgnore]
        protected readonly EffectParameter ViewProjectionParam;

        protected EffectBase(Effect cloneSource) : base(cloneSource)
        {
            WorldParam = Parameters["World"];
            ViewProjectionParam = Parameters["ViewProjection"];
            TransposedInverseWorldParam = Parameters["TransposedInverseWorld"];
        }

        public virtual void UpdateMatrices()
        {
            WorldParam.SetValue(World);
            ViewProjectionParam.SetValue(ViewProjection);
            
            // the matrix is only calculated if the TransposedInverseWorld parameter is defined in the shader code
            TransposedInverseWorldParam?.SetValue(Matrix.Transpose(Matrix.Invert(World)));
        }

        public abstract EffectProperties CreatePropertiesBase();
        public abstract Type GetPropertiesType();

        public abstract partial class EffectProperties
        {
            public virtual void Apply(Mesh mesh)
            {
                Effect.UpdateMatrices();
            }

            [JsonIgnore]
            public EffectBase Effect { get; protected set; }

            [JsonIgnore]
            public EffectTechnique? Technique { get; protected set; }
        }
    }
}
