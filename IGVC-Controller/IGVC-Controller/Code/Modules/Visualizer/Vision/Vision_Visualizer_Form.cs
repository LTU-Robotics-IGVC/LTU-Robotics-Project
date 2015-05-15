using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.Visualizer.Vision
{
    public partial class Vision_Visualizer_Form : Form
    {
        public Vision_Visualizer_Form()
        {
            InitializeComponent();
        }

        public void setImage(int index, Image<Bgr, byte> image)
        {
            image = image.Resize(0.7, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            switch(index)
            {
                case 0:
                    this.imageBox1.Image = image;
                    break;
                case 1:
                    this.imageBox2.Image = image;
                    break;
                case 2:
                    if (image != null && image.Width > 1 && image.Height > 1)
                        this.imageBox3.Image = image;
                    break;
            }
        }

        private void Vision_Visualizer_Form_Load(object sender, EventArgs e)
        {

        }
    }
}
