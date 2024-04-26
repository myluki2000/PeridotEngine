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
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = System.Drawing.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Timer = System.Timers.Timer;

namespace PeridotWindows.EditorScreen
{
    public class EditorScreen : Scene3DScreen
    {
        public ResourcesForm? FrmResources;
        public ToolboxForm? FrmToolbox;
        public EntityForm? FrmEntity;
        public SceneForm? FrmScene;

        private Archetype.Entity? selectedEntity = null;

        public Archetype.Entity? SelectedEntity
        {
            get => selectedEntity;
            set
            {
                if (value?.Id != selectedEntity?.Id)
                {
                    selectedEntity = value;
                    SelectedEntityChanged?.Invoke(this, selectedEntity);
                    if (FrmEntity != null) FrmEntity.Entity = value;
                }
            }
        }

        public event EventHandler<Archetype.Entity?>? SelectedEntityChanged;

        private Rectangle windowLastBounds;

        public EditorScreen() : base(new())
        {

        }

        public EditorScreen(Scene3D scene) : base(scene)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            Scene.Camera = new EditorCamera();

            Globals.GameMain.Window.AllowUserResizing = true;

            Control mainWindowControl = Control.FromHandle(Globals.GameMain.Window.Handle);
            
            FrmResources = new(Scene);
            FrmResources.Show(mainWindowControl);
            FrmToolbox = new(Scene);
            FrmToolbox.Show(mainWindowControl);
            FrmEntity = new(this);
            FrmEntity.Show(mainWindowControl);
            FrmScene = new(this);
            FrmScene.Show(mainWindowControl);
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

        private KeyboardState lastKeyboardState;
        private MouseState lastMouseState;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (lastKeyboardState.IsKeyUp(Keys.J) && keyboardState.IsKeyDown(Keys.J))
            {
                ((Form)Control.FromHandle(Globals.GameMain.Window.Handle)).Activate();
            }

            if (lastKeyboardState.IsKeyUp(Keys.Delete) && keyboardState.IsKeyDown(Keys.Delete))
            {
                SelectedEntity?.Delete();
            }

            if (mouseState.LeftButton == ButtonState.Pressed 
                && lastMouseState.LeftButton == ButtonState.Released
                && mouseState.X > 0
                && mouseState.X < Globals.Graphics.PreferredBackBufferWidth
                && mouseState.Y > 0
                && mouseState.Y < Globals.Graphics.PreferredBackBufferHeight)
            {
                int clickedObjectId = GetObjectIdAtScreenPos(mouseState.Position);
                SelectedEntity = Scene.Ecs.EntityById((uint)clickedObjectId);
            }

            UpdateWindowLocations();

            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            Scene.Ecs.Query().Has<SunLightComponent>().Has<PositionRotationScaleComponent>().ForEach(
                (uint _, PositionRotationScaleComponent posC) =>
                {
                    SimpleEffect effect = new();
                    effect.World = Matrix.Identity;
                    effect.View = Scene.Camera.GetViewMatrix();
                    effect.Projection = Scene.Camera.GetProjectionMatrix();
                    effect.Apply();

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
            base.Deinitialize();

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
