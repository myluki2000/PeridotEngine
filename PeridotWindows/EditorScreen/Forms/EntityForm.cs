using System.Reflection;
using PeridotEngine.ECS.Components;
using PeridotWindows.Controls;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotWindows.EditorScreen.Forms
{
    public partial class EntityForm : Form
    {
        private EditorScreen editorScreen;

        public EntityForm(EditorScreen editorScreen)
        {
            InitializeComponent();
            this.editorScreen = editorScreen;

            IEnumerable<Type> componentTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(ComponentBase)));

            foreach (Type componentType in componentTypes)
            {
                ToolStripItem item = cmsAddComponent.Items.Add(componentType.Name);
                item.Tag = componentType;
                item.Click += (_, _) =>
                {
                    entity?.AddComponent(componentType);
                    Populate();
                };
            }
        }

        public Archetype.Entity? Entity
        {
            get => entity;
            set
            {
                entity = value;
                Populate();
            }
        }

        private Archetype.Entity? entity;

        private void Populate()
        {
            pnlComponents.Controls.Clear();

            if (entity == null) return;

            Button btnAddComponent = new Button()
            {
                Text = "Add Component",
                Dock = DockStyle.Top,
            };

            btnAddComponent.Click += (_, _) =>
            {
                foreach (ToolStripItem item in cmsAddComponent.Items)
                {
                    item.Visible = !entity.Components.Select(x => x.GetType()).Contains(item.Tag);
                }

                cmsAddComponent.Show(btnAddComponent, new Point(0, 0), ToolStripDropDownDirection.BelowLeft);
            };

            pnlComponents.Controls.Add(btnAddComponent);

            foreach (ComponentBase component in entity.Components.Reverse())
            {
                UserControl? control = (UserControl?)component.PropertiesControl;
                
                if (control == null) continue;

                if (control is IComponentControl componentControl)
                {
                    componentControl.OptionsMenu = cmsComponentOptions;
                }

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

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            entity?.RemoveComponent(cmsComponentOptions.Tag.GetType());
            Populate();
        }
    }
}
