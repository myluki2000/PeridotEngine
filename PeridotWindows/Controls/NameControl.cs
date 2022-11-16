using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventArgs = System.EventArgs;

namespace PeridotWindows.Controls
{
    public partial class NameControl : UserControl
    {
        public new event EventHandler? TextChanged; 

        public NameControl()
        {
            InitializeComponent();

            tbName.TextChanged += ((_, _) => TextChanged?.Invoke(this, EventArgs.Empty));
        }

        public override string Text
        {
            get => tbName.Text;
            set => tbName.Text = value;
        }
    }
}
