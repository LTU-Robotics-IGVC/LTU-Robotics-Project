using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Vision
{
    class RobotBlackoutFiltering : IModule
    {
        GatedVariable leftImage;
        GatedVariable rightImage;

        public RobotBlackoutFiltering() : base()
        {
            this.modulePriority = 27;
            this.addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.VISION_LEFT:
                    leftImage.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.VISION_RIGHT:
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

            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)leftImage.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)rightImage.getObject();

            if(leftColor != null && rightColor != null)
            {
                //Place filters here

                leftColor = ImageFiltering.BlackoutInverted(leftColor, new Rectangle(50, 180, 220, 60));
                rightColor = ImageFiltering.BlackoutInverted(rightColor, new Rectangle(50, 180, 220, 60));
            }

            this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_LEFT, leftColor);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_RIGHT, rightColor);

            base.process();
        }
    }
}
