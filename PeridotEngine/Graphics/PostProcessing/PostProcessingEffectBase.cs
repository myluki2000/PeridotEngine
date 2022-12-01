using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace PeridotEngine.Graphics.PostProcessing
{
    public class PostProcessingEffectBase : Effect
    {
        protected PostProcessingEffectBase(Effect cloneSource) : base(cloneSource)
        {
        }
    }
}
