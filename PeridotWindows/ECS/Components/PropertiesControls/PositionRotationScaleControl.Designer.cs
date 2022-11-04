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
            this.gbPosition = new System.Windows.Forms.GroupBox();
            this.flpPosition = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPositionX = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.nudPositionY = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.nudPositionZ = new System.Windows.Forms.NumericUpDown();
            this.titleBar = new PeridotWindows.Controls.CollapsibleTitleBar();
            this.gbRotation = new System.Windows.Forms.GroupBox();
            this.flpRotation = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.nudRotationX = new System.Windows.Forms.NumericUpDown();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.nudRotationY = new System.Windows.Forms.NumericUpDown();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.nudRotationZ = new System.Windows.Forms.NumericUpDown();
            this.gbScale = new System.Windows.Forms.GroupBox();
            this.flpScale = new System.Windows.Forms.FlowLayoutPanel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.nudScaleX = new System.Windows.Forms.NumericUpDown();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.nudScaleY = new System.Windows.Forms.NumericUpDown();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.nudScaleZ = new System.Windows.Forms.NumericUpDown();
            this.gbPosition.SuspendLayout();
            this.flpPosition.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPositionX)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPositionY)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPositionZ)).BeginInit();
            this.gbRotation.SuspendLayout();
            this.flpRotation.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationX)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationY)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationZ)).BeginInit();
            this.gbScale.SuspendLayout();
            this.flpScale.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleX)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleY)).BeginInit();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleZ)).BeginInit();
            this.SuspendLayout();
            // 
            // gbPosition
            // 
            this.gbPosition.AutoSize = true;
            this.gbPosition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbPosition.Controls.Add(this.flpPosition);
            this.gbPosition.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPosition.Location = new System.Drawing.Point(0, 28);
            this.gbPosition.Name = "gbPosition";
            this.gbPosition.Size = new System.Drawing.Size(552, 70);
            this.gbPosition.TabIndex = 1;
            this.gbPosition.TabStop = false;
            this.gbPosition.Text = "Position";
            // 
            // flpPosition
            // 
            this.flpPosition.AutoSize = true;
            this.flpPosition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpPosition.Controls.Add(this.panel2);
            this.flpPosition.Controls.Add(this.panel1);
            this.flpPosition.Controls.Add(this.panel3);
            this.flpPosition.Location = new System.Drawing.Point(3, 16);
            this.flpPosition.Margin = new System.Windows.Forms.Padding(0);
            this.flpPosition.Name = "flpPosition";
            this.flpPosition.Size = new System.Drawing.Size(345, 35);
            this.flpPosition.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.nudPositionX);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(109, 29);
            this.panel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "X:";
            // 
            // nudPositionX
            // 
            this.nudPositionX.DecimalPlaces = 3;
            this.nudPositionX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPositionX.Location = new System.Drawing.Point(26, 3);
            this.nudPositionX.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudPositionX.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudPositionX.Name = "nudPositionX";
            this.nudPositionX.Size = new System.Drawing.Size(80, 23);
            this.nudPositionX.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nudPositionY);
            this.panel1.Location = new System.Drawing.Point(118, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 29);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Y:";
            // 
            // nudPositionY
            // 
            this.nudPositionY.DecimalPlaces = 3;
            this.nudPositionY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPositionY.Location = new System.Drawing.Point(26, 3);
            this.nudPositionY.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudPositionY.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudPositionY.Name = "nudPositionY";
            this.nudPositionY.Size = new System.Drawing.Size(80, 23);
            this.nudPositionY.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.nudPositionZ);
            this.panel3.Location = new System.Drawing.Point(233, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(109, 29);
            this.panel3.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z:";
            // 
            // nudPositionZ
            // 
            this.nudPositionZ.DecimalPlaces = 3;
            this.nudPositionZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPositionZ.Location = new System.Drawing.Point(26, 3);
            this.nudPositionZ.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudPositionZ.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudPositionZ.Name = "nudPositionZ";
            this.nudPositionZ.Size = new System.Drawing.Size(80, 23);
            this.nudPositionZ.TabIndex = 4;
            // 
            // titleBar
            // 
            this.titleBar.BackColor = System.Drawing.Color.White;
            this.titleBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleBar.Collapsed = false;
            this.titleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.titleBar.Location = new System.Drawing.Point(0, 0);
            this.titleBar.MaximumSize = new System.Drawing.Size(9999, 28);
            this.titleBar.MinimumSize = new System.Drawing.Size(0, 28);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(552, 28);
            this.titleBar.TabIndex = 2;
            this.titleBar.Text = "Position Rotation Scale";
            // 
            // gbRotation
            // 
            this.gbRotation.AutoSize = true;
            this.gbRotation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbRotation.Controls.Add(this.flpRotation);
            this.gbRotation.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbRotation.Location = new System.Drawing.Point(0, 98);
            this.gbRotation.Name = "gbRotation";
            this.gbRotation.Size = new System.Drawing.Size(552, 70);
            this.gbRotation.TabIndex = 3;
            this.gbRotation.TabStop = false;
            this.gbRotation.Text = "Rotation";
            // 
            // flpRotation
            // 
            this.flpRotation.AutoSize = true;
            this.flpRotation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpRotation.Controls.Add(this.panel4);
            this.flpRotation.Controls.Add(this.panel5);
            this.flpRotation.Controls.Add(this.panel6);
            this.flpRotation.Location = new System.Drawing.Point(3, 16);
            this.flpRotation.Margin = new System.Windows.Forms.Padding(0);
            this.flpRotation.Name = "flpRotation";
            this.flpRotation.Size = new System.Drawing.Size(345, 35);
            this.flpRotation.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.nudRotationX);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(109, 29);
            this.panel4.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "X:";
            // 
            // nudRotationX
            // 
            this.nudRotationX.DecimalPlaces = 3;
            this.nudRotationX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudRotationX.Location = new System.Drawing.Point(26, 3);
            this.nudRotationX.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudRotationX.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudRotationX.Name = "nudRotationX";
            this.nudRotationX.Size = new System.Drawing.Size(80, 23);
            this.nudRotationX.TabIndex = 4;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.nudRotationY);
            this.panel5.Location = new System.Drawing.Point(118, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(109, 29);
            this.panel5.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Y:";
            // 
            // nudRotationY
            // 
            this.nudRotationY.DecimalPlaces = 3;
            this.nudRotationY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudRotationY.Location = new System.Drawing.Point(26, 3);
            this.nudRotationY.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudRotationY.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudRotationY.Name = "nudRotationY";
            this.nudRotationY.Size = new System.Drawing.Size(80, 23);
            this.nudRotationY.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.label6);
            this.panel6.Controls.Add(this.nudRotationZ);
            this.panel6.Location = new System.Drawing.Point(233, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(109, 29);
            this.panel6.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 15);
            this.label6.TabIndex = 5;
            this.label6.Text = "Z:";
            // 
            // nudRotationZ
            // 
            this.nudRotationZ.DecimalPlaces = 3;
            this.nudRotationZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudRotationZ.Location = new System.Drawing.Point(26, 3);
            this.nudRotationZ.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudRotationZ.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudRotationZ.Name = "nudRotationZ";
            this.nudRotationZ.Size = new System.Drawing.Size(80, 23);
            this.nudRotationZ.TabIndex = 4;
            // 
            // gbScale
            // 
            this.gbScale.AutoSize = true;
            this.gbScale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbScale.Controls.Add(this.flpScale);
            this.gbScale.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbScale.Location = new System.Drawing.Point(0, 168);
            this.gbScale.Name = "gbScale";
            this.gbScale.Size = new System.Drawing.Size(552, 70);
            this.gbScale.TabIndex = 4;
            this.gbScale.TabStop = false;
            this.gbScale.Text = "Scale";
            // 
            // flpScale
            // 
            this.flpScale.AutoSize = true;
            this.flpScale.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpScale.Controls.Add(this.panel7);
            this.flpScale.Controls.Add(this.panel8);
            this.flpScale.Controls.Add(this.panel9);
            this.flpScale.Location = new System.Drawing.Point(3, 16);
            this.flpScale.Margin = new System.Windows.Forms.Padding(0);
            this.flpScale.Name = "flpScale";
            this.flpScale.Size = new System.Drawing.Size(345, 35);
            this.flpScale.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.AutoSize = true;
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.nudScaleX);
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(109, 29);
            this.panel7.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "X:";
            // 
            // nudScaleX
            // 
            this.nudScaleX.DecimalPlaces = 3;
            this.nudScaleX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleX.Location = new System.Drawing.Point(26, 3);
            this.nudScaleX.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudScaleX.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudScaleX.Name = "nudScaleX";
            this.nudScaleX.Size = new System.Drawing.Size(80, 23);
            this.nudScaleX.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.AutoSize = true;
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.nudScaleY);
            this.panel8.Location = new System.Drawing.Point(118, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(109, 29);
            this.panel8.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "Y:";
            // 
            // nudScaleY
            // 
            this.nudScaleY.DecimalPlaces = 3;
            this.nudScaleY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleY.Location = new System.Drawing.Point(26, 3);
            this.nudScaleY.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudScaleY.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudScaleY.Name = "nudScaleY";
            this.nudScaleY.Size = new System.Drawing.Size(80, 23);
            this.nudScaleY.TabIndex = 4;
            // 
            // panel9
            // 
            this.panel9.AutoSize = true;
            this.panel9.Controls.Add(this.label9);
            this.panel9.Controls.Add(this.nudScaleZ);
            this.panel9.Location = new System.Drawing.Point(233, 3);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(109, 29);
            this.panel9.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 15);
            this.label9.TabIndex = 5;
            this.label9.Text = "Z:";
            // 
            // nudScaleZ
            // 
            this.nudScaleZ.DecimalPlaces = 3;
            this.nudScaleZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudScaleZ.Location = new System.Drawing.Point(26, 3);
            this.nudScaleZ.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudScaleZ.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nudScaleZ.Name = "nudScaleZ";
            this.nudScaleZ.Size = new System.Drawing.Size(80, 23);
            this.nudScaleZ.TabIndex = 4;
            // 
            // PositionRotationScaleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.gbScale);
            this.Controls.Add(this.gbRotation);
            this.Controls.Add(this.gbPosition);
            this.Controls.Add(this.titleBar);
            this.Name = "PositionRotationScaleControl";
            this.Size = new System.Drawing.Size(552, 294);
            this.Load += new System.EventHandler(this.PositionRotationScaleControl_Load);
            this.ClientSizeChanged += new System.EventHandler(this.PositionRotationScaleControl_ClientSizeChanged);
            this.gbPosition.ResumeLayout(false);
            this.gbPosition.PerformLayout();
            this.flpPosition.ResumeLayout(false);
            this.flpPosition.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPositionX)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPositionY)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPositionZ)).EndInit();
            this.gbRotation.ResumeLayout(false);
            this.gbRotation.PerformLayout();
            this.flpRotation.ResumeLayout(false);
            this.flpRotation.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationX)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationY)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationZ)).EndInit();
            this.gbScale.ResumeLayout(false);
            this.gbScale.PerformLayout();
            this.flpScale.ResumeLayout(false);
            this.flpScale.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleX)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleY)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScaleZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GroupBox gbPosition;
        private Controls.CollapsibleTitleBar titleBar;
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
    }
}
