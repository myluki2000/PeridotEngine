using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeridotEngine;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.EditorScreen.Controls
{
    public partial class TextureResourcesManagementControl : UserControl
    {
        private readonly Scene3D scene;

        public TextureResources.ITextureInfo? SelectedItem { get; private set; }

        public event EventHandler<TextureResources.ITextureInfo?>? SelectedItemChanged;

        public event EventHandler<TextureResources.ITextureInfo>? ItemDoubleClicked;

        private readonly ImageList listViewImageList = new();

        public TextureResourcesManagementControl(Scene3D scene)
        {
            InitializeComponent();

            listViewImageList.ColorDepth = ColorDepth.Depth32Bit;
            listViewImageList.ImageSize = new Size(tbImageSize.Value, tbImageSize.Value);
            lvTextures.LargeImageList = listViewImageList;
            lvTextures.SmallImageList = listViewImageList;

            this.scene = scene;
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

        private void btnRemoveTexture_Click(object sender, EventArgs e)
        {
            if (lvTextures.SelectedItems.Count == 0)
                return;

            TextureResources.ITextureInfo texInfo = (TextureResources.ITextureInfo)lvTextures.SelectedItems[0].Tag;

            scene.Resources.TextureResources.RemoveTexture(texInfo.Id);
        }

        private void lvTextures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvTextures.SelectedItems.Count > 0)
            {
                TextureResources.ITextureInfo texInfo = (TextureResources.ITextureInfo)lvTextures.SelectedItems[0].Tag;
                SelectedItem = texInfo;
            }
            else
            {
                SelectedItem = null;
            }
            SelectedItemChanged?.Invoke(this, SelectedItem);
        }

        private void OnTextureAtlasChanged(object? sender, IEnumerable<TextureResources.ITextureInfo> textureInfos)
        {
            PopulateTextureList(textureInfos);
        }

        private void PopulateTextureList(IEnumerable<TextureResources.ITextureInfo> textureInfos)
        {
            lvTextures.Items.Clear();

            foreach (Image img in listViewImageList.Images)
            {
                img.Dispose();
            }
            listViewImageList.Images.Clear();

            foreach (TextureResources.ITextureInfo texInfo in textureInfos)
            {
                listViewImageList.Images.Add(Image.FromFile(Globals.Content.RootDirectory + "/" + texInfo.FilePath));

                ListViewItem item = new();
                item.Text = texInfo.FilePath;
                item.ImageIndex = listViewImageList.Images.Count - 1;
                item.Tag = texInfo;
                
                lvTextures.Items.Add(item);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            scene.Resources.TextureResources.TextureAtlasChanged += OnTextureAtlasChanged;
            PopulateTextureList(scene.Resources.TextureResources.GetAllTextures());
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            scene.Resources.TextureResources.TextureAtlasChanged -= OnTextureAtlasChanged;
        }

        private void lvTextures_DoubleClick(object sender, EventArgs e)
        {
            ItemDoubleClicked?.Invoke(this, SelectedItem!);
        }

        private void tbImageSize_Scroll(object sender, EventArgs e)
        {
            listViewImageList.ImageSize = new Size(tbImageSize.Value, tbImageSize.Value);
            PopulateTextureList(scene.Resources.TextureResources.GetAllTextures());
        }

        ~TextureResourcesManagementControl()
        {
            listViewImageList.Dispose();
        }
    }
}
