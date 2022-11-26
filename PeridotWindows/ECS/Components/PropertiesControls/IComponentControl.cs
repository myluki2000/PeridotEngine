using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeridotWindows.ECS.Components.PropertiesControls
{
    public interface IComponentControl
    {
        public ContextMenuStrip? OptionsMenu { get; set; }
    }
}
