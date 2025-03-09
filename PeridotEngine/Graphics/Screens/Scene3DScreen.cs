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
        public Scene3D Scene { get; }

        private SceneRenderPipeline? renderPipeline;

        public Scene3DScreen(Scene3D scene)
        {
            Scene = scene;
        }

        public override void Initialize()
        {
           Scene.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            Scene.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            renderPipeline ??= new SceneRenderPipeline(Scene);
            RenderTargetBinding[]? rts = Globals.GraphicsDevice.GetRenderTargets();
            renderPipeline.Render(rts.Length > 0 ? (RenderTarget2D)rts[0].RenderTarget : null);
        }

        public override void Deinitialize()
        {
            renderPipeline?.Dispose();
            renderPipeline = null;
            Scene.Deinitialize();
        }

        public int GetObjectIdAtScreenPos(Point screenPos)
        {
            return renderPipeline?.GetObjectIdAtScreenPos(screenPos) 
                   ?? throw new Exception("Can only get object at screen pos after render pipeline has been initialized!");
        }
    }

    public class SceneChangedEventArgs(Scene3D old, Scene3D @new) : EventArgs
    {
        public Scene3D? Old { get; } = old;
        public Scene3D New { get; } = @new;
    }
}
