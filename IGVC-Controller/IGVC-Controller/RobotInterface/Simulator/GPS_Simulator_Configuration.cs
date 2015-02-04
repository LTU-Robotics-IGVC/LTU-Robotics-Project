using IGVC_Controller.DataIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.RobotInterface.Simulator
{
    public partial class GPS_Simulator_Configuration : Form
    {
        public double GPS_Offset_X = 0;
        public double GPS_Offset_Y = 0;
        public double GPS_Noise = 0;
        SaveFile config;

        public GPS_Simulator_Configuration()
        {
            InitializeComponent();
            config = new SaveFile("Simulator/GPS_Config");
            this.FormClosing += GPS_Simulator_Configuration_FormClosing;

            config.BeginRead();
            GPS_Offset_X = config.Read<double>("Offset_X", 0);
            GPS_Offset_Y = config.Read<double>("Offset_Y", 0);
            GPS_Noise = config.Read<double>("Noise", 0.02);
            config.EndRead();
        }

        void GPS_Simulator_Configuration_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.BeginWrite();
            config.Write<double>("Offset_X", GPS_Offset_X);
            config.Write<double>("Offset_Y", GPS_Offset_Y);
            config.Write<double>("Noise", GPS_Noise);
            config.EndWrite();
        }

        private void OffsetX_Slider_Scroll(object sender, ScrollEventArgs e)
        {
            GPS_Offset_X = (double)OffsetX_Slider.Value / 1000.0;
            OffsetX_Label.Text = GPS_Offset_X.ToString("n") + "m";
        }

        private void OffsetY_Slider_Scroll(object sender, ScrollEventArgs e)
        {
            GPS_Offset_Y = (double)OffsetY_Slider.Value / 1000.0;
            OffsetY_Label.Text = GPS_Offset_Y.ToString("n") + "m";
        }

        private void Noise_Slider_Scroll(object sender, ScrollEventArgs e)
        {
            GPS_Noise = (double)Noise_Slider.Value / 1000.0;
            Noise_Label.Text = GPS_Noise.ToString("n") + "m";
        }
    }
}
