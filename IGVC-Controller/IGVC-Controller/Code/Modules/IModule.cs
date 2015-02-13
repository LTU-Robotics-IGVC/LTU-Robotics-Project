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
        public struct MODULE_TYPES {
            public static string VISION_TYPE = "vision";
            public static string NAVIGATION_TYPE = "navigation";
            public static string LOGGER_TYPE = "logger";
            public static string ROBOT_INTERFACE_TYPE = "robot";
            public static string ALL_TYPE = "all";
        }

        public struct LOG_TYPES
        {
            public static string SEVERITY_INFO = "info";
            public static string SEVERITY_WARNING = "warning";
            public static string SEVERITY_ERROR = "error";
            public static string SEVERITY_CRITITCAL = "critical";
        }

        private string tag = "undefined";

        private List<string> subscriptionList = new List<string>(1);

        static private int nextModuleID = 0;
        protected Registry registry { get; private set; }

        public int moduleID { get; private set; }

        public IModule()
        {
            moduleID = nextModuleID;
            nextModuleID++;
        }

        public void addSubscription(string type) {
            this.subscriptionList.Add(type); 
        }
        public List<string> getSubscriptionList() { return subscriptionList; }

        protected void setLogTag(string tag) {
            this.tag = tag;
        }

        public void bindRegistry(Registry registry) { this.registry = registry; }
        public void unbindRegistry() { this.registry = null; }
        public bool isBoundToRegistry() { return this.registry != null; }

        virtual public void recieveDataFromRegistry(string tag, object data) {}

        private void sendDataToRegistry(string tag, object data)
        {
            if(isBoundToRegistry())
                this.registry.sendData(tag, data);
        }

        protected void log(string severity, string message)
        {
            if (isBoundToRegistry())
                this.registry.sendMessageToLogger(tag, severity, message);
        }
    }
}
