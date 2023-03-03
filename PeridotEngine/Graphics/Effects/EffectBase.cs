using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes;
using PeridotEngine.Scenes.Scene3D;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics.Effects
{
    public abstract partial class EffectBase : Effect
    {
        public Matrix World { get; set; } = Matrix.Identity;
        public Matrix View { get; set; } = Matrix.Identity;
        public Matrix Projection { get; set; } = Matrix.Identity;

        [JsonIgnore]
        protected readonly EffectParameter WorldParam;
        [JsonIgnore]
        protected readonly EffectParameter? NormalMatrixParam;
        [JsonIgnore]
        protected readonly EffectParameter ViewProjectionParam;

        [JsonIgnore] protected readonly EffectParameter? ViewParam;

        protected EffectBase(Effect cloneSource) : base(cloneSource)
        {
            WorldParam = Parameters["World"];
            ViewParam = Parameters["View"];
            ViewProjectionParam = Parameters["ViewProjection"];
            NormalMatrixParam = Parameters["NormalMatrix"];
        }

        public virtual void UpdateMatrices()
        {
            WorldParam.SetValue(World);
            ViewProjectionParam.SetValue(View * Projection);
            
            // the matrix is only calculated if the NormalMatrix parameter is defined in the shader code
            NormalMatrixParam?.SetValue(Matrix.Transpose(Matrix.Invert(World * View)));
            ViewParam?.SetValue(View);
        }

        public abstract EffectProperties CreatePropertiesBase();
        public abstract Type GetPropertiesType();

        public abstract partial class EffectProperties
        {
            protected EffectProperties(EffectBase effect)
            {
                Effect = effect;
            }

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
