using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextureResources.ITextureInfo? SelectedItem { get; private set; }

        public event EventHandler<TextureResources.ITextureInfo?>? SelectedItemChanged;

        public event EventHandler<TextureResources.ITextureInfo>? ItemDoubleClicked;

        private Bitmap? textureAtlas;

        public TextureResourcesManagementControl(Scene3D scene)
        {
            InitializeComponent();

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
            /*if (lvTextures.SelectedItems.Count == 0)
                return;

            TextureResources.ITextureInfo texInfo = (TextureResources.ITextureInfo)lvTextures.SelectedItems[0].Tag;

            scene.Resources.TextureResources.RemoveTexture(texInfo.Id);*/
        }

        private void OnTextureAtlasChanged(object? sender, IEnumerable<TextureResources.ITextureInfo> _)
        {
            textureAtlas?.Dispose();

            if (scene.Resources.TextureResources.TextureAtlas == null)
            {
                textureAtlas = null;
                return;
            }

            // convert Texture2D texture atlas to a bitmap
            using MemoryStream ms = new();
            scene.Resources.TextureResources.TextureAtlas.SaveAsPng(
                ms,
                scene.Resources.TextureResources.TextureAtlas.Width,
                scene.Resources.TextureResources.TextureAtlas.Height);
            textureAtlas = new Bitmap(ms);
            pnlListView.Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            scene.Resources.TextureResources.TextureAtlasChanged += OnTextureAtlasChanged;
            OnTextureAtlasChanged(null, []);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            scene.Resources.TextureResources.TextureAtlasChanged -= OnTextureAtlasChanged;
        }

        private void tbImageSize_Scroll(object sender, EventArgs e)
        {
            pnlListView.Invalidate();
        }

        private void pnlListView_Paint(object sender, PaintEventArgs e)
        {
            if (textureAtlas == null)
                return;

            System.Drawing.Graphics g = e.Graphics;

            g.FillRectangle(Brushes.White, e.ClipRectangle);

            int i = 0;
            foreach (TextureResources.ITextureInfo textureInfo in scene.Resources.TextureResources.GetAllTextures())
            {
                if (SelectedItem == textureInfo)
                {
                    g.FillRectangle(Brushes.LightBlue, GetListViewItemRect(i));
                    g.DrawRectangle(Pens.DarkBlue, GetListViewItemRect(i));
                }

                g.DrawImage(
                    textureAtlas,
                    GetIconRect(i),
                    new RectangleF(
                        textureInfo.Bounds.X * textureAtlas.Width,
                        textureInfo.Bounds.Y * textureAtlas.Height,
                        textureInfo.Bounds.Width * textureAtlas.Width,
                        textureInfo.Bounds.Height * textureAtlas.Height),
                    GraphicsUnit.Pixel);

                Font font = new(Font.FontFamily, 6.0f);

                g.DrawString(
                    textureInfo.FilePath ?? "?",
                    font,
                    Brushes.Black,
                    GetTextRect(i));

                i++;
            }

            int itemHeight = (int)GetListViewItemRect(0).Height;
            int rowCount = i / GetItemsPerRow();
            int scrollMaximum = (int)(rowCount * itemHeight) - pnlListView.Bounds.Height;
            scrollBar.LargeChange = 40;
            scrollBar.Maximum = scrollMaximum > 0 
                ? scrollMaximum + scrollBar.LargeChange * 2 
                : 0;
            
        }

        private void pnlListView_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (TextureResources.ITextureInfo textureInfo in scene.Resources.TextureResources.GetAllTextures())
            {
                if (GetListViewItemRect(i).Contains(((MouseEventArgs)e).Location))
                {
                    if (SelectedItem == textureInfo)
                        return;

                    SelectedItem = textureInfo;
                    SelectedItemChanged?.Invoke(this, textureInfo);
                    pnlListView.Invalidate();
                    return;
                }

                i++;
            }

            if (SelectedItem != null)
            {
                SelectedItem = null;
                SelectedItemChanged?.Invoke(this, null);
                pnlListView.Invalidate();
            }
        }

        private const int ITEM_MARGIN = 4;
        private const int TEXT_HEIGHT = 22;

        private RectangleF GetListViewItemRect(int index)
        {
            int itemWidth = tbImageSize.Value + ITEM_MARGIN * 2;
            int itemHeight = tbImageSize.Value + TEXT_HEIGHT + ITEM_MARGIN * 2;
            int itemsPerRow = GetItemsPerRow();
            int row = index / itemsPerRow;
            int col = index % itemsPerRow;

            RectangleF itemRect = new(
                col * itemWidth + ITEM_MARGIN,
                row * itemHeight + ITEM_MARGIN,
                itemWidth,
                itemHeight);

            itemRect.Y -= scrollBar.Value;
            return itemRect;
        }

        private int GetItemsPerRow()
        {
            return pnlListView.Width / (tbImageSize.Value + ITEM_MARGIN * 2);
        }

        private RectangleF GetIconRect(int index)
        {
            RectangleF itemRect = GetListViewItemRect(index);
            itemRect.X += ITEM_MARGIN;
            itemRect.Y += ITEM_MARGIN;
            itemRect.Width -= ITEM_MARGIN * 2;
            itemRect.Height -= ITEM_MARGIN * 2 + TEXT_HEIGHT;
            return itemRect;
        }

        private RectangleF GetTextRect(int index)
        {
            RectangleF itemRect = GetListViewItemRect(index);
            itemRect.Y += itemRect.Height - TEXT_HEIGHT;
            itemRect.Height = TEXT_HEIGHT;
            return itemRect;
        }

        private void pnlListView_DoubleClick(object sender, EventArgs e)
        {
            int i = 0;
            foreach (TextureResources.ITextureInfo textureInfo in scene.Resources.TextureResources.GetAllTextures())
            {
                if (GetListViewItemRect(i).Contains(((MouseEventArgs)e).Location))
                {
                    ItemDoubleClicked?.Invoke(this, textureInfo);
                }

                i++;
            }
        }

        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            pnlListView.Invalidate();
        }

        ~TextureResourcesManagementControl()
        {
            textureAtlas?.Dispose();
        }
    }
}
