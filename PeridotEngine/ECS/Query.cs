using PeridotWindows.ECS.Components;

namespace PeridotWindows.ECS
{
    public class Query
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

        public void ForEach<T>(Action<T> action) where T : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int componentIndex = Array.IndexOf(archetype.ComponentTypes, typeof(T));
                foreach (T component in archetype.Components[componentIndex])
                {
                    action.Invoke(component);
                }
            }
        }

        public void ForEach<T1, T2>(Action<T1, T2> action) where T1 : ComponentBase where T2 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i]);
                }
            }
        }

        public void ForEach<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i]);
                }
            }
        }

        public void ForEach<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i],
                        (T2)archetype.Components[c2Index][i],
                        (T3)archetype.Components[c3Index][i],
                        (T4)archetype.Components[c4Index][i]);
                }
            }
        }

        public void ForEach<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i],
                        (T2)archetype.Components[c2Index][i],
                        (T3)archetype.Components[c3Index][i],
                        (T4)archetype.Components[c4Index][i],
                        (T5)archetype.Components[c5Index][i]);
                }
            }
        }
    }
}
