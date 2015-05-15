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
        public static Image<Gray, byte> HSVFilter(Hsv min, Hsv max, Image<Hsv, double> image)
        {
            int width = image.Width;
            int height = image.Height;
            Image<Gray, byte> copy = new Image<Gray, byte>(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double hue = image.Data[y, x, 0];
                    double sat = image.Data[y, x, 1];
                    double val = image.Data[y, x, 2];

                    if (min.Hue < max.Hue)
                    {
                        if (hue < min.Hue || hue > max.Hue)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (hue > min.Hue || hue < max.Hue)
                        {
                            continue;
                        }
                    }

                    if (min.Satuation < max.Satuation)
                    {
                        if (sat < min.Satuation || sat > max.Satuation)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (sat > min.Satuation || sat < max.Satuation)
                        {
                            continue;
                        }
                    }

                    if (min.Value < max.Value)
                    {
                        if (val < min.Value || val > max.Value)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (val > min.Value || val < max.Value)
                        {
                            continue;
                        }
                    }

                    //Pixel should be value if it reaches this point
                    copy.Data[y, x, 0] = 255;
                }
            }

            return copy;
        }
    }
}
