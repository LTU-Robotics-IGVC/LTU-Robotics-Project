namespace IGVC_Controller.Code.Modules.SystemInputs.ManualDrive
{
    partial class ManualDriveForm
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
            this.components = new System.ComponentModel.Container();
            this.SLeft = new System.Windows.Forms.Button();
            this.SRight = new System.Windows.Forms.Button();
            this.TLeft = new System.Windows.Forms.Button();
            this.TRight = new System.Windows.Forms.Button();
            this.Bck = new System.Windows.Forms.Button();
            this.Forward = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RMDirection = new System.Windows.Forms.Label();
            this.LMDirection = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label9 = new System.Windows.Forms.Label();
            this.RSpeedBox = new System.Windows.Forms.TextBox();
            this.LSpeedBox = new System.Windows.Forms.TextBox();
            this.estop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SLeft
            // 
            this.SLeft.Location = new System.Drawing.Point(68, 159);
            this.SLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SLeft.Name = "SLeft";
            this.SLeft.Size = new System.Drawing.Size(84, 81);
            this.SLeft.TabIndex = 11;
            this.SLeft.Text = "Swerve Left";
            this.SLeft.UseVisualStyleBackColor = true;
            this.SLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SLeft_MouseDown);
            this.SLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SLeft_MouseUp);
            // 
            // SRight
            // 
            this.SRight.Location = new System.Drawing.Point(317, 159);
            this.SRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SRight.Name = "SRight";
            this.SRight.Size = new System.Drawing.Size(84, 81);
            this.SRight.TabIndex = 10;
            this.SRight.Text = "Swerve Right";
            this.SRight.UseVisualStyleBackColor = true;
            this.SRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SRight_MouseDown);
            this.SRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SRight_MouseUp);
            // 
            // TLeft
            // 
            this.TLeft.Location = new System.Drawing.Point(33, 256);
            this.TLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TLeft.Name = "TLeft";
            this.TLeft.Size = new System.Drawing.Size(119, 102);
            this.TLeft.TabIndex = 9;
            this.TLeft.Text = "Turn Left";
            this.TLeft.UseVisualStyleBackColor = true;
            this.TLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TLeft_MouseDown);
            this.TLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TLeft_MouseUp);
            // 
            // TRight
            // 
            this.TRight.Location = new System.Drawing.Point(317, 256);
            this.TRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TRight.Name = "TRight";
            this.TRight.Size = new System.Drawing.Size(119, 102);
            this.TRight.TabIndex = 8;
            this.TRight.Text = "Turn Right";
            this.TRight.UseVisualStyleBackColor = true;
            this.TRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TRight_MouseDown);
            this.TRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TRight_MouseUp);
            // 
            // Bck
            // 
            this.Bck.Location = new System.Drawing.Point(174, 256);
            this.Bck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Bck.Name = "Bck";
            this.Bck.Size = new System.Drawing.Size(119, 102);
            this.Bck.TabIndex = 7;
            this.Bck.Text = "Backward";
            this.Bck.UseVisualStyleBackColor = true;
            this.Bck.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Bck_MouseDown);
            this.Bck.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Bck_MouseUp);
            // 
            // Forward
            // 
            this.Forward.Location = new System.Drawing.Point(174, 139);
            this.Forward.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Forward.Name = "Forward";
            this.Forward.Size = new System.Drawing.Size(119, 102);
            this.Forward.TabIndex = 6;
            this.Forward.Text = "Forward";
            this.Forward.UseVisualStyleBackColor = true;
            this.Forward.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Forward_MouseDown);
            this.Forward.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Forward_MouseUp);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(560, 467);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(298, 40);
            this.progressBar1.TabIndex = 12;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(68, 467);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(299, 40);
            this.progressBar2.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(561, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Right Motor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Left Motor";
            // 
            // RMDirection
            // 
            this.RMDirection.AutoSize = true;
            this.RMDirection.Location = new System.Drawing.Point(712, 428);
            this.RMDirection.Name = "RMDirection";
            this.RMDirection.Size = new System.Drawing.Size(70, 20);
            this.RMDirection.TabIndex = 16;
            this.RMDirection.Text = "Stopped";
            this.RMDirection.Click += new System.EventHandler(this.label3_Click);
            // 
            // LMDirection
            // 
            this.LMDirection.AutoSize = true;
            this.LMDirection.Location = new System.Drawing.Point(221, 426);
            this.LMDirection.Name = "LMDirection";
            this.LMDirection.Size = new System.Drawing.Size(70, 20);
            this.LMDirection.TabIndex = 17;
            this.LMDirection.Text = "Stopped";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 510);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "0.00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(556, 510);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "0.00";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Location = new System.Drawing.Point(316, 510);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "5.00";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Location = new System.Drawing.Point(807, 510);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "5.00";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(864, 477);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "m/s";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(373, 476);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "m/s";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(28, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 29);
            this.label9.TabIndex = 24;
            this.label9.Text = "Drive Type";
            // 
            // RSpeedBox
            // 
            this.RSpeedBox.Location = new System.Drawing.Point(679, 510);
            this.RSpeedBox.Name = "RSpeedBox";
            this.RSpeedBox.ReadOnly = true;
            this.RSpeedBox.Size = new System.Drawing.Size(61, 26);
            this.RSpeedBox.TabIndex = 25;
            // 
            // LSpeedBox
            // 
            this.LSpeedBox.Location = new System.Drawing.Point(188, 513);
            this.LSpeedBox.Name = "LSpeedBox";
            this.LSpeedBox.ReadOnly = true;
            this.LSpeedBox.Size = new System.Drawing.Size(61, 26);
            this.LSpeedBox.TabIndex = 26;
            // 
            // estop
            // 
            this.estop.Location = new System.Drawing.Point(68, 368);
            this.estop.Name = "estop";
            this.estop.Size = new System.Drawing.Size(333, 45);
            this.estop.TabIndex = 27;
            this.estop.Text = "E-Stop";
            this.estop.UseVisualStyleBackColor = true;
            this.estop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.estop_MouseDown);
            this.estop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.estop_MouseUp);
            // 
            // ManualDriveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 595);
            this.ControlBox = false;
            this.Controls.Add(this.estop);
            this.Controls.Add(this.LSpeedBox);
            this.Controls.Add(this.RSpeedBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LMDirection);
            this.Controls.Add(this.RMDirection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SLeft);
            this.Controls.Add(this.SRight);
            this.Controls.Add(this.TLeft);
            this.Controls.Add(this.TRight);
            this.Controls.Add(this.Bck);
            this.Controls.Add(this.Forward);
            this.Name = "ManualDriveForm";
            this.Text = "ManualDriveForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SLeft;
        private System.Windows.Forms.Button SRight;
        private System.Windows.Forms.Button TLeft;
        private System.Windows.Forms.Button TRight;
        private System.Windows.Forms.Button Bck;
        private System.Windows.Forms.Button Forward;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label RMDirection;
        private System.Windows.Forms.Label LMDirection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox RSpeedBox;
        private System.Windows.Forms.TextBox LSpeedBox;
        private System.Windows.Forms.Button estop;
    }
}