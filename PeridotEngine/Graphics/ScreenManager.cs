using System;
using System.Collections.Generic;
using System.Text;

namespace PeridotEngine.Graphics
{
    internal static class ScreenManager
    {
        private static Screen _currentScreen = null;

        public static Screen CurrentScreen
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
