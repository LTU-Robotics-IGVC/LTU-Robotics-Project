using IGVC_Controller.Code.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Registries
{
    public class Registry
    {
        private List<IModule> modules;

        private BackgroundWorker worker = new BackgroundWorker();

        private bool shouldBeRunning = false;

        public Registry()
        {
            //dataLogger = new DataLogger();
            modules = new List<IModule>();

            worker.DoWork += this.runProcessModules;
            worker.WorkerSupportsCancellation = true;
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

        private void runProcessModules(object sender, DoWorkEventArgs e)
        {
            while (shouldBeRunning)
            {
                for (int i = 0; i < modules.Count; i++)
                {
                    try
                    {
                        modules[i].process();
                    }
                    catch (Exception)
                    {
                        this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "Module " + modules[i].moduleID + ":"
                            + MainWindow.instance.moduleNameDictionary[modules[i]] + " with priority "
                            + modules[i].modulePriority + " had to abort due to an unhandled exception");
                    }
                }
            }

            this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "REGISTRY is begining to SHUTDOWN");

            for(int i = 0; i < modules.Count; i++)
            {
                modules[i].shutdown();
            }

            this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "REGISTRY has SUCCESSFULLY SHUTDOWN");
        }

        /// <summary>
        /// Starts the registry running
        /// </summary>
        /// <returns>Returns false if registry failes to start</returns>
        public bool start()
        {
            bool isStarted = false;
            if(!shouldBeRunning)
            {
                this.shouldBeRunning = true;
                this.sortModulesByPriority();
                if(isStarted = this.startupModules())
                    worker.RunWorkerAsync();
            }

            return isStarted;
        }

        public void stop()
        {
            this.shouldBeRunning = false;
            this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "REGISTRY has been notified to SHUTDOWN");
        }

        public void sortModulesByPriority()
        {
            SortedList<int, IModule> sortedList = new SortedList<int, IModule>();
            foreach (IModule module in modules)
            {
                sortedList.Add(module.modulePriority, module);
            }

            modules = sortedList.Values.ToList<IModule>();
        }

        /// <summary>
        /// Runs the startup of each module and will return true if all startup successfully
        /// </summary>
        /// <returns></returns>
        private bool startupModules()
        {
            for(int i = 0; i < modules.Count; i++)
            {
                this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "Module " + modules[i].moduleID + ":"
                               + MainWindow.instance.moduleNameDictionary[modules[i]] + " with priority "
                               + modules[i].modulePriority + " is being INITIALIZED");
                if(modules[i].startup())
                {
                    this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "Module " + modules[i].moduleID + ":"
                            + MainWindow.instance.moduleNameDictionary[modules[i]] + " with priority "
                            + modules[i].modulePriority + " has SUCCESSFULLY INITIALIZED");
                }
                else
                {
                    this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "Module " + modules[i].moduleID + ":"
                            + MainWindow.instance.moduleNameDictionary[modules[i]] + " with priority "
                            + modules[i].modulePriority + " has FAILED to INITIALIZE");
                    
                    this.sendData(IModule.INTERMODULE_VARIABLE.STATUS, "SYSTEM is being ABORTED");

                    return false;
                }
            }

            return true;
        }
    }
}
