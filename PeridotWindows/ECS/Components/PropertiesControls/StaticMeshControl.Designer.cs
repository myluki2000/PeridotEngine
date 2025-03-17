namespace PeridotWindows.ECS.Components.PropertiesControls
{
    partial class StaticMeshControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            cmbMesh = new ComboBox();
            label2 = new Label();
            cmbEffect = new ComboBox();
            gbEffectProperties = new GroupBox();
            cbCastShadows = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 6);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 4;
            label1.Text = "Mesh:";
            // 
            // cmbMesh
            // 
            cmbMesh.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbMesh.FormattingEnabled = true;
            cmbMesh.Location = new Point(49, 3);
            cmbMesh.Name = "cmbMesh";
            cmbMesh.Size = new Size(313, 23);
            cmbMesh.TabIndex = 5;
            cmbMesh.SelectedIndexChanged += cmbMesh_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 35);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 10;
            label2.Text = "Effect:";
            // 
            // cmbEffect
            // 
            cmbEffect.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbEffect.FormattingEnabled = true;
            cmbEffect.Location = new Point(49, 32);
            cmbEffect.Name = "cmbEffect";
            cmbEffect.Size = new Size(313, 23);
            cmbEffect.TabIndex = 11;
            // 
            // gbEffectProperties
            // 
            gbEffectProperties.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbEffectProperties.AutoSize = true;
            gbEffectProperties.Location = new Point(3, 86);
            gbEffectProperties.Name = "gbEffectProperties";
            gbEffectProperties.Size = new Size(358, 55);
            gbEffectProperties.TabIndex = 12;
            gbEffectProperties.TabStop = false;
            gbEffectProperties.Text = "Effect Properties";
            // 
            // cbCastShadows
            // 
            cbCastShadows.AutoSize = true;
            cbCastShadows.Location = new Point(4, 61);
            cbCastShadows.Name = "cbCastShadows";
            cbCastShadows.Size = new Size(99, 19);
            cbCastShadows.TabIndex = 13;
            cbCastShadows.Text = "Cast Shadows";
            cbCastShadows.UseVisualStyleBackColor = true;
            cbCastShadows.CheckedChanged += cbCastShadows_CheckedChanged;
            // 
            // StaticMeshControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(cbCastShadows);
            Controls.Add(gbEffectProperties);
            Controls.Add(cmbEffect);
            Controls.Add(label2);
            Controls.Add(cmbMesh);
            Controls.Add(label1);
            Name = "StaticMeshControl";
            Size = new Size(365, 147);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private Label label1;
        private ComboBox cmbMesh;
        private Label label2;
        private ComboBox cmbEffect;
        private GroupBox gbEffectProperties;
        private CheckBox cbCastShadows;
    }
}
