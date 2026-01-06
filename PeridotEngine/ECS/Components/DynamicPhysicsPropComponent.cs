using System;
using System.Collections.Generic;
using System.Text;
using JoltPhysicsSharp;
using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.ECS.Components
{
    public sealed partial class DynamicPhysicsPropComponent(Scene3D scene) : ComponentBase(scene)
    {
        [JsonIgnore]
        public Body? PhysicsBody { get; set; }
        [JsonIgnore]
        public bool PhysicsBodyPropertiesOutdated { get; private set; }
    }
}
