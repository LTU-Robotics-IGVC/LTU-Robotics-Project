using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.SystemInputs
{
    public partial class ManualDriveEditor : Form, IModuleEditor
    {
        ManualDrive module;

        double right_motor;
        double left_motor;

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
            //ri.Value = (int)(module.minGreen / 180.0 * 255.0);
            //maxGreen.Value = (int)(module.maxGreen / 180.0 * 255.0);
            //minVal.Value = (int)(module.minVal * 255.0);
  
        }

        void IModuleEditor.setDataToModule()
        {
            //module.minGreen = minGreen.Value / 255.0 * 180.0;
            //module.maxGreen = maxGreen.Value / 255.0 * 180.0;
            //module.minVal = minVal.Value / 255.0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            // Go forward
            if (e.Button == MouseButtons.Left)
            right_motor = 1.0;
            left_motor = 1.0;
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
