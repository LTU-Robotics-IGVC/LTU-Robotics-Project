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
        const double checkerboardBoxSize = 10;//2.7; //In centimeters
        //Note that the coordinates are relative to the center of TurtleBot
        const double checkerboardUpperLeftX = 0.0;
        const double checkerboardUpperLeftY = 0.0;

        int combinedWidth = 1000;
        int combinedHeight = 1000;

        public DualVisionObstacleReprojectionEditor()
        {
            InitializeComponent();

            prepareCalibartionPoints();
        }

        private void prepareCalibartionPoints()
        {
            //Set up calibration points
            double CheckerboardWidth = checkerboardBoxSize * width;
            double CheckerboardHeight = checkerboardBoxSize * height;
            double HalfWidth = CheckerboardWidth / 2.0;
            double HalfHeight = CheckerboardHeight / 2.0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    worldPoints[(width - x - 1) + y * width] = new PointF(
                        (float)(combinedWidth/2 + checkerboardUpperLeftX + (double)x * checkerboardBoxSize),
                        (float)(combinedHeight/2 + checkerboardUpperLeftY + (double)y * checkerboardBoxSize));
                }
            }

            /*for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    worldPoints[(width - x - 1) + y * width] = new PointF(
                        (float)(x * checkerboardBoxSize), (float)(y * checkerboardBoxSize));
                }
            }
            */
            //for(int x = 0; x < width; x++)
            //{
            //    for(int y = 0;  y < height; y++)
            //    {
            //        worldPoints[(width - x - 1) + y * width].X = x * 100;
            //        worldPoints[(width - x - 1) + y * width].Y = y * 100;
            //    }
            //}
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (DualVisionObstacleReprojection)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            this.leftHomography = module.leftHomography;
            this.rightHomography = module.rightHomography;
        }

        void IModuleEditor.setDataToModule()
        {

        }

        private void StartCamera_Click(object sender, EventArgs e)
        {
            if(leftCamera == null && rightCamera == null)
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

                    //leftHomography = CameraCalibration.GetPerspectiveTransform(cornersLeft, worldPoints);
                    //rightHomography = CameraCalibration.GetPerspectiveTransform(cornersRight, worldPoints);

                    this.isCalibrating = false;
                    this.CalibrateButton.BackColor = Color.Green;
                }
            }

            leftGray = ImageFiltering.Threshold(leftGray, 125);
            rightGray = ImageFiltering.Threshold(rightGray, 125);
            Image<Gray, byte> combined = new Image<Gray, byte>(combinedWidth, combinedHeight);
            combined = combined.Add(leftGray.WarpPerspective(leftHomography, combinedWidth, combinedHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            combined = combined.Add(rightGray.WarpPerspective(rightHomography, combinedWidth, combinedHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));

            this.imageBox1.Image = leftColor;
            this.imageBox3.Image = rightColor; 
            this.imageBox2.Image = combined;
        }

        private void CalibrateButton_Click(object sender, EventArgs e)
        {
            this.isCalibrating = true;
            this.CalibrateButton.BackColor = Color.Red;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (leftCamera != null) { leftCamera.Stop(); leftCamera.Dispose(); }
            if (rightCamera != null) { rightCamera.Stop(); rightCamera.Dispose(); }

            ((IModuleEditor)this).setDataToModule();

            this.Close();
        }
    }
}
