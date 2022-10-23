using System;
using System.Collections.Generic;
using System.Text;
using PeridotEngine.Game.ECS.Components;

namespace PeridotEngine.Game.ECS
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
