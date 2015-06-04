using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    public class MathXHelper
    {
        public static double getPointDis(int x1, int y1, int x2, int y2)
        {
            double diffX = x1 - x2;
            double diffY = y1 - y2;
            return Math.Sqrt(diffX*diffX + diffY * diffY);
        }

        public static double getPointDis(Point p1, Point p2)
        {
            return getPointDis(p1.X, p1.Y, p2.X, p2.Y);
        }
    }
}
