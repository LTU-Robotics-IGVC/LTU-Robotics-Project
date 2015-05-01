using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.CV.Stitching;
using Emgu.CV.GPU;
using Emgu.CV;
using Emgu.Util;

namespace IGVC_Controller.Code.Modules.Vision
{
    class WideAngleMonoVision : IModule
    {
        GatedVariable leftCamFeed;
        GatedVariable rightCamFeed;

        public WideAngleMonoVision() : base()
        {
            this.addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
        }

        public override void process()
        {
            using(Stitcher stitcher = new Stitcher(false))
            {
                leftCamFeed.shiftObject();
                rightCamFeed.shiftObject();

                Image<Bgr, byte> img1 = (Image<Bgr, byte>)leftCamFeed.getObject();
                Image<Bgr, byte> img2 = (Image<Bgr, byte>)rightCamFeed.getObject();

                Image<Bgr, byte> stitchedImage = stitcher.Stitch(new Image<Bgr, byte>[] { img1, img2 });

                this.sendDataToRegistry(INTERMODULE_VARIABLE.STITCHED_IMAGE, stitchedImage);
            }
            base.process();
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            base.writeToConfig(config);
        }

        public override bool startup()
        {
            leftCamFeed = new GatedVariable();
            rightCamFeed = new GatedVariable();

            return base.startup();
        }

        public override void shutdown()
        {
            base.shutdown();
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.VISION_LEFT:
                    leftCamFeed.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.VISION_RIGHT:
                    rightCamFeed.setObject(data);
                    break;
            }
        }
    }
}
