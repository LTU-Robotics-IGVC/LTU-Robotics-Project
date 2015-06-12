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
    class HSVandGuassianFilter : IModule
    {
        GatedVariable leftImage;
        GatedVariable rightImage;

        public HSVandGuassianFilter() : base()
        {
            this.modulePriority = 30;
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
            int Hue = 100; //Change this value for threshold

            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)leftImage.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)rightImage.getObject();

            Image<Hsv, byte> leftHsv = leftColor.Convert<Hsv, byte>();
            Image<Hsv, byte> rightHsv = rightColor.Convert<Hsv, byte>();

            if(leftColor != null && rightColor != null)
            {
                //Place filters here

                //filter for left Image
                for (int w = 0; w < leftHsv.Width; w++)
                {
                    for (int h = 0; h < leftHsv.Height; h++)
                    {
                        if ((leftHsv.Data[h, w, 0] > Hue - 10) && (leftHsv.Data[h, w, 0] < Hue + 10))
                        {
                            leftHsv.Data[h, w, 2] = 254;
                            leftHsv.Data[h, w, 1] = 254;
                        }
                        else
                        {
                            leftHsv.Data[h, w, 2] = 0;
                            leftHsv.Data[h, w, 1] = 0;
                        }
                    }
                }

                //FIlter for Right Image
                for (int w = 0; w < rightHsv.Width; w++)
                {
                    for (int h = 0; h < rightHsv.Height; h++)
                    {
                        if ((rightHsv.Data[h, w, 0] > Hue - 10) && (rightHsv.Data[h, w, 0] < Hue + 10))
                        {
                            rightHsv.Data[h, w, 2] = 254;
                            rightHsv.Data[h, w, 1] = 254;
                        }
                        else
                        {
                            rightHsv.Data[h, w, 2] = 0;
                            rightHsv.Data[h, w, 1] = 0;
                        }
                    }
                }
            }

            leftHsv._SmoothGaussian(19);
            rightHsv._SmoothGaussian(19);

            Image<Gray, byte> leftGray = leftHsv.Convert<Gray, byte>().ThresholdBinary(new Gray(20), new Gray(255));
            Image<Gray, byte> rightGray = rightHsv.Convert<Gray, byte>().ThresholdBinary(new Gray(20), new Gray(255)); ;

            //this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftGray);
            //this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightGray);

            base.process();
        }
    }
}
