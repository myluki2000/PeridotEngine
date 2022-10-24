using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeridotEngine.Game.ECS.Components;

namespace PeridotEngine.Game.ECS
{
    public class Ecs
    {
        public List<Archetype> Archetypes { get; } = new();

        public event EventHandler<IEnumerable<Archetype>>? ArchetypeListChanged;

        public Archetype Archetype(params Type[] componentTypes)
        {
            componentTypes = componentTypes.OrderBy(x => x.GetHashCode()).ToArray();

            Archetype? archetype = Archetypes.FirstOrDefault(x => x.ComponentTypes.SequenceEqual(componentTypes));

            if (archetype == null)
            {
                archetype = new Archetype(componentTypes);
                Archetypes.Add(archetype);
                ArchetypeListChanged?.Invoke(this, Archetypes);
            }

            return archetype;
        }

        public Query Query()
        {
            return new Query(this);
        }
    }
}
