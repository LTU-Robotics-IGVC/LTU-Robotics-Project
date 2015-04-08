using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.LIDAR
{
    public partial class LIDAR_InterfaceEditor : Form, IModuleEditor
    {
        LIDAR_Interface module;

        public LIDAR_InterfaceEditor()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ((IModuleEditor)this).setDataToModule();
            this.Close();
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (LIDAR_Interface)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            this.PriorityBox.Value = module.modulePriority;
            this.BaudrateBox.Value = module.baudrate;
            this.StartStepBox.Value = module.start_step;
            this.EndStepBox.Value = module.end_step;
            this.PortNameBox.Text = module.port_name;
        }

        void IModuleEditor.setDataToModule()
        {
            module.modulePriority = (int)PriorityBox.Value;
            module.baudrate = (int)BaudrateBox.Value;
            module.start_step = (int)StartStepBox.Value;
            module.end_step = (int)EndStepBox.Value;
            module.port_name = PortNameBox.Text;
        }

        private void StartStepBox_ValueChanged(object sender, EventArgs e)
        {
            if (EndStepBox.Value <= StartStepBox.Value)
                EndStepBox.Value = StartStepBox.Value + 1;
        }

        private void EndStepBox_ValueChanged(object sender, EventArgs e)
        {
            if (StartStepBox.Value >= EndStepBox.Value)
                StartStepBox.Value = EndStepBox.Value - 1;
        }
    }
}
