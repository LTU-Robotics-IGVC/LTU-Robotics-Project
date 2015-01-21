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
        }

        void capture_ImageGrabbed(object sender, EventArgs e)
        {
            this.imageBox1.Image = capture.RetrieveBgrFrame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
