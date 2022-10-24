﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PeridotEngine.Graphics;

namespace PeridotEngine
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        public Main()
        {
            Globals.GameMain = this;
            Globals.Graphics = new GraphicsDeviceManager(this);
            Globals.Content = Content;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            ScreenManager.CurrentScreen?.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ScreenManager.CurrentScreen?.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
