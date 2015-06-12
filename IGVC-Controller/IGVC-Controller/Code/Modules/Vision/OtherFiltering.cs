using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Vision
{
    class OtherFiltering : IModule
    {
        GatedVariable leftImage;
        GatedVariable rightImage; 
        GatedVariable leftObs;
        GatedVariable rightObs;

        public OtherFiltering() : base()
        {
            this.modulePriority = 30;
            this.addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.VISION_LEFT:
                    leftImage.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.VISION_RIGHT:
                    rightImage.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT:
                    leftObs.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT:
                    rightObs.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            leftImage = new GatedVariable();
            rightImage = new GatedVariable();
            leftObs = new GatedVariable();
            rightObs = new GatedVariable();
            return base.startup();
        }

        public override void process()
        {
            leftImage.shiftObject();
            rightImage.shiftObject();
            leftObs.shiftObject();
            rightObs.shiftObject();

            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)leftImage.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)rightImage.getObject();
            Image<Gray, byte> leftGrayObs = (Image<Gray, byte>)leftObs.getObject();
            Image<Gray, byte> rightGrayObs = (Image<Gray, byte>)rightObs.getObject();

            if(leftColor != null && rightColor != null)
            {
                //Place filters here

                //leftColor = ImageFiltering.BlackoutInverted(leftColor, new Rectangle(100, 200, 160, 40));
                //leftColor = leftColor.SmoothMedian(5);
                //Image<Gray, byte> bfilter = ImageFiltering.HSVFilter(new Hsv(166.0, 0.0, 0.0), new Hsv(75.0, 1.0, 0.8), leftColor.Convert<Hsv, byte>());
                //leftColor.SetValue(new Bgr(Color.Black), bfilter);

                //Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
                //leftGray = ImageFiltering.Threshold(leftGray, 200);

                //rightColor = ImageFiltering.BlackoutInverted(rightColor, new Rectangle(100, 200, 160, 40));
                //rightColor = rightColor.SmoothMedian(5);
                //bfilter = ImageFiltering.HSVFilter(new Hsv(166.0, 0.0, 0.0), new Hsv(75.0, 1.0, 0.8), rightColor.Convert<Hsv, byte>());
                //rightColor.SetValue(new Bgr(Color.Black), bfilter);

                //Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();
                //rightGray = rightGray.ThresholdBinary(new Gray(200), new Gray(255));

                Image<Hsv, Byte> hsvImageleft = leftColor.Convert<Hsv, Byte>();
                Image<Hsv, Byte> hsvImageRight = rightColor.Convert<Hsv, Byte>();

                /*
                for (int w = 0; w < leftColor.Width;w++ )
                {
                    for(int h = 0; h < leftColor.Height; h++)
                    {
                        if((hsvImageleft.Data[h,w,0] < 170)&&(hsvImageleft.Data[h,w,0] > 130))
                        {
                            hsvImageleft.Data[h, w, 2] = 255;
                            hsvImageleft.Data[h, w, 1] = 255;
                        }
                        else
                        {
                            hsvImageleft.Data[h, w, 2] = 0;
                            hsvImageleft.Data[h, w, 1] = 0;
                        }
                    }
                }

                for (int w = 0; w < rightColor.Width; w++)
                {
                    for (int h = 0; h < rightColor.Height; h++)
                    {
                        if ((hsvImageRight.Data[h, w, 0] < 170) && (hsvImageRight.Data[h, w, 0] > 130))
                        {
                            hsvImageRight.Data[h, w, 2] = 255;
                            hsvImageRight.Data[h, w, 1] = 255;
                        }
                        else
                        {
                            hsvImageRight.Data[h, w, 2] =0;
                            hsvImageRight.Data[h, w, 1] = 0;
                        }
                    }
                }
                /**/

                Image<Gray, byte> leftGray = hsvImageleft.Convert<Gray, byte>();//.Threshold(hsvImageleft.Convert<Gray, byte>(), 20);
                Image<Gray, byte> rightGray = hsvImageRight.Convert<Gray, byte>();// ImageFiltering.Threshold(hsvImageRight.Convert<Gray, byte>(), 20);

                double rho = 4;
                int line_threshold = 100;
                double minLineWidth = 10;
                double linegap = 10;
                double theta = Math.PI / 180;

                Image<Gray, Byte> line_CannyLeft = leftGrayObs.ThresholdBinary(new Gray(150), new Gray(255));
                Image<Gray, Byte> line_CannyRight = rightGrayObs.ThresholdBinary(new Gray(150),new Gray(255));

                line_CannyLeft = line_CannyLeft.Canny(150, 255);
                line_CannyRight = line_CannyRight.Canny(150, 255);

                LineSegment2D[][] linesLeft = line_CannyLeft.HoughLinesBinary(rho, theta, line_threshold, minLineWidth, linegap);
                LineSegment2D[][] linesRight = line_CannyRight.HoughLinesBinary(rho, theta, line_threshold, minLineWidth, linegap);

                Image<Gray, Byte> lines_Left = new Image<Gray, Byte>(line_CannyLeft.Width, line_CannyLeft.Height);
                Image<Gray, Byte> lines_Right = new Image<Gray, Byte>(line_CannyLeft.Width, line_CannyLeft.Height);

                Point closetPoint = new Point(0, 0);
                int y = 0;
                int leftLines = 0;
                int leftMIddleLines = 0;
                int middleLines = 0;
                int rightMIddleLines = 0;
                int rightLInes = 0;
                int searchWidth=30;
                int searchHeight=30;

                //left image
                if(linesLeft[0].Length>0 && linesLeft[0].Length<100)
                {
                    for(int i=0;i<linesLeft[0].Length-1;i++)
                    {
                        if(linesLeft[0][i].Length>10)
                        {
                            line_CannyLeft.Draw(linesLeft[0][i],new Gray(255),3);
                            lines_Left.Draw(linesLeft[0][i], new Gray(255), 3);
                        }
                        if (linesLeft[0][i].P1.X - searchWidth > 0 && linesLeft[0][i].P1.X + searchWidth < line_CannyLeft.Width && linesLeft[0][i].P1.X - searchHeight > 0)
                            for (int w = linesLeft[0][i].P1.X - searchWidth; w < linesLeft[0][i].P1.X + searchWidth; w++)
                                for (int h = linesLeft[0][i].P1.Y; h > linesLeft[0][i].P1.Y - searchHeight; h--)
                                    for (int lineCount=0; lineCount < linesLeft[0].Length - 1; lineCount++)
                                        if (new Point(w, h) == linesLeft[0][lineCount].P2)
                                            closetPoint = new Point(w, h);
                        if (closetPoint != new Point(0, 0))
                        {
                            line_CannyLeft.Draw(new LineSegment2D(closetPoint, linesLeft[0][i].P1), new Gray(255), 3);
                            lines_Left.Draw(new LineSegment2D(closetPoint, linesLeft[0][i].P1), new Gray(255), 3);
                            closetPoint = new Point(0, 0);
                        }
                    }
                }

                //right image
                if (linesRight[0].Length > 0 && linesRight[0].Length < 100)
                {
                    for (int i = 0; i < linesRight[0].Length - 1; i++)
                    {
                        if (linesRight[0][i].Length > 10)
                        {
                            line_CannyRight.Draw(linesRight[0][i], new Gray(255), 3);
                            lines_Right.Draw(linesRight[0][i], new Gray(255), 3);
                        }
                        if (linesRight[0][i].P1.X - searchWidth > 0 && linesRight[0][i].P1.X + searchWidth < line_CannyRight.Width && linesRight[0][i].P1.X - searchHeight > 0)
                            for (int w = linesRight[0][i].P1.X - searchWidth; w < linesRight[0][i].P1.X + searchWidth; w++)
                                for (int h = linesRight[0][i].P1.Y; h > linesRight[0][i].P1.Y - searchHeight; h--)
                                    for (int lineCount=0; lineCount < linesRight[0].Length - 1; lineCount++)
                                        if (new Point(w, h) == linesRight[0][lineCount].P2)
                                            closetPoint = new Point(w, h);
                        if (closetPoint != new Point(0, 0))
                        {
                            line_CannyRight.Draw(new LineSegment2D(closetPoint, linesRight[0][i].P1), new Gray(255), 3);
                            lines_Right.Draw(new LineSegment2D(closetPoint, linesRight[0][i].P1), new Gray(255), 3);
                            closetPoint = new Point(0, 0);
                        }
                    }
                }

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, lines_Left);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, lines_Right);

                this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_LEFT, leftGrayObs.Convert<Bgr, byte>());
                this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_RIGHT, rightGrayObs.Convert<Bgr, byte>());
            }

            base.process();
        }
    }
}
