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
                if (field == value)
                    return;

                field = value;
                RaiseValuesChanged();
            }
        }

        public bool CastShadows
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;
                RaiseValuesChanged();
            }
        } = true;

        public EffectBase.EffectProperties? EffectProperties
        {
            get;
            set
            {
                if (field == value)
                    return;

                if (field != null)
                    field.ValuesChanged.RemoveHandler(EffectPropertiesOnValuesChanged);

                field = value;

                if (value != null)
                    value.ValuesChanged.AddWeakHandler(EffectPropertiesOnValuesChanged);

                RaiseValuesChanged();
            }
        }

        private void EffectPropertiesOnValuesChanged(object? sender, EffectBase.EffectProperties e)
        {
            RaiseValuesChanged();
        }

        public StaticMeshComponent(Scene3D scene) : base(scene) { }

        public StaticMeshComponent(Scene3D scene, MeshResources.MeshInfo? mesh, EffectBase.EffectProperties? effectProperties, bool castShadows = true) : base(scene)
        {
            Mesh = mesh;
            EffectProperties = effectProperties;
            CastShadows = castShadows;
        }
    }
}
