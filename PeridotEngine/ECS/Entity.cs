using System.Collections;
using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Entity
    {
        public uint Id { get; }
        public Archetype Archetype { get; }

        public IReadOnlyCollection<ComponentBase> Components
            => Archetype.Components.Select(list => (ComponentBase)list[index]!).ToList();

        public string? Name
        {
            get => Archetype.Names[index];
            set => Archetype.Names[index] = value;
        }

        private int index;

        public Entity(uint id, Archetype archetype)
        {
            Id = id;
            Archetype = archetype;

            Archetype.EntityListChanged += GetEntityIndex;
            GetEntityIndex();
        }

        private void GetEntityIndex()
        {
            index = Archetype.Ids.IndexOf(Id);
        }
    }
}
