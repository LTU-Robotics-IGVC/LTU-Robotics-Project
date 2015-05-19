namespace IGVC_Controller.Code.Modules.SystemInputs
{
    partial class ManualDriveEditor
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 97);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 102);
            this.button1.TabIndex = 0;
            this.button1.Text = "Forward";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(189, 215);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 102);
            this.button2.TabIndex = 1;
            this.button2.Text = "Backward";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(332, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(119, 102);
            this.button3.TabIndex = 2;
            this.button3.Text = "Turn Right";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(48, 215);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 102);
            this.button4.TabIndex = 3;
            this.button4.Text = "Turn Left";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(332, 118);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(84, 81);
            this.button5.TabIndex = 4;
            this.button5.Text = "Swerve Right";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(83, 118);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(84, 81);
            this.button6.TabIndex = 5;
            this.button6.Text = "Swerve Left";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(371, 449);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(187, 66);
            this.button7.TabIndex = 6;
            this.button7.Text = "Close and Stop";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(205, 366);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(127, 26);
            this.numericUpDown1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Motor Speed";
            // 
            // ManualDriveEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 582);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "ManualDriveEditor";
            this.Text = "ManualDriveForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
    }
}