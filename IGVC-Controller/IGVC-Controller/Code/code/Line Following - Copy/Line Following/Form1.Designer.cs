namespace Line_Following
{
    partial class Form1
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
            this.Capture1 = new Emgu.CV.UI.ImageBox();
            this.Direction_Info = new System.Windows.Forms.Label();
            this.Capture1_BW = new Emgu.CV.UI.ImageBox();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.hScrollBar4 = new System.Windows.Forms.HScrollBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.hScrollBar5 = new System.Windows.Forms.HScrollBar();
            this.lineBox = new Emgu.CV.UI.ImageBox();
            this.LLLabel = new System.Windows.Forms.Label();
            this.LMLLabel = new System.Windows.Forms.Label();
            this.MLLabel = new System.Windows.Forms.Label();
            this.RMLLabel = new System.Windows.Forms.Label();
            this.RLLabel = new System.Windows.Forms.Label();
            this.totalLines = new System.Windows.Forms.Label();
            this.addedLinesLabel = new System.Windows.Forms.Label();
            this.thresh = new System.Windows.Forms.Label();
            this.linesThresh = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.hScrollBar6 = new System.Windows.Forms.HScrollBar();
            this.imageBox10 = new Emgu.CV.UI.ImageBox();
            this.hScrollBar7 = new System.Windows.Forms.HScrollBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Capture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Capture1_BW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Capture1
            // 
            this.Capture1.Location = new System.Drawing.Point(16, 14);
            this.Capture1.Margin = new System.Windows.Forms.Padding(4);
            this.Capture1.Name = "Capture1";
            this.Capture1.Size = new System.Drawing.Size(533, 370);
            this.Capture1.TabIndex = 2;
            this.Capture1.TabStop = false;
            // 
            // Direction_Info
            // 
            this.Direction_Info.AutoSize = true;
            this.Direction_Info.Location = new System.Drawing.Point(597, 425);
            this.Direction_Info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Direction_Info.Name = "Direction_Info";
            this.Direction_Info.Size = new System.Drawing.Size(0, 17);
            this.Direction_Info.TabIndex = 3;
            // 
            // Capture1_BW
            // 
            this.Capture1_BW.Location = new System.Drawing.Point(557, 14);
            this.Capture1_BW.Margin = new System.Windows.Forms.Padding(4);
            this.Capture1_BW.Name = "Capture1_BW";
            this.Capture1_BW.Size = new System.Drawing.Size(533, 370);
            this.Capture1_BW.TabIndex = 5;
            this.Capture1_BW.TabStop = false;
            // 
            // hScrollBar3
            // 
            this.hScrollBar3.Location = new System.Drawing.Point(589, 409);
            this.hScrollBar3.Maximum = 254;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(414, 26);
            this.hScrollBar3.TabIndex = 8;
            this.hScrollBar3.Value = 200;
            this.hScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar3_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(340, 398);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "label1";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(290, 439);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(148, 21);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Autonomous Mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(586, 463);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 29);
            this.button1.TabIndex = 13;
            this.button1.Text = "Get Heading";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(586, 443);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "label2";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(7, 527);
            this.hScrollBar1.Maximum = 254;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(288, 26);
            this.hScrollBar1.TabIndex = 15;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(7, 579);
            this.hScrollBar2.Maximum = 254;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(288, 26);
            this.hScrollBar2.TabIndex = 16;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar2_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 510);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 562);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Saturation";
            // 
            // hScrollBar4
            // 
            this.hScrollBar4.Location = new System.Drawing.Point(7, 652);
            this.hScrollBar4.Maximum = 20;
            this.hScrollBar4.Minimum = 1;
            this.hScrollBar4.Name = "hScrollBar4";
            this.hScrollBar4.Size = new System.Drawing.Size(292, 26);
            this.hScrollBar4.TabIndex = 19;
            this.hScrollBar4.Value = 1;
            this.hScrollBar4.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar4_Scroll);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 635);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Gaus";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 757);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Line Hue";
            // 
            // hScrollBar5
            // 
            this.hScrollBar5.Location = new System.Drawing.Point(7, 774);
            this.hScrollBar5.Maximum = 244;
            this.hScrollBar5.Minimum = 10;
            this.hScrollBar5.Name = "hScrollBar5";
            this.hScrollBar5.Size = new System.Drawing.Size(288, 26);
            this.hScrollBar5.TabIndex = 22;
            this.hScrollBar5.Value = 50;
            this.hScrollBar5.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar5_Scroll);
            // 
            // lineBox
            // 
            this.lineBox.Location = new System.Drawing.Point(1097, 439);
            this.lineBox.Margin = new System.Windows.Forms.Padding(4);
            this.lineBox.Name = "lineBox";
            this.lineBox.Size = new System.Drawing.Size(533, 370);
            this.lineBox.TabIndex = 23;
            this.lineBox.TabStop = false;
            // 
            // LLLabel
            // 
            this.LLLabel.AutoSize = true;
            this.LLLabel.Location = new System.Drawing.Point(1120, 398);
            this.LLLabel.Name = "LLLabel";
            this.LLLabel.Size = new System.Drawing.Size(61, 17);
            this.LLLabel.TabIndex = 24;
            this.LLLabel.Text = "leftLines";
            // 
            // LMLLabel
            // 
            this.LMLLabel.AutoSize = true;
            this.LMLLabel.Location = new System.Drawing.Point(1219, 398);
            this.LMLLabel.Name = "LMLLabel";
            this.LMLLabel.Size = new System.Drawing.Size(46, 17);
            this.LMLLabel.TabIndex = 25;
            this.LMLLabel.Text = "label8";
            // 
            // MLLabel
            // 
            this.MLLabel.AutoSize = true;
            this.MLLabel.Location = new System.Drawing.Point(1332, 398);
            this.MLLabel.Name = "MLLabel";
            this.MLLabel.Size = new System.Drawing.Size(46, 17);
            this.MLLabel.TabIndex = 26;
            this.MLLabel.Text = "label9";
            // 
            // RMLLabel
            // 
            this.RMLLabel.AutoSize = true;
            this.RMLLabel.Location = new System.Drawing.Point(1438, 398);
            this.RMLLabel.Name = "RMLLabel";
            this.RMLLabel.Size = new System.Drawing.Size(54, 17);
            this.RMLLabel.TabIndex = 27;
            this.RMLLabel.Text = "label10";
            // 
            // RLLabel
            // 
            this.RLLabel.AutoSize = true;
            this.RLLabel.Location = new System.Drawing.Point(1540, 398);
            this.RLLabel.Name = "RLLabel";
            this.RLLabel.Size = new System.Drawing.Size(54, 17);
            this.RLLabel.TabIndex = 28;
            this.RLLabel.Text = "label11";
            // 
            // totalLines
            // 
            this.totalLines.AutoSize = true;
            this.totalLines.Location = new System.Drawing.Point(1332, 418);
            this.totalLines.Name = "totalLines";
            this.totalLines.Size = new System.Drawing.Size(40, 17);
            this.totalLines.TabIndex = 29;
            this.totalLines.Text = "Total";
            // 
            // addedLinesLabel
            // 
            this.addedLinesLabel.AutoSize = true;
            this.addedLinesLabel.Location = new System.Drawing.Point(1416, 418);
            this.addedLinesLabel.Name = "addedLinesLabel";
            this.addedLinesLabel.Size = new System.Drawing.Size(40, 17);
            this.addedLinesLabel.TabIndex = 30;
            this.addedLinesLabel.Text = "Total";
            // 
            // thresh
            // 
            this.thresh.AutoSize = true;
            this.thresh.Location = new System.Drawing.Point(1029, 418);
            this.thresh.Name = "thresh";
            this.thresh.Size = new System.Drawing.Size(67, 17);
            this.thresh.TabIndex = 32;
            this.thresh.Text = "threshold";
            // 
            // linesThresh
            // 
            this.linesThresh.AutoSize = true;
            this.linesThresh.Location = new System.Drawing.Point(206, 757);
            this.linesThresh.Name = "linesThresh";
            this.linesThresh.Size = new System.Drawing.Size(67, 17);
            this.linesThresh.TabIndex = 33;
            this.linesThresh.Text = "threshold";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(129, 439);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 17);
            this.label7.TabIndex = 34;
            this.label7.Text = "label7";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(16, 391);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(107, 116);
            this.textBox1.TabIndex = 35;
            this.textBox1.Text = "0 = Stop\r\n1 = Left Turn\r\n2 = Forward\r\n3 = Right Turn\r\n4 = slight Left\r\n5  = sligh" +
    "t Right";
            // 
            // hScrollBar6
            // 
            this.hScrollBar6.Location = new System.Drawing.Point(7, 800);
            this.hScrollBar6.Maximum = 20;
            this.hScrollBar6.Name = "hScrollBar6";
            this.hScrollBar6.Size = new System.Drawing.Size(288, 26);
            this.hScrollBar6.TabIndex = 36;
            this.hScrollBar6.Value = 20;
            this.hScrollBar6.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar6_Scroll);
            // 
            // imageBox10
            // 
            this.imageBox10.Location = new System.Drawing.Point(1097, 14);
            this.imageBox10.Margin = new System.Windows.Forms.Padding(4);
            this.imageBox10.Name = "imageBox10";
            this.imageBox10.Size = new System.Drawing.Size(533, 370);
            this.imageBox10.TabIndex = 37;
            this.imageBox10.TabStop = false;
            // 
            // hScrollBar7
            // 
            this.hScrollBar7.Location = new System.Drawing.Point(11, 717);
            this.hScrollBar7.Maximum = 244;
            this.hScrollBar7.Minimum = 10;
            this.hScrollBar7.Name = "hScrollBar7";
            this.hScrollBar7.Size = new System.Drawing.Size(288, 26);
            this.hScrollBar7.TabIndex = 38;
            this.hScrollBar7.Value = 50;
            this.hScrollBar7.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar7_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(110, 700);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 17);
            this.label8.TabIndex = 39;
            this.label8.Text = "threshold";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 700);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 17);
            this.label9.TabIndex = 40;
            this.label9.Text = "Barrel Hue";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Items.AddRange(new object[] {
            "42?40\'40.68822\"83?11\'43.31415\"",
            "42?40\'41.56625\"83?11\'43.66609\""});
            this.listBox1.Location = new System.Drawing.Point(399, 569);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(198, 36);
            this.listBox1.TabIndex = 41;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Line_Following.Properties.Resources.arrow;
            this.pictureBox1.Location = new System.Drawing.Point(399, 635);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(224, 199);
            this.pictureBox1.TabIndex = 42;
            this.pictureBox1.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(671, 569);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 17);
            this.label10.TabIndex = 43;
            this.label10.Text = "label10";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1668, 846);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.hScrollBar7);
            this.Controls.Add(this.imageBox10);
            this.Controls.Add(this.hScrollBar6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.linesThresh);
            this.Controls.Add(this.thresh);
            this.Controls.Add(this.addedLinesLabel);
            this.Controls.Add(this.totalLines);
            this.Controls.Add(this.RLLabel);
            this.Controls.Add(this.RMLLabel);
            this.Controls.Add(this.MLLabel);
            this.Controls.Add(this.LMLLabel);
            this.Controls.Add(this.LLLabel);
            this.Controls.Add(this.lineBox);
            this.Controls.Add(this.hScrollBar5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.hScrollBar4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hScrollBar2);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hScrollBar3);
            this.Controls.Add(this.Capture1_BW);
            this.Controls.Add(this.Direction_Info);
            this.Controls.Add(this.Capture1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Capture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Capture1_BW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox Capture1;
        private System.Windows.Forms.Label Direction_Info;
        private Emgu.CV.UI.ImageBox Capture1_BW;
        private System.Windows.Forms.HScrollBar hScrollBar3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.HScrollBar hScrollBar4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.HScrollBar hScrollBar5;
        private Emgu.CV.UI.ImageBox lineBox;
        private System.Windows.Forms.Label LLLabel;
        private System.Windows.Forms.Label LMLLabel;
        private System.Windows.Forms.Label MLLabel;
        private System.Windows.Forms.Label RMLLabel;
        private System.Windows.Forms.Label RLLabel;
        private System.Windows.Forms.Label totalLines;
        private System.Windows.Forms.Label addedLinesLabel;
        private System.Windows.Forms.Label thresh;
        private System.Windows.Forms.Label linesThresh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.HScrollBar hScrollBar6;
        private Emgu.CV.UI.ImageBox imageBox10;
        private System.Windows.Forms.HScrollBar hScrollBar7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label10;
    }
}

