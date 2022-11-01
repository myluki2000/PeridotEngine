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
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class ResourcesForm : Form
    {
        private Scene3D scene;

        public ResourcesForm(Scene3D scene)
        {
            this.scene = scene;

            scene.Resources.TextureResources.TextureAtlasChanged += OnTextureAtlasChanged;

            InitializeComponent();
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

            if (ofd.FileNames.Any(x => !x.StartsWith(rootPath)))
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
    }
}
