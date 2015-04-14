using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules.Visualizer.GPS
{
    public partial class GPS_Visualizer_Form : Form
    {
        public GPS_Visualizer_Form()
        {
            InitializeComponent();
        }

        public void setGPSData(string data)
        {
            //Split Latitude and Longitude
            string[] LatLong = data.Split('-');
            
            //Split Latitude
            string[] Latitude = LatLong[0].Split(',');

            //Split Longitude
            string[] Longitude = LatLong[1].Split(',');

            Lat_textBox.Text = Latitude[0] + " " + Latitude[1];
            Long_textBox.Text = Longitude[0] + " " + Longitude[1];

        }
    }
}
