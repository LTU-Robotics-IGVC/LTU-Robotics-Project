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

        public DualVisionObstacleReprojection() : base()
        {
            this.modulePriority = 52;
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT);
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

        public override void process()
        {
            obstacleLeft.shiftObject();
            obstacleRight.shiftObject();

            Image<Gray, byte> collisionMap = new Image<Gray, byte>(600, 600);

            Image<Gray, byte> obstLeft = (Image<Gray, byte>)obstacleLeft.getObject();
            if(obstLeft != null)
            {
                //The Region of Interest may need to be adjusted to ignore the horizon
                Rectangle rect = obstLeft.ROI;
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

                HomographyMatrix homography = CameraCalibration.GetPerspectiveTransform(src, dst);
                collisionMap = collisionMap.Add(obstLeft.WarpPerspective(homography, 600, 600, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            }

            Image<Gray, byte> obstRight = (Image<Gray, byte>)obstacleRight.getObject();
            if(obstRight != null)
            {
                Rectangle rect = obstRight.ROI;
                PointF[] src = new PointF[]
                {
                    new PointF(rect.Left, rect.Bottom),
                    new PointF(rect.Right, rect.Bottom),
                    new PointF(rect.Right, rect.Top),
                    new PointF(rect.Left, rect.Top)
                };

                PointF[] dst = new PointF[]
                {
                    new PointF(300, 150),
                    new PointF(450, 300),
                    new PointF(600, 300),
                    new PointF(300, 0)
                };

                HomographyMatrix homography = CameraCalibration.GetPerspectiveTransform(src, dst);
                collisionMap = collisionMap.Add(obstRight.WarpPerspective(homography, 600, 600, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR, Emgu.CV.CvEnum.WARP.CV_WARP_DEFAULT, new Gray(0)));
            }

            this.sendDataToRegistry(INTERMODULE_VARIABLE.COLLISION_IMAGE, collisionMap);
        }
    }
}
