﻿using PeridotEngine.Engine.Editor.UI;

namespace PeridotEngine.Engine.Editor.Forms
{
    partial class ToolboxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolboxForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.nudZIndex = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new PeridotEngine.Engine.Editor.UI.ToolStripEx();
            this.btnCursor = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tpSolids = new System.Windows.Forms.TabPage();
            this.lvSolids = new System.Windows.Forms.ListView();
            this.tpEntities = new System.Windows.Forms.TabPage();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTexturePath = new System.Windows.Forms.Label();
            this.btnSelectTexture = new System.Windows.Forms.Button();
            this.pbSelectedTexture = new System.Windows.Forms.PictureBox();
            this.lvColliders = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpSolids.SuspendLayout();
            this.tpEntities.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedTexture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nudZIndex);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.nudWidth);
            this.panel1.Controls.Add(this.nudHeight);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 24);
            this.panel1.TabIndex = 0;
            // 
            // nudZIndex
            // 
            this.nudZIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudZIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudZIndex.Location = new System.Drawing.Point(67, 3);
            this.nudZIndex.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudZIndex.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.nudZIndex.Name = "nudZIndex";
            this.nudZIndex.Size = new System.Drawing.Size(40, 19);
            this.nudZIndex.TabIndex = 9;
            this.nudZIndex.ValueChanged += new System.EventHandler(this.NudZIndex_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(36, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Z-Ind.";
            // 
            // nudWidth
            // 
            this.nudWidth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudWidth.Location = new System.Drawing.Point(141, 3);
            this.nudWidth.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(50, 19);
            this.nudWidth.TabIndex = 7;
            this.nudWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.NudWidth_ValueChanged);
            // 
            // nudHeight
            // 
            this.nudHeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudHeight.Location = new System.Drawing.Point(230, 3);
            this.nudHeight.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(50, 19);
            this.nudHeight.TabIndex = 5;
            this.nudHeight.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudHeight.ValueChanged += new System.EventHandler(this.NudHeight_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(110, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(194, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Height";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCursor});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCursor
            // 
            this.btnCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCursor.Image = ((System.Drawing.Image)(resources.GetObject("btnCursor.Image")));
            this.btnCursor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCursor.Name = "btnCursor";
            this.btnCursor.Size = new System.Drawing.Size(46, 22);
            this.btnCursor.Text = "Cursor";
            this.btnCursor.Click += new System.EventHandler(this.BtnCursor_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 658);
            this.panel2.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpSolids);
            this.tabControl.Controls.Add(this.tpEntities);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 106);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(284, 552);
            this.tabControl.TabIndex = 3;
            // 
            // tpSolids
            // 
            this.tpSolids.Controls.Add(this.lvSolids);
            this.tpSolids.Location = new System.Drawing.Point(4, 22);
            this.tpSolids.Name = "tpSolids";
            this.tpSolids.Padding = new System.Windows.Forms.Padding(3);
            this.tpSolids.Size = new System.Drawing.Size(276, 526);
            this.tpSolids.TabIndex = 0;
            this.tpSolids.Text = "Solids";
            this.tpSolids.UseVisualStyleBackColor = true;
            // 
            // lvSolids
            // 
            this.lvSolids.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSolids.HideSelection = false;
            this.lvSolids.Location = new System.Drawing.Point(3, 3);
            this.lvSolids.Name = "lvSolids";
            this.lvSolids.Size = new System.Drawing.Size(270, 520);
            this.lvSolids.TabIndex = 0;
            this.lvSolids.UseCompatibleStateImageBehavior = false;
            // 
            // tpEntities
            // 
            this.tpEntities.Controls.Add(this.lvEntities);
            this.tpEntities.Location = new System.Drawing.Point(4, 22);
            this.tpEntities.Name = "tpEntities";
            this.tpEntities.Padding = new System.Windows.Forms.Padding(3);
            this.tpEntities.Size = new System.Drawing.Size(276, 526);
            this.tpEntities.TabIndex = 1;
            this.tpEntities.Text = "Entities";
            this.tpEntities.UseVisualStyleBackColor = true;
            // 
            // lvEntities
            // 
            this.lvEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(3, 3);
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(270, 520);
            this.lvEntities.TabIndex = 0;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblTexturePath);
            this.panel3.Controls.Add(this.btnSelectTexture);
            this.panel3.Controls.Add(this.pbSelectedTexture);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(284, 106);
            this.panel3.TabIndex = 4;
            // 
            // lblTexturePath
            // 
            this.lblTexturePath.Location = new System.Drawing.Point(127, 12);
            this.lblTexturePath.Name = "lblTexturePath";
            this.lblTexturePath.Size = new System.Drawing.Size(145, 50);
            this.lblTexturePath.TabIndex = 2;
            this.lblTexturePath.Text = "No Texture Selected";
            // 
            // btnSelectTexture
            // 
            this.btnSelectTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectTexture.Location = new System.Drawing.Point(127, 79);
            this.btnSelectTexture.Name = "btnSelectTexture";
            this.btnSelectTexture.Size = new System.Drawing.Size(150, 21);
            this.btnSelectTexture.TabIndex = 1;
            this.btnSelectTexture.Text = "Select Texture";
            this.btnSelectTexture.UseVisualStyleBackColor = true;
            this.btnSelectTexture.Click += new System.EventHandler(this.BtnSelectTexture_Click);
            // 
            // pbSelectedTexture
            // 
            this.pbSelectedTexture.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbSelectedTexture.Location = new System.Drawing.Point(0, 0);
            this.pbSelectedTexture.Name = "pbSelectedTexture";
            this.pbSelectedTexture.Size = new System.Drawing.Size(121, 106);
            this.pbSelectedTexture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSelectedTexture.TabIndex = 0;
            this.pbSelectedTexture.TabStop = false;
            // 
            // lvColliders
            // 
            this.lvColliders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvColliders.HideSelection = false;
            this.lvColliders.Location = new System.Drawing.Point(0, 24);
            this.lvColliders.Name = "lvColliders";
            this.lvColliders.Size = new System.Drawing.Size(284, 658);
            this.lvColliders.TabIndex = 2;
            this.lvColliders.UseCompatibleStateImageBehavior = false;
            this.lvColliders.View = System.Windows.Forms.View.List;
            this.lvColliders.Visible = false;
            // 
            // ToolboxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 682);
            this.Controls.Add(this.lvColliders);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ToolboxForm";
            this.Text = "ToolboxForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpSolids.ResumeLayout(false);
            this.tpEntities.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedTexture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpSolids;
        private System.Windows.Forms.ListView lvSolids;
        private System.Windows.Forms.TabPage tpEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private ToolStripEx toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCursor;
        private System.Windows.Forms.NumericUpDown nudZIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvColliders;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTexturePath;
        private System.Windows.Forms.Button btnSelectTexture;
        private System.Windows.Forms.PictureBox pbSelectedTexture;
    }
}