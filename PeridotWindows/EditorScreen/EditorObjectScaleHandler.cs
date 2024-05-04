using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.ECS.Components;
using PeridotEngine.Misc;
using PeridotEngine.Scenes;
using PeridotWindows.ECS;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PeridotWindows.EditorScreen
{
    internal static class EditorObjectScaleHandler
    {
        private static KeyboardState lastKeyboardState;
        private static MouseState lastMouseState;

        private static Vector3 originalObjectScale;

        private static bool lockToX = false;
        private static bool lockToY = false;
        private static bool lockToZ = false;

        public static void HandleObjectScale(EditorScreen editor)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.T)
                && lastKeyboardState.IsKeyUp(Keys.T)
                && editor.SelectedEntity != null
                && editor.SelectedEntity.Archetype.HasComponent<PositionRotationScaleComponent>()
                && editor.Mode == EditorScreen.EditorMode.NONE)
            {
                // if object selected & we're currently not in any mode, go into move mode
                editor.Mode = EditorScreen.EditorMode.OBJECT_SCALE;
                // save original object pos in case we abort the move
                originalObjectScale = editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>().Scale;

                lockToX = true;
                lockToY = true;
                lockToZ = true;
            }
            else if (editor.Mode == EditorScreen.EditorMode.OBJECT_SCALE)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // if mouse is pressed during object move, we're done
                    editor.Mode = EditorScreen.EditorMode.NONE;
                }
                else if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // cancel move, reset object position to original
                    editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>().Scale = originalObjectScale;
                    editor.Mode = EditorScreen.EditorMode.NONE;
                }
                else if (keyboardState.IsKeyDown(Keys.X) && lastKeyboardState.IsKeyUp(Keys.X))
                {
                    // lock to x axis
                    lockToX = true;
                    lockToY = false;
                    lockToZ = false;

                    // lock to yz plane
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                    {
                        lockToX = false;
                        lockToY = true;
                        lockToZ = true;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.Y) && lastKeyboardState.IsKeyUp(Keys.Y))
                {
                    // lock to y axis
                    lockToX = false;
                    lockToY = true;
                    lockToZ = false;

                    // lock to xz plane
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                    {
                        lockToX = true;
                        lockToY = false;
                        lockToZ = true;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.Z) && lastKeyboardState.IsKeyUp(Keys.Z))
                {
                    // lock to z axis
                    lockToX = false;
                    lockToY = false;
                    lockToZ = true;

                    // lock to xy plane
                    if (keyboardState.IsKeyDown(Keys.LeftControl))
                    {
                        lockToX = true;
                        lockToY = true;
                        lockToZ = false;
                    }
                }
                else
                {
                    PositionRotationScaleComponent posC = editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>();

                    Vector3 changedScale = posC.Scale;

                    changedScale += new Vector3((mouseState.X - lastMouseState.X) / 100f + (lastMouseState.Y - mouseState.Y) / 100f);


                    Vector3 newScale = originalObjectScale;

                    if (lockToX)
                        newScale.X = changedScale.X;

                    if (lockToY)
                        newScale.Y = changedScale.Y;

                    if (lockToZ)
                        newScale.Z = changedScale.Z;

                    posC.Scale = newScale;
                }

                if(mouseState.Position.X > Globals.Graphics.PreferredBackBufferWidth - 10)
                    Mouse.SetPosition(10, mouseState.Position.Y);

                if (mouseState.Position.X < 10)
                    Mouse.SetPosition(Globals.Graphics.PreferredBackBufferWidth - 10, mouseState.Position.Y);

                if (mouseState.Position.Y > Globals.Graphics.PreferredBackBufferHeight - 10)
                    Mouse.SetPosition(mouseState.Position.X, 10);

                if (mouseState.Position.Y < 10)
                    Mouse.SetPosition(mouseState.Position.X, Globals.Graphics.PreferredBackBufferHeight - 10);
            }

            lastMouseState = Mouse.GetState();
            lastKeyboardState = keyboardState;
        }
    }
}
