using System;
using System.Diagnostics;
using System.Security.AccessControl;
using PeridotEngine.ECS.Components;

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

    public abstract class Query : IDisposable
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

        protected Query(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        {
            this.ecs = ecs;
            this.includeComponents = includeComponents;
            this.excludeComponents = excludeComponents;

            ecs.ArchetypeListChanged += OnEcsOnArchetypeListChanged;
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

        protected virtual void OnMatchingArchetypesUpdated() { }

        public void Dispose()
        {
            ecs.ArchetypeListChanged -= OnEcsOnArchetypeListChanged;
        }
    }
}
