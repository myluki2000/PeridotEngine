using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PeridotEngine.Scenes
{
    public abstract class Scene
    {
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Deinitialize();
    }
}
