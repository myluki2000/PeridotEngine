namespace PeridotWindows.EditorScreen.Controls
{
    partial class ToolboxControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolboxControl));
            flowLayoutPanel1 = new FlowLayoutPanel();
            tsMain = new ToolStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            tsmiNewScene = new ToolStripMenuItem();
            tsmiLoadScene = new ToolStripMenuItem();
            tsmiSaveScene = new ToolStripMenuItem();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            tsmiAddSunlight = new ToolStripMenuItem();
            flowLayoutPanel1.SuspendLayout();
            tsMain.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(tsMain);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(800, 25);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // tsMain
            // 
            tsMain.Dock = DockStyle.Fill;
            tsMain.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripDropDownButton2 });
            tsMain.Location = new Point(0, 0);
            tsMain.Name = "tsMain";
            tsMain.Size = new Size(135, 25);
            tsMain.TabIndex = 1;
            tsMain.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { tsmiNewScene, tsmiLoadScene, tsmiSaveScene });
            toolStripDropDownButton1.Image = (Image)resources.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(38, 22);
            toolStripDropDownButton1.Text = "File";
            // 
            // tsmiNewScene
            // 
            tsmiNewScene.Name = "tsmiNewScene";
            tsmiNewScene.Size = new Size(134, 22);
            tsmiNewScene.Text = "New Scene";
            tsmiNewScene.Click += tsmiNewScene_Click;
            // 
            // tsmiLoadScene
            // 
            tsmiLoadScene.Name = "tsmiLoadScene";
            tsmiLoadScene.Size = new Size(134, 22);
            tsmiLoadScene.Text = "Load Scene";
            tsmiLoadScene.Click += tsmiLoadScene_Click;
            // 
            // tsmiSaveScene
            // 
            tsmiSaveScene.Name = "tsmiSaveScene";
            tsmiSaveScene.Size = new Size(134, 22);
            tsmiSaveScene.Text = "Save Scene";
            tsmiSaveScene.Click += tsmiSaveScene_Click;
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { tsmiAddSunlight });
            toolStripDropDownButton2.Image = (Image)resources.GetObject("toolStripDropDownButton2.Image");
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(85, 22);
            toolStripDropDownButton2.Text = "Add Objects";
            // 
            // tsmiAddSunlight
            // 
            tsmiAddSunlight.Name = "tsmiAddSunlight";
            tsmiAddSunlight.Size = new Size(180, 22);
            tsmiAddSunlight.Text = "Add Sunlight";
            tsmiAddSunlight.Click += tsmiAddSunlight_Click;
            // 
            // ToolboxControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowLayoutPanel1);
            Name = "ToolboxControl";
            Size = new Size(800, 25);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tsMain.ResumeLayout(false);
            tsMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private ToolStrip tsMain;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem tsmiNewScene;
        private ToolStripMenuItem tsmiLoadScene;
        private ToolStripMenuItem tsmiSaveScene;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem tsmiAddSunlight;
    }
}