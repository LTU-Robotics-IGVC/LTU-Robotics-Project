using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGVC_Controller.Code.Modules;

namespace IGVC_Controller.Code.Modules.Visualizer.GeneralVisualizer
{
    class GeneralVisualizer : IModule
    {
        Dictionary<INTERMODULE_VARIABLE, GatedVariable> channels;

        GeneralVisualizerForm form;

        delegate void delegateCloseForm();

        delegate void delegateSetFormData(INTERMODULE_VARIABLE var, object data);
        private delegateSetFormData setFormDataDelegate;

        public GeneralVisualizer() : base()
        {
            this.modulePriority = 99;

            channels = new Dictionary<INTERMODULE_VARIABLE, GatedVariable>();
            this.addChannel(INTERMODULE_VARIABLE.VISION_LEFT);
            this.addChannel(INTERMODULE_VARIABLE.VISION_RIGHT);
            this.addChannel(INTERMODULE_VARIABLE.COLLISION_IMAGE);
            this.addChannel(INTERMODULE_VARIABLE.LIDAR_RAW);
            this.addChannel(INTERMODULE_VARIABLE.NAV_MESH);
            this.addChannel(INTERMODULE_VARIABLE.NAV_PATH);
            this.addChannel(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT);
            this.addChannel(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT);
            this.addChannel(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT);
            this.addChannel(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT);
            this.addChannel(INTERMODULE_VARIABLE.COMPASS);
            this.addChannel(INTERMODULE_VARIABLE.CURRENT_WAYPOINT);
            this.addChannel(INTERMODULE_VARIABLE.GPS_COORDS);
            this.addChannel(INTERMODULE_VARIABLE.WAYPOINT_DISTANCE);
            this.addChannel(INTERMODULE_VARIABLE.WAYPOINT_HEADING);

            this.setFormDataDelegate = this.setFormData;
        }

        private void addChannel(INTERMODULE_VARIABLE variable)
        {
            this.addSubscription(variable);
            this.channels.Add(variable, new GatedVariable());
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            channels[tag].setObject(data);
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            foreach(INTERMODULE_VARIABLE key in channels.Keys)
            {
                channels[key].setObject(null);
            }

                form = new GeneralVisualizerForm();
            form.Show();
            return base.startup();
        }

        public override void process()
        {
            foreach(INTERMODULE_VARIABLE key in channels.Keys)
            {
                channels[key].shiftObject();
                object obj = channels[key].getObject();
                form.Invoke(this.setFormDataDelegate, new object[] { key, obj });
            }

            this.sendDataToRegistry(INTERMODULE_VARIABLE.DRIVING_ENABLED, form.motorsEnabled);
        }

        public override void shutdown()
        {
            delegateCloseForm del = this.closeForm;
            form.Invoke(del, null);
            base.shutdown();
        }

        private void closeForm()
        {
            form.Close();
        }

        private void setFormData(INTERMODULE_VARIABLE var, object data)
        {
            if(data != null)
                form.setData(var, data);
        }
    }
}
