using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Scenes.Scene3D
{
    public class TextureResources
    {
        public Texture2D? TextureAtlas { get; set; }

        private TextureInfo?[] textures = new TextureInfo[] { };

        public event EventHandler<IEnumerable<TextureInfo>>? TextureAtlasChanged;

        public RectangleF GetTextureBoundsInAtlas(uint textureId)
        {
            if (textureId >= textures.Length) return RectangleF.Empty;

            TextureInfo? tex = textures[textureId];
            return tex?.Bounds ?? RectangleF.Empty;
        }

        public IEnumerable<TextureInfo> GetAllTextures()
        {
            return textures.Where(x => x != null)!;
        }

        public void AddTexture(string filePath)
        {
            AddTextureWithoutRefreshingAtlas(filePath);

            GenerateAtlas();

            TextureAtlasChanged?.Invoke(this, GetAllTextures());
        }

        public void AddTextures(IEnumerable<string> filePaths)
        {
            foreach (string filePath in filePaths)
            {
                AddTextureWithoutRefreshingAtlas(filePath);
            }

            GenerateAtlas();

            TextureAtlasChanged?.Invoke(this, GetAllTextures());
        }

        private void AddTextureWithoutRefreshingAtlas(string filePath)
        {
            TextureInfo texInfo = new(filePath);
            bool spotFound = false;
            for (int i = 0; i < textures.Length; i++)
            {
                if (textures[i] == null)
                {
                    texInfo.TextureId = (uint)i;
                    textures[i] = texInfo;
                    spotFound = true;
                    break;
                }
            }


            if (spotFound) return;

            // if we couldn't find a free spot we'll have to increase the size of the array
            int length = textures.Length;
            Array.Resize(ref textures, (length * 2 > 0) ? (length * 2) : 1);
            texInfo.TextureId = (uint)length;
            textures[length] = texInfo;
        }

        private void GenerateAtlas()
        {
            List<(uint id, Bitmap bitmap)>[] bitmaps = new List<(uint id, Bitmap bitmap)>[16];
            for (int i = 0; i < bitmaps.Length; i++)
            {
                bitmaps[i] = new List<(uint id, Bitmap bitmap)>();
            }

            for (uint texId = 0; texId < textures.Length; texId++)
            {
                TextureInfo? texInfo = textures[texId];
                if (texInfo == null) continue;

                Bitmap bitmap = new Bitmap(Globals.Content.RootDirectory + "/" + texInfo.FilePath);

                // k is chosen such that k is the smallest value possible for which 2^k > bitmap.Height is true 
                int k = (int)Math.Ceiling(Math.Log2(bitmap.Height));

                List<(uint id, Bitmap bitmap)> bin = bitmaps[k];
                bin.Add((texId, bitmap));
            }


            // calculate the necessary minimum size of the atlas based on the heights of all the bins. This size may increase
            // later on when we try to fit all the bins inside.
            int atlasSize = 0;
            // the width of the largest texture we have
            int largestTextureWidth = 0;
            for (int i = 0; i < bitmaps.Length; i++)
            {
                if (bitmaps[i].Count > 0)
                {
                    atlasSize += (int)Math.Pow(2, i);
                    foreach ((_, Bitmap bitmap) in bitmaps[i])
                    {
                        if (bitmap.Width > largestTextureWidth) largestTextureWidth = bitmap.Width;
                    }
                }
            }

            // round up atlas size to the nearest power of 2
            atlasSize = (int)Math.Pow(2, Math.Ceiling(Math.Log2(atlasSize)));

            // The size of the atlas (both width and height) is determined by the combined height of all texture bins. In some cases a texture might be wider
            // than this calculated atlas size. In that case increase atlas size until the texture fits
            while (largestTextureWidth > atlasSize)
            {
                atlasSize *= 2;
            }

            // we want a square texture in the end, so now we have to check if all the bins fit in the width.
            // If not we will split the overflowing bin into the next "line" and check again if everything fits.
            // If we run out of height space because of splitting up bins we increase texture atlas size by one step
            // and run everything again
            while (true)
            {
                int usedHeight = 0;

                for (int i = 0; i < bitmaps.Length; i++)
                {
                    List<(uint id, Bitmap bitmap)> bin = bitmaps[i];

                    if (bin.Count == 0) continue;

                    int textureHeight = (int)Math.Pow(2, i);

                    // calculate how much height the bin requires based on how many lines we need to split it up into
                    int usedWidth = 0;
                    int lineCount = 1;
                    int texIndex = 0;
                    while (true)
                    {
                        if (usedWidth + bin[texIndex].bitmap.Width <= atlasSize)
                        {
                            usedWidth += bin[texIndex].bitmap.Width;
                            texIndex++;
                        }
                        else
                        {
                            lineCount++;
                            usedWidth = 0;
                        }

                        if (texIndex >= bin.Count) break;
                    }

                    usedHeight += lineCount * textureHeight;
                }

                // if we used less height than the atlas is tall, we managed to fit everything inside and can stop!
                if (usedHeight <= atlasSize) break;

                // otherwise increase atlas size and try again
                atlasSize *= 2;
            }

            // actually draw the textures onto the atlas
            Bitmap atlas = new(atlasSize, atlasSize);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(atlas);

            int currentY = 0;
            for (int i = 0; i < bitmaps.Length; i++)
            {
                List<(uint id, Bitmap bitmap)> bin = bitmaps[i];

                if (bin.Count == 0) continue;

                int textureHeight = (int)Math.Pow(2, i);

                int currentX = 0;
                int texIndex = 0;
                while (true)
                {
                    (uint texId, Bitmap bitmap) = bin[texIndex];
                    if (currentX + bitmap.Width <= atlasSize)
                    {
                        graphics.DrawImage(bitmap, currentX, currentY, bitmap.Width, bitmap.Height);
                        TextureInfo texInfo = textures[texId];
                        if (texInfo == null) throw new Exception();

                        texInfo.Bounds = new RectangleF(currentX / (float)atlasSize, currentY / (float)atlasSize,
                            bitmap.Width / (float)atlasSize, bitmap.Height / (float)atlasSize);
                        currentX += bitmap.Width;
                        texIndex++;
                    }
                    else
                    {
                        currentX = 0;
                        currentY += textureHeight;
                    }

                    if (texIndex >= bin.Count)
                    {
                        currentY += textureHeight;
                        break;
                    }
                }
            }

            using (MemoryStream ms = new())
            {
                atlas.Save(ms, ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                TextureAtlas = Texture2D.FromStream(Globals.Graphics.GraphicsDevice, ms);
            }
        }

        public class TextureInfo
        {
            public string FilePath;
            public RectangleF Bounds;
            public uint TextureId;

            public TextureInfo(string filePath)
            {
                this.FilePath = filePath;
                this.Bounds = RectangleF.Empty;
            }

            public TextureInfo(string filePath, RectangleF bounds)
            {
                this.FilePath = filePath;
                this.Bounds = bounds;
            }
        }
    }
}
