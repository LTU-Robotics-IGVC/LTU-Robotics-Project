namespace IGVC_Controller.RobotInterface.Simulator
{
    partial class GPS_Simulator_Configuration
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
            this.label1 = new System.Windows.Forms.Label();
            this.OffsetX_Label = new System.Windows.Forms.Label();
            this.OffsetX_Slider = new System.Windows.Forms.HScrollBar();
            this.OffsetY_Slider = new System.Windows.Forms.HScrollBar();
            this.OffsetY_Label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Noise_Slider = new System.Windows.Forms.HScrollBar();
            this.Noise_Label = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "OffsetX:";
            // 
            // OffsetX_Label
            // 
            this.OffsetX_Label.AutoSize = true;
            this.OffsetX_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OffsetX_Label.Location = new System.Drawing.Point(136, 9);
            this.OffsetX_Label.Name = "OffsetX_Label";
            this.OffsetX_Label.Size = new System.Drawing.Size(31, 32);
            this.OffsetX_Label.TabIndex = 2;
            this.OffsetX_Label.Text = "0";
            // 
            // OffsetX_Slider
            // 
            this.OffsetX_Slider.Location = new System.Drawing.Point(18, 50);
            this.OffsetX_Slider.Maximum = 10000;
            this.OffsetX_Slider.Name = "OffsetX_Slider";
            this.OffsetX_Slider.Size = new System.Drawing.Size(384, 26);
            this.OffsetX_Slider.TabIndex = 3;
            this.OffsetX_Slider.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OffsetX_Slider_Scroll);
            // 
            // OffsetY_Slider
            // 
            this.OffsetY_Slider.Location = new System.Drawing.Point(18, 134);
            this.OffsetY_Slider.Maximum = 10000;
            this.OffsetY_Slider.Name = "OffsetY_Slider";
            this.OffsetY_Slider.Size = new System.Drawing.Size(384, 26);
            this.OffsetY_Slider.TabIndex = 6;
            this.OffsetY_Slider.Scroll += new System.Windows.Forms.ScrollEventHandler(this.OffsetY_Slider_Scroll);
            // 
            // OffsetY_Label
            // 
            this.OffsetY_Label.AutoSize = true;
            this.OffsetY_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OffsetY_Label.Location = new System.Drawing.Point(136, 93);
            this.OffsetY_Label.Name = "OffsetY_Label";
            this.OffsetY_Label.Size = new System.Drawing.Size(31, 32);
            this.OffsetY_Label.TabIndex = 5;
            this.OffsetY_Label.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "OffsetY:";
            // 
            // Noise_Slider
            // 
            this.Noise_Slider.Location = new System.Drawing.Point(18, 218);
            this.Noise_Slider.Maximum = 10000;
            this.Noise_Slider.Name = "Noise_Slider";
            this.Noise_Slider.Size = new System.Drawing.Size(384, 26);
            this.Noise_Slider.TabIndex = 9;
            this.Noise_Slider.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Noise_Slider_Scroll);
            // 
            // Noise_Label
            // 
            this.Noise_Label.AutoSize = true;
            this.Noise_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Noise_Label.Location = new System.Drawing.Point(136, 177);
            this.Noise_Label.Name = "Noise_Label";
            this.Noise_Label.Size = new System.Drawing.Size(31, 32);
            this.Noise_Label.TabIndex = 8;
            this.Noise_Label.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 32);
            this.label5.TabIndex = 7;
            this.label5.Text = "Noise:";
            // 
            // GPS_Simulator_Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 264);
            this.Controls.Add(this.Noise_Slider);
            this.Controls.Add(this.Noise_Label);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OffsetY_Slider);
            this.Controls.Add(this.OffsetY_Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OffsetX_Slider);
            this.Controls.Add(this.OffsetX_Label);
            this.Controls.Add(this.label1);
            this.Name = "GPS_Simulator_Configuration";
            this.Text = "GPS_Simulator_Configuration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label OffsetX_Label;
        private System.Windows.Forms.HScrollBar OffsetX_Slider;
        private System.Windows.Forms.HScrollBar OffsetY_Slider;
        private System.Windows.Forms.Label OffsetY_Label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HScrollBar Noise_Slider;
        private System.Windows.Forms.Label Noise_Label;
        private System.Windows.Forms.Label label5;
    }
}