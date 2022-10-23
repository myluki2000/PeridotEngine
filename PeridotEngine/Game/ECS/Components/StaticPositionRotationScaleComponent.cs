using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PeridotEngine.Game.ECS.Components
{
    internal sealed class StaticPositionRotationScaleComponent : IComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }
}
