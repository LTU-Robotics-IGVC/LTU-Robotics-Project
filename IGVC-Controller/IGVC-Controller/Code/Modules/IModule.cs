using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGVC_Controller.Code.Registries;

namespace IGVC_Controller.Code.Modules
{
    /// <summary>
    /// Module base class that all module types will inherit from
    /// </summary>
    class IModule
    {
        static private int nextModuleID = 0;
        protected Registry registry { get; private set; }

        public int moduleID { get; private set; }

        public IModule()
        {
            moduleID = nextModuleID;
        }

        public void bindRegistry(Registry registry) { this.registry = registry; }
        public void unbindRegistry() { this.registry = null; }
        public bool isBoundToRegistry() { return this.registry != null; }

        public void recieveDataFromRegistry(string tag, object data);

        public void sendDataToRegistry(string tag, object data)
        {
            if(isBoundToRegistry())
                this.registry.sendData(tag, data);
        }

        public void sendLogMessageToRegistry(string tag, string message)
        {
            if (isBoundToRegistry())
                this.registry.sendMessageToLogger(tag, message);
        }
    }
}
