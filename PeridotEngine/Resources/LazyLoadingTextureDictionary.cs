﻿#nullable enable

using System;
using System.Collections.Generic;
using System.IO;

namespace PeridotEngine.Resources
{
    /// <summary>
    /// A dictionary for textures, which will automatically load a texture if it is not already in the dictionary.
    /// </summary>
    class LazyLoadingTextureDictionary : Dictionary<string, TextureData>
    {
        public new TextureData this[string key]
        {
            get
            {
                if (!base.ContainsKey(key))
                {
                    base.Add(key, TextureManager.LoadTexture(Path.Combine(TextureDirectory, key)));
                }

                if(base.ContainsKey(key))
                {
                    return base[key];
                } else
                {
                    throw new Exception("Error while lazy loading textures.");
                }
            }
        }

        public LazyLoadingTextureDictionary(string textureDirectory)
        {
            this.TextureDirectory = textureDirectory;
        }

        public string TextureDirectory { get; set; }
    }
}