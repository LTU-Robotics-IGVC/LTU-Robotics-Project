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
    class SunnyLineFilter : IModule
    {
        GatedVariable leftImage;
        GatedVariable rightImage;
        List<double> left = new List<double> { };
        List<double> right = new List<double> { };

        public SunnyLineFilter() : base()
        {
            this.modulePriority = 45;
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
            int Hue = 10; //Change this value for threshold
            int threshold1 = 200;

            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)leftImage.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)rightImage.getObject();

            //this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftGray);
            //this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightGray);

            ////CODE FOR DETECTING LINES
            Image<Hsv,byte> leftHSV=leftColor.Convert<Hsv,byte>();
            Image<Hsv,byte> rightHSV=rightColor.Convert<Hsv,byte>();
            Image<Gray, Byte>[] channelsLeft = leftColor.Split();
            Image<Gray, Byte> imgBlueLeft = channelsLeft[0];
            Image<Gray, Byte>[] channelsRight = rightColor.Split();
            Image<Gray, Byte> imgBlueRight = channelsRight[0];

            Image<Gray, Byte> combineLeft = imgBlueLeft.And(imgBlueLeft);
            Image<Gray, Byte> combineRight = imgBlueRight.And(imgBlueRight);

            left.Clear();
            for (int w = 0; w < imgBlueLeft.Width; w++)
                for (int h = 0; h < imgBlueLeft.Height / 4; h++)
                {
                    if (imgBlueLeft.Data[h, w, 0] > 30)
                        if ((imgBlueLeft.Data[h, w, 0] - imgBlueLeft.Height / 4 + h) < 0)
                            imgBlueLeft.Data[h, w, 0] = 0;
                        else
                            imgBlueLeft.Data[h, w, 0] = (byte)(imgBlueLeft.Data[h, w, 0] - imgBlueLeft.Height / 4 + h);
                }
            for (int w = 0; w < imgBlueLeft.Width; w++)
            {
                for (int h = 0; h < imgBlueLeft.Height; h++)
                {
                    if ((imgBlueLeft.Data[h, w, 0] > threshold1))
                    {
                        left.Add(imgBlueLeft.Data[h, w, 0]);
                    }
                    else
                    {
                        combineLeft.Data[h, w, 0] = 0;
                    }
                }
            }
            double newthresh = getStdDev(left, 5);
            for (int w = 0; w < imgBlueLeft.Width; w++)
            {
                for (int h = 0; h < imgBlueLeft.Height; h++)
                {
                    if ((imgBlueLeft.Data[h, w, 0] > newthresh))
                    {
                        combineLeft.Data[h, w, 0] = 255;
                    }
                    else
                    {
                        combineLeft.Data[h, w, 0] = 0;
                    }
                }
            }

            for (int w = 0; w < leftHSV.Width; w++)
            {
                for (int h = 0; h < leftHSV.Height; h++)
                {
                    if ((leftHSV.Data[h, w, 0] > Hue - 5) && (leftHSV.Data[h, w, 0] < Hue+5))
                    {
                        leftHSV.Data[h, w, 2] = 254;
                        leftHSV.Data[h, w, 1] = 254;
                    }
                    else
                    {
                        leftHSV.Data[h, w, 2] = 0;
                        leftHSV.Data[h, w, 1] = 0;
                    }
                }
            }


            right.Clear();
            for (int w = 0; w < imgBlueRight.Width; w++)
                for (int h = 0; h < imgBlueRight.Height / 4; h++)
                {
                    if (imgBlueRight.Data[h, w, 0] > 30)
                        if ((imgBlueRight.Data[h, w, 0] - imgBlueRight.Height / 4 + h) < 0)
                            imgBlueRight.Data[h, w, 0] = 0;
                        else
                            imgBlueRight.Data[h, w, 0] = (byte)(imgBlueRight.Data[h, w, 0] - imgBlueRight.Height / 4 + h);
                }
            for (int w = 0; w < imgBlueRight.Width; w++)
            {
                for (int h = 0; h < imgBlueRight.Height; h++)
                {
                    if ((imgBlueRight.Data[h, w, 0] > threshold1))
                    {
                        right.Add(imgBlueRight.Data[h, w, 0]);
                    }
                    else
                    {
                        combineRight.Data[h, w, 0] = 0;
                    }
                }
            }
            newthresh = getStdDev(right, 5);
            for (int w = 0; w < imgBlueRight.Width; w++)
            {
                for (int h = 0; h < imgBlueRight.Height; h++)
                {
                    if ((imgBlueRight.Data[h, w, 0] > newthresh))
                    {
                        combineRight.Data[h, w, 0] = 255;
                    }
                    else
                    {
                        combineRight.Data[h, w, 0] = 0;
                    }
                }
            }

            for (int w = 0; w < rightHSV.Width; w++)
            {
                for (int h = 0; h < rightHSV.Height; h++)
                {
                    if ((rightHSV.Data[h, w, 0] > Hue - 5) && (rightHSV.Data[h, w, 0] < Hue+5))
                    {
                        rightHSV.Data[h, w, 2] = 254;
                        rightHSV.Data[h, w, 1] = 254;
                    }
                    else
                    {
                        rightHSV.Data[h, w, 2] = 0;
                        rightHSV.Data[h, w, 1] = 0;
                    }
                }
            }



            Image<Gray, Byte> combine2Left = combineLeft.ThresholdBinary(new Gray(10), new Gray(255)).Add(leftHSV.Convert<Gray,byte>().ThresholdBinary(new Gray(10),new Gray(255)));
            combine2Left.Not();
            combine2Left._SmoothGaussian(5);
            combine2Left.Not();
            combine2Left = combine2Left.ThresholdBinary(new Gray(100), new Gray(255));

            Image<Gray, Byte> combine2Right = combineRight.ThresholdBinary(new Gray(10), new Gray(255)).Add(rightHSV.Convert<Gray, byte>().ThresholdBinary(new Gray(10), new Gray(255)));
            combine2Right.Not();
            combine2Right._SmoothGaussian(5);
            combine2Right.Not();
            combine2Right = combine2Right.ThresholdBinary(new Gray(100), new Gray(255));

            //display images

            this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, combine2Left);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, combine2Right);

            base.process();
        }


        public double getMean(List<double> x)
        {
            if (x.Count > 0)
            {
                int length = x.Count - 1;
                double mean = 0;
                for (int i = 0; i <= length; i++)
                    mean += x[i];
                mean = mean / x.Count;
                return mean;
            }
            return 0;
        }

        public double getStdDev(List<double> x, int std)
        {
            if (x.Count > 0)
            {
                for (int y = 0; y < std; y++)
                {
                    double mean = getMean(x);
                    double stdDev = 0;
                    double variance = 0;
                    int length = x.Count - 1;
                    List<double> temp = new List<double>(x);
                    for (int i = 0; i <= length; i++)
                        temp[i] = (temp[i] - mean) * (temp[i] - mean);
                    for (int i = 0; i <= length; i++)
                        stdDev += temp[i];
                    stdDev = stdDev / temp.Count;
                    variance = Math.Sqrt(stdDev);
                    for (int i = 0; i <= length; i++)
                        if (Math.Abs(x[i] - mean) > variance)
                        {
                            x.RemoveAt(i);
                            length--;
                            i--;
                        }
                }
                return getMean(x);
            }
            else
                return 0;
        }
    }
}
