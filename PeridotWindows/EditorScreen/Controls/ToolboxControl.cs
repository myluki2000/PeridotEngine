using PeridotEngine.Scenes.Scene3D;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeridotEngine.ECS.Components;
using PeridotEngine.IO.JsonConverters;
using PeridotWindows.EditorScreen.Forms;

namespace PeridotWindows.EditorScreen.Controls
{
    public partial class ToolboxControl : UserControl
    {
        private readonly EditorForm frmEditor;

        public ToolboxControl(EditorForm frmEditor)
        {
            InitializeComponent();

            this.frmEditor = frmEditor;
        }

        /// <summary>
        /// Sets the tool strip which should be displayed for the tool specific toolstrip portion
        /// of the toolbox form. Pass NULL to hide the tool specific toolstrip.
        /// </summary>
        public void SetToolSpecificToolStrip(ToolStrip? toolSpecificToolStrip)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control != tsMain)
                    flowLayoutPanel1.Controls.Remove(control);
            }

            if (toolSpecificToolStrip != null)
                flowLayoutPanel1.Controls.Add(toolSpecificToolStrip);
        }

        private void tsmiNewScene_Click(object sender, EventArgs e)
        {
            EditorScreen screen = new EditorScreen(frmEditor, new Scene3D());
            frmEditor.Editor = screen;
        }

        private void tsmiSaveScene_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new();
            if (sfd.ShowDialog() != DialogResult.OK) return;

            Scene3D scene = frmEditor.Editor.Scene;
            string json = JsonConvert.SerializeObject(
                scene,
                new StaticMeshComponentJsonConverter(scene),
                new EffectPropertiesJsonConverter(scene),
                new EcsJsonConverter(scene));
            File.WriteAllText(sfd.FileName, json);
        }

        private void tsmiLoadScene_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new();

            if (ofd.ShowDialog() != DialogResult.OK) return;

            string json = File.ReadAllText(ofd.FileName);
            JToken root = JToken.Parse(json);

            Scene3D newScene = new(root);
            frmEditor.Editor = new EditorScreen(frmEditor, newScene);
        }

        private void tsmiAddSunlight_Click(object sender, EventArgs e)
        {
            Scene3D scene = frmEditor.Editor.Scene;
            scene.Ecs
                .Archetype(typeof(PositionRotationScaleComponent), typeof(SunLightComponent))
                .CreateEntity(
                    new PositionRotationScaleComponent(scene),
                    new SunLightComponent(scene));
        }

        private void tsmiAddEmpty_Click(object sender, EventArgs e)
        {
            Scene3D scene = frmEditor.Editor.Scene;
            scene.Ecs
                .Archetype(typeof(PositionRotationScaleComponent))
                .CreateEntity(new PositionRotationScaleComponent(scene));
        }
    }
}
