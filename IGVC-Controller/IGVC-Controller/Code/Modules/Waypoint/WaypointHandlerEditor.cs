using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGVC_Controller.Code.MathX;

namespace IGVC_Controller.Code.Modules.Waypoint
{
    public partial class WaypointHandlerEditor : Form, IModuleEditor
    {
        WaypointHandler module;

        public WaypointHandlerEditor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text + textBox2.Text);
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (WaypointHandler)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {

        }

        void IModuleEditor.setDataToModule()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ((IModuleEditor)this).setDataToModule();
            this.Close();
        }
    }
}
