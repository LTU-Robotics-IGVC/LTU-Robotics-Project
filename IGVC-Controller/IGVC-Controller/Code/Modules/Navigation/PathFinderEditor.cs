﻿using Emgu.CV;
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
            Path path = pather.getPath(new Point(10, 10), new Point(90, 90));
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
            img = new Image<Gray, byte>(100, 100);
            img.Draw(new CircleF(new PointF(50, 50), 25), new Gray(255), 1);
            Point[] pts = new Point[]
                {
                    new Point(280, 20),
                    new Point(280, 280),
                    new Point(20, 280)
                };
            img.DrawPolyline(pts, false, new Gray(255), 1);
            navMesh = new NavMesh(100, 100);
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
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
