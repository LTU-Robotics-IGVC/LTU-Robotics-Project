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

namespace IGVC_Controller.Code.Modules.SystemInputs
{
    public partial class ManualDriveEditor : Form, IModuleEditor
    {
        ManualDrive module;

        double right_motor = 0.0;
        double left_motor = 0.0;

        /// <summary>
        /// Maximum speed motors can maintain
        /// </summary>
        const double  max_motor_speed = 5.0;

        public ManualDriveEditor()
        {
            InitializeComponent();
            Keyboard keyboard = new Keyboard(this);
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
            SetSpeed.Value = (decimal)((module.def_speed * 100.0)/max_motor_speed);
  
        }

        void IModuleEditor.setDataToModule()
        {
            //module.minGreen = minGreen.Value / 255.0 * 180.0;
            //module.maxGreen = maxGreen.Value / 255.0 * 180.0;
            //module.minVal = minVal.Value / 255.0;
            module.right_motor_speed = module.def_speed * right_motor;
            module.left_motor_speed = module.def_speed * left_motor;
            module.def_speed = ((double)(SetSpeed.Value) * max_motor_speed) / 100.0;
        }

        private void Fwd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Both motors same speed forward
                right_motor = 1.0;
                left_motor = 1.0;
            }
            ((IModuleEditor)this).setDataToModule();
        }

        private void Fwd_MouseUp(object sender, MouseEventArgs e)
        {
            right_motor = 0.0;
            left_motor = 0.0;
            ((IModuleEditor)this).setDataToModule();
        }

        private void Bck_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Both motors full speed backwards
                right_motor = -1.0;
                left_motor = -1.0;
            }
            ((IModuleEditor)this).setDataToModule();
        }

        private void Bck_MouseUp(object sender, MouseEventArgs e)
        {
            right_motor = 0.0;
            left_motor = 0.0;
            ((IModuleEditor)this).setDataToModule();
        }


        private void TRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Right motor forward, left motor backwards (Quick turn)
                right_motor = 1.0;
                left_motor = -1.0;
            }
            ((IModuleEditor)this).setDataToModule();
        }

        private void TRight_MouseUp(object sender, MouseEventArgs e)
        {
            right_motor = 0.0;
            left_motor = 0.0;
            ((IModuleEditor)this).setDataToModule();
        }

        private void SRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Slow down left motor to swerve right
                right_motor = 1.0;
                left_motor = 0.5;
            }
            ((IModuleEditor)this).setDataToModule();
        }

        private void SRight_MouseUp(object sender, MouseEventArgs e)
        {
            right_motor = 0.0;
            left_motor = 0.0;
            ((IModuleEditor)this).setDataToModule();
        }

        private void SLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                right_motor = 0.5;
                left_motor = 1.0;
            }
            ((IModuleEditor)this).setDataToModule();
        }

        private void SLeft_MouseUp(object sender, MouseEventArgs e)
        {
            right_motor = 0.0;
            left_motor = 0.0;
            ((IModuleEditor)this).setDataToModule();
        }

        private void TLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                right_motor = -1.0;
                left_motor = 1.0;
            }
            ((IModuleEditor)this).setDataToModule();
        }

        private void TLeft_MouseUp(object sender, MouseEventArgs e)
        {
            right_motor = 0.0;
            left_motor = 0.0;
            ((IModuleEditor)this).setDataToModule();
        }

        private void SetSpeed_ValueChanged(object sender, EventArgs e)
        {
            ((IModuleEditor)this).setDataToModule();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            right_motor = 0.0;
            left_motor = 0.0;
            ((IModuleEditor)this).setDataToModule();
            this.Close();
        }

        
    }
}
