using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using PeridotEngine.Misc;

namespace PeridotWindows.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    internal class ToolStripNumericUpDown : ToolStripControlHost
    {
        public ToolStripNumericUpDown()
            : base(new NumericUpDown())
        {

        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged += OnValueChanged;
        }

        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
            ((NumericUpDown)control).ValueChanged -= OnValueChanged;
        }

        public Event<EventArgs> ValueChanged { get; } = new();

        public NumericUpDown NumericUpDownControl => (Control as NumericUpDown)!;

        public void OnValueChanged(object sender, EventArgs e)
        {
            ValueChanged.Invoke(this, e);
        }
    }
}
