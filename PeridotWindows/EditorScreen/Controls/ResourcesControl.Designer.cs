﻿namespace PeridotWindows.EditorScreen.Controls
{
    partial class ResourcesControl
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
            tabControl1 = new TabControl();
            tpTextures = new TabPage();
            tpMeshes = new TabPage();
            lvMeshes = new ListView();
            panel2 = new Panel();
            btnAddModel = new Button();
            button1 = new Button();
            tabControl1.SuspendLayout();
            tpMeshes.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tpTextures);
            tabControl1.Controls.Add(tpMeshes);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(911, 227);
            tabControl1.TabIndex = 0;
            // 
            // tpTextures
            // 
            tpTextures.Location = new Point(4, 24);
            tpTextures.Name = "tpTextures";
            tpTextures.Padding = new Padding(3);
            tpTextures.Size = new Size(903, 199);
            tpTextures.TabIndex = 0;
            tpTextures.Text = "Textures";
            tpTextures.UseVisualStyleBackColor = true;
            // 
            // tpMeshes
            // 
            tpMeshes.Controls.Add(lvMeshes);
            tpMeshes.Controls.Add(panel2);
            tpMeshes.Location = new Point(4, 24);
            tpMeshes.Name = "tpMeshes";
            tpMeshes.Padding = new Padding(3);
            tpMeshes.Size = new Size(903, 199);
            tpMeshes.TabIndex = 1;
            tpMeshes.Text = "Models";
            tpMeshes.UseVisualStyleBackColor = true;
            // 
            // lvMeshes
            // 
            lvMeshes.Dock = DockStyle.Fill;
            lvMeshes.Location = new Point(3, 3);
            lvMeshes.MultiSelect = false;
            lvMeshes.Name = "lvMeshes";
            lvMeshes.Size = new Size(830, 193);
            lvMeshes.TabIndex = 3;
            lvMeshes.UseCompatibleStateImageBehavior = false;
            lvMeshes.DoubleClick += lvMeshes_DoubleClick;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnAddModel);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(833, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(67, 193);
            panel2.TabIndex = 2;
            // 
            // btnAddModel
            // 
            btnAddModel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAddModel.Location = new Point(3, 3);
            btnAddModel.Name = "btnAddModel";
            btnAddModel.Size = new Size(61, 42);
            btnAddModel.TabIndex = 1;
            btnAddModel.Text = "Add Model";
            btnAddModel.UseVisualStyleBackColor = true;
            btnAddModel.Click += btnAddModel_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(0, 42);
            button1.TabIndex = 0;
            button1.Text = "Add Texture";
            button1.UseVisualStyleBackColor = true;
            // 
            // ResourcesControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(911, 227);
            Controls.Add(tabControl1);
            Name = "ResourcesControl";
            Text = "ResourcesControl";
            tabControl1.ResumeLayout(false);
            tpMeshes.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tpTextures;
        private TabPage tpMeshes;
        private Panel panel2;
        private Button btnAddModel;
        private Button button1;
        private ListView lvMeshes;
    }
}