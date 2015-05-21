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
    public partial class MotorStartForm : Form
    {
        bool motors_on;

        public MotorStartForm()
        {
            InitializeComponent();
            motors_on = false;
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (!motors_on)
            {
                Start.Text = "MOTORS ON";
                Start.BackColor = Color.Green;
                motors_on = true;
            }
            else 
            {
                Start.Text = "MOTORS OFF";
                Start.BackColor = Color.Red;
                motors_on = false;
            }
        }

        public bool CheckStatus()
        {
            return motors_on;
        }
    }
}
