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
            this.Forward = new System.Windows.Forms.Button();
            this.Bck = new System.Windows.Forms.Button();
            this.TRight = new System.Windows.Forms.Button();
            this.TLeft = new System.Windows.Forms.Button();
            this.SRight = new System.Windows.Forms.Button();
            this.SLeft = new System.Windows.Forms.Button();
            this.Close = new System.Windows.Forms.Button();
            this.SetSpeed = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.SetSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Forward
            // 
            this.Forward.Location = new System.Drawing.Point(168, 78);
            this.Forward.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(106, 82);
            this.Forward.TabIndex = 0;
            this.Forward.Text = "Forward";
            this.Forward.UseVisualStyleBackColor = true;
            this.Forward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Fwd_MouseDown);
            this.Forward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Fwd_MouseUp);
            // 
            // Bck
            // 
            this.Bck.Location = new System.Drawing.Point(168, 172);
            this.Bck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Bck.Name = "Bck";
            this.Bck.Size = new System.Drawing.Size(106, 82);
            this.Bck.TabIndex = 1;
            this.Bck.Text = "Backward";
            this.Bck.UseVisualStyleBackColor = true;
            this.Bck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Bck_MouseDown);
            this.Bck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Bck_MouseUp);
            // 
            // TRight
            // 
            this.TRight.Location = new System.Drawing.Point(295, 172);
            this.TRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TRight.Name = "TRight";
            this.TRight.Size = new System.Drawing.Size(106, 82);
            this.TRight.TabIndex = 2;
            this.TRight.Text = "Turn Right";
            this.TRight.UseVisualStyleBackColor = true;
            this.TRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TRight_MouseDown);
            this.TRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TRight_MouseUp);
            // 
            // TLeft
            // 
            this.TLeft.Location = new System.Drawing.Point(43, 172);
            this.TLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TLeft.Name = "TLeft";
            this.TLeft.Size = new System.Drawing.Size(106, 82);
            this.TLeft.TabIndex = 3;
            this.TLeft.Text = "Turn Left";
            this.TLeft.UseVisualStyleBackColor = true;
            this.TLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TLeft_MouseDown);
            this.TLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TLeft_MouseUp);
            // 
            // SRight
            // 
            this.SRight.Location = new System.Drawing.Point(295, 94);
            this.SRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SRight.Name = "SRight";
            this.SRight.Size = new System.Drawing.Size(75, 65);
            this.SRight.TabIndex = 4;
            this.SRight.Text = "Swerve Right";
            this.SRight.UseVisualStyleBackColor = true;
            this.SRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SRight_MouseDown);
            this.SRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SRight_MouseUp);
            // 
            // SLeft
            // 
            this.SLeft.Location = new System.Drawing.Point(74, 94);
            this.SLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SLeft.Name = "SLeft";
            this.SLeft.Size = new System.Drawing.Size(75, 65);
            this.SLeft.TabIndex = 5;
            this.SLeft.Text = "Swerve Left";
            this.SLeft.UseVisualStyleBackColor = true;
            this.SLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SLeft_MouseDown);
            this.SLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SLeft_MouseUp);
            // 
            // Close
            // 
            this.Close.Location = new System.Drawing.Point(330, 359);
            this.Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(166, 53);
            this.Close.TabIndex = 6;
            this.Close.Text = "Close and Stop";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // SetSpeed
            // 
            this.SetSpeed.Location = new System.Drawing.Point(182, 293);
            this.SetSpeed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SetSpeed.Name = "SetSpeed";
            this.SetSpeed.Size = new System.Drawing.Size(113, 22);
            this.SetSpeed.TabIndex = 7;
            this.SetSpeed.ValueChanged += new System.EventHandler(this.SetSpeed_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 294);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Motor Speed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 412);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(12, 433);
            this.PriorityBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(107, 22);
            this.PriorityBox.TabIndex = 15;
            // 
            // ManualDriveEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 466);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SetSpeed);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.SLeft);
            this.Controls.Add(this.SRight);
            this.Controls.Add(this.TLeft);
            this.Controls.Add(this.TRight);
            this.Controls.Add(this.Bck);
            this.Controls.Add(this.Forward);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ManualDriveEditor";
            this.Text = "ManualDriveForm";
            ((System.ComponentModel.ISupportInitialize)(this.SetSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Forward;
        private System.Windows.Forms.Button Bck;
        private System.Windows.Forms.Button TRight;
        private System.Windows.Forms.Button TLeft;
        private System.Windows.Forms.Button SRight;
        private System.Windows.Forms.Button SLeft;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.NumericUpDown SetSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
    }
}