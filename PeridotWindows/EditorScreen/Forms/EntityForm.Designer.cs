﻿namespace PeridotWindows.EditorScreen.Forms
{
    partial class EntityForm
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
            this.flpComponents = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpComponents
            // 
            this.flpComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpComponents.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpComponents.Location = new System.Drawing.Point(0, 0);
            this.flpComponents.Name = "flpComponents";
            this.flpComponents.Size = new System.Drawing.Size(296, 594);
            this.flpComponents.TabIndex = 0;
            // 
            // EntityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 594);
            this.Controls.Add(this.flpComponents);
            this.Name = "EntityForm";
            this.Text = "EntityForm";
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flpComponents;
    }
}