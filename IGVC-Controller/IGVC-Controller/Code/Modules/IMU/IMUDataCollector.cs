using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.IMU
{
    class IMUDataCollector : IModule
    {
        RobotPort robot;

        public IMUDataCollector() : base()
        {
            this.modulePriority = 11;
        }

        public override bool startup()
        {
            try
            {
                robot = RobotPort.getRobotPort("COM3", 9600, 30);
                robot.open();
                Thread.Sleep(5000);
                return base.startup();
            }
            catch(Exception e)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "Failed to start with exception - " + e.ToString());
                return false;
            }
        }

        public override void shutdown()
        {
            robot.close();
            base.shutdown();
        }

        public override void process()
        {
            robot.sendCommand(new object[] { "LEFTMOTOR", "LED OFF" });
            List<string> response = robot.sendCommandWithResponse(new object[] { "MASTER", "COMPASS" });
            if(response.Count == 2)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "COMPASS = " + response[1]);
            }
            else
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, "Incorrect data format recieved from robot");
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, response.ToString());
            }
        }
    }
}
