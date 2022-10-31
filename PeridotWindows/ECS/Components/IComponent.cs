using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components
{
    public partial interface IComponent
    {
        public UserControl? PropertiesControl { get; }
    }
}
