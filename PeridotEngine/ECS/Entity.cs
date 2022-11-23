using System.Collections;
using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Entity
    {
        public uint Id { get; private set; }

        public Archetype Archetype
        {
            get
            {
                if (IsDeleted) throw new Exception("Cannot access a deleted entity.");
                return archetype;
            }
        }

        public IReadOnlyCollection<ComponentBase> Components => Archetype.Components.Select(list => (ComponentBase)list[index]!).ToList();

        public string? Name
        {
            get => Archetype.Names[index];
            set => Archetype.Names[index] = value;
        }

        public bool IsDeleted { get; private set; }

        private int index;
        private readonly Archetype archetype;

        public static Entity FromIndex(int index, Archetype archetype)
        {
            Entity e = new(archetype);

            e.index = index;
            e.Id = archetype.Ids[index];

            return e;
        }

        public static Entity FromEntityId(uint id, Archetype archetype)
        {
            return new Entity(id, archetype);
        }

        public Entity(uint id, Archetype archetype)
        {
            Id = id;
            this.archetype = archetype;
            UpdateEntityIndex();
        }

        private Entity(Archetype archetype)
        {
            this.archetype = archetype;
        }

        public void Delete()
        {
            IsDeleted = true;
            archetype.RemoveEntityAt(index);
        }

        public void UpdateEntityIndex()
        {
            index = Archetype.Ids.IndexOf(Id);
        }
    }
}
