using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeridotEngine.Graphics.Effects
{
    public partial class SkydomeEffect
    {
        public partial class SkydomeEffectProperties
        {
            public override UserControl PropertiesControl =>
                throw new NotSupportedException("PropertiesControl is not implemented for SkydomeEffect.");
        }
    }
}
