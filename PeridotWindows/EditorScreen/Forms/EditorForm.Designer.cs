namespace PeridotWindows.EditorScreen.Forms
{
    partial class EditorForm
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
            game = new PeridotWindows.EditorScreen.Controls.PeridotEngineControl();
            SuspendLayout();
            // 
            // game
            // 
            game.Dock = DockStyle.Fill;
            game.GraphicsProfile = Microsoft.Xna.Framework.Graphics.GraphicsProfile.HiDef;
            game.Location = new Point(0, 0);
            game.MouseHoverUpdatesOnly = false;
            game.Name = "game";
            game.Size = new Size(800, 450);
            game.TabIndex = 0;
            game.Text = "peridotEngineControl1";
            // 
            // EditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(game);
            Name = "EditorForm";
            Text = "EditorForm";
            ResumeLayout(false);
        }

        #endregion

        private Controls.PeridotEngineControl game;
    }
}