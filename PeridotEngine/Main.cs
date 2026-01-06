using System.Diagnostics;
using JoltPhysicsSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PeridotEngine;
using PeridotEngine.Graphics;
using PeridotEngine.Misc;

namespace PeridotEngine
{
    public class Main
    {
        

        public Main(ContentManager content, GraphicsDevice graphicsDevice)
        {
            Globals.GameMain = this;
            Globals.GraphicsDevice = graphicsDevice;
            Globals.Content = content;
            Globals.Content.RootDirectory = "Content";

            Foundation.SetTraceHandler(msg => Debug.WriteLine(msg));

#if DEBUG
            Foundation.SetAssertFailureHandler((inExpression, inMessage, inFile, inLine) =>
            {
                string message = inMessage ?? inExpression;

                string outMessage = $"[JoltPhysics] Assertion failure at {inFile}:{inLine}: {message}";

                Debug.WriteLine(outMessage);

                throw new Exception(outMessage);
            });
#endif

            if (!Foundation.Init(false))
            {
                throw new Exception("Physics system init failed!");
            }
        }

        public void Initialize()
        {
        }

        public void Update(GameTime gameTime)
        {
            ScreenManager.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            ScreenManager.Draw(gameTime);
        }
    }
}
