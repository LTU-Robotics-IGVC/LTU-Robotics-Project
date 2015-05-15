using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.MathX;

namespace IGVC_Controller.Code.Modules.Vision
{
    class DualVisionObstacleDetection : IModule
    {
        GatedVariable visionleft;
        GatedVariable visionright;

        public DualVisionObstacleDetection() : base()
        {
            this.modulePriority = 51;
            this.addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
        }

        public override void process()
        {
            visionleft.shiftObject();
            visionright.shiftObject();

            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)visionleft.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)visionright.getObject();

            if(leftColor != null)
            {
                Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
                Image<Gray, byte> leftObstacles = new Image<Gray, byte>(leftGray.Width, leftGray.Height);

                //Process

                //This is an example until the filters can be figured out (should filter out green and dark colors)
                Image<Hsv, byte> leftHSV = leftColor.Convert<Hsv, byte>();
                leftObstacles = leftObstacles.Add(ImageFiltering.HSVFilter(new Hsv(50.0, 0, 0.25), new Hsv(70.0, 1.0, 1.0), leftHSV));

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftObstacles);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_LEFT, leftObstacles.Convert<Bgr, byte>());
            }

            if(rightColor != null)
            {
                Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();
                Image<Gray, byte> rightObstacles = new Image<Gray, byte>(rightGray.Width, rightGray.Height);

                //Process
                
                //This is an example until the filters can be figured out (should filter out green)
                Image<Hsv, byte> rightHSV = rightColor.Convert<Hsv, byte>();
                rightObstacles.Add(ImageFiltering.HSVFilter(new Hsv(70.0, 0, 0.5), new Hsv(50.0, 1.0, 1.0), rightHSV));

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightObstacles);
            }

            base.process();
        }

        public override bool startup()
        {
            visionleft = new GatedVariable();
            visionright = new GatedVariable();

            return base.startup();
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.VISION_RIGHT:
                    this.visionright.setObject(data);
                    break;

                case INTERMODULE_VARIABLE.VISION_LEFT:
                    this.visionleft.setObject(data);
                    break;
            }
        }
    }
}
