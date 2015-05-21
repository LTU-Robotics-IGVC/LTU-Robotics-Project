using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.SystemInputs.MotorStart
{
    public partial class MotorStartEditor : Form,IModuleEditor
    {
        MotorStart module;

        bool Dyn_Dr_Enabled;
        
        public MotorStartEditor()
        {
            InitializeComponent();
        }
        
        void IModuleEditor.setModule(IModule module)
        {
            this.module = (MotorStart)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            Dyn_Dr_Enabled = module.dynamic_drive;
            if (Dyn_Dr_Enabled)
                DynDriveEnabled.Checked = true;
            else
                DynDriveEnabled.Checked = false;
            PriorityBox.Value = (decimal)module.modulePriority;
        }

        void IModuleEditor.setDataToModule()
        {
            module.dynamic_drive = DynDriveEnabled.Checked;
            module.modulePriority = (int)PriorityBox.Value;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ((IModuleEditor)this).setDataToModule();
            this.Close();
        }
    }
}
