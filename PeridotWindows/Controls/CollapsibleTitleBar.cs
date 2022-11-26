using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeridotWindows.Controls
{
    public partial class CollapsibleTitleBar : UserControl
    {
        public CollapsibleTitleBar()
        {
            InitializeComponent();
        }

        private ContextMenuStrip? _optionsMenu;
        public ContextMenuStrip? OptionsMenu
        {
            get => _optionsMenu;
            set
            {
                btnOptionsMenu.Visible = value != null;
                _optionsMenu = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => lblTitle.Text;
            set => lblTitle.Text = base.Text = value;
        }

        [Browsable(true)]
        public bool Collapsed
        {
            get => collapsed;
            set
            {
                collapsed = value;
                btnCollapse.Text = collapsed ? "▼" : "▲";
            }
        }

        [Browsable(true)]
        public event EventHandler<bool>? CollapseToggled;

        private bool collapsed = false;
        private bool parentAutoSize;
        private int parentHeight;

        private void btnCollapse_Click(object sender, EventArgs e)
        {
            Collapsed = !Collapsed;

            if (collapsed)
            {
                parentAutoSize = Parent.AutoSize;
                parentHeight = Parent.Height;
                Parent.AutoSize = false;
                Parent.Height = Height;
            }
            else
            {
                Parent.AutoSize = parentAutoSize;
                Parent.Height = parentHeight;
            }

            CollapseToggled?.Invoke(this, collapsed);
        }

        private void btnOptionsMenu_Click(object sender, EventArgs e)
        {
            if (OptionsMenu == null) return;

            OptionsMenu.Tag = Tag;
            OptionsMenu.Show(btnOptionsMenu, new Point(0, 0), ToolStripDropDownDirection.BelowLeft);
        }
    }
}
