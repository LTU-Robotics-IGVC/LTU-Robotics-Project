namespace IGVC_Controller.RobotInterface.Simulator
{
    partial class RobotSimulator
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Update_Timer = new System.Windows.Forms.Timer(this.components);
            this.UpdateHz_Slider = new System.Windows.Forms.TrackBar();
            this.UpdateHz_Label = new System.Windows.Forms.Label();
            this.VariableBox = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateHz_Slider)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(801, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gPSToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // gPSToolStripMenuItem
            // 
            this.gPSToolStripMenuItem.Name = "gPSToolStripMenuItem";
            this.gPSToolStripMenuItem.Size = new System.Drawing.Size(116, 30);
            this.gPSToolStripMenuItem.Text = "GPS";
            this.gPSToolStripMenuItem.Click += new System.EventHandler(this.gPSToolStripMenuItem_Click);
            // 
            // Update_Timer
            // 
            this.Update_Timer.Tick += new System.EventHandler(this.Update_Timer_Tick);
            // 
            // UpdateHz_Slider
            // 
            this.UpdateHz_Slider.Location = new System.Drawing.Point(12, 452);
            this.UpdateHz_Slider.Maximum = 60;
            this.UpdateHz_Slider.Minimum = 1;
            this.UpdateHz_Slider.Name = "UpdateHz_Slider";
            this.UpdateHz_Slider.Size = new System.Drawing.Size(245, 69);
            this.UpdateHz_Slider.TabIndex = 1;
            this.UpdateHz_Slider.Value = 1;
            this.UpdateHz_Slider.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // UpdateHz_Label
            // 
            this.UpdateHz_Label.AutoSize = true;
            this.UpdateHz_Label.Location = new System.Drawing.Point(12, 419);
            this.UpdateHz_Label.Name = "UpdateHz_Label";
            this.UpdateHz_Label.Size = new System.Drawing.Size(51, 20);
            this.UpdateHz_Label.TabIndex = 2;
            this.UpdateHz_Label.Text = "label1";
            // 
            // VariableBox
            // 
            this.VariableBox.Location = new System.Drawing.Point(12, 36);
            this.VariableBox.Name = "VariableBox";
            this.VariableBox.Size = new System.Drawing.Size(245, 380);
            this.VariableBox.TabIndex = 3;
            this.VariableBox.Text = "";
            // 
            // RobotSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 533);
            this.Controls.Add(this.VariableBox);
            this.Controls.Add(this.UpdateHz_Label);
            this.Controls.Add(this.UpdateHz_Slider);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RobotSimulator";
            this.Text = "RobotSimulator";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpdateHz_Slider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gPSToolStripMenuItem;
        private System.Windows.Forms.Timer Update_Timer;
        private System.Windows.Forms.Label UpdateHz_Label;
        private System.Windows.Forms.TrackBar UpdateHz_Slider;
        private System.Windows.Forms.RichTextBox VariableBox;
    }
}