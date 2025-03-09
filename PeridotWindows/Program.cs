using System.Diagnostics;
using System.Globalization;
using Microsoft.Xna.Framework;
using PeridotEngine;
using PeridotEngine.Graphics;
using PeridotWindows.EditorScreen.Forms;

namespace PeridotWindows
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            EditorForm form = args.Length == 1
                ? new EditorForm(args[0])
                : new EditorForm();
            Application.Run(form);
        }
    }
}