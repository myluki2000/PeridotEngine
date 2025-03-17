using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeridotEngine.ECS.Components;
using PeridotWindows.Controls;

namespace PeridotWindows.ECS.Components.PropertiesControls
{
    public class ComponentControlWrapper : UserControl
    {
        private readonly CollapsibleTitleBar titleBar = new()
        {
            Dock = DockStyle.Top,
        };

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ContextMenuStrip? OptionsMenu
        {
            get => titleBar.OptionsMenu;
            set => titleBar.OptionsMenu = value;
        }

        public ComponentControlWrapper(ComponentBase component, ComponentControlBase? control)
        {
            AutoSize = true;

            if (control != null)
            {
                control.AutoSize = true;
                control.Dock = DockStyle.Top;
                Controls.Add(control);
            }

            titleBar.Text = component.GetType().Name;
            titleBar.Tag = component;
            Controls.Add(titleBar);
        }
    }
}
