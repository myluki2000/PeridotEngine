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

        protected SceneRenderPipeline? RenderPipeline { get; set; }

        public Scene3DScreen(Scene3D scene)
        {
            Scene = scene;
        }

        public override void Initialize()
        {
           Scene.Initialize();
           RenderPipeline ??= new SceneRenderPipeline(Scene);
        }

        public override void Update(GameTime gameTime)
        {
            Scene.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            RenderTargetBinding[]? rts = Globals.GraphicsDevice.GetRenderTargets();
            RenderPipeline?.Render(rts.Length > 0 ? (RenderTarget2D)rts[0].RenderTarget : null);
        }

        public override void Deinitialize()
        {
            RenderPipeline?.Dispose();
            RenderPipeline = null;
            Scene.Deinitialize();
        }

        public uint? GetObjectIdAtScreenPos(Point screenPos)
        {
            if (RenderPipeline == null)
                throw new Exception("Can only get object at screen pos after render pipeline has been initialized!");

            return RenderPipeline?.GetObjectIdAtScreenPos(screenPos);
        }
    }

    public class SceneChangedEventArgs(Scene3D old, Scene3D @new) : EventArgs
    {
        public Scene3D? Old { get; } = old;
        public Scene3D New { get; } = @new;
    }
}
