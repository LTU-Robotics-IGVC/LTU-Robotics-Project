using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.Modules.Visualizer.GPS;

namespace IGVC_Controller.Code.Modules.Visualizer.GPS
{
    class GPS_Visualizer : IModule
    {
        GatedVariable GPSdata = new GatedVariable();
        GPS_Visualizer_Form GPSform;

        delegate void delegateSetGPSdata(object data);
        private delegateSetGPSdata setFormDataDelegate;

        delegate void delegateCloseForm();

        public GPS_Visualizer()
        {
            this.addSubscription(INTERMODULE_VARIABLE.GPS_COORDS);
            this.modulePriority = 99;
            this.setFormDataDelegate = this.setFormData;
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            if (tag == INTERMODULE_VARIABLE.GPS_COORDS)
                GPSdata.setObject(data);
        }

        public override bool startup()
        {
            GPSdata = new GatedVariable();
            GPSform = new GPS_Visualizer_Form();
            GPSform.Show();
            return base.startup();
        }

        public override void process()
        {
            GPSdata.shiftObject();
            if (GPSdata.getObject() == null)
                return;

            if(GPSform.InvokeRequired)
            {
                GPSform.Invoke(this.setFormDataDelegate, new object[] { GPSdata.getObject() });
            }
            base.process();
        }

        public override void shutdown()
        {
            delegateCloseForm closeform = this.closeForm;
            GPSform.Invoke(closeform, null);
            base.shutdown();
        }

        private void setFormData(object data)
        {
            GPSform.setGPSData((string)data);
        }

        private void closeForm()
        {
            GPSform.Close();
        }
    }
}
