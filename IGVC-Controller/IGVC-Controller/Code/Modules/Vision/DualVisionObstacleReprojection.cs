using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Vision
{
    class DualVisionObstacleReprojection : IModule
    {
        GatedVariable obstacleLeft;
        GatedVariable obstacleRight;

        public HomographyMatrix leftHomography;
        public HomographyMatrix rightHomography;

        public Point[] leftCaliCorners;
        public Point[] rightCaliCorners;
        public Point[] worldCaliCorners;

        public DualVisionObstacleReprojection() : base()
        {
            this.modulePriority = 54;
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT);

            //set default homography matrices
            //The Region of Interest may need to be adjusted to ignore the horizon
            Rectangle rect = new Rectangle(0, 0, 320, 240);

            PointF[] src = new PointF[]
                {
                    new PointF(rect.Left, rect.Bottom),
                    new PointF(rect.Right, rect.Bottom),
                    new PointF(rect.Right, rect.Top),
                    new PointF(rect.Left, rect.Top)
                };

            //These are the planar points of the cameras perspective
            PointF[] dst = new PointF[]
                {
                    new PointF(150, 300),
                    new PointF(300, 150),
                    new PointF(300, 0),
                    new PointF(0, 300)
                };

            leftHomography = CameraCalibration.GetPerspectiveTransform(src, dst);

            dst = new PointF[]
                {
                    new PointF(300, 150),
                    new PointF(450, 300),
                    new PointF(600, 300),
                    new PointF(300, 0)
                };


            rightHomography = CameraCalibration.GetPerspectiveTransform(src, dst);

            leftCaliCorners = new Point[]
            {
                new Point(0, 0),
                new Point(320, 0),
                new Point(320, 240),
                new Point(0, 240)
            };

            rightCaliCorners = new Point[]
            {
                new Point(0, 0),
                new Point(320, 0),
                new Point(320, 240),
                new Point(0, 240)
            };

            worldCaliCorners = new Point[]
            {
                new Point(0, 0),
                new Point(320, 0),
                new Point(320, 240),
                new Point(0, 240)
            };
            
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT:
                    obstacleLeft.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT:
                    obstacleRight.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            obstacleLeft = new GatedVariable();
            obstacleRight = new GatedVariable();

            return base.startup();
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            leftHomography = config.Read<HomographyMatrix>("leftHomography", leftHomography);
            rightHomography = config.Read<HomographyMatrix>("rightHomography", rightHomography);
            leftCaliCorners = config.Read<Point[]>("leftCaliCorners", leftCaliCorners);
            rightCaliCorners = config.Read<Point[]>("rightCaliCorners", rightCaliCorners);
            worldCaliCorners = config.Read<Point[]>("worldCaliCorners", worldCaliCorners);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<HomographyMatrix>("leftHomography", leftHomography);
            config.Write<HomographyMatrix>("rightHomography", rightHomography);
            config.Write<Point[]>("leftCaliCorners", leftCaliCorners);
            config.Write<Point[]>("rightCaliCorners", rightCaliCorners);
            config.Write<Point[]>("worldCaliCorners", worldCaliCorners);

            base.writeToConfig(config);
        }

        public override void process()
        {
            obstacleLeft.shiftObject();
            obstacleRight.shiftObject();

            Image<Gray, byte> collisionMap = new Image<Gray, byte>(1000, 1000);

            Image<Gray, byte> obstLeft = (Image<Gray, byte>)obstacleLeft.getObject();
            if(obstLeft != null)
            {
                collisionMap = collisionMap.Add(obstLeft.WarpPerspective(leftHomography, 1000, 1000, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            }

            Image<Gray, byte> obstRight = (Image<Gray, byte>)obstacleRight.getObject();
            if(obstRight != null)
            {
                collisionMap = collisionMap.Add(obstRight.WarpPerspective(rightHomography, 1000, 1000, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            }

            //Block out section of robot that is still seen
            int h = 40;
            Rectangle roi = collisionMap.ROI;
            collisionMap.ROI = new Rectangle(459, 900+h, 82, h);
            //collisionMap.SetZero();
            collisionMap.ROI = roi;

            this.sendDataToRegistry(INTERMODULE_VARIABLE.COLLISION_IMAGE, collisionMap);
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new DualVisionObstacleReprojectionEditor();
        }
    }
}
