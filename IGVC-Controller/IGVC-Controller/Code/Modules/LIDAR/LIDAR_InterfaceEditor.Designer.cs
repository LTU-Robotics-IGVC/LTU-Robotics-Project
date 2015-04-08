namespace IGVC_Controller.Code.Modules.LIDAR
{
    partial class LIDAR_InterfaceEditor
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
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            this.OKButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BaudrateBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.StartStepBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.EndStepBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.PortNameBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaudrateBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartStepBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndStepBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 280);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(81, 306);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(120, 26);
            this.PriorityBox.TabIndex = 5;
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(313, 288);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(221, 52);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Baudrate";
            // 
            // BaudrateBox
            // 
            this.BaudrateBox.Location = new System.Drawing.Point(81, 55);
            this.BaudrateBox.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.BaudrateBox.Name = "BaudrateBox";
            this.BaudrateBox.Size = new System.Drawing.Size(120, 26);
            this.BaudrateBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Start Step";
            // 
            // StartStepBox
            // 
            this.StartStepBox.Location = new System.Drawing.Point(81, 131);
            this.StartStepBox.Maximum = new decimal(new int[] {
            1079,
            0,
            0,
            0});
            this.StartStepBox.Name = "StartStepBox";
            this.StartStepBox.Size = new System.Drawing.Size(120, 26);
            this.StartStepBox.TabIndex = 9;
            this.StartStepBox.ValueChanged += new System.EventHandler(this.StartStepBox_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "End Step";
            // 
            // EndStepBox
            // 
            this.EndStepBox.Location = new System.Drawing.Point(81, 207);
            this.EndStepBox.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.EndStepBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EndStepBox.Name = "EndStepBox";
            this.EndStepBox.Size = new System.Drawing.Size(120, 26);
            this.EndStepBox.TabIndex = 11;
            this.EndStepBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.EndStepBox.ValueChanged += new System.EventHandler(this.EndStepBox_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(352, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "Port Name";
            // 
            // PortNameBox
            // 
            this.PortNameBox.Location = new System.Drawing.Point(356, 55);
            this.PortNameBox.Name = "PortNameBox";
            this.PortNameBox.Size = new System.Drawing.Size(130, 26);
            this.PortNameBox.TabIndex = 14;
            // 
            // LIDAR_InterfaceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 352);
            this.Controls.Add(this.PortNameBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EndStepBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StartStepBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BaudrateBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Controls.Add(this.OKButton);
            this.Name = "LIDAR_InterfaceEditor";
            this.Text = "LIDAR_InterfaceEditor";
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaudrateBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartStepBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndStepBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown BaudrateBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown StartStepBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown EndStepBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PortNameBox;
    }
}