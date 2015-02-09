using IGVC_Controller.MathX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace IGVC_Controller.RobotInterface
{
    public class RobotInterface
    {
        public Vector2 GPS;
        public Vector2 Heading;
        public Image<Bgr, byte> LeftCam;
        public Image<Bgr, byte> RightCam;
        public double LeftMotorVelocity;
        public double RightMotorVelocity;
    }
}
