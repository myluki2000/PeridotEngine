using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Graphics.PostProcessing;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.Graphics
{
    public static class RenderTargetRenderer
    {
        private static VertexBuffer vbf;

        static RenderTargetRenderer()
        {
            vbf = new(Globals.GraphicsDevice, typeof(VertexPositionTexture), 6, BufferUsage.WriteOnly);

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

        public static void RenderRenderTarget(PostProcessingEffectBase effect)
        {
            Globals.GraphicsDevice.SetVertexBuffer(vbf);

            foreach (EffectPass pass in effect.Technique.Passes)
            {
                pass.Apply();
                Globals.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            }
        }
    }
}
