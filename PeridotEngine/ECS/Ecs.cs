using Newtonsoft.Json;
using PeridotEngine.Misc;

namespace PeridotWindows.ECS
{
    public class Ecs
    {
        public List<Archetype> Archetypes { get; } = new();

        public Event<IEnumerable<Archetype>> ArchetypeListChanged { get; } = new();

        public Event<Archetype> EntityListChanged { get; } = new();

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
                // strong event handler because the archetype is owned by this Ecs instance
                archetype.EntityListChanged.AddHandler((sender, args) => EntityListChanged.Invoke(this, archetype));

                uint archetypeLargestId = archetype.Ids
                    .DefaultIfEmpty<uint>(0)
                    .Max();
                if (archetypeLargestId > LargestId) LargestId = archetypeLargestId;
            }
        }

        public Archetype Archetype(params Type[] componentTypes)
        {
            componentTypes = componentTypes.OrderBy(x => x.FullName).ToArray();

            Archetype? archetype = Archetypes.FirstOrDefault(x => x.ComponentTypes.SequenceEqual(componentTypes));

            if (archetype == null)
            {
                archetype = new Archetype(this, componentTypes);

                // strong event handler because the archetype is owned by this Ecs instance
                archetype.EntityListChanged.AddHandler((sender, args) => EntityListChanged.Invoke(this, archetype));
                Archetypes.Add(archetype);
                ArchetypeListChanged.Invoke(this, Archetypes);
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
