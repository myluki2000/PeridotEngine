﻿using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.ECS.Components
{
    public partial class StaticMeshComponent : ComponentBase
    {
        public MeshResources.MeshInfo? Mesh { get; set; }
        public bool CastShadows { get; set; } = true;
        public EffectBase.EffectProperties? EffectProperties { get; set; }

        public StaticMeshComponent(Scene3D scene) : base(scene)
        {

        }

        public StaticMeshComponent(Scene3D scene, MeshResources.MeshInfo? mesh, EffectBase.EffectProperties? effectProperties, bool castShadows = true) : base(scene)
        {
            Mesh = mesh;
            EffectProperties = effectProperties;
            CastShadows = castShadows;
        }
    }
}
