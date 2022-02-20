using System;

namespace PeridotWindows
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new PeridotEngine.Main())
                game.Run();
        }
    }
}
