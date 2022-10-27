using PeridotEngine.Graphics.Effects;

namespace PeridotWindows.ECS.Components
{
    public partial class EffectComponent : IComponent
    {
        public EffectBase Effect { get; set; }

        public EffectComponent(EffectBase effect)
        {
            Effect = effect;
        }
    }
}
