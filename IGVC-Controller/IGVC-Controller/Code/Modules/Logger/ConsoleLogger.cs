using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Logger
{
    class ConsoleLogger : IModule
    {
        public ConsoleLogger() : base()
        {
            this.addSubscription(INTERMODULE_VARIABLE.LOG);
            this.addSubscription(INTERMODULE_VARIABLE.STATUS);
            this.setLogTag("console_logger");
            this.modulePriority = 1000;
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            Console.WriteLine((string)data);
        }
    }
}
