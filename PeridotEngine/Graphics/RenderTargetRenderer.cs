using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Effects;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics
{
    public static class RenderTargetRenderer
    {
        private static VertexBuffer vbf;
        private static BasicEffect effect;

        static RenderTargetRenderer()
        {
            effect = new BasicEffect(Globals.Graphics.GraphicsDevice);

            vbf = new(Globals.Graphics.GraphicsDevice, typeof(VertexPositionTexture), 6, BufferUsage.WriteOnly);

            vbf.SetData(new VertexPositionTexture[]
            {
                new(new Vector3( 1,  1, 1), new Vector2(1, 0)),
                new(new Vector3( 1, -1, 1), new Vector2(1, 1)),
                new(new Vector3(-1, -1, 1), new Vector2(0, 1)),

                new(new Vector3(-1,  1, 1), new Vector2(0, 0)),
                new(new Vector3( 1,  1, 1), new Vector2(1, 0)),
                new(new Vector3(-1, -1, 1), new Vector2(0, 1)),
            });
        }

        public static void RenderRenderTarget(RenderTarget2D rt)
        {
            effect.Texture = rt;
            effect.TextureEnabled = true;

            Globals.Graphics.GraphicsDevice.SetVertexBuffer(vbf);

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Globals.Graphics.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }
        }
    }
}
