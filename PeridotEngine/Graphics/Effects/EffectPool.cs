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
        public event EventHandler<EffectBase>? EffectInstantiated; 

        public readonly Dictionary<Type, WeakReference<EffectBase>> Effects = new();
        private static readonly List<Type> effectTypes = new();

        private readonly Scene3D scene;

        static EffectPool()
        {
            RegisterEffectType<SimpleEffect>();
        }

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
            foreach (WeakReference<EffectBase> effectRef in Effects.Values)
            {
                if(effectRef.TryGetTarget(out EffectBase? effect))
                    effect.ViewProjection = viewProjection;
            }
        }

        public void UpdateEffectShadows(Texture2D shadowMap, Vector3 lightPosition, Matrix lightViewProjection)
        {
            foreach (WeakReference<EffectBase> effectRef in Effects.Values)
            {
                if (effectRef.TryGetTarget(out EffectBase? effect))
                {
                    if (effect is IEffectShadows shadowEffect)
                    {
                        shadowEffect.ShadowMap = shadowMap;
                        shadowEffect.LightPosition = lightPosition;
                        shadowEffect.LightViewProjection = lightViewProjection;
                    }
                }
            }
        }

        public EffectBase Effect(Type effectType)
        {
            if (!effectType.IsAssignableTo(typeof(EffectBase)))
                throw new ArgumentException("EffectPool.Effect(Type) expects to be passed the type " +
                                            "of an effect (which inherits from EffectBase)");

            if (Effects.TryGetValue(effectType, out WeakReference<EffectBase>? effectRef))
            {
                if (effectRef.TryGetTarget(out EffectBase? effect))
                    return effect;
            }

            EffectBase newEffect = (EffectBase)Activator.CreateInstance(effectType)!;

            if (newEffect is IEffectTexture effectTexture)
            {
                effectTexture.TextureResources = scene.Resources.TextureResources;
            }

            Effects.Add(effectType, new WeakReference<EffectBase>(newEffect));
            return newEffect;
        }

        public T Effect<T>() where T : EffectBase, new()
        {
            if (Effects.TryGetValue(typeof(T), out WeakReference<EffectBase>? effectRef))
            {
                if (effectRef.TryGetTarget(out EffectBase? effect))
                    return (T)effect;
            }

            T newEffect = new();

            if (newEffect is IEffectTexture effectTexture)
            {
                effectTexture.TextureResources = scene.Resources.TextureResources;
            }
            
            Effects[typeof(T)] = new WeakReference<EffectBase>(newEffect);
            EffectInstantiated?.Invoke(this, newEffect);
            return newEffect;
        }
    }
}
