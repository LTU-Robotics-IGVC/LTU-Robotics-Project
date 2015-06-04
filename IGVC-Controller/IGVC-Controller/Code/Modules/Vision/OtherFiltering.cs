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

                //Image<Gray, byte> hsv = ImageFiltering.HSVFilter(new Hsv(166.0, 0.0, 0.0), new Hsv(75.0, 1.0, 0.8), leftColor.Convert<Hsv, byte>());
                //leftColor._EqualizeHist();
                ////hsv = hsv.ThresholdBinaryInv(new Gray(100), new Gray(255));
                //leftColor.SetValue(new Bgr(Color.Black), hsv);
                //Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
                //Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();

                ////leftGray._EqualizeHist();
                ////rightGray._EqualizeHist();

                //leftGray = leftGray.ThresholdBinary(new Gray(150), new Gray(255));
                //rightGray = rightGray.ThresholdBinary(new Gray(150), new Gray(255));

                leftColor = ImageFiltering.BlackoutInverted(leftColor, new Rectangle(100, 200, 160, 40));
                leftColor = leftColor.SmoothMedian(5);
                Image<Gray, byte> bfilter = ImageFiltering.HSVFilter(new Hsv(166.0, 0.0, 0.0), new Hsv(75.0, 1.0, 0.8), leftColor.Convert<Hsv, byte>());
                //bfilter = bfilter.ThresholdBinaryInv(new Gray(100), new Gray(255));
                leftColor.SetValue(new Bgr(Color.Black), bfilter);

                Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();// new Image<Gray, byte>(leftColor.Width, leftColor.Height);
                leftGray = ImageFiltering.Threshold(leftGray, 220);

                rightColor = ImageFiltering.BlackoutInverted(rightColor, new Rectangle(100, 200, 160, 40));
                rightColor = rightColor.SmoothMedian(5);
                bfilter = ImageFiltering.HSVFilter(new Hsv(166.0, 0.0, 0.0), new Hsv(75.0, 1.0, 0.8), rightColor.Convert<Hsv, byte>());
                rightColor.SetValue(new Bgr(Color.Black), bfilter);

                Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();
                //rightGray = ImageFiltering.Threshold(rightGray, 230);
                rightGray = rightGray.ThresholdBinary(new Gray(220), new Gray(255));
                //leftGray = ImageFiltering.Threshold(leftGray, 254);
                //Image<Hsv, byte> leftHsv = leftColor.Convert<Hsv, byte>();
                //Image<Gray, byte> rightGray = new Image<Gray, byte>(rightColor.Width, rightColor.Height);
                //Image<Hsv, byte> rightHsv = rightColor.Convert<Hsv, byte>();

                //Image<Gray, byte>[] layers = leftColor.Split();
                //Image<Gray, byte>[] hsvLayer = leftHsv.Split();
                //for (int x = 0; x < leftColor.Width; x++)
                //    for (int y = 0; y < leftColor.Height; y++)
                //    {
                //        leftGray.Data[y, x, 0] = (byte)(2 * (int)leftColor.Data[y, x, 0] - (int)leftColor.Data[y, x, 1]);// + (int)leftHsv.Data[y, x, 0]);
                //+    }

                //leftGray = leftGray.Add(hsvLayer[0].Add(layers[0].Add(layers[0])));
                //leftGray = leftGray.Sub(layers[1]);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftGray);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightGray);
            }

            base.process();
        }
    }
}
