﻿using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;
using PeridotEngine.ECS.Components;
using PeridotEngine.Misc;
using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public partial class Archetype
    {
        public Type[] ComponentTypes { get; }

        public List<uint> Ids { get; } = new();
        public List<string?> Names { get; } = new();
        public List<IList> Components { get; } = new();

        private readonly List<WeakReference<Entity>?> entities;

        private readonly Ecs ecs;

        public event Action? EntityListChanged;

        public Archetype(Ecs ecs, Type[] componentTypes, List<uint> ids, List<string?> names, List<IList> components) : this(ecs, componentTypes)
        {
            Ids = ids;
            Names = names;
            Components = components;

            entities = new();
            for (int i = 0; i < ids.Count; i++)
            {
                entities.Add(null);
            }
        }

        public Archetype(Ecs ecs, Type[] componentTypes)
        {
            this.ecs = ecs;

            ComponentTypes = componentTypes.OrderBy(x => x.FullName).ToArray();

            entities = new();
        }

        public bool HasComponent<T>() where T : ComponentBase
        {
            return ComponentTypes.Contains(typeof(T));
        }

        public Entity? EntityById(uint entityId)
        {
            int index = Ids.IndexOf(entityId);

            if (index == -1) return null;

            return EntityAt(index);
        }

        public Entity EntityAt(int index)
        {
            if (entities[index]?.TryGetTarget(out Entity? entity) ?? false)
                return entity;

            entity = Entity.FromIndex(index, this);
            entities[index] = new WeakReference<Entity>(entity);

            return entity;
        }

        public void RemoveEntityAt(int index)
        {
            if (entities[index]?.TryGetTarget(out Entity? entity) ?? false)
            {
                entity.Delete();
            }
            else
            {
                RemoveEntityAtInternal(index);
            }

        }

        private void RemoveEntityAtInternal(int index)
        {
            Ids.RemoveAt(index);
            Names.RemoveAt(index);
            entities.RemoveAt(index);

            foreach (IList list in Components)
            {
                list.RemoveAt(index);
            }

            foreach (WeakReference<Entity>? entityRef in entities)
            {
                if (entityRef?.TryGetTarget(out Entity? e) ?? false)
                {
                    e.UpdateEntityIndex();
                }
            }

            EntityListChanged?.Invoke();
        }

        public void CreateEntity(params ComponentBase[] entityComponents)
        {
            CreateEntity(null, entityComponents);
        }

        public void CreateEntity(string? name, params ComponentBase[] entityComponents)
        {
            while (Components.Count < ComponentTypes.Length)
                Components.Add(new ArrayList());

            // check that all components of the archetype were provided for the entity
            if (entityComponents.Length != Components.Count) throw new Exception("Not all required components were passed to the CreateEntity method when creating an entity of this specific archetype.");
            foreach (Type componentType in ComponentTypes)
            {
                if (!entityComponents.Any(e => e.GetType() == componentType))
                    throw new Exception("Not all required components were passed to the CreateEntity method when creating an entity of this specific archetype.");
            }

            uint id = ++ecs.LargestId;

            Ids.Add(id);

            entityComponents = entityComponents.OrderBy(x => x.GetType().FullName).ToArray();

            Names.Add(name);
     
            for (int i = 0; i < entityComponents.Length; i++)
            {
                Components[i].Add(entityComponents[i]);
            }

            entities.Add(null);

            EntityListChanged?.Invoke();
        }

        public IEnumerable<Entity> Entities()
        {
            for (int i = 0; i < EntityCount; i++)
            {
                if (entities[i]?.TryGetTarget(out Entity? entity) ?? false)
                {
                    yield return entity;
                }
                else
                {
                    entity = Entity.FromIndex(i, this);
                    entities[i] = new WeakReference<Entity>(entity);

                    yield return entity;
                }
            }
        }

        [JsonIgnore]
        public int EntityCount => Ids.Count;
    }
}
