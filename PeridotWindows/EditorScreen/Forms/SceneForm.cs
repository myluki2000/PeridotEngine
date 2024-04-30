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
        private readonly EditorScreen screen;

        public SceneForm(EditorScreen screen)
        {
            InitializeComponent();

            this.screen = screen;
            screen.Scene.Ecs.EntityListChanged += EcsOnEntityListChanged;
            screen.SelectedEntityChanged += ScreenOnSelectedEntityChanged;
            Populate();
        }

        private void ScreenOnSelectedEntityChanged(object? sender, Archetype.Entity? entity)
        {
            lvScene.SelectedIndexChanged -= lvScene_SelectedIndexChanged;
            foreach (ListViewItem listItem in lvScene.Items)
            {
                listItem.Selected = false;
            }

            if (entity != null)
            {
                foreach (ListViewItem listItem in lvScene.Items)
                {
                    Archetype.Entity itemEntity = (Archetype.Entity)listItem.Tag;
                    if (itemEntity.Id == entity.Id)
                        listItem.Selected = true;
                }
            }
            lvScene.SelectedIndexChanged += lvScene_SelectedIndexChanged;
        }

        private void EcsOnEntityListChanged(object? sender, Archetype e)
        {
            // TODO: This check should be in EditorScreen class
            if (screen.SelectedEntity != null && screen.SelectedEntity.IsDeleted)
            {
                screen.SelectedEntity = null;
            }
            Populate();
        }

        public void Populate()
        {
            List<ListViewItem> items = new();
            screen.Scene.Ecs.Query().ForEach((Archetype.Entity entity) =>
            {
                ListViewItem item = new(entity.ToString())
                {
                    Tag = entity,
                };
                items.Add(item);
            });
            lvScene.Items.Clear();
            lvScene.Items.AddRange(items.ToArray());
        }

        private void lvScene_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lvScene.SelectedItems.Count == 0) screen.SelectedEntity = null;
            else screen.SelectedEntity = (Archetype.Entity)lvScene.SelectedItems[0].Tag;
        }
    }
}
