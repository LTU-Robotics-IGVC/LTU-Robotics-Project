﻿using IGVC_Controller.Code.DataIO;
using IGVC_Controller.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.SystemInputs.ManualDrive
{
    class ManualDrive : IModule
    {
        ManualDriveForm form;
        delegate void delegateCloseForm();

        delegate void delegateSetFormData(bool motorEnable);
        private delegateSetFormData setFormDataDelegate;

        GatedVariable drive_on;
        //GatedVariable right_speed;
        //GatedVariable left_speed;
        GatedVariable Dyn_enabled;

        /// <summary>
        /// Default speed for arrow keys movement (in m/s; must be always below 5.00 m/s)
        /// </summary>
        public double def_speed = 3.0;

        /// <summary>
        /// Value (in m/s) to be sent to right motor
        /// </summary>
        public double right_motor_speed = 0.00;

        /// <summary>
        /// Value (in m/s) to be sent to left motor
        /// </summary>
        public double left_motor_speed = 0.00;

        public ManualDrive() : base()
        {
            //Its a system input so it should be in the Range of 0-25
            //Automatic drive will be disabled through the trajectory planner module
            this.modulePriority = 10; 

            this.addSubscription(INTERMODULE_VARIABLE.DRIVING_ENABLED);
            //---------------------------------------------------------------
            //We do not need to listen for speed
            //this.addSubscription(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT);
            //this.addSubscription(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT);
            this.addSubscription(INTERMODULE_VARIABLE.DYNAMIC_DRIVE_ENABLED);

            this.setFormDataDelegate = this.setFormData;
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.def_speed = config.Read<double>("def_speed", 3.00);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<double> ("def_speed", this.def_speed);

            base.writeToConfig(config);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.DRIVING_ENABLED:
                    this.drive_on.setObject(data);
                    break;
                //-----------------------------------------------
                    //We do not need to listen for speed
                //case INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT:
                //    this.left_speed.setObject(data);
                //    break;
                //case INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT:
                //    this.right_speed.setObject(data);
                //    break;
                case INTERMODULE_VARIABLE.DYNAMIC_DRIVE_ENABLED:
                    this.Dyn_enabled.setObject(data);
                    break;

            }
        }

        public override void process()
        {          
            //Need to call the .shiftObject() first
            drive_on.shiftObject();
            Dyn_enabled.shiftObject();

            //We know an Invoke will be required for form
            //but it is good coding practice to have both true and false cases
            //handled

            if (Dyn_enabled.getObject() == null)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.DYNAMIC_DRIVE_ENABLED, false);
            }

            if (form.InvokeRequired)
            {
                //form.Invoke(this.setFormDataDelegate, new object[] { right_speed.getObject() });
                //form.Invoke(this.setFormDataDelegate, new object[] { left_speed.getObject() });
                form.Invoke(this.setFormDataDelegate, new object[] { (bool)Dyn_enabled.getObject() });
            }
            else
            {
                form.DynEnabled((bool)Dyn_enabled.getObject());
            }

            left_motor_speed = form.LeftSpeed();
            right_motor_speed = form.RightSpeed();

            this.sendDataToRegistry(INTERMODULE_VARIABLE.ESTOP_RIGHT, form.RightEstop());
            this.sendDataToRegistry(INTERMODULE_VARIABLE.ESTOP_LEFT, form.LeftEstop());
            
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, left_motor_speed);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, right_motor_speed);

            base.process();
        }

        public override bool startup()
        {
            drive_on = new GatedVariable();
            //right_speed = new GatedVariable();
            //left_speed = new GatedVariable();
            Dyn_enabled = new GatedVariable();
            Dyn_enabled.setObject(true);

            form = new ManualDriveForm();

            form.Show();
            form.SetSpeed(def_speed);
            
            return base.startup();
        }

        public override void shutdown()
        {
            if (form.InvokeRequired)
            {
                delegateCloseForm del = this.closeForm;
                form.Invoke(del, null);
            }
            else
            {
                form.Close();
            }

            base.shutdown();
        }


        private void closeForm()
        {
            form.Close();
        }

        private void setFormData(bool motorEnable)
        {        
            form.DynEnabled(motorEnable);
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new ManualDriveEditor();
        }
    }
}
