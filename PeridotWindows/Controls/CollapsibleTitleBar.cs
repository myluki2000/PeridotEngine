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

        [Browsable(true)]
        public override string Text
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
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
    }
}
