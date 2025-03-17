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
        public override ComponentBase Component => component;

        public PositionRotationScaleControl(Archetype.Entity entity) : base(entity)
        {
            InitializeComponent();

            this.component = entity.GetComponent<PositionRotationScaleComponent>();
        }

        private void NudPosition_ValueChanged(object? sender, EventArgs eventArgs)
        {
            component.Position = new Vector3((float)nudPositionX.Value,
                                             (float)nudPositionY.Value,
                                             (float)nudPositionZ.Value);
        }

        private void NudRotation_ValueChanged(object? sender, EventArgs eventArgs)
        {
            component.Rotation = new Quaternion((float)nudRotationX.Value,
                                             (float)nudRotationY.Value,
                                             (float)nudRotationZ.Value,
                                             (float)nudRotationW.Value);
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
            PopulateCbParent();
            ComponentOnValuesChanged(null, component);
        }

        private void PopulateCbParent()
        {
            cbParent.SelectedIndexChanged -= CbParent_SelectedIndexChanged;

            cbParent.Items.Clear();
            cbParent.Items.Add("<None>");
            using (EntityQuery query = component.Scene.Ecs.Query().Has<PositionRotationScaleComponent>().OnEntity())
            {
                query.ForEach(entity =>
                    {
                        // skip adding the entity to the combobox if it is the entity the component is a part of
                        if (entity.Id == Entity.Id)
                            return;

                        cbParent.Items.Add(entity);
                    });
            }

            cbParent.SelectedIndexChanged += CbParent_SelectedIndexChanged;

            // update component ui values so that the correct entity is selected again in cbParent
            ComponentOnValuesChanged(null, component);
        }

        private void CbParent_SelectedIndexChanged(object? sender, EventArgs e)
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

        private void ComponentOnValuesChanged(object? sender, ComponentBase _)
        {
            nudPositionX.ValueChanged -= NudPosition_ValueChanged;
            nudPositionY.ValueChanged -= NudPosition_ValueChanged;
            nudPositionZ.ValueChanged -= NudPosition_ValueChanged;

            nudRotationX.ValueChanged -= NudRotation_ValueChanged;
            nudRotationY.ValueChanged -= NudRotation_ValueChanged;
            nudRotationZ.ValueChanged -= NudRotation_ValueChanged;
            nudRotationW.ValueChanged -= NudRotation_ValueChanged;

            nudScaleX.ValueChanged -= NudScale_ValueChanged;
            nudScaleY.ValueChanged -= NudScale_ValueChanged;
            nudScaleZ.ValueChanged -= NudScale_ValueChanged;

            cbParent.SelectedIndexChanged -= CbParent_SelectedIndexChanged;

            nudPositionX.Value = (decimal)component.Position.X;
            nudPositionY.Value = (decimal)component.Position.Y;
            nudPositionZ.Value = (decimal)component.Position.Z;

            nudRotationX.Value = (decimal)component.Rotation.X;
            nudRotationY.Value = (decimal)component.Rotation.Y;
            nudRotationZ.Value = (decimal)component.Rotation.Z;
            nudRotationW.Value = (decimal)component.Rotation.W;

            nudScaleX.Value = (decimal)component.Scale.X;
            nudScaleY.Value = (decimal)component.Scale.Y;
            nudScaleZ.Value = (decimal)component.Scale.Z;

            if (!component.HasParent)
            {
                cbParent.SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < cbParent.Items.Count; i++)
                {
                    object? item = cbParent.Items[i];

                    if (item is not Archetype.Entity entity) continue;

                    if (entity.Id != component.ParentEntityId) continue;

                    cbParent.SelectedIndex = i;
                    break;
                }
            }
            
            nudPositionX.ValueChanged += NudPosition_ValueChanged;
            nudPositionY.ValueChanged += NudPosition_ValueChanged;
            nudPositionZ.ValueChanged += NudPosition_ValueChanged;

            nudRotationX.ValueChanged += NudRotation_ValueChanged;
            nudRotationY.ValueChanged += NudRotation_ValueChanged;
            nudRotationZ.ValueChanged += NudRotation_ValueChanged;
            nudRotationW.ValueChanged += NudRotation_ValueChanged;

            nudScaleX.ValueChanged += NudScale_ValueChanged;
            nudScaleY.ValueChanged += NudScale_ValueChanged;
            nudScaleZ.ValueChanged += NudScale_ValueChanged;

            cbParent.SelectedIndexChanged += CbParent_SelectedIndexChanged;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            component.ValuesChanged += ComponentOnValuesChanged;
            component.Scene.Ecs.EntityListChanged += EcsOnEntityListChanged;
        }

        private void EcsOnEntityListChanged(object? sender, Archetype e)
        {
            PopulateCbParent();
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);

            component.ValuesChanged -= ComponentOnValuesChanged;
            component.Scene.Ecs.EntityListChanged -= EcsOnEntityListChanged;
        }
    }
}
