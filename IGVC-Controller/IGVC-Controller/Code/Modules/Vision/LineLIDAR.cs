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
    class LineLIDAR : IModule
    {
        GatedVariable cimage;

        public LineLIDAR() : base()
        {
            this.modulePriority = 52;
            this.addSubscription(INTERMODULE_VARIABLE.COLLISION_IMAGE);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.COLLISION_IMAGE:
                    cimage.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            cimage = new GatedVariable();
            return base.startup();
        }

        public override void process()
        {
            cimage.shiftObject();

            Image<Gray, byte> collisionImage = (Image<Gray, byte>)cimage.getObject();

            if(collisionImage != null)
            {
                //Place filters here
                collisionImage = collisionImage.Resize(300, 300, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
                bool[,] data = new bool[300, 300];
                for (int x = 0; x < 300; x++)
                {
                    for (int y = 0; y < 300; y++)
                    {
                        if (collisionImage.Data[y, x, 0] == 255)
                            data[x, y] = true;
                    }
                }

                List<Point> points = new List<Point>();
                for(double i = 0; i < 180.0; i+=1.0)
                {
                    points.Add(rayTrace(i, data));
                }

                //Find lines
                List<LineSegment2D> lines = new List<LineSegment2D>();
                for(int i = 0; i < points.Count; i++)
                {
                    if (points[i].X != -1)
                    {
                        Point s = points[i];
                        Point last = s;
                        int subi;
                        for (subi = i; subi < points.Count && points[subi].X != -1; subi++)
                        {
                            if (MathXHelper.getPointDis(points[subi], last) < 3.0)
                            {
                                last = points[subi];
                            }
                            else
                                break;
                        }
                        if(MathXHelper.getPointDis(s, last) > 5.0)
                        {
                            lines.Add(new LineSegment2D(s, last));
                        }
                    }
                }

                Image<Gray, byte> collision2 = new Image<Gray, byte>(300, 300);
                //Connect lines
                Gray white = new Gray(255);
                for(int i = 0; i < lines.Count; i++)
                {
                    collision2.Draw(lines[i], white, 3);
                    Point closest1 = new Point(3000, 3000);
                    Point closest2 = new Point(3000, 3000);
                    double min1 = 1000.0;
                    double min2 = 1000.0;
                    double testMin = 0.0;
                    for(int ii = 0; ii < lines.Count; ii++)
                    {
                        if(i == ii)
                            continue;
                        if((testMin = MathXHelper.getPointDis(lines[i].P1, lines[ii].P1)) < min1)
                        {
                            closest1 = lines[ii].P1;
                            min1 = testMin;
                        } 
                        
                        if ((testMin = MathXHelper.getPointDis(lines[i].P1, lines[ii].P2)) < min1)
                        {
                            closest1 = lines[ii].P2;
                            min1 = testMin;
                        }

                        if ((testMin = MathXHelper.getPointDis(lines[i].P2, lines[ii].P1)) < min2)
                        {
                            closest2 = lines[ii].P1;
                            min2 = testMin;
                        }

                        if ((testMin = MathXHelper.getPointDis(lines[i].P2, lines[ii].P2)) < min2)
                        {
                            closest2 = lines[ii].P2;
                            min2 = testMin;
                        }
                    }

                    if(min1 < 50.0)
                    {
                        //connect closest1 to p1
                        collision2.Draw(new LineSegment2D(lines[i].P1, closest1), white, 3);
                    }

                    if(min2 < 50.0)
                    {
                        collision2.Draw(new LineSegment2D(lines[i].P2, closest2), white, 3);
                    }
                }

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, 
                    collision2.Resize(1000, 1000, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));
            }


            base.process();
        }

        public Point rayTrace(double angleD, bool[,] map)
        {
            double dis = 0.0;
            int s = 0;
            Point p;
            while((s = getCellStateForDisAndAngle(angleD / 180.0 * Math.PI, dis, map, ref p)) == 0)
            {
                dis += 0.5;
            }

            return p;
        }

        public int getCellStateForDisAndAngle(double angleR, double dis, bool[,] map, ref Point p)
        {
            double x = dis * Math.Cos(angleR) + 150.0;
            double y = -dis * Math.Sin(angleR) + 300.0;
            int X = (int)x;
            if(x - (double)X > 0.5)
                X++;
            int Y = (int)y;
            if(y - (double)Y > 0.5)
                Y++;
            p = new Point(X, Y);
            if(X >= 0 && X < 300 && Y >= 0 && Y < 300)
                if(map[X, Y])
                    return 1;
                else 
                    return 0;
            p = new Point(-1, -1);
            return -1;
        }
    }
}
