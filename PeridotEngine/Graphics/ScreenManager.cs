

namespace PeridotEngine.Graphics
{
    internal static class ScreenManager
    {
        private static Screens.Screen? _currentScreen = null;

        public static Screens.Screen? CurrentScreen
        {
            get => _currentScreen;
            set
            {
                _currentScreen?.Deinitialize();
                _currentScreen = value;
                _currentScreen?.Initialize();
            }
        }
    }
}
