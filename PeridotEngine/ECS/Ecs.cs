namespace PeridotWindows.ECS
{
    public class Ecs
    {
        private List<Archetype> archetypes = new();
        public List<Archetype> Archetypes
        {
            get => archetypes;
            private set
            {
                archetypes = value;
                foreach (Archetype archetype in archetypes)
                {
                    archetype.EntityListChanged += () => EntityListChanged?.Invoke(this, archetype);
                }
            }
        }

        public event EventHandler<IEnumerable<Archetype>>? ArchetypeListChanged;
        public event EventHandler<Archetype>? EntityListChanged;

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
