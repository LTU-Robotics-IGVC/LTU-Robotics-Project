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

        RobotInterface robotInterface = new RobotInterface();

        public RobotSimulator()
        {
            InitializeComponent();
            ReadFromConfigs();
            this.FormClosing += RobotSimulator_FormClosing;


            SaveFile config = new SaveFile("Simulator_config");
            config.BeginRead();
            this.Update_Timer.Interval = Math.Max(config.Read<int>("updateRate"), 10);
            this.UpdateHz_Label.Text = "Update Frequency = " + 
                (1000.0 / (double)this.Update_Timer.Interval).ToString("n") + " hz";
            this.UpdateHz_Slider.Value = (int)(1000.0 / (double)this.Update_Timer.Interval);
            config.EndRead();

            this.Update_Timer.Start();
        }

        void RobotSimulator_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Update_Timer.Stop();

            SaveFile config = new SaveFile("Simulator_config");
            config.BeginWrite();
            config.Write<int>("updateRate", this.Update_Timer.Interval);
            config.EndWrite();
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
            VariableBox.Text = "";
            //Get GPS settings
            SaveFile config = new SaveFile("Simulator_GPS_Config");
            config.BeginRead();
            GPS_OffsetX = config.Read<double>("OffsetX", 0);
            GPS_OffsetY = config.Read<double>("OffsetY", 0);
            GPS_Noise = config.Read<double>("Noise", 0.02);
            config.EndRead();

            AddLineToVarBox("GPS-Offset-X", GPS_OffsetX);
            AddLineToVarBox("GPS-Offset-Y", GPS_OffsetY);
            AddLineToVarBox("GPS-Noise", GPS_Noise);
        }

        private void AddLineToVarBox(string varName, object value)
        {
            VariableBox.AppendText(varName + " : " + value.ToString() + "\n");
        }

        private void AddLineToVarBox(string varName, double value)
        {
            VariableBox.AppendText(varName + " : " + value.ToString("N") + "\n");
        }

        private void Update_Timer_Tick(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Update_Timer.Interval = (int)(1000.0 / (double)this.UpdateHz_Slider.Value);
            this.UpdateHz_Label.Text = "Update Frequency = " +
                (1000.0 / (double)this.Update_Timer.Interval).ToString("N") + " hz";
        }
    }
}
