using Emgu.CV;
using Emgu.CV.Structure;
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
    public partial class HoughLinesObstacleFilteringEditor : Form, IModuleEditor
    {
        Capture capture;
        HoughLinesObstacleFiltering module;

        public HoughLinesObstacleFilteringEditor()
        {
            InitializeComponent();
        }

        private void StartCamera_Click(object sender, EventArgs e)
        {
            if (capture != null)
                capture.Stop();
            capture = new Capture(DualWebcam.cap1Index);
            capture.ImageGrabbed += capture_ImageGrabbed;
            capture.Start();
        }

        void IModuleEditor.setModule(IModule module)
        {
            this.module = (HoughLinesObstacleFiltering)module;
            ((IModuleEditor)this).loadDataFromModule();
        }

        void IModuleEditor.loadDataFromModule()
        {
            this.PriorityBox.Value = module.modulePriority;
        }

        void IModuleEditor.setDataToModule()
        {
            module.modulePriority = (int)this.PriorityBox.Value;
        }

        void capture_ImageGrabbed(object sender, EventArgs e)
        {
            Image<Bgr, byte> color = capture.RetrieveBgrFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

            Image<Gray, byte> gray = color.Convert<Gray, byte>();
            //gray.HoughLinesBinary(1, Math.PI/ 0.0, 50, 100, 1);
            LineSegment2D[][] lines = gray.HoughLines((double)cannyThresh.Value, (double)cannyThreshLinking.Value,
                1.0, (double)this.hScrollBar1.Value/360.0, 50, 20, 1);

            for(int x = 0; x < lines.Length; x++)
            {
                for(int y = 0; y < lines[x].Length; y++)
                {
                    color.Draw(lines[x][y], new Bgr(Color.Red), 3);
                    LineSegment2D line = lines[x][y];
                    Point p1 = line.P1;
                    Point p2 = line.P2;
                    double L = line.Length;
                    PointF D = line.Direction;
                    Point _p1 = new Point(p1.X + (int)((L / 2.0) * D.X), p1.Y + (int)((L / 2.0) * D.Y));
                    Point _p2 = new Point(p2.X - (int)((L / 2.0) * D.X), p2.Y - (int)((L / 2.0) * D.Y));
                    LineSegment2D newLine = new LineSegment2D(_p1, _p2);
                    color.Draw(newLine, new Bgr(Color.Blue), 1);
                }
            }

            this.imageBox1.Image = color;
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
    }
}
