using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.Graphics.Screens
{
    internal class EditorScreen : Screen
    {
        private readonly Scene3D scene = new();

        public override void Initialize()
        {
            scene.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            scene.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            scene.Draw(gameTime);
        }

        public override void Deinitialize()
        {
            scene.Deinitialize();
        }
    }
}
