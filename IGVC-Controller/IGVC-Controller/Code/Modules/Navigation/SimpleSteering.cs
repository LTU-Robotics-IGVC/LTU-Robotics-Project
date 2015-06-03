using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Navigation
{
    class SimpleSteering : IModule
    {
        GatedVariable NavPath;

        public SimpleSteering() : base()
        {
            this.modulePriority = 62;
            this.addSubscription(INTERMODULE_VARIABLE.NAV_PATH);
        }
    }
}
