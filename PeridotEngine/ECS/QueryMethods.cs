
using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS
{
    
	public sealed class ComponentQuery<T1>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase
    {
        private int[] c1Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
            }
        }

        public void ForEach(Action<uint, T1> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1> OnComponents<T1>() where T1 : ComponentBase
        {
            return new ComponentQuery<T1>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
            }
        }

        public void ForEach(Action<uint, T1, T2> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2> OnComponents<T1, T2>() where T1 : ComponentBase where T2 : ComponentBase
        {
            return new ComponentQuery<T1, T2>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3> OnComponents<T1, T2, T3>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3, T4>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];
private int[] c4Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
c4Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
Type t4 = typeof(T4);
c4Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t4 || x.IsSubclassOf(t4));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3, T4> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3, T4> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3, T4> OnComponents<T1, T2, T3, T4>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3, T4>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3, T4, T5>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];
private int[] c4Indices = [];
private int[] c5Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
c4Indices = new int[MatchingArchetypes.Count];
c5Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
Type t4 = typeof(T4);
c4Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t4 || x.IsSubclassOf(t4));
Type t5 = typeof(T5);
c5Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t5 || x.IsSubclassOf(t5));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3, T4, T5> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3, T4, T5> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3, T4, T5> OnComponents<T1, T2, T3, T4, T5>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3, T4, T5>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3, T4, T5, T6>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];
private int[] c4Indices = [];
private int[] c5Indices = [];
private int[] c6Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
c4Indices = new int[MatchingArchetypes.Count];
c5Indices = new int[MatchingArchetypes.Count];
c6Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
Type t4 = typeof(T4);
c4Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t4 || x.IsSubclassOf(t4));
Type t5 = typeof(T5);
c5Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t5 || x.IsSubclassOf(t5));
Type t6 = typeof(T6);
c6Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t6 || x.IsSubclassOf(t6));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3, T4, T5, T6> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3, T4, T5, T6> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3, T4, T5, T6> OnComponents<T1, T2, T3, T4, T5, T6>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3, T4, T5, T6>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3, T4, T5, T6, T7>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];
private int[] c4Indices = [];
private int[] c5Indices = [];
private int[] c6Indices = [];
private int[] c7Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
c4Indices = new int[MatchingArchetypes.Count];
c5Indices = new int[MatchingArchetypes.Count];
c6Indices = new int[MatchingArchetypes.Count];
c7Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
Type t4 = typeof(T4);
c4Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t4 || x.IsSubclassOf(t4));
Type t5 = typeof(T5);
c5Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t5 || x.IsSubclassOf(t5));
Type t6 = typeof(T6);
c6Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t6 || x.IsSubclassOf(t6));
Type t7 = typeof(T7);
c7Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t7 || x.IsSubclassOf(t7));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3, T4, T5, T6, T7> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3, T4, T5, T6, T7> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3, T4, T5, T6, T7> OnComponents<T1, T2, T3, T4, T5, T6, T7>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3, T4, T5, T6, T7>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];
private int[] c4Indices = [];
private int[] c5Indices = [];
private int[] c6Indices = [];
private int[] c7Indices = [];
private int[] c8Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
c4Indices = new int[MatchingArchetypes.Count];
c5Indices = new int[MatchingArchetypes.Count];
c6Indices = new int[MatchingArchetypes.Count];
c7Indices = new int[MatchingArchetypes.Count];
c8Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
Type t4 = typeof(T4);
c4Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t4 || x.IsSubclassOf(t4));
Type t5 = typeof(T5);
c5Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t5 || x.IsSubclassOf(t5));
Type t6 = typeof(T6);
c6Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t6 || x.IsSubclassOf(t6));
Type t7 = typeof(T7);
c7Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t7 || x.IsSubclassOf(t7));
Type t8 = typeof(T8);
c8Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t8 || x.IsSubclassOf(t8));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
int c8Index = c8Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei], (T8)archetype.Components[c8Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3, T4, T5, T6, T7, T8> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
int c8Index = c8Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei], (T8)archetype.Components[c8Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8> OnComponents<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];
private int[] c4Indices = [];
private int[] c5Indices = [];
private int[] c6Indices = [];
private int[] c7Indices = [];
private int[] c8Indices = [];
private int[] c9Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
c4Indices = new int[MatchingArchetypes.Count];
c5Indices = new int[MatchingArchetypes.Count];
c6Indices = new int[MatchingArchetypes.Count];
c7Indices = new int[MatchingArchetypes.Count];
c8Indices = new int[MatchingArchetypes.Count];
c9Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
Type t4 = typeof(T4);
c4Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t4 || x.IsSubclassOf(t4));
Type t5 = typeof(T5);
c5Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t5 || x.IsSubclassOf(t5));
Type t6 = typeof(T6);
c6Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t6 || x.IsSubclassOf(t6));
Type t7 = typeof(T7);
c7Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t7 || x.IsSubclassOf(t7));
Type t8 = typeof(T8);
c8Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t8 || x.IsSubclassOf(t8));
Type t9 = typeof(T9);
c9Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t9 || x.IsSubclassOf(t9));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
int c8Index = c8Indices[ai];
int c9Index = c9Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei], (T8)archetype.Components[c8Index][ei], (T9)archetype.Components[c9Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3, T4, T5, T6, T7, T8, T9> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
int c8Index = c8Indices[ai];
int c9Index = c9Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei], (T8)archetype.Components[c8Index][ei], (T9)archetype.Components[c9Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9> OnComponents<T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ecs, includeComponents, excludeComponents);
        }
    }
    
	public sealed class ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Ecs ecs, List<Type> includeComponents, List<Type> excludeComponents)
        : Query(ecs, includeComponents, excludeComponents)
        where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase where T10 : ComponentBase
    {
        private int[] c1Indices = [];
private int[] c2Indices = [];
private int[] c3Indices = [];
private int[] c4Indices = [];
private int[] c5Indices = [];
private int[] c6Indices = [];
private int[] c7Indices = [];
private int[] c8Indices = [];
private int[] c9Indices = [];
private int[] c10Indices = [];

        protected override void OnMatchingArchetypesUpdated()
        {
            c1Indices = new int[MatchingArchetypes.Count];
c2Indices = new int[MatchingArchetypes.Count];
c3Indices = new int[MatchingArchetypes.Count];
c4Indices = new int[MatchingArchetypes.Count];
c5Indices = new int[MatchingArchetypes.Count];
c6Indices = new int[MatchingArchetypes.Count];
c7Indices = new int[MatchingArchetypes.Count];
c8Indices = new int[MatchingArchetypes.Count];
c9Indices = new int[MatchingArchetypes.Count];
c10Indices = new int[MatchingArchetypes.Count];
            for (int i = 0; i < MatchingArchetypes.Count; i++)
            {
                Archetype archetype = MatchingArchetypes[i];
                Type t1 = typeof(T1);
c1Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t1 || x.IsSubclassOf(t1));
Type t2 = typeof(T2);
c2Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t2 || x.IsSubclassOf(t2));
Type t3 = typeof(T3);
c3Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t3 || x.IsSubclassOf(t3));
Type t4 = typeof(T4);
c4Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t4 || x.IsSubclassOf(t4));
Type t5 = typeof(T5);
c5Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t5 || x.IsSubclassOf(t5));
Type t6 = typeof(T6);
c6Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t6 || x.IsSubclassOf(t6));
Type t7 = typeof(T7);
c7Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t7 || x.IsSubclassOf(t7));
Type t8 = typeof(T8);
c8Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t8 || x.IsSubclassOf(t8));
Type t9 = typeof(T9);
c9Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t9 || x.IsSubclassOf(t9));
Type t10 = typeof(T10);
c10Indices[i] = Array.FindIndex(archetype.ComponentTypes, x => x == t10 || x.IsSubclassOf(t10));
            }
        }

        public void ForEach(Action<uint, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
                int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
int c8Index = c8Indices[ai];
int c9Index = c9Indices[ai];
int c10Index = c10Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei], (T8)archetype.Components[c8Index][ei], (T9)archetype.Components[c9Index][ei], (T10)archetype.Components[c10Index][ei]);
                }
            }
        }

        public void First(Action<uint, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> action)
        {
            for (int ai = 0; ai < MatchingArchetypes.Count; ai++)
            {
				int c1Index = c1Indices[ai];
int c2Index = c2Indices[ai];
int c3Index = c3Indices[ai];
int c4Index = c4Indices[ai];
int c5Index = c5Indices[ai];
int c6Index = c6Indices[ai];
int c7Index = c7Indices[ai];
int c8Index = c8Indices[ai];
int c9Index = c9Indices[ai];
int c10Index = c10Indices[ai];
                Archetype archetype = MatchingArchetypes[ai];

                for (int ei = 0; ei < archetype.EntityCount; ei++)
                {
                    uint entityId = archetype.Ids[ei];

                    action.Invoke(entityId, (T1)archetype.Components[c1Index][ei], (T2)archetype.Components[c2Index][ei], (T3)archetype.Components[c3Index][ei], (T4)archetype.Components[c4Index][ei], (T5)archetype.Components[c5Index][ei], (T6)archetype.Components[c6Index][ei], (T7)archetype.Components[c7Index][ei], (T8)archetype.Components[c8Index][ei], (T9)archetype.Components[c9Index][ei], (T10)archetype.Components[c10Index][ei]);
                    return;
                }
            }
        }
	}

    public partial class QueryBuilder {
        public ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> OnComponents<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() where T1 : ComponentBase where T2 : ComponentBase where T3 : ComponentBase where T4 : ComponentBase where T5 : ComponentBase where T6 : ComponentBase where T7 : ComponentBase where T8 : ComponentBase where T9 : ComponentBase where T10 : ComponentBase
        {
            return new ComponentQuery<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ecs, includeComponents, excludeComponents);
        }
    }
    }