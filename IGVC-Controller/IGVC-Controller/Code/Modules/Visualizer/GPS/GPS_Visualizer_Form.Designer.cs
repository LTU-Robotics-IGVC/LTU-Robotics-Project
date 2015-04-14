namespace IGVC_Controller.Code.Modules.Visualizer.GPS
{
    partial class GPS_Visualizer_Form
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
            this.Lat_label = new System.Windows.Forms.Label();
            this.Long_label = new System.Windows.Forms.Label();
            this.Lat_textBox = new System.Windows.Forms.TextBox();
            this.Long_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Lat_label
            // 
            this.Lat_label.AutoSize = true;
            this.Lat_label.Location = new System.Drawing.Point(25, 48);
            this.Lat_label.Name = "Lat_label";
            this.Lat_label.Size = new System.Drawing.Size(67, 20);
            this.Lat_label.TabIndex = 0;
            this.Lat_label.Text = "Latitude";
            // 
            // Long_label
            // 
            this.Long_label.AutoSize = true;
            this.Long_label.Location = new System.Drawing.Point(12, 95);
            this.Long_label.Name = "Long_label";
            this.Long_label.Size = new System.Drawing.Size(80, 20);
            this.Long_label.TabIndex = 1;
            this.Long_label.Text = "Longitude";
            // 
            // Lat_textBox
            // 
            this.Lat_textBox.Location = new System.Drawing.Point(110, 48);
            this.Lat_textBox.Name = "Lat_textBox";
            this.Lat_textBox.Size = new System.Drawing.Size(100, 26);
            this.Lat_textBox.TabIndex = 2;
            // 
            // Long_textBox
            // 
            this.Long_textBox.Location = new System.Drawing.Point(110, 92);
            this.Long_textBox.Name = "Long_textBox";
            this.Long_textBox.Size = new System.Drawing.Size(100, 26);
            this.Long_textBox.TabIndex = 3;
            // 
            // GPS_Visualizer_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 169);
            this.Controls.Add(this.Long_textBox);
            this.Controls.Add(this.Lat_textBox);
            this.Controls.Add(this.Long_label);
            this.Controls.Add(this.Lat_label);
            this.Name = "GPS_Visualizer_Form";
            this.Text = "GPS_Visualizer_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lat_label;
        private System.Windows.Forms.Label Long_label;
        private System.Windows.Forms.TextBox Lat_textBox;
        private System.Windows.Forms.TextBox Long_textBox;
    }
}