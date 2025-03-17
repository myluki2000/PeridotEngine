using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotEngine.ECS.Components
{
    public partial class StaticMeshComponent
    {
        protected override ComponentControlBase? CreatePropertiesControl(Archetype.Entity entity)
        {
            return new StaticMeshControl(entity);
        }
    }
}
