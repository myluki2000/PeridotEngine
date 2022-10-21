using System;
using System.Collections.Generic;
using System.Text;

namespace PeridotEngine.Graphics
{
    internal static class SceneManager
    {
        private static Scene _currentScene;
        public static Scene CurrentScene
        {
            get => _currentScene;
            set
            {
                _currentScene.Deinitialize();
                _currentScene = value;
                _currentScene.Initialize();
            }
        }
    }
}
