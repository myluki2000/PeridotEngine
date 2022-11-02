using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Entity
    {
        public string? Name { get; }
        public Archetype Archetype { get; }
        public ComponentBase[] Components { get; }

        public Entity(string? name, Archetype archetype, ComponentBase[] components)
        {
            Name = name;
            Archetype = archetype;
            Components = components;
        }
    }
}
