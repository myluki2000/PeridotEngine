using PeridotEngine.ECS.Components;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class EntityForm : Form
    {
        public EntityForm()
        {
            InitializeComponent();
        }

        public Entity? Entity
        {
            get => entity;
            set
            {
                entity = value;
                Populate();
            }
        }

        private Entity? entity;

        private void Populate()
        {
            pnlComponents.Controls.Clear();

            if (entity == null) return;

            foreach (ComponentBase component in entity.Components.Reverse())
            {
                UserControl control = component.PropertiesControl;
                
                if (control == null) continue;

                control.Dock = DockStyle.Top;
                pnlComponents.Controls.Add(control);
            }
        }
    }
}
