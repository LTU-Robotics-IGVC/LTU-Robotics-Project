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

        int combinedWidth = 1000;
        int combinedHeight = 1000;

        Point[] leftCorners;
        Point[] rightCorners;
        Point[] worldCorners;

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
            
           // worldPoints = worldPoints.Reverse().ToArray();

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
            this.leftCorners = module.leftCaliCorners;
            this.rightCorners = module.rightCaliCorners;
            this.worldCorners = module.worldCaliCorners;
            this.PriorityBox.Value = module.modulePriority;
        }

        void IModuleEditor.setDataToModule()
        {
            module.modulePriority = (int)this.PriorityBox.Value;
            module.leftCaliCorners = this.leftCorners;
            module.rightCaliCorners = this.rightCorners;
            module.worldCaliCorners = this.worldCorners;
            module.leftHomography = this.leftHomography;
            module.rightHomography = this.rightHomography;
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
            Image<Bgr, byte> leftColor = leftCamera.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_NN);
            Image<Bgr, byte> rightColor = rightCamera.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_NN);

            if (DualWebcam.intrinsic1 != null) leftColor = DualWebcam.intrinsic1.Undistort(leftColor);
            if (DualWebcam.intrinsic2 != null) rightColor = DualWebcam.intrinsic2.Undistort(rightColor);
            
            Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
            Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();
            /*
            if (this.isCalibrating)
            {
                PointF[] cornersLeft = CameraCalibration.FindChessboardCorners(leftGray, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                PointF[] cornersRight = CameraCalibration.FindChessboardCorners(rightGray, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);

                if(cornersLeft != null && cornersRight != null)
                {
                    leftGray.FindCornerSubPix(new PointF[1][] { cornersLeft }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));
                    rightGray.FindCornerSubPix(new PointF[1][] { cornersRight }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                    //leftHomography = CameraCalibration.FindHomography(cornersLeft, worldPoints, Emgu.CV.CvEnum.HOMOGRAPHY_METHOD.DEFAULT, 2.0);
                    //rightHomography = CameraCalibration.FindHomography(cornersRight, worldPoints, Emgu.CV.CvEnum.HOMOGRAPHY_METHOD.DEFAULT, 2.0);

                    PointF[] worldCorners = new PointF[]
                    {
                        worldPoints[0],
                        worldPoints[8],
                        worldPoints[width*height-1],
                        worldPoints[width*height-9]
                    };

                    PointF[] leftCorners = new PointF[]
                    {
                        cornersLeft[0],
                        cornersLeft[8],
                        cornersLeft[width*height-1],
                        cornersLeft[width*height-9]
                    };

                    PointF[] rightCorners = new PointF[]
                    {
                        cornersRight[0],
                        cornersRight[8],
                        cornersRight[width*height-1],
                        cornersRight[width*height-9]
                    };

                    leftHomography = CameraCalibration.GetPerspectiveTransform(leftCorners, worldCorners);
                    rightHomography = CameraCalibration.GetPerspectiveTransform(rightCorners, worldCorners);

                    this.isCalibrating = false;
                    this.CalibrateButton.BackColor = Color.Green;
                }
            }*/

            leftGray = ImageFiltering.Threshold(leftGray, 125);
            rightGray = ImageFiltering.Threshold(rightGray, 125);
            Image<Gray, byte> combined = new Image<Gray, byte>(combinedWidth, combinedHeight);
            combined = combined.Add(leftGray.WarpPerspective(leftHomography, combinedWidth, combinedHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            combined = combined.Add(rightGray.WarpPerspective(rightHomography, combinedWidth, combinedHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));

            Image<Bgr, byte> c = new Image<Bgr, byte>(combinedWidth, combinedHeight);
            c = c.Add(leftColor.WarpPerspective(leftHomography, combinedWidth, combinedHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Bgr(Color.Black)));
            c = c.AddWeighted(rightColor.WarpPerspective(rightHomography, combinedWidth, combinedHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Bgr(Color.Black)), 0.5, 0.5, 0.0);
            //c = c.Rotate(180, new Bgr(Color.Black));
            Bgr blue = new Bgr(Color.Blue);
            leftColor.DrawPolyline(leftCorners, true, blue, 1);
            rightColor.DrawPolyline(rightCorners, true, blue, 1);
            c.DrawPolyline(worldCorners, true, blue, 1);

            Point[] Vertical = new Point[]
            {
                new Point((int)XCoord.Value, 0),
                new Point((int)XCoord.Value, combinedHeight)
            };
            Point[] Horizontal = new Point[]
            {
                new Point(0, (int)YCoord.Value),
                new Point(combinedWidth, (int)YCoord.Value)
            };

            Bgr red = new Bgr(Color.Red);
            leftColor.DrawPolyline(Vertical, false, red, 1);
            leftColor.DrawPolyline(Horizontal, false, red, 1);
            rightColor.DrawPolyline(Vertical, false, red, 1);
            rightColor.DrawPolyline(Horizontal, false, red, 1);
            c.DrawPolyline(Vertical, false, red, 1);
            c.DrawPolyline(Horizontal, false, red, 1);

            this.imageBox1.Image = leftColor;
            this.imageBox3.Image = rightColor; 
            this.imageBox2.Image = c;
        }

        private void CalibrateButton_Click(object sender, EventArgs e)
        {
            //this.isCalibrating = true;
            //this.CalibrateButton.BackColor = Color.Red;
            PointF[] leftCornersF = new PointF[4];
            PointF[] rightCornersF = new PointF[4];
            PointF[] worldCornersF = new PointF[4];

            for (int i = 0; i < 4; i++)
            {
                leftCornersF[i] = new PointF(leftCorners[i].X, leftCorners[i].Y);
                rightCornersF[i] = new PointF(rightCorners[i].X, rightCorners[i].Y);
                worldCornersF[i] = new PointF(worldCorners[i].X, worldCorners[i].Y);
            }

            leftHomography = CameraCalibration.GetPerspectiveTransform(leftCornersF, worldCornersF);
            rightHomography = CameraCalibration.GetPerspectiveTransform(rightCornersF, worldCornersF);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (leftCamera != null) { leftCamera.Stop(); leftCamera.Dispose(); }
            if (rightCamera != null) { rightCamera.Stop(); rightCamera.Dispose(); }

            ((IModuleEditor)this).setDataToModule();

            this.Close();
        }

        private void SetPoint_Click(object sender, EventArgs e)
        {
            if(SideSelect.SelectedItem == "Right")
            {
                if(Src_Dst_Select.SelectedItem == "Source")
                {
                    rightCorners[CornerSelect.SelectedIndex] = new Point((int)XCoord.Value, (int)YCoord.Value);
                }
                else if (Src_Dst_Select.SelectedItem == "Destination")
                {
                    worldCorners[CornerSelect.SelectedIndex] = new Point((int)XCoord.Value, (int)YCoord.Value);
                }
            }
            else if(SideSelect.SelectedItem == "Left")
            {
                if (Src_Dst_Select.SelectedItem == "Source")
                {
                    leftCorners[CornerSelect.SelectedIndex] = new Point((int)XCoord.Value, (int)YCoord.Value);
                }
                else if (Src_Dst_Select.SelectedItem == "Destination")
                {
                    worldCorners[CornerSelect.SelectedIndex] = new Point((int)XCoord.Value, (int)YCoord.Value);
                }
            }
        }
    }
}
