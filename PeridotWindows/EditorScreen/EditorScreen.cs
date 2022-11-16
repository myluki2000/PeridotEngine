using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Graphics.Screens;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
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
        private readonly Scene3D scene;

        private ResourcesForm? frmResources;
        private ToolboxForm? frmToolbox;
        private EntityForm? frmEntity;
        private SceneForm? frmScene;

        private Entity? selectedEntity = null;

        private Rectangle windowLastBounds;

        public EditorScreen()
        {
            scene = new();
        }

        public EditorScreen(Scene3D scene)
        {
            this.scene = scene;
        }

        public override void Initialize()
        {
            scene.Camera = new EditorCamera();

            Globals.GameMain.Window.AllowUserResizing = true;

            Control mainWindowControl = Control.FromHandle(Globals.GameMain.Window.Handle);
            
            frmResources = new(scene);
            frmResources.Show(mainWindowControl);
            frmToolbox = new(scene);
            frmToolbox.Show(mainWindowControl);
            frmEntity = new();
            frmEntity.Show(mainWindowControl);
            frmScene = new(scene);
            frmScene.Show(mainWindowControl);
            
            frmScene.SelectedEntityChanged += FrmScene_OnSelectedEntityChanged;

            scene.Initialize();
        }

        private void UpdateWindowLocations()
        {
            int titleHeight = frmToolbox.RectangleToScreen(frmToolbox.ClientRectangle).Top - frmToolbox.Top;

            Rectangle bounds = Globals.GameMain.Window.ClientBounds;

            if (bounds != windowLastBounds)
            {
                Form mainWindow = (Form)Control.FromHandle(Globals.GameMain.Window.Handle);
                if (mainWindow.WindowState == FormWindowState.Maximized)
                {
                    int screenWith = mainWindow.Width;
                    int screenHeight = mainWindow.Height;
                    mainWindow.WindowState = FormWindowState.Normal;
                    mainWindow.Width = screenWith - frmScene.Width - frmEntity.Width;
                    mainWindow.Height = screenHeight - frmToolbox.Height - frmResources.Height;
                    mainWindow.Location = new Point(frmScene.Width, frmToolbox.Height);
                }

                frmToolbox.Location = new Point(bounds.Left, bounds.Top - titleHeight - frmToolbox.Height);
                frmToolbox.Width = bounds.Width;

                frmResources.Location = new Point(bounds.Left, bounds.Bottom);
                frmResources.Width = bounds.Width;

                frmScene.Location = new Point(bounds.Left - frmScene.Width, frmToolbox.Top);
                frmScene.Height = frmResources.Bottom - frmToolbox.Top;

                frmEntity.Location = new Point(bounds.Right, frmToolbox.Top);
                frmEntity.Height = frmResources.Bottom - frmToolbox.Top;
            }

            windowLastBounds = bounds;
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

            UpdateWindowLocations();

            lastKeyboardState = keyboardState;
        }

        public override void Draw(GameTime gameTime)
        {
            scene.Draw(gameTime);

            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            scene.Ecs.Query().Has<SunLightComponent>().Has<PositionRotationScaleComponent>().ForEach(
                (PositionRotationScaleComponent posC) =>
                {
                    SimpleEffect effect = new();
                    effect.World = Matrix.Identity;
                    effect.ViewProjection = scene.Camera.GetViewMatrix() * scene.Camera.GetProjectionMatrix();
                    effect.UpdateMatrices();

                    Vector3 direction = new Vector3(
                        (float)Math.Sin(posC.Rotation.Y),
                        0,
                        -(float)Math.Cos(posC.Rotation.Y)
                    );
                    direction.Normalize();
                    direction *= (float)Math.Cos(posC.Rotation.X);
                    direction.Y = (float)Math.Sin(posC.Rotation.X);
                    direction.Normalize();

                    VertexPosition[] verts = new[]
                    {
                        new VertexPosition(posC.Position),
                        new VertexPosition(posC.Position + direction)
                    };
                    
                    foreach(EffectPass pass in effect.Techniques[0].Passes)
                    {
                        pass.Apply();
                        gd.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, 1);
                    }
                });
        }

        public override void Deinitialize()
        {
            scene.Deinitialize();

            frmResources?.Dispose();
            frmResources = null;
            frmToolbox?.Dispose();
            frmToolbox = null;
            frmEntity?.Dispose();
            frmEntity = null;
            frmScene?.Dispose();
            frmScene = null;
        }
    }
}
