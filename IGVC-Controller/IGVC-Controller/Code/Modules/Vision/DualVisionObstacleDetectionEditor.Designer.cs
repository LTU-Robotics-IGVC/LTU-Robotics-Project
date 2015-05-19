namespace IGVC_Controller.Code.Modules.Vision
{
    partial class DualVisionObstacleDetectionEditor
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
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.StartCamera = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.minGreen = new System.Windows.Forms.HScrollBar();
            this.maxGreen = new System.Windows.Forms.HScrollBar();
            this.minVal = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Priority";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(12, 314);
            this.PriorityBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(107, 22);
            this.PriorityBox.TabIndex = 9;
            this.PriorityBox.ValueChanged += new System.EventHandler(this.PriorityBox_ValueChanged);
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(15, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(320, 240);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // StartCamera
            // 
            this.StartCamera.Location = new System.Drawing.Point(208, 262);
            this.StartCamera.Name = "StartCamera";
            this.StartCamera.Size = new System.Drawing.Size(127, 48);
            this.StartCamera.TabIndex = 11;
            this.StartCamera.Text = "Start Camera";
            this.StartCamera.UseVisualStyleBackColor = true;
            this.StartCamera.Click += new System.EventHandler(this.StartCamera_Click);
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(495, 293);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(196, 42);
            this.OKButton.TabIndex = 12;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // minGreen
            // 
            this.minGreen.Location = new System.Drawing.Point(338, 12);
            this.minGreen.Maximum = 255;
            this.minGreen.Name = "minGreen";
            this.minGreen.Size = new System.Drawing.Size(356, 27);
            this.minGreen.TabIndex = 13;
            this.minGreen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.minGreen_Scroll);
            // 
            // maxGreen
            // 
            this.maxGreen.Location = new System.Drawing.Point(338, 39);
            this.maxGreen.Maximum = 255;
            this.maxGreen.Name = "maxGreen";
            this.maxGreen.Size = new System.Drawing.Size(356, 27);
            this.maxGreen.TabIndex = 14;
            this.maxGreen.Scroll += new System.Windows.Forms.ScrollEventHandler(this.maxGreen_Scroll);
            // 
            // minVal
            // 
            this.minVal.Location = new System.Drawing.Point(338, 87);
            this.minVal.Maximum = 255;
            this.minVal.Name = "minVal";
            this.minVal.Size = new System.Drawing.Size(356, 27);
            this.minVal.TabIndex = 15;
            // 
            // DualVisionObstacleDetectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 346);
            this.ControlBox = false;
            this.Controls.Add(this.minVal);
            this.Controls.Add(this.maxGreen);
            this.Controls.Add(this.minGreen);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.StartCamera);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Name = "DualVisionObstacleDetectionEditor";
            this.Text = "DualVisionObstacleDetectionEditor";
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button StartCamera;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.HScrollBar minGreen;
        private System.Windows.Forms.HScrollBar maxGreen;
        private System.Windows.Forms.HScrollBar minVal;
    }
}