using Newtonsoft.Json;

namespace PeridotWindows.ECS
{
    public class Ecs
    {
        public List<Archetype> Archetypes { get; } = new();

        public event EventHandler<IEnumerable<Archetype>>? ArchetypeListChanged;
        public event EventHandler<Archetype>? EntityListChanged;

        [JsonIgnore]
        public uint LargestId { get; set; } = 0;

        public Ecs()
        {
        }

        [JsonConstructor]
        public Ecs(Func<Ecs, List<Archetype>> createArchetypes)
        {
            List<Archetype> archetypes = createArchetypes(this);

            Archetypes = archetypes;

            foreach (Archetype archetype in Archetypes)
            {
                archetype.EntityListChanged += () => EntityListChanged?.Invoke(this, archetype);

                uint archetypeLargestId = archetype.Ids
                    .DefaultIfEmpty<uint>(0)
                    .Max();
                if (archetypeLargestId > LargestId) LargestId = archetypeLargestId;
            }
        }

        public Archetype Archetype(params Type[] componentTypes)
        {
            componentTypes = componentTypes.OrderBy(x => x.GetHashCode()).ToArray();

            Archetype? archetype = Archetypes.FirstOrDefault(x => x.ComponentTypes.SequenceEqual(componentTypes));

            if (archetype == null)
            {
                archetype = new Archetype(this, componentTypes);
                archetype.EntityListChanged += () => EntityListChanged?.Invoke(this, archetype);
                Archetypes.Add(archetype);
                ArchetypeListChanged?.Invoke(this, Archetypes);
            }

            return archetype;
        }

        public Archetype.Entity? EntityById(uint id)
        {
            foreach (Archetype archetype in Archetypes)
            {
                Archetype.Entity? e = archetype.EntityById(id);
                if (e != null) return e;
            }

            return null;
        }

        public QueryBuilder Query()
        {
            return new QueryBuilder(this);
        }
    }
}
