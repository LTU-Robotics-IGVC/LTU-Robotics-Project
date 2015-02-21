﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGVC_Controller.Code.Registries;

namespace IGVC_Controller.Code.Modules.Vision
{
    class Vision : IModule
    {
        public Vision() : base()
        {
            setLogTag("Vision");
            addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
        }
    }
}
