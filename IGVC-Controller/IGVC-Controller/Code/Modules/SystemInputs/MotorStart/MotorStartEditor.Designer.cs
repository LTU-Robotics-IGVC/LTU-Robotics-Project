namespace IGVC_Controller.Code.Modules.SystemInputs.MotorStart
{
    partial class MotorStartEditor
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
            this.OKButton = new System.Windows.Forms.Button();
            this.DynDriveEnabled = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PriorityBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(405, 136);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(220, 52);
            this.OKButton.TabIndex = 18;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // DynDriveEnabled
            // 
            this.DynDriveEnabled.AutoSize = true;
            this.DynDriveEnabled.Location = new System.Drawing.Point(48, 103);
            this.DynDriveEnabled.Name = "DynDriveEnabled";
            this.DynDriveEnabled.Size = new System.Drawing.Size(190, 24);
            this.DynDriveEnabled.TabIndex = 19;
            this.DynDriveEnabled.Text = "Enable Dynamic Drive";
            this.DynDriveEnabled.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Priority";
            // 
            // PriorityBox
            // 
            this.PriorityBox.Location = new System.Drawing.Point(48, 54);
            this.PriorityBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PriorityBox.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PriorityBox.Name = "PriorityBox";
            this.PriorityBox.Size = new System.Drawing.Size(120, 26);
            this.PriorityBox.TabIndex = 20;
            // 
            // MotorStartEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 199);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PriorityBox);
            this.Controls.Add(this.DynDriveEnabled);
            this.Controls.Add(this.OKButton);
            this.Name = "MotorStartEditor";
            this.Text = "MotorStartEditor";
            ((System.ComponentModel.ISupportInitialize)(this.PriorityBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.CheckBox DynDriveEnabled;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown PriorityBox;
    }
}