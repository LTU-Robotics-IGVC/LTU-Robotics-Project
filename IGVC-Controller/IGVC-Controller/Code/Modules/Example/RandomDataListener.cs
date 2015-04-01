using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Example
{
    class RandomDataListener : IModule
    {
        GatedVariable gated_x = new GatedVariable();

        public RandomDataListener() : base()
        {
            this.addSubscription(INTERMODULE_VARIABLE.EXAMPLE);
            this.modulePriority = 75;
        }

        public override void process()
        {
            //Load the latest version of the variable to the front
            gated_x.shiftObject();

            //Pull the latest version of the variable
            double x = (double)gated_x.getObject();

            //Send results as it is ready
            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "Processing x");

            //Run some work on the variable
            x = ((x * x) / x) + x - x;

            //Send the results as it is ready
            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "x = " + x.ToString("N"));

            base.process();
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            this.gated_x.setObject(data);
        }
    }
}
