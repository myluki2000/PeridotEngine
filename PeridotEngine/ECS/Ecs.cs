using Newtonsoft.Json;

namespace PeridotWindows.ECS
{
    public class Ecs
    {
        public List<Archetype> Archetypes { get; } = new();

        public event EventHandler<IEnumerable<Archetype>>? ArchetypeListChanged;
        public event EventHandler<Archetype>? EntityListChanged;

        public Ecs()
        {
        }

        [JsonConstructor]
        public Ecs(List<Archetype> archetypes)
        {
            Archetypes = archetypes;

            foreach (Archetype archetype in Archetypes)
            {
                archetype.EntityListChanged += () => EntityListChanged?.Invoke(this, archetype);
            }
        }

        public Archetype Archetype(params Type[] componentTypes)
        {
            componentTypes = componentTypes.OrderBy(x => x.GetHashCode()).ToArray();

            Archetype? archetype = Archetypes.FirstOrDefault(x => x.ComponentTypes.SequenceEqual(componentTypes));

            if (archetype == null)
            {
                archetype = new Archetype(componentTypes);
                archetype.EntityListChanged += () => EntityListChanged?.Invoke(this, archetype);
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
