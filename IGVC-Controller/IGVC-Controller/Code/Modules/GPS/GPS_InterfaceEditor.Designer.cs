namespace IGVC_Controller.Code.Modules.GPS
{
    partial class GPS_InterfaceEditor
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
            this.PortNameBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BaudrateBox = new System.Windows.Forms.NumericUpDown();
            this.OKButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.BaudrateBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PortNameBox
            // 
            this.PortNameBox.Location = new System.Drawing.Point(362, 74);
            this.PortNameBox.Name = "PortNameBox";
            this.PortNameBox.Size = new System.Drawing.Size(130, 26);
            this.PortNameBox.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(358, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "Port Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "Baudrate";
            // 
            // BaudrateBox
            // 
            this.BaudrateBox.Location = new System.Drawing.Point(87, 74);
            this.BaudrateBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.BaudrateBox.Name = "BaudrateBox";
            this.BaudrateBox.Size = new System.Drawing.Size(120, 26);
            this.BaudrateBox.TabIndex = 15;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(317, 267);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(221, 52);
            this.OKButton.TabIndex = 19;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 259);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(87, 285);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(120, 26);
            this.PriorityBox.TabIndex = 20;
            // 
            // GPS_InterfaceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 352);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.PortNameBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BaudrateBox);
            this.Name = "GPS_InterfaceEditor";
            this.Text = "GPS_InterfaceEditor";
            ((System.ComponentModel.ISupportInitialize)(this.BaudrateBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox PortNameBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown BaudrateBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
    }
}