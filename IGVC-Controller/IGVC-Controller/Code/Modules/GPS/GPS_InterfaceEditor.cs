using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.GPS
{
    public partial class GPS_InterfaceEditor : Form, IModuleEditor
    {
        GPS_Interface module;

        public GPS_InterfaceEditor()
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
            this.module = (GPS_Interface)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            this.PriorityBox.Value = module.modulePriority;
            this.BaudrateBox.Value = module.baudrate;
            this.PortNameBox.Text = module.port_name;
        }

        //Set Module data from form on OKButton_Click
        void IModuleEditor.setDataToModule()
        {
            module.modulePriority = (int)PriorityBox.Value;
            module.baudrate = (int)BaudrateBox.Value;
            module.port_name = PortNameBox.Text;
        }
    }
}
