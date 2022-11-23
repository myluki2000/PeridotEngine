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
        public Entity? SelectedEntity;
        public event EventHandler<Entity?>? SelectedEntityChanged;

        private readonly Scene3D scene;

        public SceneForm(Scene3D scene)
        {
            InitializeComponent();

            this.scene = scene;
            scene.Ecs.EntityListChanged += EcsOnEntityListChanged;
            Populate();
        }

        private void EcsOnEntityListChanged(object? sender, Archetype e)
        {
            if (SelectedEntity != null && SelectedEntity.IsDeleted)
            {
                SelectedEntity = null;
                SelectedEntityChanged?.Invoke(this, null);
            }
            Populate();
        }

        public void Populate()
        {
            List<ListViewItem> items = new();
            scene.Ecs.Query().ForEach((Entity entity) =>
            {
                string name = string.IsNullOrEmpty(entity.Name)
                    ? string.Join(", ", entity.Components.Select(x => x.GetType().Name))
                    : entity.Name;

                ListViewItem item = new(name)
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
            if (lvScene.SelectedItems.Count == 0) SelectedEntity = null;
            else SelectedEntity = (Entity)lvScene.SelectedItems[0].Tag;

            SelectedEntityChanged?.Invoke(this, SelectedEntity);
        }
    }
}
