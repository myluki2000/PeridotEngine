namespace PeridotWindows.Graphics.Effects.PropertiesControls
{
    partial class SimpleEffectControl
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
            this.cbTexture = new System.Windows.Forms.CheckBox();
            this.pnlColor = new System.Windows.Forms.Panel();
            this.btnPickColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTextureId = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTextureId)).BeginInit();
            this.SuspendLayout();
            // 
            // cbTexture
            // 
            this.cbTexture.AutoSize = true;
            this.cbTexture.Location = new System.Drawing.Point(6, 34);
            this.cbTexture.Name = "cbTexture";
            this.cbTexture.Size = new System.Drawing.Size(64, 19);
            this.cbTexture.TabIndex = 13;
            this.cbTexture.Text = "Texture";
            this.cbTexture.UseVisualStyleBackColor = true;
            this.cbTexture.CheckedChanged += new System.EventHandler(this.cbTexture_CheckedChanged);
            // 
            // pnlColor
            // 
            this.pnlColor.Location = new System.Drawing.Point(74, 3);
            this.pnlColor.Name = "pnlColor";
            this.pnlColor.Size = new System.Drawing.Size(34, 23);
            this.pnlColor.TabIndex = 12;
            // 
            // btnPickColor
            // 
            this.btnPickColor.Location = new System.Drawing.Point(114, 3);
            this.btnPickColor.Name = "btnPickColor";
            this.btnPickColor.Size = new System.Drawing.Size(74, 23);
            this.btnPickColor.TabIndex = 11;
            this.btnPickColor.Text = "Pick Color";
            this.btnPickColor.UseVisualStyleBackColor = true;
            this.btnPickColor.Click += new System.EventHandler(this.btnPickColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Mix Color:";
            // 
            // nudTextureId
            // 
            this.nudTextureId.Location = new System.Drawing.Point(142, 32);
            this.nudTextureId.Name = "nudTextureId";
            this.nudTextureId.Size = new System.Drawing.Size(63, 23);
            this.nudTextureId.TabIndex = 15;
            this.nudTextureId.ValueChanged += new System.EventHandler(this.nudTextureId_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Texture ID:";
            // 
            // SimpleEffectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudTextureId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTexture);
            this.Controls.Add(this.pnlColor);
            this.Controls.Add(this.btnPickColor);
            this.Name = "SimpleEffectControl";
            this.Size = new System.Drawing.Size(394, 61);
            ((System.ComponentModel.ISupportInitialize)(this.nudTextureId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox cbTexture;
        private Panel pnlColor;
        private Button btnPickColor;
        private Label label1;
        private NumericUpDown nudTextureId;
        private Label label2;
    }
}
