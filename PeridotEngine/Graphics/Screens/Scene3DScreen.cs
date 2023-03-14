using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.Graphics.Screens
{
    public class Scene3DScreen : Screen
    {
        private Scene3D scene;
        public Scene3D Scene
        {
            get => scene;
            set
            {
                scene = value;
                renderPipeline?.Dispose();
                renderPipeline = null;
            }
        }

        private SceneRenderPipeline? renderPipeline;

        public Scene3DScreen(Scene3D scene)
        {
            this.scene = scene;
        }

        public override void Initialize()
        {
           scene.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            scene.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            renderPipeline ??= new SceneRenderPipeline(scene);
            renderPipeline.Render(null);
        }

        public override void Deinitialize()
        {
            renderPipeline?.Dispose();
            renderPipeline = null;
            scene.Deinitialize();
        }
    }
}
