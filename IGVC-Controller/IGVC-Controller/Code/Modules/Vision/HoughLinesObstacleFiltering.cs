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
    class HoughLinesObstacleFiltering : IModule
    {
        GatedVariable leftImage;
        GatedVariable rightImage;

        public double cannyThresh = 180.0;
        public double cannyThreshLink = 120.0;

        public HoughLinesObstacleFiltering() : base()
        {
            this.modulePriority = 52;
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT);
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.cannyThresh = config.Read<double>("cannyThresh", cannyThresh);
            this.cannyThreshLink = config.Read<double>("cannyThreshLink", cannyThreshLink);
            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<double>("cannyThresh", cannyThresh);
            config.Write<double>("cannyThreshLink", cannyThreshLink);
            base.writeToConfig(config);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT:
                    leftImage.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT:
                    rightImage.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            leftImage = new GatedVariable();
            rightImage = new GatedVariable();
            return base.startup();
        }

        public override void process()
        {
            leftImage.shiftObject();
            rightImage.shiftObject();

            Image<Gray, byte> leftGray = (Image<Gray, byte>)leftImage.getObject();
            Image<Gray, byte> rightGray = (Image<Gray, byte>)rightImage.getObject();

            if(leftGray != null && rightGray != null)
            {
                //Place filters here

                Gray white = new Gray(255);
                Image<Gray, byte> _leftGray = new Image<Gray, byte>(leftGray.Width, leftGray.Height);
                LineSegment2D[][] lines = leftGray.HoughLines(cannyThresh, cannyThreshLink,
                1.0, (double)1.0 / 360.0, 50, 20, 1);

                for (int x = 0; x < lines.Length; x++)
                {
                    for (int y = 0; y < lines[x].Length; y++)
                    {
                        LineSegment2D line = lines[x][y];
                        Point p1 = line.P1;
                        Point p2 = line.P2;
                        double L = line.Length;
                        PointF D = line.Direction;
                        Point _p1 = new Point(p1.X + (int)((L / 2.0) * D.X), p1.Y + (int)((L / 2.0) * D.Y));
                        Point _p2 = new Point(p2.X - (int)((L / 2.0) * D.X), p2.Y - (int)((L / 2.0) * D.Y));
                        LineSegment2D newLine = new LineSegment2D(_p1, _p2);
                        _leftGray.Draw(newLine, white, 5);
                    }
                }

                Image<Gray, byte> _rightGray = new Image<Gray, byte>(rightGray.Width, rightGray.Height);
                lines = leftGray.HoughLines(cannyThresh, cannyThreshLink,
                1.0, (double)1.0 / 360.0, 50, 20, 1);

                for (int x = 0; x < lines.Length; x++)
                {
                    for (int y = 0; y < lines[x].Length; y++)
                    {
                        LineSegment2D line = lines[x][y];
                        Point p1 = line.P1;
                        Point p2 = line.P2;
                        double L = line.Length;
                        PointF D = line.Direction;
                        Point _p1 = new Point(p1.X + (int)((L / 2.0) * D.X), p1.Y + (int)((L / 2.0) * D.Y));
                        Point _p2 = new Point(p2.X - (int)((L / 2.0) * D.X), p2.Y - (int)((L / 2.0) * D.Y));
                        LineSegment2D newLine = new LineSegment2D(_p1, _p2);
                        _rightGray.Draw(newLine, white, 5);
                    }
                }

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, _leftGray);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, _rightGray);
            }

            //this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftGray);
            //this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightGray);

            base.process();
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new HoughLinesObstacleFilteringEditor();
        }
    }
}
