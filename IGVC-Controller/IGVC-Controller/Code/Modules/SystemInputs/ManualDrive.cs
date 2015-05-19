using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.SystemInputs
{
    class ManualDrive : IModule
    {
        //IGVC_Controller.DataIO.Keyboard key_input;

        //Remember that key_input needs to be bound to a Windows Form
        //A Windows Form using the Keyboard class will have abnormal behavior
        //if any text boxes are used as the Form and Keyboard will both listen
        //to keyboard events

        GatedVariable drive_on;

        // Default speed for arrow keys movement
        public double def_speed = 3.00;

        //Variables of speeds to send to each motor
        public double right_motor_speed = 0.00;
        public double left_motor_speed = 0.00;

        public ManualDrive() : base()
        {
            //this.modulePriority = 90; // Should it be high priority in order to override automatic drive?
            //Its a system input so it should be in the Range of 0-25
            //Automatic drive will be disabled through the trajectory planner module
            this.modulePriority = 10; 

            this.addSubscription(INTERMODULE_VARIABLE.DRIVING_ENABLED);
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
            //base.recieveDataFromRegistry(tag, data);
            switch(tag)
            {
                case INTERMODULE_VARIABLE.DRIVING_ENABLED:
                    this.drive_on.setObject(data);
                    break;
            }
        }

        public override bool startup()
        {
            drive_on = new GatedVariable();

            return base.startup();
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new ManualDriveEditor();
        }
    }
}
