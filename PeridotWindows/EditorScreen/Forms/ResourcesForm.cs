using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeridotEngine;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS.Components;
using IComponent = PeridotWindows.ECS.Components.IComponent;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class ResourcesForm : Form
    {
        private Scene3D scene;

        public ResourcesForm(Scene3D scene)
        {
            InitializeComponent();

            this.scene = scene;

            scene.Resources.TextureResources.TextureAtlasChanged += OnTextureAtlasChanged;
            scene.Resources.MeshResources.MeshListChanged += OnMeshListChanged;

            OnTextureAtlasChanged(null, scene.Resources.TextureResources.GetAllTextures());
            OnMeshListChanged(null, scene.Resources.MeshResources.GetAllMeshes());
        }

        private void OnMeshListChanged(object? sender, IEnumerable<MeshResources.MeshInfo> meshInfos)
        {
            lvMeshes.Items.Clear();

            foreach(MeshResources.MeshInfo meshInfo in meshInfos)
            {
                ListViewItem item = new(meshInfo.Name);
                item.Tag = meshInfo;

                lvMeshes.Items.Add(item);
            }
        }

        private void OnTextureAtlasChanged(object? sender, IEnumerable<TextureResources.TextureInfo> textureInfos)
        {
            lvTextures.Items.Clear();

            ImageList imgList = new();

            foreach (TextureResources.TextureInfo texInfo in textureInfos)
            {
                lvTextures.LargeImageList = imgList;
                lvTextures.SmallImageList = imgList;
                
                imgList.Images.Add(Image.FromFile(Globals.Content.RootDirectory + "/" + texInfo.FilePath));

                ListViewItem item = new();
                item.Text = texInfo.FilePath;
                item.ImageIndex = imgList.Images.Count - 1;
                item.Tag = texInfo;


                lvTextures.Items.Add(item);
            }
        }

        private void btnAddTexture_Click(object sender, EventArgs e)
        {
            string rootPath = Path.GetDirectoryName(Application.ExecutablePath)!;
            string contentPath = Path.Combine(rootPath, Globals.Content.RootDirectory);

            OpenFileDialog ofd = new();
            ofd.Multiselect = true;
            ofd.Filter = "Image files (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            if (ofd.FileNames.Any(x => !x.StartsWith(contentPath)))
            {
                MessageBox.Show("Could not import asset. Asset files need to be contained within the 'Content' directory of the game.");
                return;
            }

            foreach (string path in ofd.FileNames)
            {
                string trimmedPath = path.Substring(contentPath.Length);
                trimmedPath = trimmedPath.Replace("\\", "/");
                if (trimmedPath.StartsWith("/"))
                    trimmedPath = trimmedPath.Substring(1);
                scene.Resources.TextureResources.AddTexture(trimmedPath);
            }
        }

        private void btnAddModel_Click(object sender, EventArgs e)
        {
            string rootPath = Path.GetDirectoryName(Application.ExecutablePath)!;
            string contentPath = Path.Combine(rootPath, Globals.Content.RootDirectory);

            OpenFileDialog ofd = new();
            ofd.Filter = "Models (*.xnb)|*.xnb";

            if (ofd.ShowDialog() != DialogResult.OK) return;

            if (ofd.FileNames.Any(x => !x.StartsWith(contentPath)))
            {
                MessageBox.Show("Could not import asset. Asset files need to be contained within the 'Content' directory of the game.");
                return;
            }

            foreach (string path in ofd.FileNames)
            {
                string trimmedPath = path.Substring(contentPath.Length);
                trimmedPath = trimmedPath.Replace("\\", "/");
                if (trimmedPath.StartsWith("/"))
                    trimmedPath = trimmedPath.Substring(1);
                
                // remove ".xnb" extension
                trimmedPath = trimmedPath.Substring(0, trimmedPath.Length - 4);

                scene.Resources.MeshResources.LoadModel(trimmedPath);
            }
        }

        private void lvMeshes_DoubleClick(object sender, EventArgs e)
        {
            if (lvMeshes.SelectedItems == null || lvMeshes.SelectedItems.Count == 0) return;

            IComponent[] components = new IComponent[]
            {
                new PositionRotationScaleComponent(scene),
                new StaticMeshComponent(scene, ((MeshResources.MeshInfo)lvMeshes.SelectedItems[0].Tag).Mesh, scene.Resources.EffectPool.Effect<SimpleEffect>().CreateProperties())
            };

            scene.Ecs.Archetype(typeof(PositionRotationScaleComponent), typeof(StaticMeshComponent)).CreateEntity(components);
        }
    }
}
