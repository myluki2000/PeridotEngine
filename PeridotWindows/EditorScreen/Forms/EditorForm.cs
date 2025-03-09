using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeridotEngine.Graphics;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class EditorForm: Form
    {
        public EditorForm()
        {
            InitializeComponent();
            ScreenManager.CurrentScreen = new EditorScreen();
        }

        public EditorForm(string sceneFilePath)
        {
            InitializeComponent();

            string json = File.ReadAllText(sceneFilePath);

            game.OnInitialized += (sender, args) =>
            {
                Scene3D newScene = new Scene3D(json);
                ScreenManager.CurrentScreen = new EditorScreen(newScene);
            };
        }
    }
}
