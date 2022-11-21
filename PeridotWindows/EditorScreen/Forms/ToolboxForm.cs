using PeridotEngine.Graphics;
using PeridotEngine.Scenes.Scene3D;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.IO.JsonConverters;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;

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

            string json = JsonConvert.SerializeObject(scene, new StaticMeshComponentJsonConverter(scene), new EffectPropertiesJsonConverter(scene), new ArchetypeJsonConverter());
            File.WriteAllText(sfd.FileName, json);
        }

        private void tsmiLoadScene_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string json = File.ReadAllText(ofd.FileName);

            Scene3D newScene = new Scene3D(json);

            ScreenManager.CurrentScreen = new EditorScreen(newScene);
        }

        private void tsmiAddSunlight_Click(object sender, EventArgs e)
        {
            scene.Ecs
                .Archetype(typeof(PositionRotationScaleComponent), typeof(SunLightComponent))
                .CreateEntity(
                    new PositionRotationScaleComponent(scene),
                    new SunLightComponent());
        }
    }
}
