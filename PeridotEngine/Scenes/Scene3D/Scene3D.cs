using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine.Game.ECS;
using PeridotEngine.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Scenes.Scene3D
{
    public class Scene3D : Scene
    {
        public Ecs Ecs { get; } = new Ecs();
        public SceneResources Resources { get; } = new();

        public override void Initialize()
        {
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            Globals.Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        public override void Deinitialize()
        {
        }
    }
}
