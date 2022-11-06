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
            this.titleBar = new PeridotWindows.Controls.CollapsibleTitleBar();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMesh = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbEffect = new System.Windows.Forms.ComboBox();
            this.gbEffectProperties = new System.Windows.Forms.GroupBox();
            this.cbCastShadows = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.White;
            this.titleBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleBar.Collapsed = false;
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.MaximumSize = new System.Drawing.Size(9999, 28);
            this.titleBar.MinimumSize = new System.Drawing.Size(0, 28);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(365, 28);
            this.titleBar.TabIndex = 3;
            this.titleBar.Text = "Static Mesh";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mesh:";
            // 
            // cmbMesh
            // 
            this.cmbMesh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMesh.FormattingEnabled = true;
            this.cmbMesh.Location = new System.Drawing.Point(49, 34);
            this.cmbMesh.Name = "cmbMesh";
            this.cmbMesh.Size = new System.Drawing.Size(313, 23);
            this.cmbMesh.TabIndex = 5;
            this.cmbMesh.SelectedIndexChanged += new System.EventHandler(this.cmbMesh_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Effect:";
            // 
            // cmbEffect
            // 
            this.cmbEffect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEffect.FormattingEnabled = true;
            this.cmbEffect.Location = new System.Drawing.Point(49, 63);
            this.cmbEffect.Name = "cmbEffect";
            this.cmbEffect.Size = new System.Drawing.Size(313, 23);
            this.cmbEffect.TabIndex = 11;
            // 
            // gbEffectProperties
            // 
            this.gbEffectProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEffectProperties.AutoSize = true;
            this.gbEffectProperties.Location = new System.Drawing.Point(3, 117);
            this.gbEffectProperties.Name = "gbEffectProperties";
            this.gbEffectProperties.Size = new System.Drawing.Size(358, 55);
            this.gbEffectProperties.TabIndex = 12;
            this.gbEffectProperties.TabStop = false;
            this.gbEffectProperties.Text = "Effect Properties";
            // 
            // cbCastShadows
            // 
            this.cbCastShadows.AutoSize = true;
            this.cbCastShadows.Location = new System.Drawing.Point(4, 92);
            this.cbCastShadows.Name = "cbCastShadows";
            this.cbCastShadows.Size = new System.Drawing.Size(99, 19);
            this.cbCastShadows.TabIndex = 13;
            this.cbCastShadows.Text = "Cast Shadows";
            this.cbCastShadows.UseVisualStyleBackColor = true;
            this.cbCastShadows.CheckedChanged += new System.EventHandler(this.cbCastShadows_CheckedChanged);
            // 
            // StaticMeshControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cbCastShadows);
            this.Controls.Add(this.gbEffectProperties);
            this.Controls.Add(this.cmbEffect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMesh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titleBar);
            this.Name = "StaticMeshControl";
            this.Size = new System.Drawing.Size(365, 175);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CollapsibleTitleBar titleBar;
        private Label label1;
        private ComboBox cmbMesh;
        private Label label2;
        private ComboBox cmbEffect;
        private GroupBox gbEffectProperties;
        private CheckBox cbCastShadows;
    }
}
