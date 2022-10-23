using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Game.ECS.Components
{
    internal sealed class DiffuseTextureComponent : IComponent
    {
        public uint TextureId { get; set; }
    }
}
