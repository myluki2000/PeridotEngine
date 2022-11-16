using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeridotEngine.Graphics.Screens
{
    public abstract class Screen
    {
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void Deinitialize();
    }
}
