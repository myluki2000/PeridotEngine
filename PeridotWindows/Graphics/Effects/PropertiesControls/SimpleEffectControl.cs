using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Misc;
using PeridotWindows.ECS.Components;

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
            effectProperties.TextureId = (uint)nudTextureId.Value;
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
    }
}
