using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.MathX;
using IGVC_Controller.Code.Modules.Cameras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.Vision
{
    public partial class DualVisionObstacleDetectionEditor : Form, IModuleEditor
    {
        DualVisionObstacleDetection module;
        Capture capture;

        public DualVisionObstacleDetectionEditor()
        {
            InitializeComponent();
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (DualVisionObstacleDetection)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            minGreen.Value = (int)(module.minGreen / 180.0 * 255.0);
            maxGreen.Value = (int)(module.maxGreen / 180.0 * 255.0);
            minVal.Value = (int)(module.minVal * 255.0);
            this.PriorityBox.Value = module.modulePriority;
        }

        void IModuleEditor.setDataToModule()
        {
            module.minGreen = minGreen.Value / 255.0 * 180.0;
            module.maxGreen = maxGreen.Value / 255.0 * 180.0;
            module.minVal = minVal.Value / 255.0;
            module.modulePriority = (int)this.PriorityBox.Value;
        }

        private void minGreen_Scroll(object sender, ScrollEventArgs e)
        {
            //if (minGreen.Value > maxGreen.Value)
              //  maxGreen.Value = minGreen.Value;
        }

        private void maxGreen_Scroll(object sender, ScrollEventArgs e)
        {
            //if (maxGreen.Value < minGreen.Value)
              //  minGreen.Value = maxGreen.Value;
        }

        private void StartCamera_Click(object sender, EventArgs e)
        {
            if (capture != null)
                capture.Stop();
            capture = new Capture(DualWebcam.cap1Index);
            capture.ImageGrabbed += capture_ImageGrabbed;
            capture.Start();
        }

        void capture_ImageGrabbed(object sender, EventArgs e)
        {
            Image<Bgr, byte> color = capture.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            Image<Hsv, byte> hsv = color.Convert<Hsv, byte>();
            Image<Gray, byte> filtered = ImageFiltering.HSVFilter(new Hsv((double)minGreen.Value / 255.0 * 180.0, 0, minVal.Value/255.0),
                new Hsv((double)maxGreen.Value / 255.0 * 180.0, 1.0, 1.0), hsv);

            imageBox1.Image = filtered;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (capture != null)
            {
                capture.Stop();
                capture.Dispose();
            }
            ((IModuleEditor)this).setDataToModule();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void PriorityBox_ValueChanged(object sender, EventArgs e)
        {

        }

        private void minVal_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void imageBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
