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
            btnRemoveTexture = new Button();
            btnAddTexture = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lvTextures
            // 
            lvTextures.Dock = DockStyle.Fill;
            lvTextures.Location = new Point(0, 0);
            lvTextures.Name = "lvTextures";
            lvTextures.Size = new Size(566, 296);
            lvTextures.TabIndex = 2;
            lvTextures.UseCompatibleStateImageBehavior = false;
            lvTextures.SelectedIndexChanged += lvTextures_SelectedIndexChanged;
            lvTextures.DoubleClick += lvTextures_DoubleClick;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnRemoveTexture);
            panel1.Controls.Add(btnAddTexture);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(566, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(67, 296);
            panel1.TabIndex = 3;
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
            Size = new Size(633, 296);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView lvTextures;
        private Panel panel1;
        private Button btnRemoveTexture;
        private Button btnAddTexture;
    }
}
