using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotWindows.ECS.Components
{
    public partial class StaticMeshComponent
    {
        public StaticMeshComponent(Scene3D scene, MeshResources.MeshInfo mesh, EffectBase.EffectProperties effectProperties)
        {
            Scene = scene;
            Mesh = mesh;
            EffectProperties = effectProperties;

            PropertiesControl = new StaticMeshControl(this);
        }
    }
}
