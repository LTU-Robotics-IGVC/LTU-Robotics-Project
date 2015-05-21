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
using System.Diagnostics;

namespace IGVC_Controller.Code.Modules.Navigation
{
    public partial class PathFinderEditor : Form, IModuleEditor
    {
        Image<Gray, byte> img;
        NavMesh navMesh;
        int width = 600;
        int height = 600;

        public PathFinderEditor()
        {
            InitializeComponent();
        }

        void IModuleEditor.setModule(IModule module)
        {

        }

        void IModuleEditor.loadDataFromModule()
        {

        }

        void IModuleEditor.setDataToModule()
        {

        }

        private void TestPathingButton_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            AStarPather pather = new AStarPather(navMesh);
            //pather.setCanTravelDiagonally(true);
            Path path = pather.getPath(new Point(10, 10), new Point(width - 15, height - 15));
            stopwatch.Stop();
            MessageBox.Show("Time = " + stopwatch.ElapsedMilliseconds.ToString() + "ms");
            if(path != null)
            {
                Image<Bgr, byte> color = img.Convert<Bgr, byte>();
                color.DrawPolyline(path.getPointArray(), false, new Bgr(Color.Red), 1);
                this.imageBox1.Image = color;
            }
        }

        private void CreateMapButton_Click(object sender, EventArgs e)
        {
            img = new Image<Gray, byte>(width, height);
            img.Draw(new CircleF(new PointF(50, 50), 25), new Gray(255), 2);
            img.Draw(new CircleF(new PointF(75, 75), 20), new Gray(255), 2);
            img.Draw(new CircleF(new PointF(50, 25), 30), new Gray(25), 2);
            img.Draw(new CircleF(new PointF(25, 50), 30), new Gray(100), 2);
            img.Draw(new CircleF(new PointF(75, 25), 30), new Gray(25), 2);
            img.Draw(new CircleF(new PointF(25, 75), 30), new Gray(100), 2);
            Point[] pts = new Point[]
                {
                    new Point(280, 20),
                    new Point(280, 280),
                    new Point(20, 280)
                };
            img.DrawPolyline(pts, false, new Gray(255), 1);
            navMesh = new NavMesh(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(img.Data[y, x, 0] == 255)
                    {
                        navMesh.getNode(x, y).traverseCost = NavMesh.impassable;
                    }
                    else
                    {
                        navMesh.getNode(x, y).traverseCost = img.Data[y, x, 0] + 1;
                    }
                }
            }

            this.imageBox1.Image = img;
        }
    }
}
