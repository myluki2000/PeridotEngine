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

        public static void UpdateEffectMatrices(Matrix world, Matrix view, Matrix projection)
        {
            Matrix worldViewProj = world * view * projection;

            foreach (EffectBase effect in effects.Values)
            {
                effect.WorldViewProjectionMatrix = worldViewProj;
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
