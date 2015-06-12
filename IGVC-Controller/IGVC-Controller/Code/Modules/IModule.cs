using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGVC_Controller.Code.Registries;
using IGVC_Controller.DataIO;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules
{
    /// <summary>
    /// Module base class that all module types will inherit from
    /// </summary>
    public class IModule
    {
        //public struct module_types {
        //    public static string vision_type = "vision";
        //    public static string navigation_type = "navigation";
        //    public static string logger_type = "logger";
        //    public static string robot_interface_type = "robot";
        //    public static string all_type = "all";
        //}

        public enum INTERMODULE_VARIABLE
        {
            /// <summary>
            /// Represents the raw image feed from the LEFT stereo camera
            /// <para>Source : Robot Interface</para>
            /// <para>Format : Image(Bgr, Byte)</para>
            /// </summary>
            VISION_LEFT,

            /// <summary>
            /// Represents the raw image feed from the RIGHT stereo camera
            /// <para>Source : Robot Interface</para>
            /// <para>Format : Image(Bgr, Byte)</para>
            /// </summary>
            VISION_RIGHT,

            /// <summary>
            /// Represents a binary image that holds marked obstacles
            /// <para>Source : ObstacleDetection</para>
            /// <para>Format : Image(Gray, Byte)</para>
            /// </summary>
            OBSTACLE_IMAGE_RIGHT,

            /// <summary>
            /// Represents the raw image feed from the RIGHT stereo camera
            /// <para>Source : ObstacleDetection</para>
            /// <para>Format : Image(Gray, Byte)</para>
            /// </summary>
            OBSTACLE_IMAGE_LEFT,

            /// <summary>
            /// Represents the stitched image from the left and right camera
            /// <para>Source : WideAngleMonoVision</para>
            /// <para>Format : Image(Bgr, Byte)</para>
            /// </summary>
            STITCHED_IMAGE,

            /// <summary>
            /// Represents the GPS coordinates in a format to be determined
            /// <para>Source : Robot Interface</para>
            /// <para>Format : unkown</para>
            /// </summary>
            GPS_COORDS,

            /// <summary>
            /// Variable is intended to be stored by a data logger
            /// <para>Source : Any Module</para>
            /// <para>Format : string</para>
            /// </summary>
            LOG,

            /// <summary>
            /// Variable that should be indicated to the user but is not necessarily critical
            /// <para>Source : Any Module</para>
            /// <para>Format : string</para>
            /// </summary>
            STATUS,

            /// <summary>
            /// Variable that contains distance information (mm) per angle (in system units)
            /// <para>Source : LIDAR Interfaces</para>
            /// <para>Format : List(long) : distances[angles]</para>
            /// </summary>
            LIDAR_RAW,

            /// <summary>
            /// Variable that indicates if the robot is allowed to drive
            /// <para>Source : Any module that wishes to activate the motors</para>
            /// <para>Format : bool</para>
            /// </summary>
            DRIVING_ENABLED,

            /// <summary>
            /// This is just to be used for module example
            /// </summary>
            EXAMPLE,

            /// <summary>
            /// Variable that holds a combined image from the collision detection
            /// <para>that exists on the world plane</para>
            /// </summary>
            COLLISION_IMAGE,

            MOTOR_SPEED_LEFT,

            MOTOR_SPEED_RIGHT,

            /// <summary>
            /// True if dynamic drive is working; false for three state drive
            /// </summary>
            DYNAMIC_DRIVE_ENABLED,

            NAV_MESH,

            NAV_PATH,

            GPS_RELATIVE_VECTOR2,

            IS_AUTONOMOUS,

            ESTOP_RIGHT,

            ESTOP_LEFT,

            COMPASS,

            CURRENT_WAYPOINT,

            WAYPOINT_DISTANCE,

            WAYPOINT_HEADING,

            GPS_IS_STEERING,

            LANE_FOLLOWING,

            NO_MANS_LAND
        }

        public struct LOG_TYPES
        {
            public static string SEVERITY_INFO = "info";
            public static string SEVERITY_WARNING = "warning";
            public static string SEVERITY_ERROR = "error";
            public static string SEVERITY_CRITITCAL = "critical";
        }

        private List<INTERMODULE_VARIABLE> subscriptionList = new List<INTERMODULE_VARIABLE>(1);

        static private int nextModuleID = 0;
        protected Registry registry { get; private set; }

        string logTag = "unkown";

        public int moduleID { get; private set; }

        public int modulePriority;

        public IModule()
        {
            moduleID = nextModuleID;
            nextModuleID++;
            this.modulePriority = 100;
        }

        public void addSubscription(INTERMODULE_VARIABLE tag) {
            this.subscriptionList.Add(tag); 
        }
        public List<INTERMODULE_VARIABLE> getSubscriptionList() { return subscriptionList; }

        protected void setLogTag(string logTag) {
            this.logTag = logTag;
        }

        /// <summary>
        /// This will set the registry that the module will send its data to
        /// <para>This should never have to be called outside of a registry</para>
        /// </summary>
        /// <param name="registry"></param>
        public void bindRegistry(Registry registry) { this.registry = registry; }

        /// <summary>
        /// This will remove the registry that the module will send its data to
        /// <para>This should never have to be called outside of a registry</para>
        /// </summary>
        public void unbindRegistry() { this.registry = null; }

        /// <summary>
        /// Checks if the module actually is registered to a registry
        /// </summary>
        /// <returns></returns>
        public bool isBoundToRegistry() { return this.registry != null; }

        virtual public void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data) {}

        protected void sendDataToRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            if(isBoundToRegistry())
                this.registry.sendData(tag, data);
        }

        protected void log(string severity, string message)
        {
            if (isBoundToRegistry())
                this.registry.sendMessageToLogger(this.logTag, severity, message);
        }

        virtual public void loadFromConfig(SaveFile config) { this.modulePriority = config.Read<int>("priority", this.modulePriority); }

        virtual public void writeToConfig(SaveFile config) { config.Write<int>("priority", this.modulePriority); }

        virtual public void process() { }
       
        /// <summary>
        /// Called when the registry starts up before it begins to process the modules
        /// </summary>
        /// <returns>Returns true if startup is successful</returns>
        virtual public bool startup() { return true; }

        /// <summary>
        /// Called when the registry stops processing the modules
        /// </summary>
        virtual public void shutdown() { }

        virtual public Form getEditorForm()
        {
            return new NoModulePropertiesWindow();
        }
    }
}
