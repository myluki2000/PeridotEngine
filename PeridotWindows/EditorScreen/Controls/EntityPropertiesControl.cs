using System.ComponentModel;
using System.Reflection;
using PeridotEngine.ECS.Components;
using PeridotWindows.Controls;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using PeridotWindows.ECS.Components.PropertiesControls;
using PeridotWindows.EditorScreen.Forms;

namespace PeridotWindows.EditorScreen.Controls
{
    public partial class EntityPropertiesControl : UserControl
    {
        private EditorForm frmEditor;

        public EntityPropertiesControl(EditorForm frmEditor)
        {
            InitializeComponent();
            this.frmEditor = frmEditor;

            IEnumerable<Type> componentTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass => ass.GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(ComponentBase))));

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

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Archetype.Entity? Entity
        {
            get => entity;
            set
            {
                entity = value;
                Populate();
                Text = "EntityPropertiesControl - Selected Entity ID: " + entity?.Id;
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
                    Type itemType = item.Tag!.GetType();
                    item.Visible = !entity.Components
                        .Select(x => x.GetType())
                        .Any(x => x == itemType || x.IsSubclassOf(itemType) || itemType.IsSubclassOf(x));
                }

                cmsAddComponent.Show(btnAddComponent, new Point(0, 0), ToolStripDropDownDirection.BelowLeft);
            };

            pnlComponents.Controls.Add(btnAddComponent);

            foreach (ComponentBase component in entity.Components.Reverse())
            {
                UserControl? control = (UserControl?)component.GetPropertiesControlWrapper(entity);
                
                if (control == null) continue;

                if (control is ComponentControlWrapper componentControl)
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
                frmEditor.SceneControl.Populate();
            };
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            entity?.RemoveComponent(cmsComponentOptions.Tag.GetType());
            Populate();
        }
    }
}
