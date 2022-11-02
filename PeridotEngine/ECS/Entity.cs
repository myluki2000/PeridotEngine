using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Entity
    {
        public string? Name { get; }
        public Archetype Archetype { get; }
        public IComponent[] Components { get; }

        public Entity(string? name, Archetype archetype, IComponent[] components)
        {
            Name = name;
            Archetype = archetype;
            Components = components;
        }
    }
}
