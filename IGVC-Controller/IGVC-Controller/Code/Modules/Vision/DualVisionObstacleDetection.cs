using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.MathX;
using Emgu.CV.GPU;
using System.Drawing;

namespace IGVC_Controller.Code.Modules.Vision
{
    class DualVisionObstacleDetection : IModule
    {
        GatedVariable visionleft;
        GatedVariable visionright;

        public double minGreen = 70.0;
        public double maxGreen = 50.0;
        public double minVal = 0.25;

        public Rectangle leftVisionRange;
        public Rectangle rightVisionRange;

        public DualVisionObstacleDetection() : base()
        {
            this.modulePriority = 51;
            leftVisionRange = new Rectangle(0, 0, 320, 240);
            rightVisionRange = new Rectangle(0, 0, 320, 240);
            this.addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            //Note that the min is greater than the max because we want everything but green
            this.minGreen = config.Read<double>("minGreen", 70.0);
            this.maxGreen = config.Read<double>("maxGreen", 50.0);
            this.minVal = config.Read<double>("minVal", 0.25);
            this.leftVisionRange = config.Read<Rectangle>("leftVisionRange", leftVisionRange);
            this.rightVisionRange = config.Read<Rectangle>("rightVisionRange", rightVisionRange);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<double>("minGreen", this.minGreen);
            config.Write<double>("maxGreen", this.maxGreen);
            config.Write<double>("minVal", this.minVal);
            config.Write<Rectangle>("leftVisionRange", leftVisionRange);
            config.Write<Rectangle>("rightVisionRange", rightVisionRange);

            base.writeToConfig(config);
        }

        public override void process()
        {
            visionleft.shiftObject();
            visionright.shiftObject();

            if(GpuInvoke.HasCuda)
            {
                processWithoutGPU();
            }
            else
            {
                processWithoutGPU();
            }

            base.process();
        }

        private void processWithoutGPU()
        {
            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)visionleft.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)visionright.getObject();

            if (leftColor != null)
            {
                Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
                Image<Gray, byte> leftObstacles = new Image<Gray, byte>(leftGray.Width, leftGray.Height);

                //Process

                //This is an example until the filters can be figured out (should filter out green and dark colors)
                Image<Hsv, byte> leftHSV = leftColor.Convert<Hsv, byte>();
                leftObstacles = leftObstacles.Add(ImageFiltering.HSVFilter(new Hsv(minGreen, 0, minVal), new Hsv(maxGreen, 1.0, 1.0), leftHSV));

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftObstacles);
                //this.sendDataToRegistry(INTERMODULE_VARIABLE.VISION_LEFT, leftObstacles.Convert<Bgr, byte>());
            }

            if (rightColor != null)
            {
                Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();
                Image<Gray, byte> rightObstacles = new Image<Gray, byte>(rightGray.Width, rightGray.Height);

                //Process

                //This is an example until the filters can be figured out (should filter out green)
                Image<Hsv, byte> rightHSV = rightColor.Convert<Hsv, byte>();
                rightObstacles = rightObstacles.Add(ImageFiltering.HSVFilter(new Hsv(minGreen, 0, minVal), new Hsv(maxGreen, 1.0, 1.0), rightHSV));

                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightObstacles);
            }
        }

        public override bool startup()
        {
            visionleft = new GatedVariable();
            visionright = new GatedVariable();

            return base.startup();
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.VISION_RIGHT:
                    this.visionright.setObject(data);
                    break;

                case INTERMODULE_VARIABLE.VISION_LEFT:
                    this.visionleft.setObject(data);
                    break;
            }
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new DualVisionObstacleDetectionEditor();
        }
    }
}
