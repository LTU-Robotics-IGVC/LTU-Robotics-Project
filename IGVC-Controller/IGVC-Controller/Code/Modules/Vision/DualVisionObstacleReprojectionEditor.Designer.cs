namespace IGVC_Controller.Code.Modules.Vision
{
    partial class DualVisionObstacleReprojectionEditor
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
            this.OKButton = new System.Windows.Forms.Button();
            this.StartCamera = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            this.CalibrateButton = new System.Windows.Forms.Button();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.XCoord = new System.Windows.Forms.NumericUpDown();
            this.YCoord = new System.Windows.Forms.NumericUpDown();
            this.SideSelect = new System.Windows.Forms.ComboBox();
            this.Src_Dst_Select = new System.Windows.Forms.ComboBox();
            this.CornerSelect = new System.Windows.Forms.ComboBox();
            this.SetPoint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCoord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCoord)).BeginInit();
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
            this.imageBox2.Location = new System.Drawing.Point(338, 12);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(320, 240);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox2.TabIndex = 3;
            this.imageBox2.TabStop = false;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(821, 412);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(196, 42);
            this.OKButton.TabIndex = 16;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // StartCamera
            // 
            this.StartCamera.Location = new System.Drawing.Point(369, 400);
            this.StartCamera.Name = "StartCamera";
            this.StartCamera.Size = new System.Drawing.Size(127, 48);
            this.StartCamera.TabIndex = 15;
            this.StartCamera.Text = "Start Camera";
            this.StartCamera.UseVisualStyleBackColor = true;
            this.StartCamera.Click += new System.EventHandler(this.StartCamera_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 405);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(39, 426);
            this.PriorityBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(107, 22);
            this.PriorityBox.TabIndex = 13;
            // 
            // CalibrateButton
            // 
            this.CalibrateButton.Location = new System.Drawing.Point(502, 399);
            this.CalibrateButton.Name = "CalibrateButton";
            this.CalibrateButton.Size = new System.Drawing.Size(127, 48);
            this.CalibrateButton.TabIndex = 17;
            this.CalibrateButton.Text = "Calibrate";
            this.CalibrateButton.UseVisualStyleBackColor = true;
            this.CalibrateButton.Click += new System.EventHandler(this.CalibrateButton_Click);
            // 
            // imageBox3
            // 
            this.imageBox3.Location = new System.Drawing.Point(664, 12);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(320, 240);
            this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox3.TabIndex = 18;
            this.imageBox3.TabStop = false;
            // 
            // XCoord
            // 
            this.XCoord.Location = new System.Drawing.Point(338, 258);
            this.XCoord.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.XCoord.Name = "XCoord";
            this.XCoord.Size = new System.Drawing.Size(158, 22);
            this.XCoord.TabIndex = 19;
            // 
            // YCoord
            // 
            this.YCoord.Location = new System.Drawing.Point(502, 258);
            this.YCoord.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.YCoord.Name = "YCoord";
            this.YCoord.Size = new System.Drawing.Size(158, 22);
            this.YCoord.TabIndex = 20;
            // 
            // SideSelect
            // 
            this.SideSelect.FormattingEnabled = true;
            this.SideSelect.Items.AddRange(new object[] {
            "Left",
            "Right"});
            this.SideSelect.Location = new System.Drawing.Point(339, 286);
            this.SideSelect.Name = "SideSelect";
            this.SideSelect.Size = new System.Drawing.Size(88, 24);
            this.SideSelect.TabIndex = 21;
            // 
            // Src_Dst_Select
            // 
            this.Src_Dst_Select.FormattingEnabled = true;
            this.Src_Dst_Select.Items.AddRange(new object[] {
            "Source",
            "Destination"});
            this.Src_Dst_Select.Location = new System.Drawing.Point(433, 286);
            this.Src_Dst_Select.Name = "Src_Dst_Select";
            this.Src_Dst_Select.Size = new System.Drawing.Size(106, 24);
            this.Src_Dst_Select.TabIndex = 22;
            // 
            // CornerSelect
            // 
            this.CornerSelect.FormattingEnabled = true;
            this.CornerSelect.Items.AddRange(new object[] {
            "Upper-Left",
            "Upper-Right",
            "Lower-Right",
            "Lower-Left"});
            this.CornerSelect.Location = new System.Drawing.Point(545, 286);
            this.CornerSelect.Name = "CornerSelect";
            this.CornerSelect.Size = new System.Drawing.Size(115, 24);
            this.CornerSelect.TabIndex = 23;
            // 
            // SetPoint
            // 
            this.SetPoint.Location = new System.Drawing.Point(433, 316);
            this.SetPoint.Name = "SetPoint";
            this.SetPoint.Size = new System.Drawing.Size(127, 48);
            this.SetPoint.TabIndex = 24;
            this.SetPoint.Text = "Set Point";
            this.SetPoint.UseVisualStyleBackColor = true;
            this.SetPoint.Click += new System.EventHandler(this.SetPoint_Click);
            // 
            // DualVisionObstacleReprojectionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 459);
            this.ControlBox = false;
            this.Controls.Add(this.SetPoint);
            this.Controls.Add(this.CornerSelect);
            this.Controls.Add(this.Src_Dst_Select);
            this.Controls.Add(this.SideSelect);
            this.Controls.Add(this.YCoord);
            this.Controls.Add(this.XCoord);
            this.Controls.Add(this.imageBox3);
            this.Controls.Add(this.CalibrateButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.StartCamera);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Controls.Add(this.imageBox2);
            this.Controls.Add(this.imageBox1);
            this.Name = "DualVisionObstacleReprojectionEditor";
            this.Text = "DualVisionObstacleReprojectionEditor";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCoord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCoord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button StartCamera;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
        private System.Windows.Forms.Button CalibrateButton;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.NumericUpDown XCoord;
        private System.Windows.Forms.NumericUpDown YCoord;
        private System.Windows.Forms.ComboBox SideSelect;
        private System.Windows.Forms.ComboBox Src_Dst_Select;
        private System.Windows.Forms.ComboBox CornerSelect;
        private System.Windows.Forms.Button SetPoint;
    }
}