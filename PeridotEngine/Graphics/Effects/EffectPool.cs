using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.Effects
{
    public static class EffectPool
    {
        private static readonly Dictionary<Type, EffectBase> effects = new();

        public static void UpdateEffectViewProjection(Matrix viewProjection)
        {
            foreach (EffectBase effect in effects.Values)
            {
                effect.ViewProjection = viewProjection;
            }
        }

        public static void UpdateEffectTextures(Texture2D texture)
        {
            foreach (IEffectTexture effect in effects.Values.OfType<IEffectTexture>())
            {
                effect.Texture = texture;
            }
        }

        public static T Effect<T>() where T : EffectBase, new()
        {
            if (effects.TryGetValue(typeof(T), out EffectBase? value))
            {
                return (T)value;
            }

            T newEffect = new();
            effects.Add(typeof(T), newEffect);
            return newEffect;
        }
    }
}
