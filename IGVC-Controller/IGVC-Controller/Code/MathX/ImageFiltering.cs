using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

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
            //int width = image.Width;
            //int height = image.Height;
            //Image<Gray, byte> copy = new Image<Gray, byte>(width, height);

            //for (int x = 0; x < width; x++)
            //{
            //    for (int y = 0; y < height; y++)
            //    {
            //        double hue = ((double)image.Data[y, x, 0]) / 255.0 * 180.0;
            //        double sat = ((double)image.Data[y, x, 1]) / 255.0;
            //        double val = ((double)image.Data[y, x, 2]) / 255.0;

            //        if (min.Hue < max.Hue)
            //        {
            //            if (hue < min.Hue || hue > max.Hue)
            //            {
            //                continue;
            //            }
            //        }
            //        else
            //        {
            //            if (hue > min.Hue || hue < max.Hue)
            //            {
            //                continue;
            //            }
            //        }

            //        if (min.Satuation < max.Satuation)
            //        {
            //            if (sat < min.Satuation || sat > max.Satuation)
            //            {
            //                continue;
            //            }
            //        }
            //        else
            //        {
            //            if (sat > min.Satuation || sat < max.Satuation)
            //            {
            //                continue;
            //            }
            //        }

            //        if (min.Value < max.Value)
            //        {
            //            if (val < min.Value || val > max.Value)
            //            {
            //                continue;
            //            }
            //        }
            //        else
            //        {
            //            if (val > min.Value || val < max.Value)
            //            {
            //                continue;
            //            }
            //        }

            //        //Pixel should be value if it reaches this point
            //        copy.Data[y, x, 0] = 255;
            //    }
            //}

            //return copy;
        }

        public static Image<Gray, byte> Threshold(Image<Gray, byte> img, int threshold)
        {
            return img.ThresholdBinary(new Gray(threshold), new Gray(255));
        }
    }
}
