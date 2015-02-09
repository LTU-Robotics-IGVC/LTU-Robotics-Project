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
        private List<IModule> interfaceModules;
        private List<IModule> navigationModules;
        private List<IModule> mappingModules;
        private List<IModule> positioningModules;
        private List<IModule> visionModules;

        private object dataLogger;

        public Registry()
        {
            //dataLogger = new DataLogger();
        }

        public void register(IModule module)
        {
            //Maybe we can use types so there only needs to be on register function
            Type type = module.GetType();

            if(type == typeof(IModule))
            {
                
            }

            //give access to this registry to the module
            module.bindRegistry(this);
        }

        public void unbindModule(IModule module)
        {
            //remove module from correct list

            //remove access to this registry from the module
            module.unbindRegistry();
        }

        public void sendData(string tag, object data)
        {
            foreach (IModule module in interfaceModules)
                module.recieveDataFromRegistry(tag, data);

            foreach (IModule module in navigationModules)
                module.recieveDataFromRegistry(tag, data);

            foreach (IModule module in mappingModules)
                module.recieveDataFromRegistry(tag, data);

            foreach (IModule module in positioningModules)
                module.recieveDataFromRegistry(tag, data);

            foreach (IModule module in visionModules)
                module.recieveDataFromRegistry(tag, data);
        }

        public void sendMessageToLogger(string tag, string message)
        {
            //this.dataLogger.sendMessage(string tag, string message);
        }
    }
}
