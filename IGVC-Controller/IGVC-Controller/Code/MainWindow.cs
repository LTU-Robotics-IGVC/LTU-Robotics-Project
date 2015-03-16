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
using IGVC_Controller.Code.Registries;
using IGVC_Controller.Code.Modules.Logger;
using IGVC_Controller.Code.Modules.Vision;
using IGVC_Controller.DataIO;

namespace IGVC_Controller
{
    public partial class MainWindow : Form
    {
        Registry registry = new Registry();
        public static SaveFile config = new SaveFile("config");

        public MainWindow()
        {
            InitializeComponent();
            //capture = new Capture();
            //capture.ImageGrabbed += capture_ImageGrabbed;
            //capture.ImageGrabbed += process_FPS;
            //capture.Start();
            this.FormClosing += Form1_FormClosing;

            Logger logger = new Logger();
            registry.register(logger);

            Vision vision = new Vision();
            registry.register(vision);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
