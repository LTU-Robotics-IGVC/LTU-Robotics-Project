namespace IGVC_Controller.Code.Modules.SystemInputs
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
            this.progressBar1.Location = new System.Drawing.Point(577, 159);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(298, 40);
            this.progressBar1.TabIndex = 12;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(576, 318);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(299, 40);
            this.progressBar2.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(578, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Right Motor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(578, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Left Motor";
            // 
            // RMDirection
            // 
            this.RMDirection.AutoSize = true;
            this.RMDirection.Location = new System.Drawing.Point(729, 120);
            this.RMDirection.Name = "RMDirection";
            this.RMDirection.Size = new System.Drawing.Size(70, 20);
            this.RMDirection.TabIndex = 16;
            this.RMDirection.Text = "Stopped";
            this.RMDirection.Click += new System.EventHandler(this.label3_Click);
            // 
            // LMDirection
            // 
            this.LMDirection.AutoSize = true;
            this.LMDirection.Location = new System.Drawing.Point(729, 277);
            this.LMDirection.Name = "LMDirection";
            this.LMDirection.Size = new System.Drawing.Size(70, 20);
            this.LMDirection.TabIndex = 17;
            this.LMDirection.Text = "Stopped";
            // 
            // ManualDriveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 595);
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
    }
}