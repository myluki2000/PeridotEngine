using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class SceneForm : Form
    {
        public event EventHandler<Entity?>? SelectedEntityChanged;

        private Scene3D scene;

        public SceneForm(Scene3D scene)
        {
            InitializeComponent();

            this.scene = scene;
            scene.Ecs.EntityListChanged += Scene_EntityListChanged;
        }

        private void Scene_EntityListChanged(object? sender, Archetype archetype)
        {
            List<ListViewItem> items = new();
            scene.Ecs.Query().ForEach((Entity entity) =>
            {
                ListViewItem item = new(string.Join(", ", entity.Components.Select(x => x.GetType().Name)))
                {
                    Tag = entity,
                };
                items.Add(item);
            });
            lvScene.Items.Clear();
            lvScene.Items.AddRange(items.ToArray());
        }

        private void lvScene_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvScene.SelectedItems.Count == 0) SelectedEntityChanged?.Invoke(this, null);
            else SelectedEntityChanged?.Invoke(this, (Entity)lvScene.SelectedItems[0].Tag);
        }
    }
}
