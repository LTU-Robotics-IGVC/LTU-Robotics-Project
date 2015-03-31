namespace IGVC_Controller.Code
{
    partial class ModuleSelectionWindow
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
            this.inactiveModuleList = new System.Windows.Forms.ListBox();
            this.activeModuleList = new System.Windows.Forms.ListBox();
            this.ModuleActivateButton = new System.Windows.Forms.Button();
            this.EditModuleProperties = new System.Windows.Forms.Button();
            this.ModuleDeactivateButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inactiveModuleList
            // 
            this.inactiveModuleList.FormattingEnabled = true;
            this.inactiveModuleList.ItemHeight = 20;
            this.inactiveModuleList.Location = new System.Drawing.Point(22, 96);
            this.inactiveModuleList.Name = "inactiveModuleList";
            this.inactiveModuleList.Size = new System.Drawing.Size(255, 504);
            this.inactiveModuleList.TabIndex = 0;
            this.inactiveModuleList.SelectedIndexChanged += new System.EventHandler(this.inactiveModuleList_SelectedIndexChanged);
            // 
            // activeModuleList
            // 
            this.activeModuleList.FormattingEnabled = true;
            this.activeModuleList.ItemHeight = 20;
            this.activeModuleList.Location = new System.Drawing.Point(436, 96);
            this.activeModuleList.Name = "activeModuleList";
            this.activeModuleList.Size = new System.Drawing.Size(238, 504);
            this.activeModuleList.TabIndex = 1;
            this.activeModuleList.SelectedIndexChanged += new System.EventHandler(this.activeModuleList_SelectedIndexChanged);
            // 
            // ModuleActivateButton
            // 
            this.ModuleActivateButton.BackColor = System.Drawing.Color.Lime;
            this.ModuleActivateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ModuleActivateButton.Location = new System.Drawing.Point(283, 168);
            this.ModuleActivateButton.Name = "ModuleActivateButton";
            this.ModuleActivateButton.Size = new System.Drawing.Size(147, 48);
            this.ModuleActivateButton.TabIndex = 2;
            this.ModuleActivateButton.Text = "Activate >>";
            this.ModuleActivateButton.UseVisualStyleBackColor = false;
            this.ModuleActivateButton.Click += new System.EventHandler(this.ModuleActivateButton_Click);
            // 
            // EditModuleProperties
            // 
            this.EditModuleProperties.Location = new System.Drawing.Point(283, 262);
            this.EditModuleProperties.Name = "EditModuleProperties";
            this.EditModuleProperties.Size = new System.Drawing.Size(147, 48);
            this.EditModuleProperties.TabIndex = 3;
            this.EditModuleProperties.Text = "Edit Module Properties";
            this.EditModuleProperties.UseVisualStyleBackColor = true;
            this.EditModuleProperties.Click += new System.EventHandler(this.EditModuleProperties_Click);
            // 
            // ModuleDeactivateButton
            // 
            this.ModuleDeactivateButton.BackColor = System.Drawing.Color.Tomato;
            this.ModuleDeactivateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ModuleDeactivateButton.Location = new System.Drawing.Point(283, 362);
            this.ModuleDeactivateButton.Name = "ModuleDeactivateButton";
            this.ModuleDeactivateButton.Size = new System.Drawing.Size(147, 48);
            this.ModuleDeactivateButton.TabIndex = 4;
            this.ModuleDeactivateButton.Text = "<< Deactivate";
            this.ModuleDeactivateButton.UseVisualStyleBackColor = false;
            this.ModuleDeactivateButton.Click += new System.EventHandler(this.ModuleDeactivateButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(547, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Active";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Inactive";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(283, 552);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(147, 48);
            this.SaveSettingsButton.TabIndex = 7;
            this.SaveSettingsButton.Text = "Save Settings";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // ModuleSelectionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 638);
            this.Controls.Add(this.SaveSettingsButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ModuleDeactivateButton);
            this.Controls.Add(this.EditModuleProperties);
            this.Controls.Add(this.ModuleActivateButton);
            this.Controls.Add(this.activeModuleList);
            this.Controls.Add(this.inactiveModuleList);
            this.Name = "ModuleSelectionWindow";
            this.Text = "ModuleSelectionWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox inactiveModuleList;
        private System.Windows.Forms.ListBox activeModuleList;
        private System.Windows.Forms.Button ModuleActivateButton;
        private System.Windows.Forms.Button EditModuleProperties;
        private System.Windows.Forms.Button ModuleDeactivateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SaveSettingsButton;
    }
}