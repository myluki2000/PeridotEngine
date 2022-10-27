

using Microsoft.Xna.Framework;

namespace PeridotEngine.Graphics
{
    internal static class ScreenManager
    {
        private static Screens.Screen? _currentScreen = null;

        private static bool screenChanged = false;
        private static Screens.Screen? oldScreen = null;

        public static Screens.Screen? CurrentScreen
        {
            get => _currentScreen;
            set
            {
                screenChanged = true;
                oldScreen = _currentScreen;
                _currentScreen = value;
            }
        }

        public static void Update(GameTime gameTime)
        {
            if (screenChanged)
            {
                screenChanged = false;
                oldScreen?.Deinitialize();
                oldScreen = null;
                _currentScreen?.Initialize();
            }

            _currentScreen?.Update(gameTime);
        }

        public static void Draw(GameTime gameTime)
        {
            _currentScreen?.Draw(gameTime);
        }
    }
}
