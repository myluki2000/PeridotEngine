using PeridotEngine.Graphics;
using PeridotEngine.Scenes.Scene3D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class ToolboxForm : Form
    {
        private readonly Scene3D scene;

        public ToolboxForm(Scene3D scene)
        {
            InitializeComponent();

            this.scene = scene;
        }

        private void tsmiNewScene_Click(object sender, EventArgs e)
        {
            EditorScreen screen = new EditorScreen();

            ScreenManager.CurrentScreen = screen;
        }

        private void tsmiSaveScene_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new();

            if (sfd.ShowDialog() != DialogResult.OK) return;

            string json = JsonSerializer.Serialize(scene);
            File.WriteAllText(sfd.FileName, json);
        }
    }
}
