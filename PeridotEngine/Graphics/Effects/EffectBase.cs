﻿using System;
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
        protected readonly EffectParameter WorldViewProjParam;

        protected EffectBase(Effect cloneSource) : base(cloneSource)
        {
            WorldViewProjParam = Parameters["WorldViewProjection"];
        }

        public abstract EffectProperties CreatePropertiesBase();
        public abstract Type GetPropertiesType();

        public abstract partial class EffectProperties
        {
            public virtual void Apply(Mesh mesh)
            {
                Matrix worldViewProjection = Effect.World * Effect.ViewProjection;
                Effect.WorldViewProjParam.SetValue(worldViewProjection);
            }

            [JsonIgnore]
            public EffectBase Effect { get; protected set; }

            [JsonIgnore]
            public EffectTechnique? Technique { get; protected set; }
        }
    }
}
