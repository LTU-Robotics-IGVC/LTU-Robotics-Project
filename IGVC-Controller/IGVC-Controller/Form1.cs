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

namespace IGVC_Controller
{
    public partial class Form1 : Form
    {
        //This is for testing purposes.  To be removed and organized into stereo vision later.
        Capture capture;

        public Form1()
        {
            InitializeComponent();
            capture = new Capture();
            capture.ImageGrabbed += capture_ImageGrabbed;
            capture.Start();
            this.FormClosing += Form1_FormClosing;
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (capture != null)
                capture.Stop();
        }

        void capture_ImageGrabbed(object sender, EventArgs e)
        {
            this.imageBox1.Image = capture.RetrieveBgrFrame().Resize(640, 480, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
