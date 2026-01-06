using PeridotWindows.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeridotEngine.ECS
{
    public class EntityListChangedEventArgs(uint entityId, Archetype? oldArchetype, Archetype? newArchetype)
    {
        public uint EntityId { get; } = entityId;

        /// <summary>
        /// The old archetype of the entity, or null if the entity was added.
        /// </summary>
        public Archetype? OldArchetype { get; } = oldArchetype;

        /// <summary>
        /// New archetype of the entity, or null if the entity was removed.
        /// </summary>
        public Archetype? NewArchetype { get; } = newArchetype;
    }
}
