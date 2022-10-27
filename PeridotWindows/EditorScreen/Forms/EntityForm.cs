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
            flpComponents.Controls.Clear();

            if (entity == null) return;

            foreach (IComponent component in entity.Components)
            {
                UserControl control = component.PropertiesControl;
                if(control != null) flpComponents.Controls.Add(control);
            }
        }
    }
}
