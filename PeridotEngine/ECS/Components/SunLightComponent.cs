using System;
using System.Collections.Generic;
using System.Text;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS.Components;

namespace PeridotEngine.ECS.Components
{
    public partial class SunLightComponent(Scene3D scene) : ComponentBase(scene)
    {
        public override event EventHandler<ComponentBase>? ValuesChanged;
    }
}
