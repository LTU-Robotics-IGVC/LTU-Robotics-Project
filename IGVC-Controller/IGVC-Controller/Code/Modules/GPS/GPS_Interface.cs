using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.MathX;

namespace IGVC_Controller.Code.Modules.GPS
{
    class GPS_Interface : IModule
    {
        SerialPort phoneGPS;

        public string port_name = "COM11";
        public int baudrate = 4800;
        public string Lat, Long;
        GatedVariable lastGPS;

        public GPS_Interface() : base()
        {
            //this.addSubscription(INTERMODULE_VARIABLE.GPS_COORDS);
            this.addSubscription(INTERMODULE_VARIABLE.GPS_COORDS);
            this.modulePriority = 1;
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

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.GPS_COORDS:
                    this.lastGPS.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "\tOpening GPS on port " + port_name
                + " with baudrate " + baudrate.ToString());

            lastGPS = new GatedVariable();

            try //opening the serail port
            {
                phoneGPS = new SerialPort(port_name, baudrate);
                //As far as I am aware you do not need to change the NewLine indicator
                //The NewLine character "\n\n" was specific for the LIDAR
                //phoneGPS.NewLine = "\n\n";

                phoneGPS.Open();
                phoneGPS.NewLine = "\n";
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
            phoneGPS.Dispose();

            base.shutdown();
        }

        public override void process()
        {
            if(phoneGPS.IsOpen)
            {
                try
                {
                    while (phoneGPS.BytesToRead > 0)
                    {
                        string coord = phoneGPS.ReadLine();//Raw data scan

                        //Note that status information is for somewhat major events (not periodic)
                        //This is okay when you are specifically testing if it works but make sure
                        //to remove (or comment out) the "Coordinates were Parsed Successfully" case
                        if (Parse(coord))//Still need to test logic of this Funciton
                        {
                            //this.sendDataToRegistry(INTERMODULE_VARIABLE.GPS_COORDS, Lat + " - " + Long);//'-' is used to distinguish Lat and Long=

                            //First two numbers are the latitude in degrees while the rest is in minutes
                            double latDegrees = Convert.ToDouble(Lat.Substring(0, 2));
                            double latMinutes = Convert.ToDouble(Lat.Substring(2, Lat.Length - 2));

                            double longDegrees = Convert.ToDouble(Long.Substring(0, 3));
                            double longMinutes = Convert.ToDouble(Long.Substring(3, Long.Length - 3));
                            ///60 to scale turn them into angles in degrees
                            ///

                            lastGPS.shiftObject();
                            GPSCoordinate lastCoord = (GPSCoordinate)lastGPS.getObject();

                            GPSCoordinate currentCoord = new GPSCoordinate((float)(latDegrees + latMinutes / 60.0),
                                (float)(longDegrees + longMinutes / 60.0));
                            this.sendDataToRegistry(INTERMODULE_VARIABLE.GPS_COORDS, currentCoord);

                            if (lastCoord != null && (lastCoord.latitude != currentCoord.latitude
                                || lastCoord.longitude != currentCoord.longitude))
                            {
                                Vector2 relativePosition = currentCoord.getLinearConversionCoordinates() - lastCoord.getLinearConversionCoordinates();
                                double heading = relativePosition.Angle() / Math.PI * 180.0;
                                if (heading < 0.0)
                                    heading += 360.0;
                                //this.sendDataToRegistry(INTERMODULE_VARIABLE.COMPASS, heading);
                            }
                            //break;
                        }
                        else
                        { /*this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "NEMA data was not recognized");*/}
                    }
                }
                catch { /*Program ran to fast to read phoneGPS*/}

            }
            else
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "Serial Port is not Open, GPS could not be read data");
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
            string[] Words = GetWords(sentence);
            switch (Words[0])
            {
                case "$GPGGA":           
                    {
                    return ParseGPGLL(Words);
                    }
                case "$GPHDG":
                    {
                        return ParseHDG(Words);
                    }
                default:
                    return false;//Indicate that the sentence was not recognized
            }
        }
        private bool ParseHDG(string[] Words)
        {
            if(Words != null)
            {
                double angle = Convert.ToDouble(Words[1]);
                //angle = 360.0 - angle;
                this.sendDataToRegistry(INTERMODULE_VARIABLE.COMPASS, angle);
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ParseGPGLL(string[] Words)
        {
            //Divide the senetnce into words
            if (Words != null)
            {
                //comma is used for ease of Split numerica and directional value
                Lat = Words[2];
                Long = Words[4];
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
