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

            scene.Resources.TextureListChanged += OnTextureListChanged;

            InitializeComponent();
        }

        private void OnTextureListChanged(object? sender, IEnumerable<SceneResources.TextureInfo> textureInfos)
        {
            lvTextures.Items.Clear();

            foreach (SceneResources.TextureInfo texInfo in textureInfos)
            {
                ImageList imgList = new();
                lvTextures.LargeImageList = imgList;
                lvTextures.SmallImageList = imgList;
                
                imgList.Images.Add(Image.FromFile(texInfo.FilePath));
                lvTextures.Items.Add(texInfo.FilePath, imgList.Images.Count - 1);
            }
        }

        private void btnAddTexture_Click(object sender, EventArgs e)
        {
            string? path = InputBox.Show("Add Texture", "Path to texture file:");

            if (!string.IsNullOrEmpty(path))
            {
                scene.Resources.AddTexture(path);
            }
        }
    }
}
