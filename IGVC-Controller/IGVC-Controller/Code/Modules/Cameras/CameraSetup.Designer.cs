namespace IGVC_Controller.Code.Modules.Cameras
{
    partial class CameraSetup
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
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.CalibrateHomography = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            this.OKButton = new System.Windows.Forms.Button();
            this.calibrateRight = new System.Windows.Forms.Button();
            this.calibrateLeft = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LeftCamIndex = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.RightCamIndex = new System.Windows.Forms.NumericUpDown();
            this.StartCameras = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftCamIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightCamIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(12, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(320, 240);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // imageBox2
            // 
            this.imageBox2.Location = new System.Drawing.Point(664, 12);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(320, 240);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox2.TabIndex = 2;
            this.imageBox2.TabStop = false;
            // 
            // imageBox3
            // 
            this.imageBox3.Location = new System.Drawing.Point(338, 12);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(320, 240);
            this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox3.TabIndex = 3;
            this.imageBox3.TabStop = false;
            // 
            // CalibrateHomography
            // 
            this.CalibrateHomography.Location = new System.Drawing.Point(418, 258);
            this.CalibrateHomography.Name = "CalibrateHomography";
            this.CalibrateHomography.Size = new System.Drawing.Size(153, 62);
            this.CalibrateHomography.TabIndex = 4;
            this.CalibrateHomography.Text = "Calibrate Homography";
            this.CalibrateHomography.UseVisualStyleBackColor = true;
            this.CalibrateHomography.Click += new System.EventHandler(this.CalibrateHomography_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 465);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(12, 486);
            this.PriorityBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(107, 22);
            this.PriorityBox.TabIndex = 7;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(791, 466);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(196, 42);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // calibrateRight
            // 
            this.calibrateRight.Location = new System.Drawing.Point(740, 258);
            this.calibrateRight.Name = "calibrateRight";
            this.calibrateRight.Size = new System.Drawing.Size(153, 62);
            this.calibrateRight.TabIndex = 10;
            this.calibrateRight.Text = "Calibrate Right Camera Intrinsics";
            this.calibrateRight.UseVisualStyleBackColor = true;
            this.calibrateRight.Click += new System.EventHandler(this.calibrateRight_Click);
            // 
            // calibrateLeft
            // 
            this.calibrateLeft.Location = new System.Drawing.Point(83, 258);
            this.calibrateLeft.Name = "calibrateLeft";
            this.calibrateLeft.Size = new System.Drawing.Size(153, 62);
            this.calibrateLeft.TabIndex = 11;
            this.calibrateLeft.Text = "Calibrate Left Camera Intrinsics";
            this.calibrateLeft.UseVisualStyleBackColor = true;
            this.calibrateLeft.Click += new System.EventHandler(this.calibrateLeft_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 327);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "LeftCamIndex";
            // 
            // LeftCamIndex
            // 
            this.LeftCamIndex.Location = new System.Drawing.Point(83, 348);
            this.LeftCamIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LeftCamIndex.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.LeftCamIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.LeftCamIndex.Name = "LeftCamIndex";
            this.LeftCamIndex.Size = new System.Drawing.Size(107, 22);
            this.LeftCamIndex.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(740, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 15;
            this.label3.Text = "RightCamIndex";
            // 
            // RightCamIndex
            // 
            this.RightCamIndex.Location = new System.Drawing.Point(740, 348);
            this.RightCamIndex.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RightCamIndex.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RightCamIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.RightCamIndex.Name = "RightCamIndex";
            this.RightCamIndex.Size = new System.Drawing.Size(107, 22);
            this.RightCamIndex.TabIndex = 14;
            // 
            // StartCameras
            // 
            this.StartCameras.Location = new System.Drawing.Point(418, 326);
            this.StartCameras.Name = "StartCameras";
            this.StartCameras.Size = new System.Drawing.Size(153, 62);
            this.StartCameras.TabIndex = 16;
            this.StartCameras.Text = "Start Cameras";
            this.StartCameras.UseVisualStyleBackColor = true;
            this.StartCameras.Click += new System.EventHandler(this.StartCameras_Click);
            // 
            // CameraSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 519);
            this.ControlBox = false;
            this.Controls.Add(this.StartCameras);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RightCamIndex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LeftCamIndex);
            this.Controls.Add(this.calibrateLeft);
            this.Controls.Add(this.calibrateRight);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Controls.Add(this.CalibrateHomography);
            this.Controls.Add(this.imageBox3);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.imageBox1);
            this.Name = "CameraSetup";
            this.Text = "CameraSetup";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftCamIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightCamIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.Button CalibrateHomography;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button calibrateRight;
        private System.Windows.Forms.Button calibrateLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown LeftCamIndex;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown RightCamIndex;
        private System.Windows.Forms.Button StartCameras;
    }
}