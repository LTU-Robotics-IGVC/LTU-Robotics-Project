using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.Visualizer.GeneralVisualizer
{
    public partial class GeneralVisualizerForm : Form
    {
        public GeneralVisualizerForm()
        {
            InitializeComponent();
        }

        public void setData(IModule.INTERMODULE_VARIABLE var, object data)
        {
            switch(var)
            {
                case IModule.INTERMODULE_VARIABLE.VISION_LEFT:
                    this.imageBox1.Image = (Image<Bgr, byte>)data;
                    break;
                case IModule.INTERMODULE_VARIABLE.VISION_RIGHT:
                    this.imageBox2.Image = (Image<Bgr, byte>)data;
                    break;
                case IModule.INTERMODULE_VARIABLE.COLLISION_IMAGE:
                    this.imageBox3.Image = (Image<Gray, byte>)data;
                    break;
                case IModule.INTERMODULE_VARIABLE.NAV_MESH:
                    NavMesh mesh = (NavMesh)data;
                    Image<Gray, byte> img = new Image<Gray, byte>(mesh.Width, mesh.Height);
                    for (int x = 0; x < mesh.Width; x++)
                        for (int y = 0; y < mesh.Height; y++)
                        {
                            img.Data[y, x, 0] = (byte)Math.Min(255, mesh.getNode(x, y).traverseCost);
                        }
                    this.imageBox4.Image = img;
                    break;
                case IModule.INTERMODULE_VARIABLE.NAV_PATH:
                    Path path = (Path)data;
            }
        }
    }
}
