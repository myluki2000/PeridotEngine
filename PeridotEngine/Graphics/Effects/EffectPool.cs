using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.Graphics.Effects
{
    public class EffectPool
    {
        private readonly Dictionary<Type, WeakReference<EffectBase>> effects = new();
        private static readonly List<Type> effectTypes = new();

        private readonly Scene3D scene;

        public EffectPool(Scene3D scene)
        {
            this.scene = scene;
        }

        public static void RegisterEffectType<T>()
        {
            effectTypes.Add(typeof(T));
        }

        public static IEnumerable<Type> GetRegisteredEffectTypes()
        {
            return effectTypes;
        }

        public void UpdateEffectViewProjection(Matrix viewProjection)
        {
            foreach (WeakReference<EffectBase> effectRef in effects.Values)
            {
                if(effectRef.TryGetTarget(out EffectBase? effect))
                    effect.ViewProjection = viewProjection;
            }
        }

        public EffectBase Effect(Type effectType)
        {
            if (!effectType.IsAssignableTo(typeof(EffectBase)))
                throw new ArgumentException("EffectPool.Effect(Type) expects to be passed the type " +
                                            "of an effect (which inherits from EffectBase)");

            if (effects.TryGetValue(effectType, out WeakReference<EffectBase>? effectRef))
            {
                if (effectRef.TryGetTarget(out EffectBase? effect))
                    return effect;
            }

            EffectBase newEffect = (EffectBase)Activator.CreateInstance(effectType)!;

            if (newEffect is IEffectTexture effectTexture)
            {
                effectTexture.TextureResources = scene.Resources.TextureResources;
            }

            effects.Add(effectType, new WeakReference<EffectBase>(newEffect));
            return newEffect;
        }

        public T Effect<T>() where T : EffectBase, new()
        {
            if (effects.TryGetValue(typeof(T), out WeakReference<EffectBase>? effectRef))
            {
                if (effectRef.TryGetTarget(out EffectBase? effect))
                    return (T)effect;
            }

            T newEffect = new();

            if (newEffect is IEffectTexture effectTexture)
            {
                effectTexture.TextureResources = scene.Resources.TextureResources;
            }

            effects.Add(typeof(T), new WeakReference<EffectBase>(newEffect));
            return newEffect;
        }
    }
}
