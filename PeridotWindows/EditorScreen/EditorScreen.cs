using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.Graphics.Screens;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.EditorScreen.Forms;
using PeridotWindows.Graphics.Camera;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = System.Drawing.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Timer = System.Timers.Timer;

namespace PeridotWindows.EditorScreen
{
    internal class EditorScreen : PeridotEngine.Graphics.Screens.Screen
    {
        private readonly Scene3D scene = new();

        private ResourcesForm? frmResources;
        private ToolboxForm? frmToolbox;
        private EntityForm? frmEntity;
        private SceneForm? frmScene;

        private Entity? selectedEntity = null;

        private readonly Timer windowTimer = new(50);
        private Rectangle windowLastBounds;


        public override void Initialize()
        {
            scene.Camera = new EditorCamera();

            Globals.GameMain.Window.AllowUserResizing = true;

            Control mainWindowControl = Control.FromHandle(Globals.GameMain.Window.Handle);

            frmResources = new(scene);
            frmResources.Show(mainWindowControl);
            frmToolbox = new();
            frmToolbox.Show(mainWindowControl);
            frmEntity = new();
            frmEntity.Show(mainWindowControl);
            frmScene = new(scene);
            frmScene.Show(mainWindowControl);
            
            frmScene.SelectedEntityChanged += FrmScene_OnSelectedEntityChanged;

            windowTimer.Start();
            windowTimer.Elapsed += (sender, args) =>
            {
                frmToolbox.Invoke(() =>
                {
                    int titleHeight = frmToolbox.RectangleToScreen(frmToolbox.ClientRectangle).Top - frmToolbox.Top;

                    Rectangle bounds = Globals.GameMain.Window.ClientBounds;

                    if (bounds != windowLastBounds)
                    {

                        frmToolbox.Location = new Point(bounds.Left, bounds.Top - titleHeight - 3 - frmToolbox.Height);
                        frmToolbox.Width = bounds.Width;

                        frmResources.Location = new Point(bounds.Left, bounds.Bottom + 3);
                        frmResources.Width = bounds.Width;

                        frmScene.Location = new Point(bounds.Left - frmScene.Width - 3, frmToolbox.Top);
                        frmScene.Height = frmResources.Bottom - frmToolbox.Top;

                        frmEntity.Location = new Point(bounds.Right + 3, frmToolbox.Top);
                        frmEntity.Height = frmResources.Bottom - frmToolbox.Top;
                    }

                    windowLastBounds = bounds;
                });
            };

            scene.Initialize();
        }

        private void FrmScene_OnSelectedEntityChanged(object? sender, Entity? e)
        {
            selectedEntity = e;

            if (frmEntity != null) frmEntity.Entity = e;
        }

        private KeyboardState lastKeyboardState;
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (lastKeyboardState.IsKeyUp(Keys.J) && keyboardState.IsKeyDown(Keys.J))
            {
                ((Form)Control.FromHandle(Globals.GameMain.Window.Handle)).Activate();
            }

            scene.Update(gameTime);

            lastKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime)
        {
            scene.Draw(gameTime);
        }

        public override void Deinitialize()
        {
            frmResources?.Dispose();
            frmResources = null;
            frmToolbox?.Dispose();
            frmToolbox = null;
            frmEntity?.Dispose();
            frmEntity = null;

            scene.Deinitialize();
        }
    }
}
