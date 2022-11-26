using System.Collections;
using System.ComponentModel;
using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS
{
    public partial class Archetype
    {
        public class Entity
        {
            public uint Id { get; private set; }

            public Archetype Archetype
            {
                get
                {
                    if (IsDeleted) throw new Exception("Cannot access a deleted entity.");
                    return archetype;
                }
            }

            public IReadOnlyCollection<ComponentBase> Components => Archetype.Components.Select(list => (ComponentBase)list[index]!).ToList();

            public string? Name
            {
                get => Archetype.Names[index];
                set => Archetype.Names[index] = value;
            }

            public bool IsDeleted { get; private set; }

            private int index;
            private Archetype archetype;

            public static Entity FromIndex(int index, Archetype archetype)
            {
                Entity e = new(archetype);

                e.index = index;
                e.Id = archetype.Ids[index];

                return e;
            }

            public static Entity FromEntityId(uint id, Archetype archetype)
            {
                return new Entity(id, archetype);
            }

            public Entity(uint id, Archetype archetype)
            {
                Id = id;
                this.archetype = archetype;
                UpdateEntityIndex();
            }

            private Entity(Archetype archetype)
            {
                this.archetype = archetype;
            }

            public void Delete()
            {
                IsDeleted = true;
                archetype.RemoveEntityAtInternal(index);
            }

            public void UpdateEntityIndex()
            {
                index = Archetype.Ids.IndexOf(Id);
            }

            public void AddComponent<T>(T component) where T : ComponentBase
            {
                ComponentBase[] newComponents = Components.Concat(new[] { component }).ToArray();
                ModifyEntity(newComponents);
            }

            public void AddComponent<T>() where T : ComponentBase, new()
            {
                ComponentBase[] newComponents = Components.Concat(new[] { new T() }).ToArray();
                ModifyEntity(newComponents);
            }

            public void AddComponent(Type componentType)
            {
                ComponentBase obj = (ComponentBase)Activator.CreateInstance(componentType, new object?[] { Components.First().Scene });
                ComponentBase[] newComponents = Components.Concat(new[] { obj }).ToArray();
                ModifyEntity(newComponents);
            }

            public void RemoveComponent<T>() where T : ComponentBase
            {
                ComponentBase[] newComponents = Components.Where(x => x.GetType() != typeof(T)).ToArray();
                ModifyEntity(newComponents);
            }

            public void RemoveComponent(Type componentType)
            {
                ComponentBase[] newComponents = Components.Where(x => x.GetType() != componentType).ToArray();
                ModifyEntity(newComponents);
            }

            private void ModifyEntity(ComponentBase[] newComponents)
            {
                Archetype newArchetype = Archetype.ecs.Archetype(newComponents.Select(x => x.GetType()).ToArray());

                newArchetype.Ids.Add(Id);
                newArchetype.Names.Add(Name);
                newArchetype.entities.Add(new WeakReference<Entity>(this));

                while (newArchetype.Components.Count < newArchetype.ComponentTypes.Length)
                    newArchetype.Components.Add(new ArrayList());

                newComponents = newComponents.OrderBy(x => x.GetType().FullName).ToArray();

                for (int i = 0; i < newComponents.Length; i++)
                {
                    newArchetype.Components[i].Add(newComponents[i]);
                }

                int oldIndex = index;
                Archetype oldArchetype = archetype;
                archetype = newArchetype;
                UpdateEntityIndex();
                oldArchetype.RemoveEntityAtInternal(index);
                
                Archetype.EntityListChanged?.Invoke();
            }
        }
    }
}
