using System.Collections;
using System.Text.Json.Serialization;
using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Archetype
    {
        [JsonIgnore]
        public Type[] ComponentTypes { get; }

        public List<string?> Names { get; } = new();
        public List<IList> Components { get; } = new();

        public event Action? EntityListChanged;

        public Archetype(Type[] componentTypes)
        {
            ComponentTypes = componentTypes.OrderBy(x => x.GetHashCode()).ToArray();

            foreach (Type componentType in ComponentTypes)
            {
                Components.Add(new ArrayList());
            }
        }

        public void CreateEntity(params ComponentBase[] entityComponents)
        {
            CreateEntity(null, entityComponents);
        }

        public void CreateEntity(string? name, params ComponentBase[] entityComponents)
        {
            entityComponents = entityComponents.OrderBy(x => x.GetType().GetHashCode()).ToArray();

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
                ComponentBase[] c = new ComponentBase[ComponentTypes.Length];
                for (int j = 0; j < ComponentTypes.Length; j++)
                {
                    c[j] = (ComponentBase)Components[j][i];
                }
                yield return new Entity(Names[i], this, c);
            }
        }

        public int EntityCount => Components[0].Count;
    }
}
