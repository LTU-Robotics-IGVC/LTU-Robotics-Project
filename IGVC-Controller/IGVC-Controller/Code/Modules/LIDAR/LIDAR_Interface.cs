﻿using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.LIDAR
{
    class LIDAR_Interface : IModule
    {
        public string port_name = "COM3";
        public int baudrate = 115200;
        public int start_step = 0;
        public int end_step = 1080;

        SerialPort urg;

        public LIDAR_Interface()
        {
            this.modulePriority = 0;
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.port_name = config.Read<string>("port_name", "COM3");
            this.baudrate = config.Read<int>("baudrate", 115200);
            this.start_step = config.Read<int>("start_step", 0);
            this.end_step = config.Read<int>("end_step", 1080);
            
            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<string>("port_name", this.port_name);
            config.Write<int>("baudrate", this.baudrate);
            config.Write<int>("start_step", this.start_step);
            config.Write<int>("end_step", this.end_step);

            base.writeToConfig(config);
        }

        public override bool startup()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tOpening LIDAR on port " + port_name
                + " with baudrate " + baudrate.ToString());

            try
            {
                urg = new SerialPort(port_name, baudrate);
                urg.NewLine = "\n\n";

                urg.Open();
            }
            catch(Exception e)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tLIDAR failed to open with exception : " + e.ToString());

                return false;
            }

            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\t\tLIDAR has been successfully connected");

            try
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tSetting LIDAR to SCIP2.0 protocol");

                urg.Write(SCIP_Writer.SCIP2());
                urg.ReadLine();//ignore echo
            }
            catch(Exception e)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, 
                    "\t\tFailed LIDAR activation with exception : " + e.ToString());

                return false;
            }

            return base.startup();
        }

        public override void shutdown()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tLIDAR is turning off");
            //urg.Write(SCIP_Writer.QT());
            urg.ReadLine(); //ignore echo
            urg.Close();

            base.shutdown();
        }

        public override void process()
        {
            try
            {
                urg.Write(SCIP_Writer.MD(start_step, end_step, 1, 0, 1));//Make 1 scan
                urg.ReadLine();//ignore echo

                string sData = urg.ReadLine();//Raw scanner data
                long timeStamp = 0;
                List<long> distances = new List<long>();
                SCIP_Reader.MD(sData, ref timeStamp, ref distances);
                
                //Need to project distances[angle] to a list of points
                this.sendDataToRegistry(INTERMODULE_VARIABLE.LIDAR_RAW, distances);
                //this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, distances[540].ToString());
            }
            catch(Exception e)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "Error in process for Module "
                    + MainWindow.instance.moduleNameDictionary[this] + " with priority "
                    + this.modulePriority + " with exception : " + e.ToString());

                return;
            }
            base.process();
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new LIDAR_InterfaceEditor();
        }
    }
}
