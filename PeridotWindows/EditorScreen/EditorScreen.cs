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
    public class EditorScreen : PeridotEngine.Graphics.Screens.Screen
    {
        private readonly Scene3D scene;

        public ResourcesForm? FrmResources;
        public ToolboxForm? FrmToolbox;
        public EntityForm? FrmEntity;
        public SceneForm? FrmScene;

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
            
            FrmResources = new(scene);
            FrmResources.Show(mainWindowControl);
            FrmToolbox = new(scene);
            FrmToolbox.Show(mainWindowControl);
            FrmEntity = new(this);
            FrmEntity.Show(mainWindowControl);
            FrmScene = new(scene);
            FrmScene.Show(mainWindowControl);
            
            FrmScene.SelectedEntityChanged += FrmScene_OnSelectedEntityChanged;

            scene.Initialize();
        }

        private void UpdateWindowLocations()
        {
            int titleHeight = FrmToolbox.RectangleToScreen(FrmToolbox.ClientRectangle).Top - FrmToolbox.Top;

            Rectangle bounds = Globals.GameMain.Window.ClientBounds;

            if (bounds != windowLastBounds)
            {
                Form mainWindow = (Form)Control.FromHandle(Globals.GameMain.Window.Handle);
                if (mainWindow.WindowState == FormWindowState.Maximized)
                {
                    int screenWith = mainWindow.Width;
                    int screenHeight = mainWindow.Height;
                    mainWindow.WindowState = FormWindowState.Normal;
                    mainWindow.Width = screenWith - FrmScene.Width - FrmEntity.Width;
                    mainWindow.Height = screenHeight - FrmToolbox.Height - FrmResources.Height;
                    mainWindow.Location = new Point(FrmScene.Width, FrmToolbox.Height);
                }

                FrmToolbox.Location = new Point(bounds.Left, bounds.Top - titleHeight - FrmToolbox.Height);
                FrmToolbox.Width = bounds.Width;

                FrmResources.Location = new Point(bounds.Left, bounds.Bottom);
                FrmResources.Width = bounds.Width;

                FrmScene.Location = new Point(bounds.Left - FrmScene.Width, FrmToolbox.Top);
                FrmScene.Height = FrmResources.Bottom - FrmToolbox.Top;

                FrmEntity.Location = new Point(bounds.Right, FrmToolbox.Top);
                FrmEntity.Height = FrmResources.Bottom - FrmToolbox.Top;
            }

            windowLastBounds = bounds;
        }

        private void FrmScene_OnSelectedEntityChanged(object? sender, Entity? e)
        {
            selectedEntity = e;

            if (FrmEntity != null) FrmEntity.Entity = e;
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

            FrmResources?.Dispose();
            FrmResources = null;
            FrmToolbox?.Dispose();
            FrmToolbox = null;
            FrmEntity?.Dispose();
            FrmEntity = null;
            FrmScene?.Dispose();
            FrmScene = null;
        }
    }
}
