using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.MathX;
using IGVC_Controller.Code.Modules.Cameras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.Vision
{
    public partial class DualVisionObstacleReprojectionEditor : Form, IModuleEditor
    {
        DualVisionObstacleReprojection module;
        Capture leftCamera;
        Capture rightCamera;
        bool isCalibrating = false;
        HomographyMatrix leftHomography;
        HomographyMatrix rightHomography;
        const int width = 9;
        const int height = 6;
        Size patternSize = new Size(width, height);
        PointF[] worldPoints = new PointF[width * height];
        const double checkerboardBoxSize = 2.7; //In centimeters
        //Note that the coordinates are relative to the center of TurtleBot
        const double checkerboardUpperLeftX = 0.0;
        const double checkerboardUpperLeftY = 0.0;

        public DualVisionObstacleReprojectionEditor()
        {
            InitializeComponent();

            //Set up calibration points
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    worldPoints[x + y * height] = new PointF(
                        (float)(checkerboardUpperLeftX + (double)x * checkerboardBoxSize),
                        (float)(checkerboardUpperLeftY - (double)y * checkerboardBoxSize));
                }
            }
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (DualVisionObstacleReprojection)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {

        }

        void IModuleEditor.setDataToModule()
        {

        }

        private void StartCamera_Click(object sender, EventArgs e)
        {
            if(leftCamera == null && rightCamera != null)
            {
                leftCamera = new Capture(DualWebcam.cap1Index);
                rightCamera = new Capture(DualWebcam.cap2Index);

                leftCamera.ImageGrabbed += leftCamera_ImageGrabbed;

                leftCamera.Start();
                rightCamera.Start();
            }
        }

        void leftCamera_ImageGrabbed(object sender, EventArgs e)
        {
            Image<Bgr, byte> leftColor = leftCamera.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            Image<Bgr, byte> rightColor = rightCamera.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

            if (DualWebcam.intrinsic1 != null) leftColor = DualWebcam.intrinsic1.Undistort(leftColor);
            if (DualWebcam.intrinsic2 != null) rightColor = DualWebcam.intrinsic2.Undistort(rightColor);
            
            Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
            Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();

            if (this.isCalibrating)
            {
                PointF[] cornersLeft = CameraCalibration.FindChessboardCorners(leftGray, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                PointF[] cornersRight = CameraCalibration.FindChessboardCorners(rightGray, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);

                if(cornersLeft != null && cornersRight != null)
                {
                    leftGray.FindCornerSubPix(new PointF[1][] { cornersLeft }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));
                    rightGray.FindCornerSubPix(new PointF[1][] { cornersRight }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                    leftHomography = CameraCalibration.FindHomography(cornersLeft, worldPoints, Emgu.CV.CvEnum.HOMOGRAPHY_METHOD.DEFAULT, 2.0);
                    rightHomography = CameraCalibration.FindHomography(cornersRight, worldPoints, Emgu.CV.CvEnum.HOMOGRAPHY_METHOD.DEFAULT, 2.0);
                }
            }

            this.imageBox1.Image = leftColor;
            this.imageBox3.Image = rightColor;
            leftGray = ImageFiltering.Threshold(leftGray, 125);
            rightGray = ImageFiltering.Threshold(rightGray, 125);
            Image<Gray, byte> combined = new Image<Gray, byte>(1000, 1000);
            combined.Add(leftGray.WarpPerspective(leftHomography, 1000, 1000, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            combined.Add(rightGray.WarpPerspective(rightHomography, 1000, 1000, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            this.imageBox2.Image = combined;
        }

        private void CalibrateButton_Click(object sender, EventArgs e)
        {
            this.isCalibrating = true;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (leftCamera != null) leftCamera.Stop();
            if (rightCamera != null) rightCamera.Stop();

            ((IModuleEditor)this).setDataToModule();

            this.Close();
        }
    }
}
