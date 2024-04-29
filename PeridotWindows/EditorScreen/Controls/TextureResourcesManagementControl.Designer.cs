namespace PeridotWindows.EditorScreen.Controls
{
    partial class TextureResourcesManagementControl
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
            lvTextures = new ListView();
            panel1 = new Panel();
            label1 = new Label();
            tbImageSize = new TrackBar();
            btnRemoveTexture = new Button();
            btnAddTexture = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbImageSize).BeginInit();
            SuspendLayout();
            // 
            // lvTextures
            // 
            lvTextures.Dock = DockStyle.Fill;
            lvTextures.Location = new Point(0, 0);
            lvTextures.Name = "lvTextures";
            lvTextures.Size = new Size(566, 255);
            lvTextures.TabIndex = 2;
            lvTextures.UseCompatibleStateImageBehavior = false;
            lvTextures.SelectedIndexChanged += lvTextures_SelectedIndexChanged;
            lvTextures.DoubleClick += lvTextures_DoubleClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(label1);
            panel1.Controls.Add(tbImageSize);
            panel1.Controls.Add(btnRemoveTexture);
            panel1.Controls.Add(btnAddTexture);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(566, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(67, 255);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 105);
            label1.Name = "label1";
            label1.Size = new Size(54, 15);
            label1.TabIndex = 3;
            label1.Text = "Img Size:";
            // 
            // tbImageSize
            // 
            tbImageSize.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            tbImageSize.Location = new Point(12, 123);
            tbImageSize.Maximum = 128;
            tbImageSize.MaximumSize = new Size(0, 130);
            tbImageSize.Minimum = 16;
            tbImageSize.Name = "tbImageSize";
            tbImageSize.Orientation = Orientation.Vertical;
            tbImageSize.Size = new Size(45, 129);
            tbImageSize.TabIndex = 2;
            tbImageSize.Value = 32;
            tbImageSize.Scroll += tbImageSize_Scroll;
            // 
            // btnRemoveTexture
            // 
            btnRemoveTexture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnRemoveTexture.Location = new Point(3, 51);
            btnRemoveTexture.Name = "btnRemoveTexture";
            btnRemoveTexture.Size = new Size(61, 42);
            btnRemoveTexture.TabIndex = 1;
            btnRemoveTexture.Text = "Remove Texture";
            btnRemoveTexture.UseVisualStyleBackColor = true;
            btnRemoveTexture.Click += btnRemoveTexture_Click;
            // 
            // btnAddTexture
            // 
            btnAddTexture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAddTexture.Location = new Point(3, 3);
            btnAddTexture.Name = "btnAddTexture";
            btnAddTexture.Size = new Size(61, 42);
            btnAddTexture.TabIndex = 0;
            btnAddTexture.Text = "Add Texture";
            btnAddTexture.UseVisualStyleBackColor = true;
            btnAddTexture.Click += btnAddTexture_Click;
            // 
            // TextureResourcesManagementControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lvTextures);
            Controls.Add(panel1);
            Name = "TextureResourcesManagementControl";
            Size = new Size(633, 255);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbImageSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListView lvTextures;
        private Panel panel1;
        private Button btnRemoveTexture;
        private Button btnAddTexture;
        private Label label1;
        private TrackBar tbImageSize;
    }
}
