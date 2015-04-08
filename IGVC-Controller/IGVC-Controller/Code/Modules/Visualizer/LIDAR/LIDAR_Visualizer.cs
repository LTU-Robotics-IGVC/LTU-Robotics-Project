using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.Modules.Visualizer.LIDAR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Visualizer
{
    class LIDAR_Visualizer : IModule
    {
        GatedVariable LIDARdata = new GatedVariable();
        LIDAR_Visualizer_Form form;// = new LIDAR_Visualizer_Form();

        public LIDAR_Visualizer()
        {
            this.addSubscription(INTERMODULE_VARIABLE.LIDAR_RAW);
            this.modulePriority = 100;
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            if (tag == INTERMODULE_VARIABLE.LIDAR_RAW)
                LIDARdata.setObject(data);
        }

        public override void process()
        {
            LIDARdata.shiftObject();
            this.form.setLIDARData((List<long>)LIDARdata.getObject());
            base.process();
        }

        public override bool startup()
        {
            form = new LIDAR_Visualizer_Form();
            form.Show();

            return base.startup();
        }

        public override void shutdown()
        {
            form.Close();

            base.shutdown();
        }
    }
}
