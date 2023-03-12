using System.Diagnostics;
using System.Globalization;
using Microsoft.Xna.Framework;
using PeridotEngine;
using PeridotEngine.Graphics;

namespace PeridotWindows
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new MainForm());
            using (var game = new Main())
            {
                ScreenManager.CurrentScreen = new EditorScreen.EditorScreen();
                game.Run();
            }
        }
    }
}