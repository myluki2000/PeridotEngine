using Newtonsoft.Json.Linq;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.EditorScreen.Forms;

namespace PeridotWindows
{
    internal static class Program
    {
        private static string[] args;
        private static EditorForm form;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Program.args = args;
            form = new();
            
            if (args.Length > 0)
            {
                form.Engine.Initialized += EngineInitialized;
            }
            Application.Run(form);
        }

        private static void EngineInitialized(object? sender, EventArgs e)
        {
            Scene3D scene = Scene3D.FromJson(JToken.Parse(File.ReadAllText(args[0])));
            EditorScreen.EditorScreen editor = new(form, scene);
            form.Editor = editor;
            form.Engine.Initialized -= EngineInitialized;
        }
    }
}