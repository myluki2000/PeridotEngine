using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine.Scenes.Scene3D;
using Point = Microsoft.Xna.Framework.Point;

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
            RenderTargetBinding[]? rts = Globals.GraphicsDevice.GetRenderTargets();
            renderPipeline.Render(rts.Length > 0 ? (RenderTarget2D)rts[0].RenderTarget : null);
        }

        public override void Deinitialize()
        {
            renderPipeline?.Dispose();
            renderPipeline = null;
            scene.Deinitialize();
        }

        public int GetObjectIdAtScreenPos(Point screenPos)
        {
            return renderPipeline?.GetObjectIdAtScreenPos(screenPos) 
                   ?? throw new Exception("Can only get object at screen pos after render pipeline has been initialized!");
        }
    }
}
