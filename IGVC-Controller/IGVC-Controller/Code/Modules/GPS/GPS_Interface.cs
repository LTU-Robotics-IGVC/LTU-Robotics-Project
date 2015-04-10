using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.GPS
{
    class GPS_Interface : IModule
    {
        public string port_name = "COM1";
        public int baudrate = 115200;

        public GPS_Interface()
        {
            this.modulePriority = 75;
        }
    }
}
