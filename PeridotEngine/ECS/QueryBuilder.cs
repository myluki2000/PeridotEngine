using System;
using System.Diagnostics;
using System.Security.AccessControl;
using PeridotEngine.ECS;
using PeridotEngine.ECS.Components;
using PeridotEngine.Misc;

namespace PeridotWindows.ECS
{
    public partial class QueryBuilder(Ecs ecs)
    {
        private readonly List<Type> includeComponents = [];
        private readonly List<Type> excludeComponents = [];

        public QueryBuilder Has<T>() where T : ComponentBase
        {
            includeComponents.Add(typeof(T));
            return this;
        }

        public QueryBuilder HasNot<T>() where T : ComponentBase
        {
            excludeComponents.Add(typeof(T));
            return this;
        }

        public EntityQuery OnEntity()
        {
            return new EntityQuery(ecs, includeComponents, excludeComponents);
        }
    }

    public sealed class EntityQuery(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
    {
        public void ForEach(Action<Archetype.Entity> action)
        {
            foreach (Archetype archetype in MatchingArchetypes)
            {
                foreach (Archetype.Entity entity in archetype.Entities())
                {
                    action.Invoke(entity);
                }
            }
        }
    }

    public abstract class Query
    {
        protected List<Archetype> MatchingArchetypes
        {
            get
            {
                if (ArchetypesOutdated)
                    UpdateMatchingArchetypes();

                return matchingArchetypes;
            }
        }

        protected bool ArchetypesOutdated { get; set; } = true;
        private List<Archetype> matchingArchetypes = [];
        private readonly Ecs ecs;
        private readonly List<Type> includeComponents;
        private readonly List<Type> excludeComponents;

        public Event<QueryEntityListChangedEventArgs> EntityListChanged { get; } = new();

        protected Query(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        {
            this.ecs = ecs;
            this.includeComponents = includeComponents;
            this.excludeComponents = excludeComponents;

            ecs.ArchetypeListChanged.AddWeakHandler(OnEcsOnArchetypeListChanged);
            ecs.EntityListChanged.AddWeakHandler(OnEcsEntityListChanged);
        }

        private void UpdateMatchingArchetypes()
        {
            matchingArchetypes = [..ecs.Archetypes];
            matchingArchetypes.RemoveAll(a =>
            {
                // if |includeComponents \ a.ComponentTypes| > 0 this archetype is missing
                // some required components
                if (includeComponents.Any(ic => !a.ComponentTypes.Any(ac => ac.IsSubclassOf(ic) || ac == ic))) return true;
                
                // if a component is included in both a.ComponentTypes and excludeComponents
                // then the archetype has an excluded component
                if (excludeComponents.Any(ec => a.ComponentTypes.Any(ac => ac.IsSubclassOf(ec) || ac == ec))) return true;

                return false;
            });
            ArchetypesOutdated = false;

            OnMatchingArchetypesUpdated();
        }

        public int EntityCount => MatchingArchetypes.Sum(x => x.EntityCount);

        private void OnEcsOnArchetypeListChanged(object? o, IEnumerable<Archetype> archetypes)
        {
            ArchetypesOutdated = true;
        }

        private void OnEcsEntityListChanged(object? o, EntityListChangedEventArgs args)
        {
            if (EntityListChanged.HasAttachedHandlers)
            {
                Archetype? old = args.OldArchetype;
                Archetype? now = args.NewArchetype;

                if (now != null && MatchingArchetypes.Contains(now)
                                && (old == null || !MatchingArchetypes.Contains(old)))
                {
                    // Entity added to a matching archetype
                    EntityListChanged.Invoke(this, 
                        new QueryEntityListChangedEventArgs(QueryEntityListChangedEventArgs.ChangeOperation.Added, args.EntityId));
                }
                else if ((now == null || !MatchingArchetypes.Contains(now))
                         && (old != null && MatchingArchetypes.Contains(old)))
                {
                    // Entity removed from a matching archetype
                    EntityListChanged.Invoke(this, 
                        new QueryEntityListChangedEventArgs(QueryEntityListChangedEventArgs.ChangeOperation.Removed, args.EntityId));
                }
                else if ((now != null && MatchingArchetypes.Contains(now))
                         && (old != null && MatchingArchetypes.Contains(old)))
                {
                    // Entity changed components but remained in a matching archetype
                    EntityListChanged.Invoke(this, 
                        new QueryEntityListChangedEventArgs(QueryEntityListChangedEventArgs.ChangeOperation.ComponentsChanged, args.EntityId));
                }
            }
        }

        protected virtual void OnMatchingArchetypesUpdated() { }
    }

    public class QueryEntityListChangedEventArgs(QueryEntityListChangedEventArgs.ChangeOperation operation, uint entityId)
    {
        public ChangeOperation Operation { get; } = operation;
        public uint EntityId { get; } = entityId;

        public enum ChangeOperation
        {
            Added,
            Removed,
            ComponentsChanged,
        }
    }
}
