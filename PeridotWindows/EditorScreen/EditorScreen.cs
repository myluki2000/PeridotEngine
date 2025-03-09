using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Geometry;
using PeridotEngine.Graphics.Screens;
using PeridotEngine.Misc;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using PeridotWindows.EditorScreen.Forms;
using PeridotWindows.Graphics.Camera;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

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

        public EditorMode Mode { get; set; }

        private readonly EditorObjectMoveTool moveTool = new();

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

            FrmResources = new(Scene);
            FrmResources.Show();
            FrmToolbox = new(Scene);
            FrmToolbox.Show();
            FrmEntity = new(this);
            FrmEntity.Show();
            FrmScene = new(this);
            FrmScene.Show();
        }

        private KeyboardState lastKeyboardState;
        private MouseState lastMouseState;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (lastKeyboardState.IsKeyUp(Keys.Delete) && keyboardState.IsKeyDown(Keys.Delete))
            {
                SelectedEntity?.Delete();
            }

            if (Mode == EditorMode.NONE
                && mouseState.LeftButton == ButtonState.Pressed
                && lastMouseState.LeftButton == ButtonState.Released
                && mouseState.X > 0
                && mouseState.X < Globals.GraphicsDevice.PresentationParameters.BackBufferWidth
                && mouseState.Y > 0
                && mouseState.Y < Globals.GraphicsDevice.PresentationParameters.BackBufferHeight)
            {
                int clickedObjectId = GetObjectIdAtScreenPos(mouseState.Position);
                SelectedEntity = Scene.Ecs.EntityById((uint)clickedObjectId);
            }

            moveTool.HandleObjectMove(this);
            EditorObjectRotateHandler.HandleObjectRotate(this);
            EditorObjectScaleHandler.HandleObjectScale(this);

            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice gd = Globals.GraphicsDevice;

            DrawSunlightVisualization(gd);
            DrawSelectedObjectBox(gd);
        }

        /// <summary>
        /// Helper method to draw a box around the currently selected object.
        /// </summary>
        private void DrawSelectedObjectBox(GraphicsDevice gd)
        {
            if (SelectedEntity == null)
                return;

            if (SelectedEntity.Archetype.HasComponent<StaticMeshComponent>() && SelectedEntity.Archetype.HasComponent<PositionRotationScaleComponent>())
            {
                StaticMeshComponent meshC = SelectedEntity.GetComponent<StaticMeshComponent>();
                PositionRotationScaleComponent posC = SelectedEntity.GetComponent<PositionRotationScaleComponent>();

                SimpleEffect effect = new();
                effect.World = posC.Transformation;
                effect.View = Scene.Camera.GetViewMatrix();
                effect.Projection = Scene.Camera.GetProjectionMatrix();
                effect.Apply();

                BoundingBox bounds = meshC.Mesh.Mesh.Bounds;

                VertexPosition[] verts = new VertexPosition[24]
                {
                    // bottom layer
                    new(bounds.Min), // front bottom left
                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Min.Z)), // front bottom right

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Min.Z)), // front bottom right
                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Max.Z)), // back bottom right

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Max.Z)), // back bottom right
                    new(new(bounds.Min.X, bounds.Min.Y, bounds.Max.Z)), // back bottom left
                    
                    new(new(bounds.Min.X, bounds.Min.Y, bounds.Max.Z)), // back bottom left
                    new(bounds.Min), // front bottom left

                    // edges between bottom and top layer
                    new(bounds.Min), // front bottom left
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Min.Z)), // front top left

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Min.Z)), // front bottom right
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Min.Z)), // front top right

                    new(new(bounds.Max.X, bounds.Min.Y, bounds.Max.Z)), // back bottom right
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Max.Z)), // back top right

                    new(new(bounds.Min.X, bounds.Min.Y, bounds.Max.Z)), // back bottom left
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Max.Z)), // back top left

                    // top layer
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Min.Z)), // front top left
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Min.Z)), // front top right

                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Min.Z)), // front top right
                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Max.Z)), // back top right

                    new(new(bounds.Max.X, bounds.Max.Y, bounds.Max.Z)), // back top right
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Max.Z)), // back top left
                    
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Max.Z)), // back top left
                    new(new(bounds.Min.X, bounds.Max.Y, bounds.Min.Z)), // front top left
                };

                foreach (EffectPass pass in effect.Techniques[0].Passes)
                {
                    pass.Apply();
                    gd.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, verts.Length / 2);
                }
            }

        }

        /// <summary>
        /// Helper method to draw a representation of sunlight objects in the editor.
        /// </summary>
        private void DrawSunlightVisualization(GraphicsDevice gd)
        {
            Scene.Ecs.Query().Has<SunLightComponent>().Has<PositionRotationScaleComponent>().ForEach(
                (uint _, PositionRotationScaleComponent posC) =>
                {
                    SimpleEffect effect = new();
                    effect.World = Matrix.Identity;
                    effect.View = Scene.Camera.GetViewMatrix();
                    effect.Projection = Scene.Camera.GetProjectionMatrix();
                    effect.Apply();

                    // direction the sun is "facing"
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

                    foreach (EffectPass pass in effect.Techniques[0].Passes)
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

        public enum EditorMode
        {
            NONE,
            OBJECT_MOVE,
            OBJECT_ROTATE,
            OBJECT_SCALE
        }
    }
}
