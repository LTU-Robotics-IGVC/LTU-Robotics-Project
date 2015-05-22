using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.GPS
{
    class GPSCoordinate
    {
        private float latitude;
        private float longitude;

        public GPSCoordinate(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
    }
}
