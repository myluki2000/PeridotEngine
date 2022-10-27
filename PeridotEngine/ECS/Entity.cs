using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Entity
    {
        public Archetype Archetype { get; }
        public IComponent[] Components { get; }

        public Entity(Archetype archetype, IComponent[] components)
        {
            Archetype = archetype;
            Components = components;
        }
    }
}
