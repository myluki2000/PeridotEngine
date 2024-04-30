using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using PeridotEngine.ECS.Components;

namespace PeridotWindows.ECS.Components.PropertiesControls
{
    public partial class PositionRotationScaleControl : ComponentControlBase
    {
        private readonly PositionRotationScaleComponent component;

        public PositionRotationScaleControl(Archetype.Entity entity) : base(entity)
        {
            InitializeComponent();

            this.component = entity.GetComponent<PositionRotationScaleComponent>();
            titleBar.Tag = component;
        }

        private void NudPosition_ValueChanged(object? sender, EventArgs eventArgs)
        {
            component.Position = new Vector3((float)nudPositionX.Value,
                                             (float)nudPositionY.Value,
                                             (float)nudPositionZ.Value);
        }

        private void NudRotation_ValueChanged(object? sender, EventArgs eventArgs)
        {
            component.Rotation = new Vector3((float)nudRotationX.Value,
                                             (float)nudRotationY.Value,
                                             (float)nudRotationZ.Value);
        }

        private void NudScale_ValueChanged(object? sender, EventArgs eventArgs)
        {
            component.Scale = new Vector3((float)nudScaleX.Value,
                                          (float)nudScaleY.Value,
                                          (float)nudScaleZ.Value);
        }

        private void PositionRotationScaleControl_ClientSizeChanged(object sender, EventArgs e)
        {
            flpPosition.MaximumSize = new Size(ClientSize.Width, flpPosition.MaximumSize.Height);
            flpRotation.MaximumSize = new Size(ClientSize.Width, flpRotation.MaximumSize.Height);
            flpScale.MaximumSize = new Size(ClientSize.Width, flpScale.MaximumSize.Height);
        }

        private void PositionRotationScaleControl_Load(object sender, EventArgs e)
        {
            nudPositionX.Value = (decimal)component.Position.X;
            nudPositionY.Value = (decimal)component.Position.Y;
            nudPositionZ.Value = (decimal)component.Position.Z;

            nudRotationX.Value = (decimal)component.Rotation.X;
            nudRotationY.Value = (decimal)component.Rotation.Y;
            nudRotationZ.Value = (decimal)component.Rotation.Z;

            nudScaleX.Value = (decimal)component.Scale.X;
            nudScaleY.Value = (decimal)component.Scale.Y;
            nudScaleZ.Value = (decimal)component.Scale.Z;

            int noneItemIndex = cbParent.Items.Add("<None>");
            component.Scene.Ecs.Query().Has<PositionRotationScaleComponent>().ForEach(
                (Archetype.Entity entity) =>
                {
                    // skip adding the entity to the combobox if it is the entity the component is a part of
                    if (entity.Id == Entity.Id)
                        return;

                    cbParent.Items.Add(entity);
                });
            cbParent.SelectedIndex = noneItemIndex;

            nudPositionX.ValueChanged += NudPosition_ValueChanged;
            nudPositionY.ValueChanged += NudPosition_ValueChanged;
            nudPositionZ.ValueChanged += NudPosition_ValueChanged;

            nudRotationX.ValueChanged += NudRotation_ValueChanged;
            nudRotationY.ValueChanged += NudRotation_ValueChanged;
            nudRotationZ.ValueChanged += NudRotation_ValueChanged;

            nudScaleX.ValueChanged += NudScale_ValueChanged;
            nudScaleY.ValueChanged += NudScale_ValueChanged;
            nudScaleZ.ValueChanged += NudScale_ValueChanged;
        }

        private void cbParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint? parentId = (cbParent.SelectedItem as Archetype.Entity)?.Id;

            while (parentId != null)
            {
                if (parentId == Entity.Id)
                {
                    MessageBox.Show("Cannot reference parent entities in a loop!");
                    return;
                }

                parentId = component.Scene.Ecs.EntityById(parentId.Value)?.GetComponent<PositionRotationScaleComponent>().ParentEntityId;
            }

            component.ParentEntityId = (cbParent.SelectedItem as Archetype.Entity)?.Id;
        }

        public override ContextMenuStrip? OptionsMenu
        {
            get => titleBar.OptionsMenu;
            set => titleBar.OptionsMenu = value;
        }
    }
}
