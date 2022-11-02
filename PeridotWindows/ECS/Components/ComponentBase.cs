using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components
{
    public abstract partial class ComponentBase
    {
        [JsonIgnore]
        public UserControl? PropertiesControl { get; protected set; }
    }
}
