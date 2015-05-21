namespace IGVC_Controller.Code.Modules.SystemInputs.ManualDrive
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
            this.SetSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            this.OKButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SetSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            this.SuspendLayout();
            // 
            // SetSpeed
            // 
            this.SetSpeed.Location = new System.Drawing.Point(70, 160);
            this.SetSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SetSpeed.Name = "SetSpeed";
            this.SetSpeed.Size = new System.Drawing.Size(120, 26);
            this.SetSpeed.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Motor Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(70, 82);
            this.PriorityBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(120, 26);
            this.PriorityBox.TabIndex = 15;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(465, 238);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(220, 52);
            this.OKButton.TabIndex = 17;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(414, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "(Percentage of max motor speed to use in Dynamic Drive)";
            // 
            // ManualDriveEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 301);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SetSpeed);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ManualDriveEditor";
            this.Text = "ManualDriveEditor";
            ((System.ComponentModel.ISupportInitialize)(this.SetSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown SetSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label3;
    }
}