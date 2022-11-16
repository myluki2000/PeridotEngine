using System.Collections;
using Newtonsoft.Json;
using PeridotEngine.ECS.Components;
using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Archetype
    {
        public Type[] ComponentTypes { get; }

        public List<uint> Ids { get; } = new();
        public List<string?> Names { get; } = new();
        public List<IList> Components { get; } = new();

        public event Action? EntityListChanged;

        public Archetype(Type[] componentTypes, List<uint> ids, List<string?> names, List<IList> components) : this(componentTypes)
        {
            Ids = ids;
            Names = names;
            Components = components;
        }

        public Archetype(Type[] componentTypes)
        {
            ComponentTypes = componentTypes.OrderBy(x => x.FullName).ToArray();
        }

        public void CreateEntity(params ComponentBase[] entityComponents)
        {
            CreateEntity(null, entityComponents);
        }

        public void CreateEntity(string? name, params ComponentBase[] entityComponents)
        {
            uint id = Ids.Count > 0
                ? Ids.Max() + 1
                : 0;

            Ids.Add(id);

            while (Components.Count < ComponentTypes.Length)
                Components.Add(new ArrayList());

            entityComponents = entityComponents.OrderBy(x => x.GetType().FullName).ToArray();

            // check that all components of the archetype were provided for the entity
            if (entityComponents.Length != Components.Count) throw new Exception("Not all required components were passed to the CreateEntity method when creating an entity of this specific archetype.");
            foreach(Type componentType in ComponentTypes)
            {
                if(!entityComponents.Any(e => e.GetType() == componentType))
                    throw new Exception("Not all required components were passed to the CreateEntity method when creating an entity of this specific archetype.");
            }

            Names.Add(name);
     
            for (int i = 0; i < entityComponents.Length; i++)
            {
                Components[i].Add(entityComponents[i]);
            }

            EntityListChanged?.Invoke();
        }


        public IEnumerable<Entity> Entities()
        {
            for (int i = 0; i < EntityCount; i++)
            {
                yield return new Entity(Ids[i], this);
            }
        }

        [JsonIgnore]
        public int EntityCount => Components[0].Count;
    }
}
