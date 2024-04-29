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
            components = new System.ComponentModel.Container();
            pnlComponents = new Panel();
            cmsComponentOptions = new ContextMenuStrip(components);
            tsmiDelete = new ToolStripMenuItem();
            cmsAddComponent = new ContextMenuStrip(components);
            cmsComponentOptions.SuspendLayout();
            SuspendLayout();
            // 
            // pnlComponents
            // 
            pnlComponents.AutoScroll = true;
            pnlComponents.Dock = DockStyle.Fill;
            pnlComponents.Location = new Point(0, 0);
            pnlComponents.Name = "pnlComponents";
            pnlComponents.Size = new Size(353, 594);
            pnlComponents.TabIndex = 0;
            // 
            // cmsComponentOptions
            // 
            cmsComponentOptions.Items.AddRange(new ToolStripItem[] { tsmiDelete });
            cmsComponentOptions.Name = "cmsComponentOptions";
            cmsComponentOptions.Size = new Size(108, 26);
            // 
            // tsmiDelete
            // 
            tsmiDelete.Name = "tsmiDelete";
            tsmiDelete.Size = new Size(107, 22);
            tsmiDelete.Text = "Delete";
            tsmiDelete.Click += tsmiDelete_Click;
            // 
            // cmsAddComponent
            // 
            cmsAddComponent.Name = "cmsAddComponent";
            cmsAddComponent.Size = new Size(61, 4);
            // 
            // EntityForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 594);
            Controls.Add(pnlComponents);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "EntityForm";
            ShowInTaskbar = false;
            Text = "EntityForm";
            cmsComponentOptions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlComponents;
        private ContextMenuStrip cmsComponentOptions;
        private ToolStripMenuItem tsmiDelete;
        private ContextMenuStrip cmsAddComponent;
    }
}