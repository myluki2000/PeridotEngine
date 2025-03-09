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
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            pnlEngineContainer = new Panel();
            pnlSceneContainer = new Panel();
            pnlEntityPropertiesContainer = new Panel();
            pnlResourcesContainer = new Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pnlSceneContainer);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 173;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(pnlEntityPropertiesContainer);
            splitContainer2.Size = new Size(623, 450);
            splitContainer2.SplitterDistance = 423;
            splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(pnlEngineContainer);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(pnlResourcesContainer);
            splitContainer3.Size = new Size(423, 450);
            splitContainer3.SplitterDistance = 306;
            splitContainer3.TabIndex = 0;
            // 
            // pnlEngineContainer
            // 
            pnlEngineContainer.Dock = DockStyle.Fill;
            pnlEngineContainer.Location = new Point(0, 0);
            pnlEngineContainer.Name = "pnlEngineContainer";
            pnlEngineContainer.Size = new Size(423, 306);
            pnlEngineContainer.TabIndex = 0;
            // 
            // pnlSceneContainer
            // 
            pnlSceneContainer.Dock = DockStyle.Fill;
            pnlSceneContainer.Location = new Point(0, 0);
            pnlSceneContainer.Name = "pnlSceneContainer";
            pnlSceneContainer.Size = new Size(173, 450);
            pnlSceneContainer.TabIndex = 1;
            // 
            // pnlEntityPropertiesContainer
            // 
            pnlEntityPropertiesContainer.Dock = DockStyle.Fill;
            pnlEntityPropertiesContainer.Location = new Point(0, 0);
            pnlEntityPropertiesContainer.Name = "pnlEntityPropertiesContainer";
            pnlEntityPropertiesContainer.Size = new Size(196, 450);
            pnlEntityPropertiesContainer.TabIndex = 1;
            // 
            // pnlResourcesContainer
            // 
            pnlResourcesContainer.Dock = DockStyle.Fill;
            pnlResourcesContainer.Location = new Point(0, 0);
            pnlResourcesContainer.Name = "pnlResourcesContainer";
            pnlResourcesContainer.Size = new Size(423, 140);
            pnlResourcesContainer.TabIndex = 1;
            // 
            // EditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "EditorForm";
            Text = "EditorForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel pnlSceneContainer;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private Panel pnlEngineContainer;
        private Panel pnlResourcesContainer;
        private Panel pnlEntityPropertiesContainer;
    }
}