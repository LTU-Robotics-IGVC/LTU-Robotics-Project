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

namespace IGVC_Controller.Code.Modules.Navigation
{
    class LaneSteering : IModule
    {
        GatedVariable obstacles;
        GatedVariable isGPSSteering;
        GatedVariable laneFollowing;

        double baseSpeed = 1.0;

        public LaneSteering() : base()
        {
            this.modulePriority = 70;
            this.addSubscription(INTERMODULE_VARIABLE.COLLISION_IMAGE);
            this.addSubscription(INTERMODULE_VARIABLE.GPS_IS_STEERING);
            this.addSubscription(INTERMODULE_VARIABLE.LANE_FOLLOWING);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.COLLISION_IMAGE:
                    obstacles.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.GPS_IS_STEERING:
                    isGPSSteering.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.LANE_FOLLOWING:
                    laneFollowing.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            obstacles = new GatedVariable();
            isGPSSteering = new GatedVariable();
            isGPSSteering.setObject(false);
            laneFollowing = new GatedVariable();
            laneFollowing.setObject(true);
            return base.startup();
        }

        public override void shutdown()
        {
            base.shutdown();
        }

        public override void process()
        {
            obstacles.shiftObject();
            isGPSSteering.shiftObject();
            laneFollowing.shiftObject();

            if ((bool)laneFollowing.getObject() == false)
                return;

            Image<Gray, byte> img = (Image<Gray, byte>)obstacles.getObject();
            if(img != null)
            {
                img = img.Resize(200, 200, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

                if(isGPSSteering.getObject() != null && (bool)isGPSSteering.getObject() == true)
                {
                    //check if image has enough detection to warrant overriding the steering info
                    int[] counts = img.CountNonzero();
                    int whiteCount = counts[0];
                    if (whiteCount < 20)
                    {
                        this.sendDataToRegistry(INTERMODULE_VARIABLE.LANE_FOLLOWING, false);
                        return;
                    }
                }

                Stack<Point> centers = new Stack<Point>();
                Point s = new Point(100, 199);
                centers.Push(s);
                for(int y = 198; y >= 0; y--)
                {
                    Point p = centers.Peek();
                    Point l = p;
                    Point r = p;
                    while(img.Data[l.Y, l.X, 0] == 0 && l.X > 0)
                    {
                        l.X--;
                    }

                    while(img.Data[r.Y, r.X, 0] == 0 && r.X < 199)
                    {
                        r.X++;
                    }

                    Point c = MathXHelper.getMidPoint(l, r);
                    c.Y--;

                    centers.Push(c);
                }

                Path path = new Path(200, 200);
                int diffY = 200;
                int sumX = 0;
                double slopesum = 0.0;
                double count = 0.0;
                double weight = 1.0;
                while(centers.Count > 1)
                {
                    Point p = centers.Pop();
                    path.AddNode(p);
                    int rise = s.Y - p.Y;
                    int run = p.X - s.X;
                    slopesum += 1.0/((double)rise / (double)run);
                    sumX += p.X;
                    count += 1.0;
                    weight *= 0.9;
                }
                this.sendDataToRegistry(INTERMODULE_VARIABLE.NAV_PATH, path);
                //note that slopesum is inverted
                double slope = slopesum / count;

                slope *= -1;
                double xx = (double)sumX / count;
                slope = (xx / 100.0) - 1.0;

                this.proportionalSteeering(slope);
                /*if (slope < -0.2)
                    hardLeft();
                else if (slope < -0.05)
                    slightLeft();
                else if (slope < 0.05)
                    forward();
                else if (slope < 0.2)
                    slightRight();
                else
                    hardRight();*/
            }

            //this.sendDataToRegistry(INTERMODULE_VARIABLE.COLLISION_IMAGE, img);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.IS_AUTONOMOUS, true);

            base.process();
        }

        private void proportionalSteeering(double slope)
        {
            double bias = -0.1;
            slope = slope + bias;
            if (slope < 0)
            {
                double s = baseSpeed + (1.1-bias) * (slope * baseSpeed);
                if (s < 0)
                    s = 0;
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, s);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
            }
            else if(slope > 0)
            {
                double s = baseSpeed - (1.1+bias) * (slope * baseSpeed);
                if (s < 0)
                    s = 0;
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, s);
            }
            else
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
            }
        }

        private void slightLeft()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed * 0.5);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
        }

        private void slightRight()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed * 0.5);
        }

        private void hardLeft()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, 0.0);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
        }

        private void hardRight()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, 0.0);
        }

        private void forward()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
        }
    }
}
