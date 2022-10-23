using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine;
using PeridotEngine.Graphics.Screens;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.EditorScreen.Forms;

namespace PeridotWindows.EditorScreen
{
    internal class EditorScreen : PeridotEngine.Graphics.Screens.Screen
    {
        private readonly Scene3D scene = new();

        private ResourcesForm? frmResources;
        private ToolboxForm? frmToolbox;
        private EntityForm? frmEntity;

        public override void Initialize()
        {
            frmResources = new(scene);
            frmResources.Show();
            frmToolbox = new();
            frmToolbox.Show();
            frmEntity = new();
            frmEntity.Show();

            scene.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            scene.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            scene.Draw(gameTime);
        }

        public override void Deinitialize()
        {
            frmResources?.Dispose();
            frmResources = null;
            frmToolbox?.Dispose();
            frmToolbox = null;
            frmEntity?.Dispose();
            frmEntity = null;

            scene.Deinitialize();
        }
    }
}
