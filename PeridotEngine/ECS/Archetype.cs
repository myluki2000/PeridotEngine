using System.Collections;
using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Archetype
    {
        public Type[] ComponentTypes { get; }

        public List<IList> Components { get; } = new();

        public Archetype(Type[] componentTypes)
        {
            ComponentTypes = componentTypes.OrderBy(x => x.GetHashCode()).ToArray();

            foreach (Type componentType in ComponentTypes)
            {
                Components.Add(new ArrayList());
            }
        }

        public void CreateEntity(params IComponent[] entityComponents)
        {
            entityComponents = entityComponents.OrderBy(x => x.GetType().GetHashCode()).ToArray();

            // check that all components of the archetype were provided for the entity
            if (entityComponents.Length != Components.Count) throw new Exception("Not all required components were passed to the CreateEntity method when creating an entity of this specific archetype.");
            foreach(Type componentType in ComponentTypes)
            {
                if(!entityComponents.Any(e => componentType.IsInstanceOfType(e)))
                    throw new Exception("Not all required components were passed to the CreateEntity method when creating an entity of this specific archetype.");
            }
     
            for (int i = 0; i < entityComponents.Length; i++)
            {
                Components[i].Add(entityComponents[i]);
            }
        }


        public IEnumerable<Entity> Entities()
        {
            for (int i = 0; i < EntityCount; i++)
            {
                List<IComponent> c = new(ComponentTypes.Length);
                for (int j = 0; j < ComponentTypes.Length; j++)
                {
                    c[j] = (IComponent)Components[j][i];
                }
                yield return new Entity(this, c);
            }
        }

        public int EntityCount => Components[0].Count;
    }
}
