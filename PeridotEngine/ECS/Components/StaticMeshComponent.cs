using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotWindows.ECS.Components
{
    public sealed partial class StaticMeshComponent : IComponent
    {
        public Mesh Mesh { get; set; }
        public EffectBase.EffectProperties EffectProperties { get; set; }
        
        public Scene3D Scene { get; }
    }
}
