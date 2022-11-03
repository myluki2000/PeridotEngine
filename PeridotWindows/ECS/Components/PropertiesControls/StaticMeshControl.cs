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
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Misc;
using PeridotEngine.Scenes;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components.PropertiesControls
{
    public partial class StaticMeshControl : UserControl
    {
        private readonly StaticMeshComponent component;

        public StaticMeshControl(StaticMeshComponent component)
        {
            InitializeComponent();

            this.component = component;

            component.Scene.Resources.MeshResources.MeshListChanged += (_, _) => UpdateMeshList();

            UpdateMeshList();

            foreach (Type effectType in EffectPool.GetRegisteredEffectTypes())
            {
                cmbEffect.Items.Add(effectType);

                if (effectType == component.EffectProperties.Effect.GetType()) cmbEffect.SelectedItem = effectType;
            }

            cmbEffect.SelectedIndexChanged += cmbEffect_SelectedIndexChanged;
        }

        private void UpdateMeshList()
        {
            cmbMesh.Items.Clear();

            foreach (MeshResources.MeshInfo meshInfo in component.Scene.Resources.MeshResources.GetAllMeshes())
            {
                cmbMesh.Items.Add(meshInfo);

                if (meshInfo == component.Mesh) cmbMesh.SelectedItem = meshInfo;
            }
        }

        private void cmbMesh_SelectedIndexChanged(object sender, EventArgs e)
        {
            component.Mesh = (MeshResources.MeshInfo)cmbMesh.SelectedItem;
        }

        private void cmbEffect_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Type effectType = (Type)cmbEffect.SelectedItem;
            component.EffectProperties = component.Scene.Resources.EffectPool.Effect(effectType).CreatePropertiesBase();

            gbEffectProperties.Controls.Clear();
            gbEffectProperties.Controls.Add(component.EffectProperties.PropertiesControl);
            component.EffectProperties.PropertiesControl.Location = new Point(5, 15);
        }
    }
}
