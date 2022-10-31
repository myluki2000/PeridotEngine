using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeridotWindows.Graphics.Effects.PropertiesControls;

// ReSharper disable once CheckNamespace
namespace PeridotEngine.Graphics.Effects
{
    public partial class SimpleEffect
    {
        public partial class SimpleEffectProperties
        {
            public override UserControl PropertiesControl { get; }

            public SimpleEffectProperties(SimpleEffect effect)
            {
                this.effect = effect;

                PropertiesControl = new SimpleEffectControl(this);
            }
        }
    }
}
