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

        /// <summary>
        /// Called to bind a module to this registry
        /// <para>This will bind this registry to the module as well</para>
        /// </summary>
        /// <param name="module"></param>
        public void register(IModule module)
        {
            if (!modules.Contains(module))
                modules.Add(module);

            //give access to this registry to the module
            module.bindRegistry(this);
        }

        /// <summary>
        /// Called to unbind a module from this registry
        /// <para>This will unbind this registry from the module as well</para>
        /// </summary>
        /// <param name="module"></param>
        public void unbindModule(IModule module)
        {
            if (modules.Contains(module))
                modules.Remove(module);

            //remove access to this registry from the module
            module.unbindRegistry();
        }

        public void sendData(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            foreach (IModule module in modules)
            {
                if (module.getSubscriptionList().Contains(tag))
                {
                    module.recieveDataFromRegistry(tag, data);
                }
            }
        }

        public void sendMessageToLogger(string logTag, string severity, string message)
        {
            sendData(IModule.INTERMODULE_VARIABLE.LOG, new KeyValuePair<string, KeyValuePair<string, string>>(logTag, new KeyValuePair<string, string>(severity, message)));
        }
    }
}
