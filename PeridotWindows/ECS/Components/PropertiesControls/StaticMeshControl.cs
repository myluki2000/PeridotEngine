using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Misc;
using PeridotEngine.Scenes;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components.PropertiesControls
{
    public partial class StaticMeshControl : ComponentControlBase
    {
        private readonly StaticMeshComponent component;
        public override ComponentBase Component => component;

        public StaticMeshControl(Archetype.Entity entity) : base(entity)
        {
            InitializeComponent();

            this.component = entity.GetComponent<StaticMeshComponent>();

            component.Scene.Resources.MeshResources.MeshListChanged.AddWeakHandler(MeshResourcesOnMeshListChanged);
            EffectPool.RegisteredEffectTypesChanged.AddWeakHandler(EffectPoolOnRegisteredEffectTypesChanged);
            component.ValuesChanged.AddWeakHandler(ComponentOnValuesChanged);

            EffectPoolOnRegisteredEffectTypesChanged(null, EventArgs.Empty);
            MeshResourcesOnMeshListChanged(null, []);
            ComponentOnValuesChanged(null, component);

            cmbEffect.SelectedIndexChanged += cmbEffect_SelectedIndexChanged;
        }

        private void ComponentOnValuesChanged(object? sender, ComponentBase c)
        {
            cbCastShadows.Checked = component.CastShadows;
        }

        private void EffectPoolOnRegisteredEffectTypesChanged(object? sender, EventArgs e)
        {
            component.ValuesChanged.RemoveHandler(ComponentOnValuesChanged);
            cmbEffect.Items.Clear();

            foreach (Type effectType in EffectPool.GetRegisteredEffectTypes())
            {
                cmbEffect.Items.Add(effectType);

                if (effectType == component.EffectProperties?.Effect.GetType())
                {
                    cmbEffect.SelectedItem = effectType;
                    PopulateEffectProperties();
                }
            }
            component.ValuesChanged.AddWeakHandler(ComponentOnValuesChanged);
        }

        private void MeshResourcesOnMeshListChanged(object? sender, IEnumerable<MeshResources.MeshInfo> _)
        {
            component.ValuesChanged.RemoveHandler(ComponentOnValuesChanged);
            cmbMesh.Items.Clear();

            foreach (MeshResources.MeshInfo meshInfo in component.Scene.Resources.MeshResources.GetAllMeshes())
            {
                cmbMesh.Items.Add(meshInfo);

                if (meshInfo == component.Mesh) cmbMesh.SelectedItem = meshInfo;
            }
            component.ValuesChanged.AddWeakHandler(ComponentOnValuesChanged);
        }

        private void cmbMesh_SelectedIndexChanged(object sender, EventArgs e)
        {
            component.Mesh = (MeshResources.MeshInfo)cmbMesh.SelectedItem;
        }

        private void cmbEffect_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Type effectType = (Type)cmbEffect.SelectedItem;
            component.EffectProperties = component.Scene.Resources.EffectPool.Effect(effectType).CreatePropertiesBase();

            PopulateEffectProperties();
        }

        private void PopulateEffectProperties()
        {
            gbEffectProperties.Controls.Clear();

            gbEffectProperties.Controls.Add(component.EffectProperties.PropertiesControl);
            component.EffectProperties.PropertiesControl.Location = new Point(5, 15);
        }

        private void cbCastShadows_CheckedChanged(object sender, EventArgs e)
        {
            component.CastShadows = cbCastShadows.Checked;
        }
    }
}
