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
            this.titleBar.Size = new System.Drawing.Size(550, 28);
            this.titleBar.TabIndex = 3;
            // 
            // StaticMeshControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.titleBar);
            this.Name = "StaticMeshControl";
            this.Size = new System.Drawing.Size(550, 177);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CollapsibleTitleBar titleBar;
    }
}
