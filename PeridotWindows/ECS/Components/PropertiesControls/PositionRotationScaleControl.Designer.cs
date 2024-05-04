namespace PeridotWindows.ECS.Components.PropertiesControls
{
    partial class PositionRotationScaleControl
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            titleBar = new Controls.CollapsibleTitleBar();
            gbParent = new GroupBox();
            cbParent = new ComboBox();
            gbScale = new GroupBox();
            flpScale = new FlowLayoutPanel();
            panel7 = new Panel();
            label7 = new Label();
            nudScaleX = new NumericUpDown();
            panel8 = new Panel();
            label8 = new Label();
            nudScaleY = new NumericUpDown();
            panel9 = new Panel();
            label9 = new Label();
            nudScaleZ = new NumericUpDown();
            gbRotation = new GroupBox();
            flpRotation = new FlowLayoutPanel();
            panel4 = new Panel();
            label4 = new Label();
            nudRotationX = new NumericUpDown();
            panel5 = new Panel();
            label5 = new Label();
            nudRotationY = new NumericUpDown();
            panel6 = new Panel();
            label6 = new Label();
            nudRotationZ = new NumericUpDown();
            gbPosition = new GroupBox();
            flpPosition = new FlowLayoutPanel();
            panel2 = new Panel();
            label2 = new Label();
            nudPositionX = new NumericUpDown();
            panel1 = new Panel();
            label1 = new Label();
            nudPositionY = new NumericUpDown();
            panel3 = new Panel();
            label3 = new Label();
            nudPositionZ = new NumericUpDown();
            panel10 = new Panel();
            label10 = new Label();
            nudRotationW = new NumericUpDown();
            gbParent.SuspendLayout();
            gbScale.SuspendLayout();
            flpScale.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudScaleX).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudScaleY).BeginInit();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudScaleZ).BeginInit();
            gbRotation.SuspendLayout();
            flpRotation.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationX).BeginInit();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationY).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationZ).BeginInit();
            gbPosition.SuspendLayout();
            flpPosition.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPositionX).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPositionY).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPositionZ).BeginInit();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationW).BeginInit();
            SuspendLayout();
            // 
            // titleBar
            // 
            titleBar.BackColor = Color.White;
            titleBar.BorderStyle = BorderStyle.FixedSingle;
            titleBar.Collapsed = false;
            titleBar.Dock = DockStyle.Top;
            titleBar.Location = new Point(0, 0);
            titleBar.MaximumSize = new Size(9999, 28);
            titleBar.MinimumSize = new Size(0, 28);
            titleBar.Name = "titleBar";
            titleBar.OptionsMenu = null;
            titleBar.Size = new Size(552, 28);
            titleBar.TabIndex = 2;
            titleBar.Text = "Position Rotation Scale";
            // 
            // gbParent
            // 
            gbParent.AutoSize = true;
            gbParent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbParent.Controls.Add(cbParent);
            gbParent.Dock = DockStyle.Top;
            gbParent.Location = new Point(0, 28);
            gbParent.Name = "gbParent";
            gbParent.Size = new Size(552, 67);
            gbParent.TabIndex = 1;
            gbParent.TabStop = false;
            gbParent.Text = "Parent";
            // 
            // cbParent
            // 
            cbParent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbParent.FormattingEnabled = true;
            cbParent.Location = new Point(6, 22);
            cbParent.Name = "cbParent";
            cbParent.Size = new Size(543, 23);
            cbParent.TabIndex = 0;
            cbParent.SelectedIndexChanged += cbParent_SelectedIndexChanged;
            // 
            // gbScale
            // 
            gbScale.AutoSize = true;
            gbScale.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbScale.Controls.Add(flpScale);
            gbScale.Dock = DockStyle.Top;
            gbScale.Location = new Point(0, 235);
            gbScale.Name = "gbScale";
            gbScale.Size = new Size(552, 70);
            gbScale.TabIndex = 7;
            gbScale.TabStop = false;
            gbScale.Text = "Scale";
            // 
            // flpScale
            // 
            flpScale.AutoSize = true;
            flpScale.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpScale.Controls.Add(panel7);
            flpScale.Controls.Add(panel8);
            flpScale.Controls.Add(panel9);
            flpScale.Location = new Point(3, 16);
            flpScale.Margin = new Padding(0);
            flpScale.Name = "flpScale";
            flpScale.Size = new Size(345, 35);
            flpScale.TabIndex = 1;
            // 
            // panel7
            // 
            panel7.AutoSize = true;
            panel7.Controls.Add(label7);
            panel7.Controls.Add(nudScaleX);
            panel7.Location = new Point(3, 3);
            panel7.Name = "panel7";
            panel7.Size = new Size(109, 29);
            panel7.TabIndex = 6;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 5);
            label7.Name = "label7";
            label7.Size = new Size(17, 15);
            label7.TabIndex = 5;
            label7.Text = "X:";
            // 
            // nudScaleX
            // 
            nudScaleX.DecimalPlaces = 3;
            nudScaleX.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudScaleX.Location = new Point(26, 3);
            nudScaleX.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudScaleX.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudScaleX.Name = "nudScaleX";
            nudScaleX.Size = new Size(80, 23);
            nudScaleX.TabIndex = 4;
            // 
            // panel8
            // 
            panel8.AutoSize = true;
            panel8.Controls.Add(label8);
            panel8.Controls.Add(nudScaleY);
            panel8.Location = new Point(118, 3);
            panel8.Name = "panel8";
            panel8.Size = new Size(109, 29);
            panel8.TabIndex = 7;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(3, 5);
            label8.Name = "label8";
            label8.Size = new Size(17, 15);
            label8.TabIndex = 5;
            label8.Text = "Y:";
            // 
            // nudScaleY
            // 
            nudScaleY.DecimalPlaces = 3;
            nudScaleY.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudScaleY.Location = new Point(26, 3);
            nudScaleY.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudScaleY.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudScaleY.Name = "nudScaleY";
            nudScaleY.Size = new Size(80, 23);
            nudScaleY.TabIndex = 4;
            // 
            // panel9
            // 
            panel9.AutoSize = true;
            panel9.Controls.Add(label9);
            panel9.Controls.Add(nudScaleZ);
            panel9.Location = new Point(233, 3);
            panel9.Name = "panel9";
            panel9.Size = new Size(109, 29);
            panel9.TabIndex = 7;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 5);
            label9.Name = "label9";
            label9.Size = new Size(17, 15);
            label9.TabIndex = 5;
            label9.Text = "Z:";
            // 
            // nudScaleZ
            // 
            nudScaleZ.DecimalPlaces = 3;
            nudScaleZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudScaleZ.Location = new Point(26, 3);
            nudScaleZ.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudScaleZ.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudScaleZ.Name = "nudScaleZ";
            nudScaleZ.Size = new Size(80, 23);
            nudScaleZ.TabIndex = 4;
            // 
            // gbRotation
            // 
            gbRotation.AutoSize = true;
            gbRotation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbRotation.Controls.Add(flpRotation);
            gbRotation.Dock = DockStyle.Top;
            gbRotation.Location = new Point(0, 165);
            gbRotation.Name = "gbRotation";
            gbRotation.Size = new Size(552, 70);
            gbRotation.TabIndex = 6;
            gbRotation.TabStop = false;
            gbRotation.Text = "Rotation";
            // 
            // flpRotation
            // 
            flpRotation.AutoSize = true;
            flpRotation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpRotation.Controls.Add(panel4);
            flpRotation.Controls.Add(panel5);
            flpRotation.Controls.Add(panel6);
            flpRotation.Controls.Add(panel10);
            flpRotation.Location = new Point(3, 16);
            flpRotation.Margin = new Padding(0);
            flpRotation.Name = "flpRotation";
            flpRotation.Size = new Size(460, 35);
            flpRotation.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.AutoSize = true;
            panel4.Controls.Add(label4);
            panel4.Controls.Add(nudRotationX);
            panel4.Location = new Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(109, 29);
            panel4.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 5);
            label4.Name = "label4";
            label4.Size = new Size(17, 15);
            label4.TabIndex = 5;
            label4.Text = "X:";
            // 
            // nudRotationX
            // 
            nudRotationX.DecimalPlaces = 3;
            nudRotationX.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRotationX.Location = new Point(26, 3);
            nudRotationX.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudRotationX.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudRotationX.Name = "nudRotationX";
            nudRotationX.Size = new Size(80, 23);
            nudRotationX.TabIndex = 4;
            // 
            // panel5
            // 
            panel5.AutoSize = true;
            panel5.Controls.Add(label5);
            panel5.Controls.Add(nudRotationY);
            panel5.Location = new Point(118, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(109, 29);
            panel5.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 5);
            label5.Name = "label5";
            label5.Size = new Size(17, 15);
            label5.TabIndex = 5;
            label5.Text = "Y:";
            // 
            // nudRotationY
            // 
            nudRotationY.DecimalPlaces = 3;
            nudRotationY.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRotationY.Location = new Point(26, 3);
            nudRotationY.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudRotationY.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudRotationY.Name = "nudRotationY";
            nudRotationY.Size = new Size(80, 23);
            nudRotationY.TabIndex = 4;
            // 
            // panel6
            // 
            panel6.AutoSize = true;
            panel6.Controls.Add(label6);
            panel6.Controls.Add(nudRotationZ);
            panel6.Location = new Point(233, 3);
            panel6.Name = "panel6";
            panel6.Size = new Size(109, 29);
            panel6.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 5);
            label6.Name = "label6";
            label6.Size = new Size(17, 15);
            label6.TabIndex = 5;
            label6.Text = "Z:";
            // 
            // nudRotationZ
            // 
            nudRotationZ.DecimalPlaces = 3;
            nudRotationZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRotationZ.Location = new Point(26, 3);
            nudRotationZ.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudRotationZ.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudRotationZ.Name = "nudRotationZ";
            nudRotationZ.Size = new Size(80, 23);
            nudRotationZ.TabIndex = 4;
            // 
            // gbPosition
            // 
            gbPosition.AutoSize = true;
            gbPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gbPosition.Controls.Add(flpPosition);
            gbPosition.Dock = DockStyle.Top;
            gbPosition.Location = new Point(0, 95);
            gbPosition.Name = "gbPosition";
            gbPosition.Size = new Size(552, 70);
            gbPosition.TabIndex = 5;
            gbPosition.TabStop = false;
            gbPosition.Text = "Position";
            // 
            // flpPosition
            // 
            flpPosition.AutoSize = true;
            flpPosition.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flpPosition.Controls.Add(panel2);
            flpPosition.Controls.Add(panel1);
            flpPosition.Controls.Add(panel3);
            flpPosition.Location = new Point(3, 16);
            flpPosition.Margin = new Padding(0);
            flpPosition.Name = "flpPosition";
            flpPosition.Size = new Size(345, 35);
            flpPosition.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(nudPositionX);
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(109, 29);
            panel2.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 5);
            label2.Name = "label2";
            label2.Size = new Size(17, 15);
            label2.TabIndex = 5;
            label2.Text = "X:";
            // 
            // nudPositionX
            // 
            nudPositionX.DecimalPlaces = 3;
            nudPositionX.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudPositionX.Location = new Point(26, 3);
            nudPositionX.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudPositionX.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudPositionX.Name = "nudPositionX";
            nudPositionX.Size = new Size(80, 23);
            nudPositionX.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(nudPositionY);
            panel1.Location = new Point(118, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(109, 29);
            panel1.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 5;
            label1.Text = "Y:";
            // 
            // nudPositionY
            // 
            nudPositionY.DecimalPlaces = 3;
            nudPositionY.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudPositionY.Location = new Point(26, 3);
            nudPositionY.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudPositionY.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudPositionY.Name = "nudPositionY";
            nudPositionY.Size = new Size(80, 23);
            nudPositionY.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.AutoSize = true;
            panel3.Controls.Add(label3);
            panel3.Controls.Add(nudPositionZ);
            panel3.Location = new Point(233, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(109, 29);
            panel3.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 5);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 5;
            label3.Text = "Z:";
            // 
            // nudPositionZ
            // 
            nudPositionZ.DecimalPlaces = 3;
            nudPositionZ.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudPositionZ.Location = new Point(26, 3);
            nudPositionZ.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudPositionZ.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudPositionZ.Name = "nudPositionZ";
            nudPositionZ.Size = new Size(80, 23);
            nudPositionZ.TabIndex = 4;
            // 
            // panel10
            // 
            panel10.AutoSize = true;
            panel10.Controls.Add(label10);
            panel10.Controls.Add(nudRotationW);
            panel10.Location = new Point(348, 3);
            panel10.Name = "panel10";
            panel10.Size = new Size(109, 29);
            panel10.TabIndex = 8;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(3, 5);
            label10.Name = "label10";
            label10.Size = new Size(21, 15);
            label10.TabIndex = 5;
            label10.Text = "W:";
            // 
            // nudRotationW
            // 
            nudRotationW.DecimalPlaces = 3;
            nudRotationW.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudRotationW.Location = new Point(26, 3);
            nudRotationW.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudRotationW.Minimum = new decimal(new int[] { 99999, 0, 0, int.MinValue });
            nudRotationW.Name = "nudRotationW";
            nudRotationW.Size = new Size(80, 23);
            nudRotationW.TabIndex = 4;
            // 
            // PositionRotationScaleControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(gbScale);
            Controls.Add(gbRotation);
            Controls.Add(gbPosition);
            Controls.Add(gbParent);
            Controls.Add(titleBar);
            Name = "PositionRotationScaleControl";
            Size = new Size(552, 305);
            Load += PositionRotationScaleControl_Load;
            ClientSizeChanged += PositionRotationScaleControl_ClientSizeChanged;
            gbParent.ResumeLayout(false);
            gbScale.ResumeLayout(false);
            gbScale.PerformLayout();
            flpScale.ResumeLayout(false);
            flpScale.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudScaleX).EndInit();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudScaleY).EndInit();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudScaleZ).EndInit();
            gbRotation.ResumeLayout(false);
            gbRotation.PerformLayout();
            flpRotation.ResumeLayout(false);
            flpRotation.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationX).EndInit();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationY).EndInit();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationZ).EndInit();
            gbPosition.ResumeLayout(false);
            gbPosition.PerformLayout();
            flpPosition.ResumeLayout(false);
            flpPosition.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPositionX).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPositionY).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPositionZ).EndInit();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudRotationW).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Controls.CollapsibleTitleBar titleBar;
        private GroupBox gbParent;
        private ComboBox cbParent;
        private GroupBox gbScale;
        private FlowLayoutPanel flpScale;
        private Panel panel7;
        private Label label7;
        private NumericUpDown nudScaleX;
        private Panel panel8;
        private Label label8;
        private NumericUpDown nudScaleY;
        private Panel panel9;
        private Label label9;
        private NumericUpDown nudScaleZ;
        private GroupBox gbRotation;
        private FlowLayoutPanel flpRotation;
        private Panel panel4;
        private Label label4;
        private NumericUpDown nudRotationX;
        private Panel panel5;
        private Label label5;
        private NumericUpDown nudRotationY;
        private Panel panel6;
        private Label label6;
        private NumericUpDown nudRotationZ;
        private GroupBox gbPosition;
        private FlowLayoutPanel flpPosition;
        private Panel panel2;
        private Label label2;
        private NumericUpDown nudPositionX;
        private Panel panel1;
        private Label label1;
        private NumericUpDown nudPositionY;
        private Panel panel3;
        private Label label3;
        private NumericUpDown nudPositionZ;
        private Panel panel10;
        private Label label10;
        private NumericUpDown nudRotationW;
    }
}
