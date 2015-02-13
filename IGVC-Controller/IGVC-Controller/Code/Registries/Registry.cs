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

        public Registry()
        {
            //dataLogger = new DataLogger();
            modules = new List<IModule>();
        }

        public void register(IModule module)
        {
            if (!modules.Contains(module))
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
            {
                if (module.getSubscriptionList().Contains(tag))
                {
                    module.recieveDataFromRegistry(tag, data);
                }
            }
        }

        public void sendMessageToLogger(string tag, string severity, string message)
        {
            sendData(IModule.MODULE_TYPES.LOGGER_TYPE, new KeyValuePair<string, KeyValuePair<string, string>>(tag, new KeyValuePair<string, string>(severity, message)));
        }
    }
}
