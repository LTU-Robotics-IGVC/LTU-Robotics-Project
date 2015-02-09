using IGVC_Controller.Code.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Registries
{
    class Registry
    {
        private List<IModule> modules;

        private object dataLogger;

        public Registry()
        {
            //dataLogger = new DataLogger();
            modules = new List<IModule>();
        }

        public void register(IModule module)
        {
            if (!modules.Contains(module))
                modules.Add(module);
            modules.Add(module);

            //give access to this registry to the module
            module.bindRegistry(this);
        }

        public void unbindModule(IModule module)
        {
            if (modules.Contains(module))
                modules.Remove(module);

            //remove access to this registry from the module
            module.unbindRegistry();
        }

        public void sendData(string tag, object data)
        {
            foreach (IModule module in modules)
                module.recieveDataFromRegistry(tag, data);
        }

        public void sendMessageToLogger(string tag, string message)
        {
            //this.dataLogger.sendMessage(string tag, string message);
        }
    }
}
