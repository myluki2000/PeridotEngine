using PeridotEngine.Graphics;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Misc;
using PeridotWindows.EditorScreen.Forms;

namespace PeridotWindows.Graphics.Effects.PropertiesControls
{
    public partial class SimpleEffectControl : UserControl
    {
        private readonly SimpleEffect.SimpleEffectProperties effectProperties;

        public SimpleEffectControl(SimpleEffect.SimpleEffectProperties effectProperties)
        {
            InitializeComponent();

            this.effectProperties = effectProperties;

            Populate();
        }

        public void Populate()
        {
            cbTexture.Checked = effectProperties.TextureEnabled;
            nudTextureId.Value = effectProperties.TextureId;
            pnlColor.BackColor = effectProperties.MixColor.ToSystemColor();
            nudTextureRepeatX.Value = effectProperties.TextureRepeatX;
            nudTextureRepeatY.Value = effectProperties.TextureRepeatY;
            cbReceiveShadows.Checked = effectProperties.ShadowsEnabled;
            cbRandomTextureRotation.Checked = effectProperties.RandomTextureRotationEnabled;
        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new();

            if (colorDialog.ShowDialog() != DialogResult.OK) return;

            pnlColor.BackColor = colorDialog.Color;
            effectProperties.MixColor = colorDialog.Color.ToXnaColor();
        }

        private void nudTextureId_ValueChanged(object sender, EventArgs e)
        {
            effectProperties.TextureId = (int)nudTextureId.Value;
        }

        private void cbTexture_CheckedChanged(object sender, EventArgs e)
        {
            nudTextureId.Enabled = cbTexture.Checked;
            nudTextureRepeatX.Enabled = cbTexture.Checked;
            nudTextureRepeatY.Enabled = cbTexture.Checked;

            effectProperties.TextureEnabled = cbTexture.Checked;
        }

        private void nudTextureRepeatX_ValueChanged(object sender, EventArgs e)
        {
            effectProperties.TextureRepeatX = (int)nudTextureRepeatX.Value;
        }

        private void nudTextureRepeatY_ValueChanged(object sender, EventArgs e)
        {
            effectProperties.TextureRepeatY = (int)nudTextureRepeatY.Value;
        }

        private void cbReceiveShadows_CheckedChanged(object sender, EventArgs e)
        {
            effectProperties.ShadowsEnabled = cbReceiveShadows.Checked;
        }

        private void cbRandomTextureRotation_CheckedChanged(object sender, EventArgs e)
        {
            effectProperties.RandomTextureRotationEnabled = cbRandomTextureRotation.Checked;
        }

        private void btnPickTexture_Click(object sender, EventArgs e)
        {
            // TODO: Scene should instead be passed down into this control
            TexturePickerForm frmPicker = new(((EditorScreen.EditorScreen)ScreenManager.CurrentScreen).Scene);
            frmPicker.ShowDialog();

            if (frmPicker.DialogResult == DialogResult.OK && frmPicker.SelectedTexture != null)
            {
                nudTextureId.Value = frmPicker.SelectedTexture.Id;
            }
        }
    }
}
