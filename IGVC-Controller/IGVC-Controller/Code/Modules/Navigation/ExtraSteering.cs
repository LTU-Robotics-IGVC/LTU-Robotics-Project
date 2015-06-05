using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Navigation
{
    class ExtraSteering : IModule
    {
        GatedVariable obstacles;

        public ExtraSteering() : base()
        {
            this.modulePriority = 70;
            this.addSubscription(INTERMODULE_VARIABLE.COLLISION_IMAGE);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.COLLISION_IMAGE:
                    obstacles.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            obstacles = new GatedVariable();
            return base.startup();
        }

        public override void shutdown()
        {
            base.shutdown();
        }

        public override void process()
        {
            obstacles.shiftObject();

            Image<Gray, byte> img = (Image<Gray, byte>)obstacles.getObject();
            if(img != null)
            {
                img.Resize(300, 300, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                
            }

            this.sendDataToRegistry(INTERMODULE_VARIABLE.COLLISION_IMAGE, img);

            base.process();
        }
    }
}
