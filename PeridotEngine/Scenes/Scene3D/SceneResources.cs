using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using PeridotEngine.Graphics.Effects;
using RectangleF = SharpDX.RectangleF;

namespace PeridotEngine.Scenes.Scene3D
{
    public class SceneResources
    {
        public readonly TextureResources TextureResources;
        public readonly MeshResources MeshResources;
        [JsonIgnore]
        public readonly EffectPool EffectPool;

        public SceneResources(Scene3D scene) : this(new(), new(), new(scene))
        {
        }

        public SceneResources(TextureResources textureResources, MeshResources meshResources, EffectPool effectPool)
        {
            TextureResources = textureResources;
            MeshResources = meshResources;
            EffectPool = effectPool;
        }
    }
}
