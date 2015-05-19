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
        IGVC_Controller.DataIO.Keyboard key_input;

        GatedVariable drive_on;

        // Default speed for arrow keys movement
        public double def_speed = 3.00;

        public ManualDrive() : base()
        {
            this.modulePriority = 90; // Should it be high priority in order to override automatic drive?
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
    }
}
