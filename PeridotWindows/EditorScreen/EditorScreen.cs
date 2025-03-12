using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.Screens;
using PeridotEngine.Misc;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Systems;
using PeridotWindows.EditorScreen.Controls;
using PeridotWindows.EditorScreen.Forms;
using PeridotWindows.Graphics.Camera;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace PeridotWindows.EditorScreen
{
    public class EditorScreen : Scene3DScreen
    {
        private readonly EditorForm frmEditor;

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
                    frmEditor.EntityPropertiesPanel.Entity = value;
                }
            }
        }

        public event EventHandler<Archetype.Entity?>? SelectedEntityChanged;

        private Rectangle windowLastBounds;

        public EditorMode Mode { get; set; }

        private readonly EditorObjectMoveTool moveTool = new();
        private readonly EditorLightVisualizationRenderingSystem lightVisRenderingSystem;

        public EditorScreen(EditorForm frmEditor, Scene3D scene) : base(scene)
        {
            this.frmEditor = frmEditor;
            this.lightVisRenderingSystem = new EditorLightVisualizationRenderingSystem(scene);
        }

        public override void Initialize()
        {
            base.Initialize();

            Scene.Camera = new EditorCamera();
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
                uint? clickedObjectId = GetObjectIdAtScreenPos(mouseState.Position);
                SelectedEntity = clickedObjectId != null 
                    ? Scene.Ecs.EntityById(clickedObjectId.Value)
                    : null;
            }

            moveTool.HandleObjectMove(frmEditor);
            EditorObjectRotateHandler.HandleObjectRotate(this);
            EditorObjectScaleHandler.HandleObjectScale(this);

            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice gd = Globals.GraphicsDevice;

            lightVisRenderingSystem.DrawLightVisualization(gd);
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

                if (meshC.Mesh == null || meshC.Mesh.Mesh == null)
                    return;

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

        

        public override void Deinitialize()
        {
            base.Deinitialize();

            lightVisRenderingSystem.Dispose();
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
