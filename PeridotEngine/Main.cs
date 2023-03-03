﻿using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.Graphics;
using PeridotEngine.Misc;

namespace PeridotEngine
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        private readonly FpsMeasurer fpsMeasurer = new();

        public Main()
        {
            Globals.GameMain = this;
            Globals.Graphics = new GraphicsDeviceManager(this);
            Globals.Graphics.GraphicsProfile = GraphicsProfile.HiDef;
            Globals.Graphics.PreferMultiSampling = true;
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
            
            ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            fpsMeasurer.StartFrameTimeMeasure();
            ScreenManager.Draw(gameTime);
            fpsMeasurer.StopFrameTimeMeasure();

            Window.Title = "PeridotEngine (Avg. Frame Time: " + fpsMeasurer.GetAverageFrameTime().ToString("#00.00") + "ms)";

            base.Draw(gameTime);
        }
    }
}
