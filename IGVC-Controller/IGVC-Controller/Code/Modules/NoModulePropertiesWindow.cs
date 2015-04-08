using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules
{
    public partial class NoModulePropertiesWindow : Form, IModuleEditor
    {
        IModule module;

        public NoModulePropertiesWindow()
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
            this.module = module;
            this.label1.Text = "The module (" + MainWindow.instance.moduleNameDictionary[module] +
                ") \n does not have any \n changeable properties!";
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            this.PriorityBox.Value = module.modulePriority;
        }

        void IModuleEditor.setDataToModule()
        {
            module.modulePriority = (int)PriorityBox.Value;
        }

        private void PriorityBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
