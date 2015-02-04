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
    public partial class RobotSimulator : Form
    {
        double GPS_OffsetX;
        double GPS_OffsetY;
        double GPS_Noise;

        public RobotSimulator()
        {
            InitializeComponent();
            ReadFromConfigs();
        }

        private void gPSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form GPS_Config = new GPS_Simulator_Configuration();
            GPS_Config.Visible = true;
            GPS_Config.Focus();
            GPS_Config.FormClosed += Config_Change;
        }

        void Config_Change(object sender, FormClosedEventArgs e)
        {
            ReadFromConfigs();
        }

        void ReadFromConfigs()
        {
            //Get GPS settings
            SaveFile config = new SaveFile("Simulator / GPS_Config");
            config.BeginRead();
            GPS_OffsetX = config.Read<double>("OffsetX", 0);
            GPS_OffsetY = config.Read<double>("OffsetY", 0);
            GPS_Noise = config.Read<double>("Noise", 0.02);
            config.EndRead();
        }
    }
}
