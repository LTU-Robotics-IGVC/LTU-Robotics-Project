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
        public bool motorsEnabled = false;
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
                    this.imageBox4.Image = img.Convert<Bgr, byte>();
                    break;
                case IModule.INTERMODULE_VARIABLE.NAV_PATH:
                    Path path = (Path)data;
                    if(this.imageBox4.Image != null)
                    {
                        Point[] points = path.getPointArray();
                        Image<Bgr, byte> img2 = (Image<Bgr, byte>)this.imageBox4.Image;
                        for(int i = 0; i < points.Length; i++)
                        {
                            img2.Data[points[i].Y, points[i].X, 2] = 255;
                        }
                        this.imageBox4.Image = img2;
                    }
                    break;
                case IModule.INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT:
                    double speed = Math.Abs((double)data);
                    this.label1.Text = "L:" + speed.ToString("N");
                    this.vScrollBar1.Value = (int)(speed * 100.0);
                    break;
                case IModule.INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT:
                    speed = Math.Abs((double)data);
                    this.label2.Text = "R:" + speed.ToString("N");
                    this.vScrollBar2.Value = (int)(speed * 100.0);
                    break;
                case IModule.INTERMODULE_VARIABLE.OBSTACLE_IMAGE_LEFT:
                    this.imageBox5.Image = (Image<Gray, byte>)data;
                    break;
                case IModule.INTERMODULE_VARIABLE.OBSTACLE_IMAGE_RIGHT:
                    this.imageBox6.Image = (Image<Gray, byte>)data;
                    break;
                case IModule.INTERMODULE_VARIABLE.COMPASS:
                    double angle = (double)data;
                    this.label3.Text = "Compass: " + angle.ToString("N");
                    break;
                case IModule.INTERMODULE_VARIABLE.GPS_COORDS:
                    GPSCoordinate coord = (GPSCoordinate)data;
                    this.GPSLabel.Text = "GPS: " + coord.ToString();
                    break;
                case IModule.INTERMODULE_VARIABLE.CURRENT_WAYPOINT:
                    GPSWaypoint waypoint = (GPSWaypoint)data;
                    this.WaypointLabel.Text = "Waypoint: " + waypoint.ToString();
                    break;
                case IModule.INTERMODULE_VARIABLE.WAYPOINT_DISTANCE:
                    double distance = (double)data;
                    this.WaypointDistanceLabel.Text = "Distance: " + distance.ToString("N") + "meters";
                    break;
                case IModule.INTERMODULE_VARIABLE.WAYPOINT_HEADING:
                    double heading = (double)data;
                    this.WaypointHeadingLabel.Text = "Heading: " + heading.ToString("N");
                    break;
                default:
                    break;
            }
        }

        private void GeneralVisualizerForm_Load(object sender, EventArgs e)
        {

        }

        private void imageBox6_Click(object sender, EventArgs e)
        {

        }

        private void MotorEnableButton_Click(object sender, EventArgs e)
        {
            if(!motorsEnabled)
            {
                MotorEnableButton.Text = "Disable Motors";
                MotorEnableButton.BackColor = Color.Red;
                motorsEnabled = true;
            }
            else
            {
                MotorEnableButton.Text = "EnableMotors";
                MotorEnableButton.BackColor = Color.LimeGreen;
                motorsEnabled = false;
            }
        }
    }
}
