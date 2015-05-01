using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Visualizer.Vision
{
    class Vision_Visualizer : IModule
    {
        GatedVariable visionLeft;
        GatedVariable visionRight;
        GatedVariable stitchedImage;

        Vision_Visualizer_Form form;

        delegate void delegateCloseForm();

        delegate void delegateSetImage(int index, object data);
        private delegateSetImage setImageDelegate;

        public Vision_Visualizer() : base()
        {
            this.addSubscription(INTERMODULE_VARIABLE.VISION_RIGHT);
            this.addSubscription(INTERMODULE_VARIABLE.VISION_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.STITCHED_IMAGE);
            this.setImageDelegate = this.setImage;
            this.modulePriority = 90;
        }

        public override bool startup()
        {
            visionLeft = new GatedVariable();
            visionRight = new GatedVariable();
            stitchedImage = new GatedVariable();
            form = new Vision_Visualizer_Form();
            form.Show();
            return base.startup();
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.VISION_LEFT:
                    visionLeft.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.VISION_RIGHT:
                    visionRight.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.STITCHED_IMAGE:
                    stitchedImage.setObject(data);
                    break;
            }
        }

        public override void process()
        {
            visionLeft.shiftObject();
            visionRight.shiftObject();
            stitchedImage.shiftObject();

            if (form.InvokeRequired)
            {
                form.Invoke(this.setImageDelegate, new object[] { 0, visionLeft.getObject() });
                form.Invoke(this.setImageDelegate, new object[] { 1, visionRight.getObject() });
                form.Invoke(this.setImageDelegate, new object[] { 2, stitchedImage.getObject() });
            }

            base.process();
        }

        public override void shutdown()
        {
            if (form.InvokeRequired)
            {
                delegateCloseForm del = this.closeForm;
                form.Invoke(del, null);
            }
            else
            {
                form.Close();
            }
            base.shutdown();
        }

        private void closeForm()
        {
            form.Close();
        }

        private void setImage(int index, object data)
        {
            form.setImage(index, (Image<Bgr, byte>)data);
        }
    }
}
