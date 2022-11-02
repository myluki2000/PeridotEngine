using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotWindows.ECS.Components
{
    public sealed partial class PositionRotationScaleComponent
    {
        public PositionRotationScaleComponent(Scene3D scene)
        {
            Scene = scene;
            PropertiesControl = new PositionRotationScaleControl(this);
        }
    }
}
