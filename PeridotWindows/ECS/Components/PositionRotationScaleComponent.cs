using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotEngine.ECS.Components
{
    public sealed partial class PositionRotationScaleComponent
    {
        private ComponentControlBase? propertiesControl;

        public override ComponentControlBase? GetPropertiesControl(Archetype.Entity entity)
        {
            propertiesControl ??= new PositionRotationScaleControl(entity);
            return propertiesControl;
        }
    }
}
