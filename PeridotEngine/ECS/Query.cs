using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS
{
    public partial class Query
    {
        private readonly Ecs ecs;

        private List<Archetype> matchingArchetypes;
        private bool archetypesOutdated = false;

        private readonly List<Type> includeComponents = new();
        private readonly List<Type> excludeComponents = new();

        public Query(Ecs ecs)
        {
            this.ecs = ecs;
            matchingArchetypes = new List<Archetype>(ecs.Archetypes);

            // when the list of archetypes of the ECS changes we have to update our list of matching
            // archetypes as well
            this.ecs.ArchetypeListChanged += (_, _) => archetypesOutdated = true;
        }

        public Query Has<T>() where T : ComponentBase
        {
            matchingArchetypes.RemoveAll(a => !a.ComponentTypes.Contains(typeof(T)));
            includeComponents.Add(typeof(T));
            return this;
        }

        public Query HasNot<T>() where T : ComponentBase
        {
            matchingArchetypes.RemoveAll(a => a.ComponentTypes.Contains(typeof(T)));
            excludeComponents.Add(typeof(T));
            return this;
        }

        private void UpdateMatchingArchetypes()
        {
            matchingArchetypes = new List<Archetype>(ecs.Archetypes);
            matchingArchetypes.RemoveAll(a =>
            {
                // if |includeComponents \ a.ComponentTypes| > 0 this archetype is missing
                // some required components
                if (includeComponents.Except(a.ComponentTypes).Any()) return true;

                // if a component is included in both a.ComponentTypes and excludeComponents
                // then the archetype has an excluded component
                if (a.ComponentTypes.Intersect(excludeComponents).Any()) return true;

                return false;
            });
        }

        public int EntityCount
        {
            get
            {
                if (archetypesOutdated) UpdateMatchingArchetypes();

                return matchingArchetypes.Sum(x => x.EntityCount);
            }
        }

        public void ForEach(Action<Entity> action)
        {
            if(archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                foreach (Entity entity in archetype.Entities())
                {
                    action.Invoke(entity);
                }
            }
        }
    }
}
