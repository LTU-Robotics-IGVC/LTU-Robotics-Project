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
    public partial class ManualDriveForm : Form
    {
        double right_motor = 0.0;
        double left_motor = 0.0;

        public enum DriveSignal { FORWARD, BACKWARD, TRIGHT, SRIGHT, TLEFT, SLEFT, STOP };

        //SetSpeed.Value = (decimal)((module.def_speed * 100.0)/max_motor_speed);

        //module.right_motor_speed = module.def_speed * right_motor;
        //module.left_motor_speed = module.def_speed * left_motor;
        //module.def_speed = ((double)(SetSpeed.Value) * max_motor_speed) / 100.0;

        public ManualDriveForm()
        {
            InitializeComponent();
            label3.Text = "0.0";
            label4.Text = "0.0";            
        }

        public void SetSpeed(double data)
        {
            label5.Text = data.ToString();
            label6.Text = data.ToString();
        }

        public void Speed_control(DriveSignal d)
        {
            switch(d)
            {
                case DriveSignal.FORWARD:
                    right_motor = 1.0;
                    left_motor = 1.0;
                    break;
                case DriveSignal.BACKWARD:
                    right_motor = -1.0;
                    left_motor = -1.0;
                    break;
                case DriveSignal.TRIGHT:
                    right_motor = 1.0;
                    left_motor = -1.0;
                    break;
                case DriveSignal.TLEFT:
                    right_motor = -1.0;
                    left_motor = 1.0;
                    break;
                case DriveSignal.SRIGHT:
                    right_motor = 1.0;
                    left_motor = 0.5;
                    break;
                case DriveSignal.SLEFT:
                    right_motor = 0.5;
                    left_motor = 1.0;
                    break;
                case DriveSignal.STOP:
                    right_motor = 0.0;
                    left_motor = 0.0;
                    break;

            }
            UpdateDisplay();
        }

        private void UpdateDisplay ()
        {
            progressBar1.Value = (int)((double)progressBar1.Maximum * Math.Abs(right_motor));
            progressBar2.Value = (int)((double)progressBar1.Maximum * Math.Abs(left_motor));

            if (right_motor > 0)
                RMDirection.Text = "Forward";
            else if (right_motor < 0)
                RMDirection.Text = "Reverse";
            else
                RMDirection.Text = "Stopped";

            if (left_motor > 0)
                LMDirection.Text = "Forward";
            else if (left_motor < 0)
                LMDirection.Text = "Reverse";
            else
                LMDirection.Text = "Stopped";
        }

        private void Forward_MouseUp(object sender, MouseEventArgs e)
        {
            Speed_control(DriveSignal.STOP);
        }

        private void Forward_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Speed_control(DriveSignal.FORWARD);
            }
        }

        private void Bck_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Speed_control(DriveSignal.BACKWARD);
            }
        }

        private void Bck_MouseUp(object sender, MouseEventArgs e)
        {
            Speed_control(DriveSignal.STOP);
        }

        private void TRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Speed_control(DriveSignal.TRIGHT);
            }
        }

        private void TRight_MouseUp(object sender, MouseEventArgs e)
        {
            Speed_control(DriveSignal.STOP);
        }

        private void TLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Speed_control(DriveSignal.TLEFT);
            }
        }

        private void TLeft_MouseUp(object sender, MouseEventArgs e)
        {
            Speed_control(DriveSignal.STOP);
        }

        private void SRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Speed_control(DriveSignal.SRIGHT);
            }
        }

        private void SRight_MouseUp(object sender, MouseEventArgs e)
        {
            Speed_control(DriveSignal.STOP);
        }

        private void SLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Speed_control(DriveSignal.SLEFT);
            }
        }

        private void SLeft_MouseUp(object sender, MouseEventArgs e)
        {
            Speed_control(DriveSignal.STOP);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
