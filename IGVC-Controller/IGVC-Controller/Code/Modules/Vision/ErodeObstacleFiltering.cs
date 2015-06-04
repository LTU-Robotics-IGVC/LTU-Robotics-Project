using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Vision
{
    class ErodeObstacleFiltering : IModule
    {
        GatedVariable leftImage;
        GatedVariable rightImage;

        public ErodeObstacleFiltering() : base()
        {
            this.modulePriority = 52;
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT:
                    leftImage.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT:
                    rightImage.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            leftImage = new GatedVariable();
            rightImage = new GatedVariable();
            return base.startup();
        }

        public override void process()
        {
            leftImage.shiftObject();
            rightImage.shiftObject();

            Image<Gray, byte> leftGray = (Image<Gray, byte>)leftImage.getObject();
            Image<Gray, byte> rightGray = (Image<Gray, byte>)rightImage.getObject();

            if(leftGray != null && rightGray != null)
            {
                //Place filters here

                leftGray = leftGray.Erode(2);
                rightGray = rightGray.Erode(2);
            }

            this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftGray);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightGray);

            base.process();
        }
    }
}
