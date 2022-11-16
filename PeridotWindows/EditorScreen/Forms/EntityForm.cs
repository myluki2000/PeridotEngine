using PeridotEngine.ECS.Components;
using PeridotWindows.Controls;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class EntityForm : Form
    {
        private EditorScreen editorScreen;

        public EntityForm(EditorScreen editorScreen)
        {
            InitializeComponent();
            this.editorScreen = editorScreen;
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

            NameControl nameControl = new();
            nameControl.Dock = DockStyle.Top;
            pnlComponents.Controls.Add(nameControl);

            nameControl.Text = entity.Name ?? "";
            nameControl.TextChanged += (_, _) =>
            {
                entity.Name = nameControl.Text;
                editorScreen.FrmScene?.Populate();
            };
        }
    }
}
