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
        }

        public void start()
        {
            if(!shouldBeRunning)
            {
                this.shouldBeRunning = true;
                this.sortModulesByPriority();
                worker.RunWorkerAsync();
            }
        }

        public void stop()
        {
            this.shouldBeRunning = false;
            worker.CancelAsync();
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
    }
}
