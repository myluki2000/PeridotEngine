using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.Graphics;
using PeridotEngine.Misc;

namespace PeridotEngine
{
    public class Main
    {
        

        public Main(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Globals.GameMain = this;
            Globals.GraphicsDevice = graphicsDevice;
            Globals.Content = content;
            Globals.Content.RootDirectory = "Content";
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            ScreenManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            ScreenManager.Draw(gameTime);
        }
    }
}
