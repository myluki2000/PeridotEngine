namespace PeridotWindows.EditorScreen.Forms
{
    partial class SceneForm
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
            this.lvScene = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvScene
            // 
            this.lvScene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvScene.Location = new System.Drawing.Point(0, 0);
            this.lvScene.MultiSelect = false;
            this.lvScene.Name = "lvScene";
            this.lvScene.Size = new System.Drawing.Size(308, 525);
            this.lvScene.TabIndex = 0;
            this.lvScene.UseCompatibleStateImageBehavior = false;
            this.lvScene.View = System.Windows.Forms.View.List;
            this.lvScene.SelectedIndexChanged += new System.EventHandler(this.lvScene_SelectedIndexChanged);
            // 
            // SceneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 525);
            this.Controls.Add(this.lvScene);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "SceneForm";
            this.Text = "SceneForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ListView lvScene;
    }
}