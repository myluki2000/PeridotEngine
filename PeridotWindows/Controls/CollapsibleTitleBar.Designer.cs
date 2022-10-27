namespace PeridotWindows.Controls
{
    partial class CollapsibleTitleBar
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
            this.btnCollapse = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCollapse
            // 
            this.btnCollapse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCollapse.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCollapse.Location = new System.Drawing.Point(3, 3);
            this.btnCollapse.Name = "btnCollapse";
            this.btnCollapse.Size = new System.Drawing.Size(20, 20);
            this.btnCollapse.TabIndex = 0;
            this.btnCollapse.Text = "▲";
            this.btnCollapse.UseVisualStyleBackColor = true;
            this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.Location = new System.Drawing.Point(29, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(40, 15);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "label1";
            // 
            // CollapsibleTitleBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnCollapse);
            this.MaximumSize = new System.Drawing.Size(9999, 28);
            this.MinimumSize = new System.Drawing.Size(0, 28);
            this.Name = "CollapsibleTitleBar";
            this.Size = new System.Drawing.Size(694, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnCollapse;
        private Label lblTitle;
    }
}
