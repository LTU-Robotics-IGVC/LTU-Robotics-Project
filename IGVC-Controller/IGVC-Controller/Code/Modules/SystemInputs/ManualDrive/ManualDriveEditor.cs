using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGVC_Controller.DataIO;

namespace IGVC_Controller.Code.Modules.SystemInputs.ManualDrive
{
    public partial class ManualDriveEditor : Form, IModuleEditor
    {
        ManualDrive module;

        /// <summary>
        /// Maximum speed motors can maintain in m/s (Not modifiable in editor)
        /// </summary>
        const double max_motor_speed = 5.0;
        
        public ManualDriveEditor()
        {
            InitializeComponent();
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (ManualDrive)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            PriorityBox.Value = (decimal)module.modulePriority;
            SetSpeed.Value = (decimal)((module.def_speed * 100.0)/max_motor_speed);
        }

        void IModuleEditor.setDataToModule()
        {
            module.def_speed = ((double)(SetSpeed.Value) * max_motor_speed) / 100.0;
            module.modulePriority = (int)PriorityBox.Value;
        }

        

        private void OKButton_Click(object sender, EventArgs e)
        {
            ((IModuleEditor)this).setDataToModule();
            this.Close();
        }

        private void PriorityBox_ValueChanged(object sender, EventArgs e)
        {

        }
        
        private void SetSpeed_ValueChanged(object sender, EventArgs e)
        {

        }

        
    }
}
