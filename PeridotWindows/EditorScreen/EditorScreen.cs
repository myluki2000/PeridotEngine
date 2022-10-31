using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine;
using PeridotEngine.Graphics.Screens;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.EditorScreen.Forms;
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
            Globals.GameMain.Window.AllowUserResizing = true;
            
            frmResources = new(scene);
            frmResources.Show();
            frmToolbox = new();
            frmToolbox.Show();
            frmEntity = new();
            frmEntity.Show();
            frmScene = new(scene);
            frmScene.Show();

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

            // TODO: Need to refocus the game window afterwards
            /*Globals.GameMain.Activated += (sender, args) =>
            {
                frmToolbox.Focus();
                frmResources.Focus();
                frmScene.Focus();
                frmEntity.Focus();
                
            };*/

            scene.Initialize();
        }

        private void FrmScene_OnSelectedEntityChanged(object? sender, Entity? e)
        {
            selectedEntity = e;

            if (frmEntity != null) frmEntity.Entity = e;
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
