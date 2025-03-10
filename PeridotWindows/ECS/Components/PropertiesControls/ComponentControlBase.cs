using PeridotWindows.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeridotWindows.ECS.Components.PropertiesControls
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<ComponentControlBase, UserControl>))]
    public abstract class ComponentControlBase : UserControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public abstract ContextMenuStrip? OptionsMenu { get; set; }
        public Archetype.Entity Entity { get; }

        protected ComponentControlBase(Archetype.Entity entity)
        {
            Entity = entity;
        }
    }
}
