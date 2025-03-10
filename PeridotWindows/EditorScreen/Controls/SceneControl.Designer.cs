namespace PeridotWindows.EditorScreen.Controls
{
    partial class SceneControl
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
            lvScene = new ListView();
            chName = new ColumnHeader();
            SuspendLayout();
            // 
            // lvScene
            // 
            lvScene.Columns.AddRange(new ColumnHeader[] { chName });
            lvScene.Dock = DockStyle.Fill;
            lvScene.FullRowSelect = true;
            lvScene.Location = new Point(0, 0);
            lvScene.MultiSelect = false;
            lvScene.Name = "lvScene";
            lvScene.Size = new Size(308, 525);
            lvScene.TabIndex = 0;
            lvScene.UseCompatibleStateImageBehavior = false;
            lvScene.View = View.Details;
            lvScene.SelectedIndexChanged += lvScene_SelectedIndexChanged;
            // 
            // chName
            // 
            chName.Text = "Name";
            chName.Width = 500;
            // 
            // SceneControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(308, 525);
            Controls.Add(lvScene);
            Name = "SceneControl";
            Text = "SceneControl";
            ResumeLayout(false);
        }

        #endregion

        private ListView lvScene;
        private ColumnHeader chName;
    }
}