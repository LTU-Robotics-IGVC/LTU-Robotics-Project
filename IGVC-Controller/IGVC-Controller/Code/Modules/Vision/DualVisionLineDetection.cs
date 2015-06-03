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
    class DualVisionLineDetection : IModule
    {
        GatedVariable visionleft;
        GatedVariable visionright;

        public int whiteMin = 150;

        public Rectangle leftVisionRange;
        public Rectangle rightVisionRange;

        public DualVisionLineDetection() : base()
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
            this.whiteMin = config.Read<int>("whiteMin", this.whiteMin);
            this.leftVisionRange = config.Read<Rectangle>("leftVisionRange", leftVisionRange);
            this.rightVisionRange = config.Read<Rectangle>("rightVisionRange", rightVisionRange);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write("whiteMin", whiteMin);
            config.Write<Rectangle>("leftVisionRange", leftVisionRange);
            config.Write<Rectangle>("rightVisionRange", rightVisionRange);

            base.writeToConfig(config);
        }

        public override void process()
        {
            visionleft.shiftObject();
            visionright.shiftObject();

            Image<Bgr, byte> leftColor = (Image<Bgr, byte>)visionleft.getObject();
            Image<Bgr, byte> rightColor = (Image<Bgr, byte>)visionright.getObject();

            if(leftColor != null)
            {
                Image<Gray, byte> leftGray = leftColor.Convert<Gray, byte>();
                leftGray = leftGray.ThresholdBinary(new Gray(whiteMin), new Gray(255));
                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT, leftGray);
            }
            if(rightColor != null)
            {
                Image<Gray, byte> rightGray = rightColor.Convert<Gray, byte>();
                rightGray = rightGray.ThresholdBinary(new Gray(whiteMin), new Gray(255));
                this.sendDataToRegistry(INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT, rightGray);
            }

            base.process();
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
            return base.getEditorForm();
        }
    }
}
