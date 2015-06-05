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
    class OtherFiltering : IModule
    {
        GatedVariable leftImage;
        GatedVariable rightImage;

        public OtherFiltering() : base()
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

            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)leftImage.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)rightImage.getObject();

            if(leftColor != null && rightColor != null)
            {
                //Place filters here

                //leftColor = ImageFiltering.BlackoutInverted(leftColor, new Rectangle(100, 200, 160, 40));
                //leftColor = leftColor.SmoothMedian(5);
                //Image<Gray, byte> bfilter = ImageFiltering.HSVFilter(new Hsv(166.0, 0.0, 0.0), new Hsv(75.0, 1.0, 0.8), leftColor.Convert<Hsv, byte>());
                //leftColor.SetValue(new Bgr(Color.Black), bfilter);

                //Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
                //leftGray = ImageFiltering.Threshold(leftGray, 200);

                //rightColor = ImageFiltering.BlackoutInverted(rightColor, new Rectangle(100, 200, 160, 40));
                //rightColor = rightColor.SmoothMedian(5);
                //bfilter = ImageFiltering.HSVFilter(new Hsv(166.0, 0.0, 0.0), new Hsv(75.0, 1.0, 0.8), rightColor.Convert<Hsv, byte>());
                //rightColor.SetValue(new Bgr(Color.Black), bfilter);

                //Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();
                //rightGray = rightGray.ThresholdBinary(new Gray(200), new Gray(255));

                Image<Hsv, Byte> hsvImageleft = leftColor.Convert<Hsv, Byte>();
                Image<Hsv, Byte> hsvImageRight = rightColor.Convert<Hsv, Byte>();

                for (int w = 0; w < leftColor.Width;w++ )
                {
                    for(int h = 0; h < leftColor.Height; h++)
                    {
                        if((hsvImageleft.Data[h,w,0] < 170)&&(hsvImageleft.Data[h,w,0] > 130))
                        {
                            hsvImageleft.Data[h, w, 2] = 255;
                            hsvImageleft.Data[h, w, 1] = 255;
                        }
                        else
                        {
                            hsvImageleft.Data[h, w, 2] = 0;
                            hsvImageleft.Data[h, w, 1] = 0;
                        }
                    }
                }

                for (int w = 0; w < rightColor.Width; w++)
                {
                    for (int h = 0; h < rightColor.Height; h++)
                    {
                        if ((hsvImageRight.Data[h, w, 0] < 170) && (hsvImageRight.Data[h, w, 0] > 130))
                        {
                            hsvImageRight.Data[h, w, 2] = 255;
                            hsvImageRight.Data[h, w, 1] = 255;
                        }
                        else
                        {
                            hsvImageRight.Data[h, w, 2] =0;
                            hsvImageRight.Data[h, w, 1] = 0;
                        }
                    }
                }


                Image<Gray, byte> leftGray = ImageFiltering.Threshold(hsvImageleft.Convert<Gray, byte>(), 20);
                Image<Gray, byte> rightGray = ImageFiltering.Threshold(hsvImageRight.Convert<Gray, byte>(), 20);

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftGray);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightGray);

                this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_LEFT, hsvImageleft.Convert<Bgr, byte>());
                this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_RIGHT, hsvImageRight.Convert<Bgr, byte>());
            }

            base.process();
        }
    }
}
