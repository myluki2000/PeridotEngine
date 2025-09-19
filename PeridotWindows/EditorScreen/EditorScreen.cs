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
using PeridotWindows.Graphics;
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
                    (RenderPipeline as EditorSceneRenderPipeline)!.SelectedEntity = value;
                    SelectedEntityChanged.Invoke(this, selectedEntity);
                    frmEditor.EntityPropertiesPanel.Entity = value;
                }
            }
        }
        public Event<Archetype.Entity?> SelectedEntityChanged { get; } = new();

        private Rectangle windowLastBounds;

        public EditorMode Mode { get; set; }

        private readonly EditorObjectMoveTool moveTool = new();

        public EditorScreen(EditorForm frmEditor, Scene3D scene) : base(scene)
        {
            this.frmEditor = frmEditor;
        }

        public override void Initialize()
        {
            base.Initialize();

            RenderPipeline = new EditorSceneRenderPipeline(Scene);

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
        }

        public override void Deinitialize()
        {
            base.Deinitialize();
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
