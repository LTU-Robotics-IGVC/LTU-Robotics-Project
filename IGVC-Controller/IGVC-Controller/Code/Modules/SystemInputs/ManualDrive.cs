using IGVC_Controller.Code.DataIO;
using IGVC_Controller.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.SystemInputs
{
    class ManualDrive : IModule
    {
        //IGVC_Controller.DataIO.Keyboard key_input;

        //Remember that key_input needs to be bound to a Windows Form
        //A Windows Form using the Keyboard class will have abnormal behavior
        //if any text boxes are used as the Form and Keyboard will both listen
        //to keyboard events

        ManualDriveForm form;
        delegate void delegateCloseForm();

        delegate void delegateSetFormData(object data);
        private delegateSetFormData setFormDataDelegate;

        GatedVariable drive_on;
        GatedVariable right_motor_speed;
        GatedVariable left_motor_speed;
        GatedVariable Dyn_enabled;

        /// <summary>
        /// Default speed for arrow keys movement (in m/s; must be always below 5.00 m/s)
        /// </summary>
        public double def_speed = 3.00;

        /// <summary>
        /// Enables dynamic drive (true) or three state drive (false) 
        /// </summary>
        public bool dynamic_drive = false;

        ///// <summary>
        ///// Value (in m/s) to be sent to right motor
        ///// </summary>
        //public double right_motor_speed = 0.00;

        ///// <summary>
        ///// Value (in m/s) to be sent to left motor
        ///// </summary>
        //public double left_motor_speed = 0.00;

        public ManualDrive() : base()
        {
            //this.modulePriority = 90; // Should it be high priority in order to override automatic drive?
            //Its a system input so it should be in the Range of 0-25
            //Automatic drive will be disabled through the trajectory planner module
            this.modulePriority = 10; 

            this.addSubscription(INTERMODULE_VARIABLE.DRIVING_ENABLED);
            this.addSubscription(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT);
            this.addSubscription(INTERMODULE_VARIABLE.DYNAMIC_DRIVE_ENABLED);

            this.setFormDataDelegate = this.setFormData;
            //VisualizerForm a = new VisualizerForm();
            //Keyboard k = new Keyboard(a);
            //a.Show();
            //Keyboard a;
            //a.isKeyDown()
            
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.def_speed = config.Read<double>("def_speed", 3.00);
            this.dynamic_drive = config.Read<bool>("dynamic_drive", false);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<double> ("def_speed", this.def_speed);
            config.Write<bool>("dynamic_drive", this.dynamic_drive);

            base.writeToConfig(config);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            //base.recieveDataFromRegistry(tag, data);
            switch(tag)
            {
                case INTERMODULE_VARIABLE.DRIVING_ENABLED:
                    this.drive_on.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT:
                    this.left_motor_speed.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT:
                    this.right_motor_speed.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.DYNAMIC_DRIVE_ENABLED:
                    this.Dyn_enabled.setObject(data);
                    break;

            }
        }

        public override void process()
        {
            if (form.InvokeRequired)
            {
                form.Invoke(this.setFormDataDelegate, new object[] { drive_on.getObject() });
                form.Invoke(this.setFormDataDelegate, new object[] { right_motor_speed.getObject() });
                form.Invoke(this.setFormDataDelegate, new object[] { left_motor_speed.getObject() });
                form.Invoke(this.setFormDataDelegate, new object[] { Dyn_enabled.getObject() });
            }

            
            base.process();
        }

        public override bool startup()
        {
            drive_on = new GatedVariable();
            right_motor_speed = new GatedVariable();
            left_motor_speed = new GatedVariable();
            Dyn_enabled = new GatedVariable();

            form = new ManualDriveForm();
            
            
            form.Show();

            
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

        private void setFormData(object data)
        {
            //form.setLIDARData((List<long>)data);
            form.SetSpeed(def_speed);
            form.DynEnabled(dynamic_drive);
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new ManualDriveEditor();
        }

    }
}
