using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.RobotInterface.Simulator;

namespace IGVC_Controller
{
    public partial class Form1 : Form
    {
        //This is for testing purposes.  To be removed and organized into stereo vision later.
        Capture capture;
        int lastGrabbedTicks = -1;
        int frames;

        public Form1()
        {
            InitializeComponent();
            //capture = new Capture();
            //capture.ImageGrabbed += capture_ImageGrabbed;
            //capture.ImageGrabbed += process_FPS;
            //capture.Start();
            this.FormClosing += Form1_FormClosing;
            Form form = new RobotSimulator();
            form.Visible = true;
            form.Focus();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
                capture.Stop();
        }

        void capture_ImageGrabbed(object sender, EventArgs e)
        {
            this.imageBox1.Image = capture.RetrieveBgrFrame().Resize(640, 480, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).Convert<Gray, byte>();
        }

        void process_FPS(object sender, EventArgs e)
        {
            frames++;
            int now = Environment.TickCount & Int32.MaxValue;
            if (lastGrabbedTicks < 0 || now - lastGrabbedTicks < 0)
            {
                lastGrabbedTicks = now;
                frames = 0;
            }
            if (now - lastGrabbedTicks > 1000)
            {
                this.Invoke(new Action(() => lblFPS.Text = "" + Math.Round(1000 * (frames / (double)(now - lastGrabbedTicks)), 2)));
                lastGrabbedTicks = now;
                frames = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
