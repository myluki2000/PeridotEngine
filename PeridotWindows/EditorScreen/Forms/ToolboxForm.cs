using PeridotEngine.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class ToolboxForm : Form
    {
        public ToolboxForm()
        {
            InitializeComponent();
        }

        private void tsmiNewScene_Click(object sender, EventArgs e)
        {
            EditorScreen screen = new EditorScreen();

            ScreenManager.CurrentScreen = screen;
        }
    }
}
