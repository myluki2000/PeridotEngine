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
                Random r = new();
                Vector3[] kernel = new Vector3[256];

                for (int i = 0; i < kernel.Length; i++)
                {
                    Vector3 v = new(
                        r.NextSingle() * 2 - 1,
                        r.NextSingle() * 2 - 1,
                        r.NextSingle() * 2 - 1
                    );

                    v.Normalize();
                    v *= MathF.Pow((float)i / kernel.Length, 2) * 0.9f + 0.1f;
                    kernel[i] = v;
                    Debug.Write("float3(" + v.X.ToString(CultureInfo.InvariantCulture) + "f, "
                                    + v.Y.ToString(CultureInfo.InvariantCulture) + "f, "
                                    + v.Z.ToString(CultureInfo.InvariantCulture) + "f),");
                    if(i % 4 == 0) Debug.WriteLine("");
                }
                ScreenManager.CurrentScreen = new EditorScreen.EditorScreen();
                game.Run();
            }
        }
    }
}