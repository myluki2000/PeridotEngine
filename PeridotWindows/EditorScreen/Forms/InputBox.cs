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
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        public static string? Show(string title, string text, string? defaultInputValue)
        {
            InputBox frm = new();

            frm.lbl.Text = text;
            frm.Text = title;
            frm.tbInput.Text = defaultInputValue ?? "";

            DialogResult result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                return frm.tbInput.Text;
            }
            else
            {
                return null;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
