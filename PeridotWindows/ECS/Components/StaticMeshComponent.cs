using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotWindows.ECS.Components
{
    internal partial class StaticMeshComponent
    {
        public UserControl? PropertiesControl { get; } = new StaticMeshControl();
    }
}
