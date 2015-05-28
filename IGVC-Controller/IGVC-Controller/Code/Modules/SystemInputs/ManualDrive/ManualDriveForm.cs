using IGVC_Controller.DataIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.SystemInputs.ManualDrive
{
    public partial class ManualDriveForm : Form
    {
        double right_motor = 0.0;
        double left_motor = 0.0;

        double speed;

        bool moving;
        bool dynd = false;

        Keyboard k;

        public enum DriveSignal { FORWARD, BACKWARD, TRIGHT, SRIGHT, TLEFT, SLEFT, STOP };

        public ManualDriveForm()
        {
            InitializeComponent();
            label3.Text = "0.00";
            label4.Text = "0.00";
            k = new Keyboard(this);
            timer1.Interval = 10;
            timer1.Start();
            
        }

        public void SetSpeed(double data)
        {
            string s = string.Format("{0:N2}%", data);
            label5.Text = s;
            label6.Text = s;
            speed = data;
        }

        public void DynEnabled(bool data)
        {
            if (data)
            {
                label9.Text = "Dynamic Drive";
                dynd = true;
            }  
            else
            {
                label9.Text = "Three-State Drive";
                Bck.Enabled = false;
                TRight.Enabled = false;
                TLeft.Enabled = false;
                dynd = false;
            }
        }

        public double RightSpeed()
        {
            return right_motor;
        }

        public double LeftSpeed()
        {
            return left_motor;
        }

        private void Speed_control(DriveSignal d)
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
                    right_motor = -1.0;
                    left_motor = 1.0;
                    break;
                case DriveSignal.TLEFT:
                    right_motor = 1.0;
                    left_motor = -1.0;
                    break;
                case DriveSignal.SRIGHT:
                    right_motor = 0.5;
                    left_motor = 1.0;
                    break;
                case DriveSignal.SLEFT:
                    right_motor = 1.0;
                    left_motor = 0.5;
                    break;
                case DriveSignal.STOP:
                    right_motor = 0.0;
                    left_motor = 0.0;
                    break;

            }
            UpdateDisplay(right_motor, left_motor);
        }

        public void UpdateDisplay (double right, double left)
        {
            progressBar1.Value = (int)((double)progressBar1.Maximum * Math.Abs(right));
            progressBar2.Value = (int)((double)progressBar1.Maximum * Math.Abs(left));

            string r = string.Format("{0:N2}", right*speed);
            string l = string.Format("{0:N2}", left*speed);
            RSpeedBox.Text = r;
            LSpeedBox.Text = l;

            if (right > 0)
                RMDirection.Text = "Forward";
            else if (right < 0)
                RMDirection.Text = "Reverse";
            else
                RMDirection.Text = "Stopped";

            if (left > 0)
                LMDirection.Text = "Forward";
            else if (left < 0)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dynd)
            {
                if (k.isKeyDown(Keys.W))
                {
                    Speed_control(DriveSignal.FORWARD);
                    moving = true;
                }
                else if (k.isKeyDown(Keys.S))
                {
                    Speed_control(DriveSignal.BACKWARD);
                    moving = true;
                }
                else if (k.isKeyDown(Keys.A))
                {
                    Speed_control(DriveSignal.TLEFT);
                    moving = true;
                }
                else if (k.isKeyDown(Keys.D))
                {
                    Speed_control(DriveSignal.TRIGHT);
                    moving = true;
                }
                else if (k.isKeyDown(Keys.Q))
                {
                    Speed_control(DriveSignal.SLEFT);
                    moving = true;
                }
                else if (k.isKeyDown(Keys.E))
                {
                    Speed_control(DriveSignal.SRIGHT);
                    moving = true;
                }
                else if (moving == true)
                {
                    Speed_control(DriveSignal.STOP);
                    moving = false;
                }
            }
            else
            {
                if (k.isKeyDown(Keys.W))
                {
                    Speed_control(DriveSignal.FORWARD);
                    moving = true;
                }
                else if (k.isKeyDown(Keys.Q))
                {
                    Speed_control(DriveSignal.SLEFT);
                    moving = true;
                }
                else if (k.isKeyDown(Keys.E))
                {
                    Speed_control(DriveSignal.SRIGHT);
                    moving = true;
                }
                else if (moving == true)
                {
                    Speed_control(DriveSignal.STOP);
                    moving = false;
                }
            }
        }
    }
}
