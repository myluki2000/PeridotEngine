using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeridotEngine.ECS;
using PeridotEngine.Graphics.Screens;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.EditorScreen.Forms;

namespace PeridotWindows.EditorScreen.Controls
{
    public partial class SceneControl : UserControl
    {
        private readonly EditorForm frmEditor;

        public SceneControl(EditorForm frmEditor)
        {
            InitializeComponent();

            this.frmEditor = frmEditor;

            frmEditor.EditorScreenChanged.AddWeakHandler(OnEditorScreenChanged);
        }

        private void OnEditorScreenChanged(object? sender, EditorScreenChangedEventArgs args)
        {
            if (args.Old != null)
            {
                args.Old.Scene.Ecs.EntityListChanged.RemoveHandler(EcsOnEntityListChanged);
                args.Old.SelectedEntityChanged.RemoveHandler(ScreenOnSelectedEntityChanged);
            }

            args.New.Scene.Ecs.EntityListChanged.AddWeakHandler(EcsOnEntityListChanged);
            args.New.SelectedEntityChanged.AddWeakHandler(ScreenOnSelectedEntityChanged);

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

        private void EcsOnEntityListChanged(object? sender, EntityListChangedEventArgs e)
        {
            // TODO: This check should be in EditorScreen class
            if (frmEditor.Editor.SelectedEntity != null && frmEditor.Editor.SelectedEntity.IsDeleted)
            {
                frmEditor.Editor.SelectedEntity = null;
            }
            Populate();
        }

        public void Populate()
        {
            List<ListViewItem> items = new();
            EntityQuery query = frmEditor.Editor.Scene.Ecs.Query().OnEntity();
            query.ForEach(entity =>
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
            if (lvScene.SelectedItems.Count == 0) frmEditor.Editor.SelectedEntity = null;
            else frmEditor.Editor.SelectedEntity = (Archetype.Entity)lvScene.SelectedItems[0].Tag;
        }
    }
}
