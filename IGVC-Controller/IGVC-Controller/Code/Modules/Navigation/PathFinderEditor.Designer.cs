namespace IGVC_Controller.Code.Modules.Navigation
{
    partial class PathFinderEditor
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
            this.TestPathingButton = new System.Windows.Forms.Button();
            this.CreateMapButton = new System.Windows.Forms.Button();
            this.WidthBox = new System.Windows.Forms.NumericUpDown();
            this.HeightBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightBox)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox1
            // 
            this.imageBox1.Location = new System.Drawing.Point(12, 12);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(300, 300);
            this.imageBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // TestPathingButton
            // 
            this.TestPathingButton.Location = new System.Drawing.Point(321, 39);
            this.TestPathingButton.Name = "TestPathingButton";
            this.TestPathingButton.Size = new System.Drawing.Size(75, 26);
            this.TestPathingButton.TabIndex = 3;
            this.TestPathingButton.Text = "Path Find";
            this.TestPathingButton.UseVisualStyleBackColor = true;
            this.TestPathingButton.Click += new System.EventHandler(this.TestPathingButton_Click);
            // 
            // CreateMapButton
            // 
            this.CreateMapButton.Location = new System.Drawing.Point(321, 71);
            this.CreateMapButton.Name = "CreateMapButton";
            this.CreateMapButton.Size = new System.Drawing.Size(75, 30);
            this.CreateMapButton.TabIndex = 4;
            this.CreateMapButton.Text = "PrepMap";
            this.CreateMapButton.UseVisualStyleBackColor = true;
            this.CreateMapButton.Click += new System.EventHandler(this.CreateMapButton_Click);
            // 
            // WidthBox
            // 
            this.WidthBox.Location = new System.Drawing.Point(318, 107);
            this.WidthBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.WidthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(100, 22);
            this.WidthBox.TabIndex = 5;
            this.WidthBox.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // HeightBox
            // 
            this.HeightBox.Location = new System.Drawing.Point(318, 135);
            this.HeightBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.HeightBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HeightBox.Name = "HeightBox";
            this.HeightBox.Size = new System.Drawing.Size(100, 22);
            this.HeightBox.TabIndex = 6;
            this.HeightBox.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(318, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // PathFinderEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 373);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HeightBox);
            this.Controls.Add(this.WidthBox);
            this.Controls.Add(this.CreateMapButton);
            this.Controls.Add(this.TestPathingButton);
            this.Controls.Add(this.imageBox1);
            this.Name = "PathFinderEditor";
            this.Text = "PathFinderEditor";
            this.Load += new System.EventHandler(this.PathFinderEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WidthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button TestPathingButton;
        private System.Windows.Forms.Button CreateMapButton;
        private System.Windows.Forms.NumericUpDown WidthBox;
        private System.Windows.Forms.NumericUpDown HeightBox;
        private System.Windows.Forms.Label label1;
    }
}