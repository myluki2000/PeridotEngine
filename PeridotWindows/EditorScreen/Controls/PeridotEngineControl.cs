using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.NET.Controls;
using PeridotEngine;
using PeridotEngine.Misc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotWindows.EditorScreen.Controls
{
    public class PeridotEngineControl : MonoGameControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Main? Main { get; private set; }

        private readonly FpsMeasurer fpsMeasurer = new();

        public event EventHandler<double>? OnFpsMeasurement;
        public event EventHandler? OnInitialized;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInitialized { get; private set; } = false;

        protected override void Initialize()
        {
            Editor.GraphicsDevice.BlendState = BlendState.Opaque;

            Main = new Main(Editor.Content, Editor.GraphicsDevice);
            Main.Initialize();

            IsInitialized = true;
            OnInitialized?.Invoke(this, EventArgs.Empty);
        }

        protected override void Update(GameTime gameTime)
        {
            Main!.Update(gameTime);
        }

        protected override void Draw()
        {
            fpsMeasurer.StartFrameTimeMeasure();
            Main!.Draw(Editor.GameTime);
            fpsMeasurer.StopFrameTimeMeasure();

            OnFpsMeasurement?.Invoke(this, fpsMeasurer.GetAverageFrameTime());
        }
    }
}
