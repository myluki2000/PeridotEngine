using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using RectangleF = SharpDX.RectangleF;

namespace PeridotEngine.Scenes.Scene3D
{
    public class TextureResources
    {
        [JsonIgnore]
        public Texture2D? TextureAtlas { get; set; }

        [JsonProperty]
        private TextureInfo?[] textures = new TextureInfo[] { };

        [JsonIgnore]
        private RectangleF missingTextureBounds;

        public event EventHandler<IEnumerable<ITextureInfo>>? TextureAtlasChanged;

        public RectangleF GetTextureBoundsInAtlas(int textureId)
        {
            if (textureId >= textures.Length) return missingTextureBounds;

            TextureInfo? tex = textures[textureId];
            return tex?.Bounds ?? missingTextureBounds;
        }

        public ITextureInfo GetTexture(int textureId)
        {
            // if we try to query texture outside of array bounds, resize array and add dummy texture info object
            if (textureId >= textures.Length)
            {
                Array.Resize(ref textures, textureId + 1);
                TextureInfo texInfo = new(textureId, null)
                {
                    Bounds = missingTextureBounds
                };
                textures[textureId] = texInfo;
                return texInfo;
            }

            // if we're inside the array, get the texture info
            TextureInfo? tex = textures[textureId];

            // it it is null, create an new one with the "missing texture" texture
            if (tex == null)
            {
                tex = new TextureInfo(textureId, null)
                {
                    Bounds = missingTextureBounds,
                };
                textures[textureId] = tex;
            }

            return tex;
        }

        public IEnumerable<ITextureInfo> GetAllTextures()
        {
            return textures.Where(x => x != null && x.FilePath != null)!;
        }

        public void AddTexture(string filePath)
        {
            AddTextureWithoutRefreshingAtlas(filePath);

            GenerateAtlas();
        }

        public void AddTextures(IEnumerable<string> filePaths)
        {
            foreach (string filePath in filePaths)
            {
                AddTextureWithoutRefreshingAtlas(filePath);
            }

            GenerateAtlas();
        }

        private void AddTextureWithoutRefreshingAtlas(string filePath)
        {
            TextureInfo? texInfo = null;
            for (int i = 0; i < textures.Length; i++)
            {
                if (textures[i] == null)
                {
                    texInfo = new(i, filePath);
                    textures[i] = texInfo;
                    break;
                }
            }


            if (texInfo != null) return;

            // if we couldn't find a free spot we'll have to increase the size of the array
            int length = textures.Length;
            Array.Resize(ref textures, (length * 2 > 0) ? (length * 2) : 1);
            texInfo = new(length, filePath);
            textures[length] = texInfo;
        }

        public void RemoveTexture(int textureId)
        {
            textures[textureId] = null;
            GenerateAtlas();
        }

        public void ReloadTextures()
        {
            GenerateAtlas();
        }

        public void GenerateAtlas()
        {
            // create 16 bins for 16 different texture heights: 0-2, 3-4, 5-8, 9-16, 17-32, 33-64, and so on
            List<(int id, Bitmap bitmap)>[] bitmaps = new List<(int id, Bitmap bitmap)>[16];
            for (int i = 0; i < bitmaps.Length; i++)
            {
                bitmaps[i] = new List<(int id, Bitmap bitmap)>();
            }

            // add the "missing texture" texture to its bin
            {
                Bitmap bitmap = new Bitmap(Globals.Content.RootDirectory + "/Textures/missing_texture.png");

                // k is chosen such that k is the smallest value possible for which 2^k > bitmap.Height is true 
                int k = (int)Math.Ceiling(Math.Log2(bitmap.Height));

                List<(int id, Bitmap bitmap)> bin = bitmaps[k];
                bin.Add((-1, bitmap));
            }

            // add all textures we have to the different bins
            for (int texId = 0; texId < textures.Length; texId++)
            {
                TextureInfo? texInfo = textures[texId];
                if (texInfo == null) continue;

                Bitmap bitmap = new Bitmap(Globals.Content.RootDirectory + "/" + texInfo.FilePath);

                // k is chosen such that k is the smallest value possible for which 2^k > bitmap.Height is true 
                int k = (int)Math.Ceiling(Math.Log2(bitmap.Height));

                List<(int id, Bitmap bitmap)> bin = bitmaps[k];
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
                    List<(int id, Bitmap bitmap)> bin = bitmaps[i];

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
                List<(int id, Bitmap bitmap)> bin = bitmaps[i];

                if (bin.Count == 0) continue;

                int textureHeight = (int)Math.Pow(2, i);

                int currentX = 0;
                int texIndex = 0;
                while (true)
                {
                    (int texId, Bitmap bitmap) = bin[texIndex];
                    if (currentX + bitmap.Width <= atlasSize)
                    {
                        graphics.DrawImage(bitmap, currentX, currentY, bitmap.Width, bitmap.Height);

                        RectangleF bounds = new(currentX / (float)atlasSize, currentY / (float)atlasSize,
                            bitmap.Width / (float)atlasSize, bitmap.Height / (float)atlasSize);


                        if (texId < 0)
                        {
                            // the "missing texture" texture has id -1
                            missingTextureBounds = bounds;
                        }
                        else
                        {
                            // for regular textures, set their bounds in their texture info object
                            TextureInfo? texInfo = textures[texId];
                            if (texInfo == null) throw new Exception();
                            texInfo.Bounds = bounds;
                        }

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
                atlas.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Seek(0, SeekOrigin.Begin);
                TextureAtlas = Texture2D.FromStream(Globals.Graphics.GraphicsDevice, ms);
            }


            TextureAtlasChanged?.Invoke(this, GetAllTextures());
        }

        public interface ITextureInfo
        {
            public int Id { get; }
            public string? FilePath { get; }
            public RectangleF Bounds { get; }
        }

        private class TextureInfo : ITextureInfo
        {
            public int Id { get; }
            public string? FilePath { get; }
            [JsonIgnore]
            public RectangleF Bounds { get; set; }

            public TextureInfo(int id, string? filePath)
            {
                this.Id = id;
                this.FilePath = filePath;
                this.Bounds = RectangleF.Empty;
            }
        }
    }
}
