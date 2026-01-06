using System;
using System.Collections.Generic;
using System.Text;
using JoltPhysicsSharp;
using Newtonsoft.Json;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.ECS.Components
{
    public sealed partial class StaticPhysicsPropComponent(Scene3D scene) : ComponentBase(scene)
    {
        [JsonIgnore]
        public bool PhysicsBodyPropertiesOutdated { get; private set; }

        /// <summary>
        /// Mass of the object in kilograms.
        /// </summary>
        public float Mass
        {
            get;
            set
            {
                if (value != field)
                {
                    field = value;
                    PhysicsBodyPropertiesOutdated = true;
                }
            }
        }


    }
}
