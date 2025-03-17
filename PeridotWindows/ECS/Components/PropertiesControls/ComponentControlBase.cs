using PeridotWindows.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS.Components.PropertiesControls
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<ComponentControlBase, UserControl>))]
    public abstract class ComponentControlBase(Archetype.Entity entity) : UserControl
    {
        public Archetype.Entity Entity { get; } = entity;
        public abstract ComponentBase Component { get; }
    }
}
