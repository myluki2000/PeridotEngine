﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotEngine.ECS.Components
{
    public partial class SunLightComponent
    {
        protected override ComponentControlBase? CreatePropertiesControl(Archetype.Entity entity) => null;
    }
}
