﻿using System;
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
    internal static class EditorObjectRotateHandler
    {
        private static KeyboardState lastKeyboardState;
        private static MouseState lastMouseState;

        private static Quaternion originalObjectRotation;

        private static bool lockToX = false;
        private static bool lockToY = false;
        private static bool lockToZ = false;

        public static void HandleObjectRotate(EditorScreen editor)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.R)
                && lastKeyboardState.IsKeyUp(Keys.R)
                && editor.SelectedEntity != null
                && editor.SelectedEntity.Archetype.HasComponent<PositionRotationScaleComponent>()
                && editor.Mode == EditorScreen.EditorMode.NONE)
            {
                // if object selected & we're currently not in any mode, go into move mode
                editor.Mode = EditorScreen.EditorMode.OBJECT_ROTATE;
                // save original object pos in case we abort the move
                originalObjectRotation = editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>().Rotation;

                lockToX = false;
                lockToY = false;
                lockToZ = false;
            }
            else if (editor.Mode == EditorScreen.EditorMode.OBJECT_ROTATE)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    // if mouse is pressed during object move, we're done
                    editor.Mode = EditorScreen.EditorMode.NONE;
                }
                else if (mouseState.RightButton == ButtonState.Pressed)
                {
                    // cancel move, reset object position to original
                    editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>().Rotation = originalObjectRotation;
                    editor.Mode = EditorScreen.EditorMode.NONE;
                }
                else if (keyboardState.IsKeyDown(Keys.X) && lastKeyboardState.IsKeyUp(Keys.X))
                {
                    // lock to x axis
                    lockToX = true;
                    lockToY = false;
                    lockToZ = false;
                }
                else if (keyboardState.IsKeyDown(Keys.Y) && lastKeyboardState.IsKeyUp(Keys.Y))
                {
                    // lock to y axis
                    lockToX = false;
                    lockToY = true;
                    lockToZ = false;
                }
                else if (keyboardState.IsKeyDown(Keys.Z) && lastKeyboardState.IsKeyUp(Keys.Z))
                {
                    // lock to z axis
                    lockToX = false;
                    lockToY = false;
                    lockToZ = true;
                }
                else
                {
                    PositionRotationScaleComponent posC = editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>();

                    Quaternion rot = posC.Rotation;

                    Matrix rotMat = Matrix.CreateFromQuaternion(rot);

                    float mouseDiff = (mouseState.X - lastMouseState.X) / 100f + (lastMouseState.Y - mouseState.Y) / 100f;

                    if (!lockToX && !lockToY && !lockToZ)
                    {
                        // transform to view space
                        rotMat = rotMat * editor.Scene.Camera.GetViewMatrix();
                        rotMat = rotMat * Matrix.CreateRotationZ(mouseDiff);
                        // back to world space
                        rotMat = rotMat * Matrix.Invert(editor.Scene.Camera.GetViewMatrix());
                    }
                    else if (lockToX)
                    {
                        rotMat = rotMat * Matrix.CreateRotationX(mouseDiff);
                    }
                    else if (lockToY)
                    {
                        rotMat = rotMat * Matrix.CreateRotationY(mouseDiff);
                    }
                    else if (lockToZ)
                    {
                        rotMat = rotMat * Matrix.CreateRotationZ(mouseDiff);
                    }

                    rotMat.Decompose(out Vector3 newScale, out Quaternion newRot, out Vector3 newPos);
                    posC.Rotation = newRot;
                }

                if (mouseState.Position.X > Globals.GraphicsDevice.PresentationParameters.BackBufferWidth - 10)
                    Mouse.SetPosition(10, mouseState.Position.Y);

                if (mouseState.Position.X < 10)
                    Mouse.SetPosition(Globals.GraphicsDevice.PresentationParameters.BackBufferWidth - 10, mouseState.Position.Y);

                if (mouseState.Position.Y > Globals.GraphicsDevice.PresentationParameters.BackBufferHeight - 10)
                    Mouse.SetPosition(mouseState.Position.X, 10);

                if (mouseState.Position.Y < 10)
                    Mouse.SetPosition(mouseState.Position.X, Globals.GraphicsDevice.PresentationParameters.BackBufferHeight - 10);
            }

            lastMouseState = Mouse.GetState();
            lastKeyboardState = keyboardState;
        }
    }
}
