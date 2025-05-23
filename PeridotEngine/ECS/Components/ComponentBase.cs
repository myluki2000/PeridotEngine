﻿using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.ECS.Components
{
    public abstract partial class ComponentBase
    {
        [JsonIgnore]
        public Scene3D Scene { get; protected set; }

        public event EventHandler<ComponentBase>? ValuesChanged;

        protected ComponentBase(Scene3D scene)
        {
            Scene = scene;
        }

        protected void RaiseValuesChanged()
        {
            ValuesChanged?.Invoke(this, this);
        }

        public static IEnumerable<Type> GetComponentTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass => ass.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(ComponentBase))));
        }
    }
}
