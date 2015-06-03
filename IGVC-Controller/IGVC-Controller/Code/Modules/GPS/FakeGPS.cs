using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.GPS
{
    class FakeGPS : IModule
    {
        public FakeGPS() : base()
        {
            this.modulePriority = 11;
        }

        public override void process()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.GPS_RELATIVE_VECTOR2, new Vector2(0, 5));
            base.process();
        }
    }
}
