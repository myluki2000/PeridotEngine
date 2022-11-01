using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = Microsoft.Xna.Framework.Point;

namespace PeridotWindows.Graphics.Camera
{
    public class EditorCamera : PeridotEngine.Graphics.Camera.Camera
    {
        private MouseState lastMouseState;
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                MoveForward((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                MoveBackward((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                MoveLeft((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                MoveRight((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                MoveUp((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyState.IsKeyDown(Keys.LeftShift))
            {
                MoveDown((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (mouseState.RightButton == ButtonState.Pressed)
            {
                if (lastMouseState.RightButton == ButtonState.Pressed)
                {
                    Point diff = mouseState.Position - lastMouseState.Position;

                    Pitch += diff.Y * -0.005f;
                    Yaw += diff.X * 0.005f;
                }
            }

            lastMouseState = mouseState;
            base.Update(gameTime);
        }
    }
}
