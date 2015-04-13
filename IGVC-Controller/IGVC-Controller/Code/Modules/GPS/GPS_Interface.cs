﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGVC_Controller.Code.DataIO;

namespace IGVC_Controller.Code.Modules.GPS
{
    class GPS_Interface : IModule
    {
        SerialPort phoneGPS;

        public string port_name = "COM1";
        public int baudrate = 115200;
        public string Lat, Long;

        public GPS_Interface() : base()
        {
            this.addSubscription(INTERMODULE_VARIABLE.GPS_COORDS);
            this.modulePriority = 75;
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.port_name = config.Read<string>("port_name", this.port_name);
            this.baudrate = config.Read<int>("baudrate", this.baudrate);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<string>("port_name", this.port_name);
            config.Write<int>("baudrate", this.baudrate);

            base.writeToConfig(config);
        }

        public override bool startup()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tOpening GPS on port" + port_name
                + " with baudrate " + baudrate.ToString());

            try //opening the serail port
            {
                phoneGPS = new SerialPort(port_name, baudrate);
                //As far as I am aware you do not need to change the NewLine indicator
                //The NewLine character "\n\n" was specific for the LIDAR
                //phoneGPS.NewLine = "\n\n";

                phoneGPS.Open();
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\t\tGPS has successfully connected");
            }
            catch(Exception e)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tGPS port failed to open with exception : " + e.ToString());

                return false;
            }

            return base.startup();
        }

        public override void shutdown()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tGPS is turning off");
            phoneGPS.Close();

            base.shutdown();
        }

        public override void process()
        {
            try
            {
                string coord = phoneGPS.ReadLine();//Raw data scan

                //Note that status information is for somewhat major events (not periodic)
                //This is okay when you are specifically testing if it works but make sure
                //to remove (or comment out) the "Coordinates were Parsed Successfully" case
                if(Parse(coord))//Still need to test logic of this Funciton
                    this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "Coordinates were Parsed Successfully");
                else
                    this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "NEMA data was not recognized");

            }
            catch(Exception e)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "Error in process for Module "
                    + MainWindow.instance.moduleNameDictionary[this] + " with priority "
                    + this.modulePriority + " with excepetion : " + e.ToString());
            }
            base.process();
        }

        private string[] GetWords(string sentence)
        {
            //remove the * and checksum from the end of the sentence
            sentence = sentence.Substring(0, sentence.IndexOf("*"));

            //Split the senetence into a string array
            return sentence.Split(',');
        }

        private bool Parse(string sentence)
        {
            switch(sentence)
            {
                case "$GPGLL":
                    return ParseGPGLL(sentence);
                default:
                    return false;//Indicate that the sentence was not recognized
            }
        }

        private bool ParseGPGLL(string sentence)
        {
            //Divide the senetnce into words
            string[] Words = GetWords(sentence);
            if (sentence != "")
            {
                Lat = Words[1] + " " + Words[2]; //Ex. "2916.26 N"
                Long = Words[3] + " " + Words[4];//Ex. "2345.45 W"
                return true;
            }
            else { return false; }
        }
        public override System.Windows.Forms.Form getEditorForm()
        {
            return new GPS_InterfaceEditor();
        }
    }
}
