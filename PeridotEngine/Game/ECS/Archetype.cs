using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PeridotEngine.Game.ECS.Components;
using SharpDX.Direct3D11;
using SharpDX.WIC;

namespace PeridotEngine.Game.ECS
{
    internal class Archetype
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
            Debug.Assert(entityComponents.Select(x => x.GetType()).SequenceEqual(ComponentTypes));

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
