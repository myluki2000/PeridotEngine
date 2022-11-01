namespace PeridotWindows.EditorScreen.Forms
{
    partial class ResourcesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpTextures = new System.Windows.Forms.TabPage();
            this.lvTextures = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnAddTexture = new System.Windows.Forms.Button();
            this.tpModels = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddModel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpTextures.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tpModels.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpTextures);
            this.tabControl1.Controls.Add(this.tpModels);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(911, 227);
            this.tabControl1.TabIndex = 0;
            // 
            // tpTextures
            // 
            this.tpTextures.Controls.Add(this.lvTextures);
            this.tpTextures.Controls.Add(this.panel1);
            this.tpTextures.Location = new System.Drawing.Point(4, 24);
            this.tpTextures.Name = "tpTextures";
            this.tpTextures.Padding = new System.Windows.Forms.Padding(3);
            this.tpTextures.Size = new System.Drawing.Size(903, 199);
            this.tpTextures.TabIndex = 0;
            this.tpTextures.Text = "Textures";
            this.tpTextures.UseVisualStyleBackColor = true;
            // 
            // lvTextures
            // 
            this.lvTextures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTextures.Location = new System.Drawing.Point(3, 3);
            this.lvTextures.Name = "lvTextures";
            this.lvTextures.Size = new System.Drawing.Size(830, 193);
            this.lvTextures.TabIndex = 0;
            this.lvTextures.UseCompatibleStateImageBehavior = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnAddTexture);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(833, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(67, 193);
            this.panel1.TabIndex = 1;
            // 
            // btnAddTexture
            // 
            this.btnAddTexture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddTexture.Location = new System.Drawing.Point(3, 3);
            this.btnAddTexture.Name = "btnAddTexture";
            this.btnAddTexture.Size = new System.Drawing.Size(61, 42);
            this.btnAddTexture.TabIndex = 0;
            this.btnAddTexture.Text = "Add Texture";
            this.btnAddTexture.UseVisualStyleBackColor = true;
            this.btnAddTexture.Click += new System.EventHandler(this.btnAddTexture_Click);
            // 
            // tpModels
            // 
            this.tpModels.Controls.Add(this.panel2);
            this.tpModels.Location = new System.Drawing.Point(4, 24);
            this.tpModels.Name = "tpModels";
            this.tpModels.Padding = new System.Windows.Forms.Padding(3);
            this.tpModels.Size = new System.Drawing.Size(903, 199);
            this.tpModels.TabIndex = 1;
            this.tpModels.Text = "Models";
            this.tpModels.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAddModel);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(833, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(67, 193);
            this.panel2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(0, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add Texture";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnAddModel
            // 
            this.btnAddModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddModel.Location = new System.Drawing.Point(3, 3);
            this.btnAddModel.Name = "btnAddModel";
            this.btnAddModel.Size = new System.Drawing.Size(61, 42);
            this.btnAddModel.TabIndex = 1;
            this.btnAddModel.Text = "Add Model";
            this.btnAddModel.UseVisualStyleBackColor = true;
            this.btnAddModel.Click += new System.EventHandler(this.btnAddModel_Click);
            // 
            // ResourcesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 227);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ResourcesForm";
            this.ShowInTaskbar = false;
            this.Text = "ResourcesForm";
            this.tabControl1.ResumeLayout(false);
            this.tpTextures.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tpModels.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tpTextures;
        private TabPage tpModels;
        private ListView lvTextures;
        private Panel panel1;
        private Button btnAddTexture;
        private Panel panel2;
        private Button btnAddModel;
        private Button button1;
    }
}