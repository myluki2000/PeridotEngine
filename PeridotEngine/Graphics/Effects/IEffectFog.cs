namespace PeridotEngine.Graphics.Effects
{
    public interface IEffectFog
    {
        public Microsoft.Xna.Framework.Color FogColor { get; set; }
        public float FogStart { get; set; }
        public float FogEnd { get; set; }
    }
}
