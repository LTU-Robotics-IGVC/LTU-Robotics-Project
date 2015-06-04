using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace IGVC_Controller.Code.MathX
{
    class ImageFiltering
    {
        public static Image<Gray, byte> HSVFilter(Hsv min, Hsv max, Image<Hsv, byte> image)
        {
            Image<Gray, byte> Return = new Image<Gray, byte>(image.Width, image.Height);

            int minHue = (int)((double)min.Hue / 180.0 * 255.0);
            int maxHue = (int)((double)max.Hue / 180.0 * 255.0);
            int minSat = (int)((double)min.Satuation * 255.0);
            int maxSat = (int)((double)max.Satuation * 255.0);
            int minVal = (int)((double)min.Value * 255.0);
            int maxVal = (int)((double)max.Value * 255.0);

            Image<Gray, byte>[] channels = image.Split();
            Image<Gray, byte> Hue = channels[0];
            Image<Gray, byte> Sat = channels[1];
            Image<Gray, byte> Val = channels[2];

            if(minHue < maxHue)
            {
                Hue = Threshold(Hue.InRange(new Gray(minHue), new Gray(maxHue)), 0);
            }
            else
            {
                Image<Gray, byte> lower = Threshold(Hue.InRange(new Gray(0), new Gray(maxHue)), 0);
                Image<Gray, byte> upper = Threshold(Hue.InRange(new Gray(minHue), new Gray(255)), 0);
                Hue = lower.Or(upper);
            }

            if(minSat < maxSat)
            {
                Sat = Threshold(Sat.InRange(new Gray(minSat), new Gray(maxSat)), 0);
            }
            else
            {
                Image<Gray, byte> lower = Threshold(Sat.InRange(new Gray(0), new Gray(maxSat)), 0);
                Image<Gray, byte> upper = Threshold(Sat.InRange(new Gray(minSat), new Gray(255)), 0);
                Sat = lower.Or(upper);
            }

            if(minVal < maxVal)
            {
                Val = Threshold(Val.InRange(new Gray(minVal), new Gray(maxVal)), 0);
            }
            else
            {
                Image<Gray, byte> lower = Threshold(Val.InRange(new Gray(0), new Gray(maxVal)), 0);
                Image<Gray, byte> upper = Threshold(Val.InRange(new Gray(minVal), new Gray(255)), 0);
                Val = lower.Or(upper);
            }

            return Hue.And(Sat.And(Val));
           
        }

        public static Image<Gray, byte> Threshold(Image<Gray, byte> img, int threshold)
        {
            return img.ThresholdBinary(new Gray(threshold), new Gray(255));
        }

        /// <summary>
        /// Blacks out the sections of the image not contained in RegionOfInterest
        /// <para>This does not use image.ROI</para>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="RegionOfInterest"></param>
        /// <returns></returns>
        public static Image<Bgr, byte> Blackout(Image<Bgr, byte> image, Rectangle RegionOfInterest)
        {
            Image<Bgr, byte> Return = image.Clone();

            Bgr black = new Bgr(0, 0, 0);

            Point[] pts_upper = new Point[]
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(image.Width, RegionOfInterest.Top),
                new Point(0, RegionOfInterest.Top)
            };

            Point[] pts_lower = new Point[]
            {
                new Point(0, RegionOfInterest.Bottom),
                new Point(image.Width, RegionOfInterest.Bottom),
                new Point(image.Width, image.Height),
                new Point(0, image.Height)
            };

            Point[] pts_left = new Point[]
            {
                new Point(0, 0),
                new Point(RegionOfInterest.Left, 0),
                new Point(RegionOfInterest.Left, image.Height),
                new Point(0, image.Height)
            };

            Point[] pts_right = new Point[]
            {
                new Point(RegionOfInterest.Right, 0),
                new Point(image.Width, 0),
                new Point(image.Width, image.Height),
                new Point(RegionOfInterest.Right, image.Height)
            };

            Return.FillConvexPoly(pts_upper, black);
            Return.FillConvexPoly(pts_lower, black);
            Return.FillConvexPoly(pts_left, black);
            Return.FillConvexPoly(pts_right, black);

            return Return;
        }

        public static Image<Bgr, byte> BlackoutInverted(Image<Bgr, byte> image, Rectangle RegionOfInterest)
        {
            Image<Bgr, byte> c = new Image<Bgr, byte>(image.Width, image.Height);
            c.SetValue(new Bgr(Color.White));
            c = Blackout(c, RegionOfInterest);
            Image<Gray, byte> cc = c.Convert<Gray, byte>();
            //cc = cc.ThresholdBinaryInv(new Gray(100), new Gray(255));
            image.SetValue(new Bgr(Color.Black), cc);

            return image;
        }
    }
}
