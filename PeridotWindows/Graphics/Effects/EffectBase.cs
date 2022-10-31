using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeridotEngine.Graphics.Effects
{
    public abstract partial class EffectBase
    {
        public abstract partial class EffectProperties
        {
            public abstract UserControl PropertiesControl { get; }
        }
    }
}
