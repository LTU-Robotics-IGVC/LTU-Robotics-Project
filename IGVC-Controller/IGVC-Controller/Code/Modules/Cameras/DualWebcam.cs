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
        public static int cap1Index = 1;
        public static int cap2Index = 2;
        int imageWidth = 320;
        int imageHeight = 240;
        public static IntrinsicCameraParameters intrinsic1;
        public static IntrinsicCameraParameters intrinsic2;

        public DualWebcam() : base()
        {
            this.modulePriority = 5;
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.leftGoesToCapture1 = config.Read<bool>("leftGoesToCapture1", true);

            cap1Index = config.Read<int>("leftCamIndex", 1);
            cap2Index = config.Read<int>("rightCamIndex", -1);

            intrinsic1 = config.Read<IntrinsicCameraParameters>("intrinsic1", null);
            intrinsic2 = config.Read<IntrinsicCameraParameters>("intrinsic2", null);
            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<bool>("leftGoesToCapture1", this.leftGoesToCapture1);
            //config.Write<Matrix<double>>("intrinsic1_Distortion", this.intrinsic1.DistortionCoeffs);
            //config.Write<Matrix<double>>("intrinsic1_Intrinsic", this.intrinsic1.IntrinsicMatrix);
            //config.Write<Matrix<double>>("intrinsic2_Distortion", this.intrinsic2.DistortionCoeffs);
            //config.Write<Matrix<double>>("intrinsic2_Intrinsic", this.intrinsic2.IntrinsicMatrix);
            config.Write<IntrinsicCameraParameters>("intrinsic1", intrinsic1);
            config.Write<IntrinsicCameraParameters>("intrinsic2", intrinsic2);
            config.Write<int>("leftCamIndex", cap1Index);
            config.Write<int>("rightCamIndex", cap2Index);

            base.writeToConfig(config);
        }

        public override bool startup()
        {
            capture1 = new Capture(cap1Index);
            capture1.Start();
            capture2 = new Capture(cap2Index);
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
                left = capture1.QueryFrame().Resize(imageWidth, imageHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                right = capture2.QueryFrame().Resize(imageWidth, imageHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
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

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new CameraSetup();
        }
    }
}
