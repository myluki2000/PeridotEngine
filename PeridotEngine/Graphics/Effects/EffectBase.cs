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
        /// <summary>
        /// Object ID rendered to the object picking output. Should be set to uint.MaxValue to represent no particular ECS object.
        /// </summary>
        public uint ObjectId { get; set; } = uint.MaxValue;

        protected readonly EffectParameter WorldParam;
        protected readonly EffectParameter? NormalMatrixParam;
        protected readonly EffectParameter ViewProjectionParam;

        protected readonly EffectParameter? ObjectIdParam;

        protected readonly EffectParameter? ViewParam;

        protected EffectBase(Effect cloneSource) : base(cloneSource)
        {
            WorldParam = Parameters["World"];
            ViewParam = Parameters["View"];
            ViewProjectionParam = Parameters["ViewProjection"];
            NormalMatrixParam = Parameters["NormalMatrix"];
            ObjectIdParam = Parameters["ObjectId"];
        }

        public virtual void Apply()
        {
            WorldParam.SetValue(World);
            ViewProjectionParam.SetValue(View * Projection);
            
            // the matrix is only calculated if the NormalMatrix parameter is defined in the shader code
            NormalMatrixParam?.SetValue(Matrix.Transpose(Matrix.Invert(World * View)));
            ViewParam?.SetValue(View);

            uint objectId = ObjectId;
            unsafe
            {
                // reinterpret the uint as a float because the shader expects a float. We convert it back
                // when getting an object id from the shader
                ObjectIdParam?.SetValue(*((float*)(&objectId)));
            }
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
                Effect.Apply();
            }

            [JsonIgnore]
            public EffectBase Effect { get; protected set; }

            [JsonIgnore]
            public EffectTechnique? Technique { get; protected set; }
        }
    }
}
