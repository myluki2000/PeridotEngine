using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotEngine.ECS.Components
{
    public abstract partial class ComponentBase
    {
        private ComponentControlWrapper? propertiesControlWrapper;

        protected abstract ComponentControlBase? CreatePropertiesControl(Archetype.Entity entity);
        public ComponentControlWrapper GetPropertiesControlWrapper(Archetype.Entity entity)
        {
            propertiesControlWrapper ??= new ComponentControlWrapper(this, CreatePropertiesControl(entity));
            return propertiesControlWrapper;
        }
    }
}
