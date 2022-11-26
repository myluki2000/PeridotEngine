namespace PeridotWindows.EditorScreen.Forms
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
            this.components = new System.ComponentModel.Container();
            this.pnlComponents = new System.Windows.Forms.Panel();
            this.cmsComponentOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAddComponent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsComponentOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlComponents
            // 
            this.pnlComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlComponents.Location = new System.Drawing.Point(0, 0);
            this.pnlComponents.Name = "pnlComponents";
            this.pnlComponents.Size = new System.Drawing.Size(353, 594);
            this.pnlComponents.TabIndex = 0;
            // 
            // cmsComponentOptions
            // 
            this.cmsComponentOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDelete});
            this.cmsComponentOptions.Name = "cmsComponentOptions";
            this.cmsComponentOptions.Size = new System.Drawing.Size(108, 26);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(107, 22);
            this.tsmiDelete.Text = "Delete";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // cmsAddComponent
            // 
            this.cmsAddComponent.Name = "cmsAddComponent";
            this.cmsAddComponent.Size = new System.Drawing.Size(181, 26);
            // 
            // EntityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 594);
            this.Controls.Add(this.pnlComponents);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "EntityForm";
            this.ShowInTaskbar = false;
            this.Text = "EntityForm";
            this.cmsComponentOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlComponents;
        private ContextMenuStrip cmsComponentOptions;
        private ToolStripMenuItem tsmiDelete;
        private ContextMenuStrip cmsAddComponent;
    }
}