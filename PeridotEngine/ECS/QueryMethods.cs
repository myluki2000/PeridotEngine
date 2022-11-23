
using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS
{
    public partial class Query
    {
        public void ForEach<T1>(Action<T1> action) where T1 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i]);
                }
            }
        }

        public void First<T1>(Action<T1> action) where T1 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i]);
                    return;
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

        public void First<T1, T2>(Action<T1, T2> action) where T1 : ComponentBase where T2 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i]);
                    return;
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

        public void First<T1, T2, T3>(Action<T1, T2, T3> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase
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
                    return;
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
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i]);
                }
            }
        }

        public void First<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase
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
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i]);
                    return;
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
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i]);
                }
            }
        }

        public void First<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase
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
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i]);
                    return;
                }
            }

        }

        public void ForEach<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i]);
                }
            }
        }

        public void First<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i]);
                    return;
                }
            }

        }

        public void ForEach<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i]);
                }
            }
        }

        public void First<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i]);
                    return;
                }
            }

        }

        public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));
                int c8Index = Array.IndexOf(archetype.ComponentTypes, typeof(T8));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i], (T8)archetype.Components[c8Index][i]);
                }
            }
        }

        public void First<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));
                int c8Index = Array.IndexOf(archetype.ComponentTypes, typeof(T8));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i], (T8)archetype.Components[c8Index][i]);
                    return;
                }
            }

        }

        public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));
                int c8Index = Array.IndexOf(archetype.ComponentTypes, typeof(T8));
                int c9Index = Array.IndexOf(archetype.ComponentTypes, typeof(T9));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i], (T8)archetype.Components[c8Index][i], (T9)archetype.Components[c9Index][i]);
                }
            }
        }

        public void First<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));
                int c8Index = Array.IndexOf(archetype.ComponentTypes, typeof(T8));
                int c9Index = Array.IndexOf(archetype.ComponentTypes, typeof(T9));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i], (T8)archetype.Components[c8Index][i], (T9)archetype.Components[c9Index][i]);
                    return;
                }
            }

        }

        public void ForEach<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase where T10 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));
                int c8Index = Array.IndexOf(archetype.ComponentTypes, typeof(T8));
                int c9Index = Array.IndexOf(archetype.ComponentTypes, typeof(T9));
                int c10Index = Array.IndexOf(archetype.ComponentTypes, typeof(T10));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i], (T8)archetype.Components[c8Index][i], (T9)archetype.Components[c9Index][i], (T10)archetype.Components[c10Index][i]);
                }
            }
        }

        public void First<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action) where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase where T10 : ComponentBase
        {
            if (archetypesOutdated) UpdateMatchingArchetypes();

            foreach (Archetype archetype in matchingArchetypes)
            {
                int c1Index = Array.IndexOf(archetype.ComponentTypes, typeof(T1));
                int c2Index = Array.IndexOf(archetype.ComponentTypes, typeof(T2));
                int c3Index = Array.IndexOf(archetype.ComponentTypes, typeof(T3));
                int c4Index = Array.IndexOf(archetype.ComponentTypes, typeof(T4));
                int c5Index = Array.IndexOf(archetype.ComponentTypes, typeof(T5));
                int c6Index = Array.IndexOf(archetype.ComponentTypes, typeof(T6));
                int c7Index = Array.IndexOf(archetype.ComponentTypes, typeof(T7));
                int c8Index = Array.IndexOf(archetype.ComponentTypes, typeof(T8));
                int c9Index = Array.IndexOf(archetype.ComponentTypes, typeof(T9));
                int c10Index = Array.IndexOf(archetype.ComponentTypes, typeof(T10));

                for (int i = 0; i < archetype.EntityCount; i++)
                {
                    action.Invoke((T1)archetype.Components[c1Index][i], (T2)archetype.Components[c2Index][i], (T3)archetype.Components[c3Index][i], (T4)archetype.Components[c4Index][i], (T5)archetype.Components[c5Index][i], (T6)archetype.Components[c6Index][i], (T7)archetype.Components[c7Index][i], (T8)archetype.Components[c8Index][i], (T9)archetype.Components[c9Index][i], (T10)archetype.Components[c10Index][i]);
                    return;
                }
            }

        }

    }
}