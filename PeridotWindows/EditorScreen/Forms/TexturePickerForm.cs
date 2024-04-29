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
using PeridotWindows.EditorScreen.Controls;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class TexturePickerForm : Form
    {
        public TextureResources.ITextureInfo? SelectedTexture => textureResourcesControl.SelectedItem;
        
        private readonly TextureResourcesManagementControl textureResourcesControl;

        public TexturePickerForm(Scene3D scene)
        {
            InitializeComponent();

            textureResourcesControl = new(scene);
            textureResourcesControl.Dock = DockStyle.Fill;
            textureResourcesControl.ItemDoubleClicked += TextureResourcesControlOnItemDoubleClicked;
            pnlList.Controls.Add(textureResourcesControl);
        }

        private void TextureResourcesControlOnItemDoubleClicked(object? sender, TextureResources.ITextureInfo e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
