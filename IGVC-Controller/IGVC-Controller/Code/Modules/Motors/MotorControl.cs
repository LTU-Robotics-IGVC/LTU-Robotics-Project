using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Motors
{
    class MotorControl : IModule
    {
        GatedVariable leftMotor;
        GatedVariable rightMotor;
        GatedVariable motorEnable;
        SerialPort comPort;

        public MotorControl() : base()
        {
            this.modulePriority = 93;
            this.addSubscription(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT);
            this.addSubscription(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT);
            this.addSubscription(INTERMODULE_VARIABLE.DRIVING_ENABLED);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT:
                    rightMotor.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT:
                    leftMotor.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.DRIVING_ENABLED:
                    motorEnable.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            leftMotor = new GatedVariable();
            rightMotor = new GatedVariable();
            motorEnable = new GatedVariable();
            motorEnable.setObject(false);
            comPort = new SerialPort();
            comPort.PortName = "COM7";
            comPort.Open();

            return base.startup();
        }

        public override void shutdown()
        {
            if (comPort != null && comPort.IsOpen)
                comPort.Close();

            base.shutdown();
        }

        public override void process()
        {
            double leftMotorSpeed = (double)leftMotor.getObject();
            double rightMotorSpeed = (double)rightMotor.getObject();
            bool motorEnabled = (bool)motorEnable.getObject();

            if(motorEnabled)
            {
                comPort.WriteLine("LMO:" + leftMotorSpeed.ToString("F"));
                comPort.WriteLine("RMO:" + rightMotorSpeed.ToString("F"));
            }

            base.process();
        }
    }
}
