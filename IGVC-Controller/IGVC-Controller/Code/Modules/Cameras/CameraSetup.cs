using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.Cameras
{
    enum CalibrationState { Intrinsics_Right, Intrinsics_Left, Homography, Demo};

    public partial class CameraSetup : Form, IModuleEditor
    {
        DualWebcam module;
        Capture cam1;
        Capture cam2;

        const int width = 9;
        const int height = 6;
        Size patternSize = new Size(width, height);
        const int sampleSize = 25;
        int sampleIndex = 0;
        PointF[][] corners_points_Left = new PointF[sampleSize][];
        PointF[][] corners_points_Right = new PointF[sampleSize][];
        MCvPoint3D32f[][] corners_object_Points = new MCvPoint3D32f[sampleSize][];
        IntrinsicCameraParameters IntrinsicCam1 = new IntrinsicCameraParameters();
        IntrinsicCameraParameters IntrinsicCam2 = new IntrinsicCameraParameters();
        ExtrinsicCameraParameters[] ExtrinsicCam1;
        ExtrinsicCameraParameters[] ExtrinsicCam2;
        CalibrationState state;
        HomographyMatrix homographyMatrix;

        delegate void delegateSetText(string data);
        private delegateSetText setText;

        public CameraSetup()
        {
            InitializeComponent();
            this.setCalibrationState(CalibrationState.Demo, null);
            this.CalibrateHomography.Enabled = false;
            this.calibrateLeft.Enabled = false;
            this.calibrateRight.Enabled = false;

            //fill the MCvPoint3D32f with correct mesurments
            for (int k = 0; k < sampleSize; k++)
            {
                //Fill our objects list with the real world mesurments for the intrinsic calculations
                List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        object_list.Add(new MCvPoint3D32f(j * 1.0F, i * 1.0F, 0.0F));
                    }
                }
                corners_object_Points[k] = object_list.ToArray();
            }
            this.setText = this.setTitleText;
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (DualWebcam)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            //value = module;
            this.PriorityBox.Value = module.modulePriority;
            this.IntrinsicCam1 = DualWebcam.intrinsic1;
            this.IntrinsicCam2 = DualWebcam.intrinsic2;
            this.LeftCamIndex.Value = module.cap1Index;
            this.RightCamIndex.Value = module.cap2Index;
        }

        void IModuleEditor.setDataToModule()
        {
            module.modulePriority = (int)this.PriorityBox.Value;
            DualWebcam.intrinsic1 = this.IntrinsicCam1;
            DualWebcam.intrinsic2 = this.IntrinsicCam2;
            module.cap1Index = (int)this.LeftCamIndex.Value;
            module.cap2Index = (int)this.RightCamIndex.Value;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ((IModuleEditor)this).setDataToModule();
            if (cam1 != null)
            {
                cam1.Stop();
                cam2.Stop();
                cam1.Dispose();
                cam2.Dispose();
            }
            this.Close();
        }

        private void CalibrateHomography_Click(object sender, EventArgs e)
        {
            if (this.state == CalibrationState.Demo)
            {
                sampleIndex = 0;
                this.setCalibrationState(CalibrationState.Homography, null);
            }
        }

        void cam1_ImageGrabbed(object sender, EventArgs e)
        {
            Image<Bgr, byte> img1 = cam1.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            Image<Bgr, byte> img2 = cam2.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

            Image<Gray, byte> gray1 = img1.Convert<Gray, byte>();
            Image<Gray, byte> gray2 = img2.Convert<Gray, byte>();

            switch(state)
            {
                case CalibrationState.Demo:


                    if(homographyMatrix != null)
                    {
                        Rectangle rect = img1.ROI;
                        PointF[] pts = new PointF[]
                        {
                            new PointF(rect.Left, rect.Bottom),
                            new PointF(rect.Right, rect.Bottom),
                            new PointF(rect.Right, rect.Top),
                            new PointF(rect.Left, rect.Top)
                        };
                        homographyMatrix.ProjectPoints(pts);

                        HomographyMatrix origin = new HomographyMatrix();
                        origin.SetIdentity();
                        origin.Data[0, 2] = 0;
                        origin.Data[1, 2] = 0;
                        Image<Bgr, byte> mosaic = new Image<Bgr, byte>(1000, 1000);

                        Image<Bgr, byte> warp_image = mosaic.Clone();

                        mosaic = img1.WarpPerspective(origin, mosaic.Width, mosaic.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR,
                            Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Bgr(0, 0, 0));
                        warp_image = img2.WarpPerspective(homographyMatrix, warp_image.Width, warp_image.Height, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR,
                            Emgu.CV.CvEnum.WARP.CV_WARP_INVERSE_MAP, new Bgr(200, 0, 0));
                        Image<Gray, byte> warp_image_mask = gray2.Clone();
                        warp_image_mask.SetValue(new Gray(255));
                        Image<Gray, byte> warp_mosaic_mask = mosaic.Convert<Gray, byte>();
                        warp_image_mask.SetValue(255);
                        warp_mosaic_mask = warp_image_mask.WarpPerspective(homographyMatrix, warp_mosaic_mask.Width, warp_mosaic_mask.Height,
                            Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_INVERSE_MAP, new Gray(0));
                        warp_image.Copy(mosaic, warp_mosaic_mask);

                        this.imageBox3.Image = mosaic;
                        this.imageBox1.Image = warp_mosaic_mask;
                        this.imageBox2.Image = warp_image;
                    }
                    else
                    {
                        if (this.IntrinsicCam1 != null)
                            this.imageBox1.Image = this.IntrinsicCam1.Undistort(img1);
                        else
                            this.imageBox1.Image = img1;

                        if (this.IntrinsicCam2 != null)
                            this.imageBox2.Image = this.IntrinsicCam2.Undistort(img2);
                        else
                            this.imageBox2.Image = img2;
                    }
                    break;

                case CalibrationState.Homography:

                    if (this.IntrinsicCam1 != null)
                        gray1 = IntrinsicCam1.Undistort(gray1);
                    if (this.IntrinsicCam2 != null)
                        gray2 = IntrinsicCam2.Undistort(gray2);

                    //Find Homography Matrix
                    if (sampleIndex < sampleSize)
                    {
                        PointF[] cornersLeft = CameraCalibration.FindChessboardCorners(gray1, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                        PointF[] cornersRight = CameraCalibration.FindChessboardCorners(gray2, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                        if (cornersLeft != null && cornersRight != null)
                        {
                            gray1.FindCornerSubPix(new PointF[1][] { cornersLeft }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));
                            gray2.FindCornerSubPix(new PointF[1][] { cornersRight }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                            corners_points_Left[sampleIndex] = cornersLeft;
                            corners_points_Right[sampleIndex] = cornersRight;
                            this.setCalibrationState(CalibrationState.Homography, sampleIndex.ToString());
                            sampleIndex++;
                        }
                        this.imageBox1.Image = gray1;
                        this.imageBox2.Image = gray2;
                        Thread.Sleep(200);
                    }
                    else
                    {
                        homographyMatrix = CameraCalibration.FindHomography(corners_points_Left[sampleSize - 1], corners_points_Right[sampleSize - 1], Emgu.CV.CvEnum.HOMOGRAPHY_METHOD.DEFAULT, 2.0);
                        this.setCalibrationState(CalibrationState.Demo, "Homography Complete");
                    }
                        
                    break;

                case CalibrationState.Intrinsics_Left:
                    
                    if(sampleIndex < sampleSize)
                    { 
                        PointF[] cornersLeft = CameraCalibration.FindChessboardCorners(gray1, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                        if(cornersLeft != null)
                        {
                            gray1.FindCornerSubPix(new PointF[1][] { cornersLeft }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                            corners_points_Left[sampleIndex] = cornersLeft;
                            this.setCalibrationState(CalibrationState.Intrinsics_Left, sampleIndex.ToString());
                            sampleIndex++;
                        }
                        this.imageBox1.Image = gray1;
                        Thread.Sleep(200);
                    }
                    else
                    {
                        CameraCalibration.CalibrateCamera(corners_object_Points, corners_points_Left, img1.Size,
                            IntrinsicCam1, Emgu.CV.CvEnum.CALIB_TYPE.DEFAULT, out ExtrinsicCam1);
                        this.setCalibrationState(CalibrationState.Demo, "Calibration Left Finished");
                    }

                    break;

                case CalibrationState.Intrinsics_Right: 
                    
                    if (sampleIndex < sampleSize)
                    {
                        PointF[] cornersRight = CameraCalibration.FindChessboardCorners(gray2, patternSize, Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH);
                        if (cornersRight != null)
                        {
                            gray2.FindCornerSubPix(new PointF[1][] { cornersRight }, new Size(11, 11), new Size(-1, -1), new MCvTermCriteria(30, 0.01));

                            corners_points_Right[sampleIndex] = cornersRight;
                            this.setCalibrationState(CalibrationState.Intrinsics_Right, sampleIndex.ToString());
                            sampleIndex++;
                        }
                        this.imageBox2.Image = gray2;
                        Thread.Sleep(200);
                    }
                    else
                    {
                        CameraCalibration.CalibrateCamera(corners_object_Points, corners_points_Right, img2.Size,
                            IntrinsicCam2, Emgu.CV.CvEnum.CALIB_TYPE.DEFAULT, out ExtrinsicCam2);
                        this.setCalibrationState(CalibrationState.Demo, "Calibration Right Finished");
                    }

                    break;
            }
        }

        private void calibrateLeft_Click(object sender, EventArgs e)
        {
            if (this.state == CalibrationState.Demo)
            {
                sampleIndex = 0;
                this.IntrinsicCam1 = new IntrinsicCameraParameters();
                this.setCalibrationState(CalibrationState.Intrinsics_Left, null);
            }
        }

        private void calibrateRight_Click(object sender, EventArgs e)
        {
            if (this.state == CalibrationState.Demo)
            {
                sampleIndex = 0;
                this.IntrinsicCam2 = new IntrinsicCameraParameters();
                this.setCalibrationState(CalibrationState.Intrinsics_Right, null);
            }
        }

        private void StartCameras_Click(object sender, EventArgs e)
        {
            cam1 = new Capture((int)this.LeftCamIndex.Value);
            cam2 = new Capture((int)this.RightCamIndex.Value);

            cam1.ImageGrabbed += cam1_ImageGrabbed;

            cam1.Start();
            cam2.Start();

            this.CalibrateHomography.Enabled = true;
            this.calibrateLeft.Enabled = true;
            this.calibrateRight.Enabled = true;
        }

        private void setCalibrationState(CalibrationState state, string extra)
        {
            this.state = state;
            string text = "CameraSetup - " + state.ToString();
            if(extra != null)
                text += " - " + extra;

            if (this.InvokeRequired)
            {
                this.Invoke(this.setText, new object[] { text });
            }
            else
                this.Text = text;
        }

        private void setTitleText(string text)
        {
            this.Text = text;
        }
    }
}
