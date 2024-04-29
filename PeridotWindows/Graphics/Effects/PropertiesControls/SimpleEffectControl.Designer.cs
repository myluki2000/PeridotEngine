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
            cbTexture = new CheckBox();
            pnlColor = new Panel();
            btnPickColor = new Button();
            label1 = new Label();
            nudTextureId = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            nudTextureRepeatX = new NumericUpDown();
            nudTextureRepeatY = new NumericUpDown();
            label4 = new Label();
            cbReceiveShadows = new CheckBox();
            cbRandomTextureRotation = new CheckBox();
            btnPickTexture = new Button();
            ((System.ComponentModel.ISupportInitialize)nudTextureId).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTextureRepeatX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTextureRepeatY).BeginInit();
            SuspendLayout();
            // 
            // cbTexture
            // 
            cbTexture.AutoSize = true;
            cbTexture.Location = new Point(6, 34);
            cbTexture.Name = "cbTexture";
            cbTexture.Size = new Size(64, 19);
            cbTexture.TabIndex = 13;
            cbTexture.Text = "Texture";
            cbTexture.UseVisualStyleBackColor = true;
            cbTexture.CheckedChanged += cbTexture_CheckedChanged;
            // 
            // pnlColor
            // 
            pnlColor.Location = new Point(74, 3);
            pnlColor.Name = "pnlColor";
            pnlColor.Size = new Size(34, 23);
            pnlColor.TabIndex = 12;
            // 
            // btnPickColor
            // 
            btnPickColor.Location = new Point(114, 3);
            btnPickColor.Name = "btnPickColor";
            btnPickColor.Size = new Size(74, 23);
            btnPickColor.TabIndex = 11;
            btnPickColor.Text = "Pick Color";
            btnPickColor.UseVisualStyleBackColor = true;
            btnPickColor.Click += btnPickColor_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 7);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 14;
            label1.Text = "Mix Color:";
            // 
            // nudTextureId
            // 
            nudTextureId.Location = new Point(142, 32);
            nudTextureId.Name = "nudTextureId";
            nudTextureId.Size = new Size(63, 23);
            nudTextureId.TabIndex = 15;
            nudTextureId.ValueChanged += nudTextureId_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(74, 35);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 16;
            label2.Text = "Texture ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 63);
            label3.Name = "label3";
            label3.Size = new Size(97, 15);
            label3.TabIndex = 17;
            label3.Text = "Texture Repeat X:";
            // 
            // nudTextureRepeatX
            // 
            nudTextureRepeatX.Location = new Point(142, 61);
            nudTextureRepeatX.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudTextureRepeatX.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTextureRepeatX.Name = "nudTextureRepeatX";
            nudTextureRepeatX.Size = new Size(63, 23);
            nudTextureRepeatX.TabIndex = 18;
            nudTextureRepeatX.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudTextureRepeatX.ValueChanged += nudTextureRepeatX_ValueChanged;
            // 
            // nudTextureRepeatY
            // 
            nudTextureRepeatY.Location = new Point(142, 90);
            nudTextureRepeatY.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudTextureRepeatY.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTextureRepeatY.Name = "nudTextureRepeatY";
            nudTextureRepeatY.Size = new Size(63, 23);
            nudTextureRepeatY.TabIndex = 19;
            nudTextureRepeatY.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudTextureRepeatY.ValueChanged += nudTextureRepeatY_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(39, 92);
            label4.Name = "label4";
            label4.Size = new Size(97, 15);
            label4.TabIndex = 20;
            label4.Text = "Texture Repeat Y:";
            // 
            // cbReceiveShadows
            // 
            cbReceiveShadows.AutoSize = true;
            cbReceiveShadows.Location = new Point(6, 119);
            cbReceiveShadows.Name = "cbReceiveShadows";
            cbReceiveShadows.Size = new Size(116, 19);
            cbReceiveShadows.TabIndex = 21;
            cbReceiveShadows.Text = "Receive Shadows";
            cbReceiveShadows.UseVisualStyleBackColor = true;
            cbReceiveShadows.CheckedChanged += cbReceiveShadows_CheckedChanged;
            // 
            // cbRandomTextureRotation
            // 
            cbRandomTextureRotation.AutoSize = true;
            cbRandomTextureRotation.Location = new Point(6, 144);
            cbRandomTextureRotation.Name = "cbRandomTextureRotation";
            cbRandomTextureRotation.Size = new Size(160, 19);
            cbRandomTextureRotation.TabIndex = 22;
            cbRandomTextureRotation.Text = "Random Texture Rotation";
            cbRandomTextureRotation.UseVisualStyleBackColor = true;
            cbRandomTextureRotation.CheckedChanged += cbRandomTextureRotation_CheckedChanged;
            // 
            // btnPickTexture
            // 
            btnPickTexture.Location = new Point(211, 32);
            btnPickTexture.Name = "btnPickTexture";
            btnPickTexture.Size = new Size(40, 23);
            btnPickTexture.TabIndex = 23;
            btnPickTexture.Text = "Pick";
            btnPickTexture.UseVisualStyleBackColor = true;
            btnPickTexture.Click += btnPickTexture_Click;
            // 
            // SimpleEffectControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnPickTexture);
            Controls.Add(cbRandomTextureRotation);
            Controls.Add(cbReceiveShadows);
            Controls.Add(label4);
            Controls.Add(nudTextureRepeatY);
            Controls.Add(nudTextureRepeatX);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(nudTextureId);
            Controls.Add(label1);
            Controls.Add(cbTexture);
            Controls.Add(pnlColor);
            Controls.Add(btnPickColor);
            Name = "SimpleEffectControl";
            Size = new Size(394, 193);
            ((System.ComponentModel.ISupportInitialize)nudTextureId).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTextureRepeatX).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTextureRepeatY).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox cbTexture;
        private Panel pnlColor;
        private Button btnPickColor;
        private Label label1;
        private NumericUpDown nudTextureId;
        private Label label2;
        private Label label3;
        private NumericUpDown nudTextureRepeatX;
        private NumericUpDown nudTextureRepeatY;
        private Label label4;
        private CheckBox cbReceiveShadows;
        private CheckBox cbRandomTextureRotation;
        private Button btnPickTexture;
    }
}
