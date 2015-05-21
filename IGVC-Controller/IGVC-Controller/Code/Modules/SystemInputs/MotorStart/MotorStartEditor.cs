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
    public partial class MotorStartEditor : Form
    {
        MotorStart module;
        
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
            
        }

        void IModuleEditor.setDataToModule()
        {
            
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ((IModuleEditor)this).setDataToModule();
            this.Close();
        }
    }
}
