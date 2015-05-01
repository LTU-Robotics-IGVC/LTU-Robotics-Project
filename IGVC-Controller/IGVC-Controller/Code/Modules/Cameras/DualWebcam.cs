using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace IGVC_Controller.Code.Modules.Cameras
{
    class DualWebcam : IModule
    {
        Capture capture1;
        Capture capture2;
        bool leftGoesToCapture1 = true;
        int cap1Index = 1;
        int cap2Index = 2;
        int imageWidth = 320;
        int imageHeight = 240;

        public DualWebcam() : base()
        {
            this.modulePriority = 5;
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.leftGoesToCapture1 = config.Read<bool>("leftGoesToCapture1", true);
            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<bool>("leftGoesToCapture1", this.leftGoesToCapture1);
            base.writeToConfig(config);
        }

        public override bool startup()
        {
            capture1 = new Capture(0);
            capture1.Start();
            capture2 = new Capture(2);
            return base.startup();
        }

        public override void shutdown()
        {
            capture1.Stop();
            capture2.Stop();
            base.shutdown();
        }

        public override void process()
        {
            Image<Bgr, byte> left;
            Image<Bgr, byte> right;

            if(leftGoesToCapture1)
            {
                left = new Image<Bgr, byte>(capture1.QueryFrame().Resize(imageWidth, imageHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap());
                right = new Image<Bgr, byte>(capture2.QueryFrame().Resize(imageWidth, imageHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap());
            }
            else
            {

                left = capture2.QueryFrame().Resize(imageWidth, imageHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                right = capture1.QueryFrame().Resize(imageWidth, imageHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            }
            IntrinsicCameraParameters icp = new IntrinsicCameraParameters();
            //icp.DistortionCoeffs = new Matrix<double>(new double[] { 1, 1, 0, 0 });
            //left = icp.Undistort<Bgr, byte>(left);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_LEFT, left);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_RIGHT, right);
        }
    }
}
