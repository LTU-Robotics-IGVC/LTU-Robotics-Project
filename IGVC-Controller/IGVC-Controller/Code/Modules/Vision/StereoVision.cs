using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGVC_Controller.Code.Registries;

namespace IGVC_Controller.Code.Modules.Vision
{
    class StereoVision : IModule
    {
        public StereoVision() : base()
        {
            setLogTag("Vision");
            addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
        }
    }
}
