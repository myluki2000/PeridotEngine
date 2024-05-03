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
    internal static class EditorObjectMoveHandler
    {
        private static KeyboardState lastKeyboardState;
        private static MouseState lastMouseState;

        private static Vector3 originalObjectPos;

        private static bool lockToX = false;
        private static bool lockToY = false;
        private static bool lockToZ = false;

        public static void HandleObjectMove(EditorScreen editor)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.G)
                && lastKeyboardState.IsKeyUp(Keys.G)
                && editor.SelectedEntity != null
                && editor.SelectedEntity.Archetype.HasComponent<PositionRotationScaleComponent>()
                && editor.Mode == EditorScreen.EditorMode.NONE)
            {
                // if object selected & we're currently not in any mode, go into move mode
                editor.Mode = EditorScreen.EditorMode.OBJECT_MOVE;
                // save original object pos in case we abort the move
                originalObjectPos = editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>().Position;

                lockToX = true;
                lockToY = true;
                lockToZ = true;
            }
            else if (editor.Mode == EditorScreen.EditorMode.OBJECT_MOVE)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // if mouse is pressed during object move, we're done
                    editor.Mode = EditorScreen.EditorMode.NONE;
                }
                else if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // cancel move, reset object position to original
                    editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>().Position = originalObjectPos;
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

                    Vector4 movePos = new(posC.Position, 1);

                    // from world space into view space
                    movePos = movePos.Transform(editor.Scene.Camera.GetViewMatrix());

                    // translate model matrix in view space by mouse movement
                    movePos = movePos.Transform(
                        Matrix.CreateTranslation(
                            new Vector3((mouseState.X - lastMouseState.X) / 100f,
                                (lastMouseState.Y - mouseState.Y) / 100f,
                                0)
                        )
                    );

                    // back from view space to world space
                    movePos = movePos.Transform(Matrix.Invert(editor.Scene.Camera.GetViewMatrix()));

                    movePos /= movePos.W;

                    Vector3 newPos = originalObjectPos;

                    if (lockToX)
                        newPos.X = movePos.X;

                    if (lockToY)
                        newPos.Y = movePos.Y;

                    if (lockToZ)
                        newPos.Z = movePos.Z;

                    posC.Position = newPos;
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
