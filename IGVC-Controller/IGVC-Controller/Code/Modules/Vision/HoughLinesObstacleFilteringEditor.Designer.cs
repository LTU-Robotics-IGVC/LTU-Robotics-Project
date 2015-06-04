namespace IGVC_Controller.Code.Modules.Vision
{
    partial class HoughLinesObstacleFilteringEditor
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
            this.minVal = new System.Windows.Forms.HScrollBar();
            this.cannyThreshLinking = new System.Windows.Forms.HScrollBar();
            this.cannyThresh = new System.Windows.Forms.HScrollBar();
            this.OKButton = new System.Windows.Forms.Button();
            this.StartCamera = new System.Windows.Forms.Button();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            this.SuspendLayout();
            // 
            // minVal
            // 
            this.minVal.Location = new System.Drawing.Point(335, 87);
            this.minVal.Maximum = 255;
            this.minVal.Name = "minVal";
            this.minVal.Size = new System.Drawing.Size(356, 27);
            this.minVal.TabIndex = 23;
            // 
            // cannyThreshLinking
            // 
            this.cannyThreshLinking.Location = new System.Drawing.Point(335, 39);
            this.cannyThreshLinking.Maximum = 360;
            this.cannyThreshLinking.Name = "cannyThreshLinking";
            this.cannyThreshLinking.Size = new System.Drawing.Size(356, 27);
            this.cannyThreshLinking.TabIndex = 22;
            // 
            // cannyThresh
            // 
            this.cannyThresh.Location = new System.Drawing.Point(335, 12);
            this.cannyThresh.Maximum = 360;
            this.cannyThresh.Name = "cannyThresh";
            this.cannyThresh.Size = new System.Drawing.Size(356, 27);
            this.cannyThresh.TabIndex = 21;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(492, 293);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(196, 42);
            this.OKButton.TabIndex = 20;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // StartCamera
            // 
            this.StartCamera.Location = new System.Drawing.Point(205, 262);
            this.StartCamera.Name = "StartCamera";
            this.StartCamera.Size = new System.Drawing.Size(127, 48);
            this.StartCamera.TabIndex = 19;
            this.StartCamera.Text = "Start Camera";
            this.StartCamera.UseVisualStyleBackColor = true;
            this.StartCamera.Click += new System.EventHandler(this.StartCamera_Click);
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(12, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(320, 240);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 16;
            this.imageBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(9, 314);
            this.PriorityBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(107, 22);
            this.PriorityBox.TabIndex = 17;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(335, 114);
            this.hScrollBar1.Maximum = 360;
            this.hScrollBar1.Minimum = 1;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(356, 27);
            this.hScrollBar1.TabIndex = 24;
            this.hScrollBar1.Value = 1;
            // 
            // HoughLinesObstacleFilteringEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 364);
            this.ControlBox = false;
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.minVal);
            this.Controls.Add(this.cannyThreshLinking);
            this.Controls.Add(this.cannyThresh);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.StartCamera);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Name = "HoughLinesObstacleFilteringEditor";
            this.Text = "HoughLinesObstacleFilteringEditor";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.HScrollBar minVal;
        private System.Windows.Forms.HScrollBar cannyThreshLinking;
        private System.Windows.Forms.HScrollBar cannyThresh;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button StartCamera;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}