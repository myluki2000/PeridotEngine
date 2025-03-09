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
using PeridotWindows.Controls;
using PeridotWindows.ECS;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace PeridotWindows.EditorScreen
{
    internal class EditorObjectMoveTool : IDisposable
    {
        private KeyboardState lastKeyboardState;
        private MouseState lastMouseState;

        private Vector3 originalObjectPos;

        private bool lockToX = false;
        private bool lockToY = false;
        private bool lockToZ = false;

        private readonly ToolStrip propertiesToolstrip = new();
        private readonly ToolStripNumericUpDown nudStepSize;

        private decimal stepSize = 0;

        private Vector3 preciseLastPos;

        public EditorObjectMoveTool()
        {
            propertiesToolstrip.Items.Add(new ToolStripLabel("Move Tool:"));
            propertiesToolstrip.Items.Add(new ToolStripLabel("Snapping:"));

            nudStepSize = new ToolStripNumericUpDown()
            {
                NumericUpDownControl =
                {
                    DecimalPlaces = 2,
                    Maximum = 10,
                    Minimum = 0,
                    Value = 0,
                    Increment = 0.1M
                }
            };

            nudStepSize.ValueChanged += NudStepSizeOnValueChanged;

            propertiesToolstrip.Items.Add(nudStepSize);
        }

        private void NudStepSizeOnValueChanged(object? sender, EventArgs e)
        {
            stepSize = nudStepSize.NumericUpDownControl.Value;
        }

        public void HandleObjectMove(EditorScreen editor)
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

                editor.FrmToolbox.SetToolSpecificToolStrip(propertiesToolstrip);

                lockToX = true;
                lockToY = true;
                lockToZ = true;

                preciseLastPos = editor.SelectedEntity.GetComponent<PositionRotationScaleComponent>().Position;
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

                    Vector4 movePos = new(preciseLastPos, 1);

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
                    
                    preciseLastPos = newPos;

                    if (stepSize > 0)
                    {
                        newPos.X = (float)Math.Round(preciseLastPos.X / (float)stepSize) * (float)stepSize;
                        newPos.Y = (float)Math.Round(preciseLastPos.Y / (float)stepSize) * (float)stepSize;
                        newPos.Z = (float)Math.Round(preciseLastPos.Z / (float)stepSize) * (float)stepSize;
                    }

                    posC.Position = newPos;
                }

                if(mouseState.Position.X > Globals.GraphicsDevice.PresentationParameters.BackBufferWidth - 10)
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

        public ToolStrip GetToolStrip()
        {
            return propertiesToolstrip;
        }

        public void Dispose()
        {
            propertiesToolstrip.Dispose();
        }
    }
}
