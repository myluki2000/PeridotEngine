using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotWindows.ECS.Components
{
    public sealed partial class StaticMeshComponent : ComponentBase
    {
        public MeshResources.MeshInfo Mesh { get; set; }
        public bool CastShadows { get; set; } = true;
        public EffectBase.EffectProperties EffectProperties { get; set; }
    }
}
