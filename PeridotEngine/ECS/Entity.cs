using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Entity
    {
        public Archetype Archetype { get; }
        public List<IComponent> Components { get; }

        public Entity(Archetype archetype, List<IComponent> components)
        {
            Archetype = archetype;
            Components = components;
        }
    }
}
