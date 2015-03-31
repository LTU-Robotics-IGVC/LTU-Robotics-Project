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
            this.Close();
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = module;
            this.label1.Text = "The module (" + MainWindow.instance.moduleNameDictionary[module] +
                ") \n does not have any \n changeable properties!";
        }

        void IModuleEditor.loadDataFromModule()
        {

        }

        void IModuleEditor.setDataToModule()
        {

        }
    }
}
