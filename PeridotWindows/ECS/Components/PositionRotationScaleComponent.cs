using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotWindows.ECS.Components
{
    public sealed partial class PositionRotationScaleComponent
    {
        public UserControl? PropertiesControl { get; }

        public PositionRotationScaleComponent()
        {
            PropertiesControl = new PositionRotationScaleControl(this);
        }
    }
}
