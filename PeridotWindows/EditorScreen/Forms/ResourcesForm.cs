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

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class ResourcesForm : Form
    {
        private Scene3D scene;

        public ResourcesForm(Scene3D scene)
        {
            this.scene = scene;

            scene.Resources.TextureResources.TextureListChanged += OnTextureListChanged;

            InitializeComponent();
        }

        private void OnTextureListChanged(object? sender, IEnumerable<TextureResources.TextureInfo> textureInfos)
        {
            lvTextures.Items.Clear();

            foreach (TextureResources.TextureInfo texInfo in textureInfos)
            {
                ImageList imgList = new();
                lvTextures.LargeImageList = imgList;
                lvTextures.SmallImageList = imgList;
                
                imgList.Images.Add(Image.FromFile(texInfo.FilePath));

                ListViewItem item = new();
                item.Text = texInfo.FilePath;
                item.ImageIndex = imgList.Images.Count - 1;
                item.Tag = texInfo;


                lvTextures.Items.Add(item);
            }
        }

        private void btnAddTexture_Click(object sender, EventArgs e)
        {
            string? path = InputBox.Show("Add Texture", "Path to texture file:");

            if (!string.IsNullOrEmpty(path))
            {
                scene.Resources.TextureResources.AddTexture(path);
            }
        }
    }
}
