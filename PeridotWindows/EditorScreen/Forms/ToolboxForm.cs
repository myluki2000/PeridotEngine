using PeridotEngine.Graphics;
using PeridotEngine.Scenes.Scene3D;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using PeridotEngine.IO.JsonConverters;

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

            string json = JsonConvert.SerializeObject(scene, new StaticMeshComponentJsonConverter(), new EffectPropertiesJsonConverter());
            File.WriteAllText(sfd.FileName, json);
        }
    }
}
