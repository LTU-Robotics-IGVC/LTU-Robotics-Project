using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.DataIO
{
    class RobotPort
    {
        static Dictionary<string, RobotPort> currentPorts = new Dictionary<string, RobotPort>();
        SerialPort comPort;
        int users = 0;
        int messageSize = 30;

        private RobotPort(string portname, int baudrate, int messageSize)
        {
            comPort = new SerialPort(portname, baudrate);
            //comPort.ReadTimeout = 1000;
            comPort.DataReceived += comPort_DataReceived;
            comPort.Parity = Parity.None;
            comPort.DataBits = 8;
            comPort.StopBits = StopBits.One;
            comPort.ReceivedBytesThreshold = 1;
            this.messageSize = messageSize;
            currentPorts.Add(portname, this);
        }

        void comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int a = 1;
        }

        /// <summary>
        /// Gets the existing instance of the port if available
        /// <para>otherwise it creates a new instance of the port</para>
        /// </summary>
        /// <param name="portname"></param>
        /// <param name="baudrate"></param>
        /// <returns></returns>
        public static RobotPort getRobotPort(string portname, int baudrate, int messageSize)
        {
            RobotPort port;
            if(currentPorts.ContainsKey(portname))
            {
                port = currentPorts[portname];
                if (port.comPort.BaudRate == baudrate && port.messageSize == messageSize)
                {
                    return port;
                }
                else
                {
                    throw new Exception("portname is already in use with differnt buadrate or messageSize");
                }
            }

            port = new RobotPort(portname, baudrate, messageSize);
            return port;
        }

        /// <summary>
        /// Opens port if it is not already open and increases the number
        /// <para>of users for this port</para>
        /// </summary>
        /// <returns>Return true if port is actually opened in this call</returns>
        public bool open()
        {
            users++;
            if(!comPort.IsOpen)
            {
                comPort.Open();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reduces the number of users of this port and closes it if
        /// <para>there are no users remaining</para>
        /// </summary>
        /// <returns>Return true if the port was actually closed this call</returns>
        public bool close()
        {
            if(comPort.IsOpen)
            {
                users--;
                if(users <= 0)
                {
                    comPort.Close();
                    comPort.Dispose();
                    currentPorts.Remove(comPort.PortName);
                    return true;
                }
            }
            return false;
        }

        public void sendCommand(object[] objects)
        {
            string message = "";
            int i;
            for(i = 0; i < objects.Length; i++)
            {
                message += objects[i].ToString() + ":";
            }
            i = message.Length;
            while(i < messageSize)
            {
                message += ' ';
                i++;
            }

            if(message.Length > messageSize)
            {
                throw new Exception("Message has exceeded message size limit");
            }

            if (comPort.IsOpen)
            {
                comPort.Write(message);
            }
            else
                throw new Exception("RobotPort has not been opened");
        }

        public List<string> sendCommandWithResponse(object[] objects)
        {
            //Send the command message first
            this.sendCommand(objects);

            //Wait for response to be ready

            //Collect response
            char[] buffer = new char[messageSize];
            int count = comPort.Read(buffer, 0, 1);
            while(count < messageSize)
            {
                count += comPort.Read(buffer, count, 1);
            }
            string response = new string(buffer);

            //Parse response
            //Note the structure example COMPASS:130.0:                <---Ends with 30 characters
            response.TrimEnd(' ');
            string[] components = response.Split(':');
            List<string> Return = new List<string>();
            //filter empty components
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] != "")
                {
                    Return.Add(components[i]);
                }
            }
            
            return Return;
        }
    }
}
