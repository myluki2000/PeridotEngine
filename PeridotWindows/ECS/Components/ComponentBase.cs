using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotEngine.ECS.Components
{
    public abstract partial class ComponentBase
    {
        [JsonIgnore]
        public abstract IComponentControl? PropertiesControl { get; }
    }
}
