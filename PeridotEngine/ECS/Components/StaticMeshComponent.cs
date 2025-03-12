using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.ECS.Components
{
    public partial class StaticMeshComponent : ComponentBase
    {
        public MeshResources.MeshInfo? Mesh
        {
            get;
            set
            {
                field = value;
                ValuesChanged?.Invoke(this, this);
            }
        }

        public bool CastShadows
        {
            get;
            set
            {
                field = value;
                ValuesChanged?.Invoke(this, this);
            }
        } = true;

        public EffectBase.EffectProperties? EffectProperties
        {
            get;
            set
            {
                field = value;
                ValuesChanged?.Invoke(this, this);
            }
        }

        private void EffectPropertiesOnValuesChanged(object? sender, EffectBase.EffectProperties e)
        {
            ValuesChanged?.Invoke(this, this);
        }

        public override event EventHandler<ComponentBase>? ValuesChanged;

        public StaticMeshComponent(Scene3D scene) : base(scene) { }

        public StaticMeshComponent(Scene3D scene, MeshResources.MeshInfo? mesh, EffectBase.EffectProperties? effectProperties, bool castShadows = true) : base(scene)
        {
            Mesh = mesh;
            EffectProperties = effectProperties;
            CastShadows = castShadows;
        }
    }
}
