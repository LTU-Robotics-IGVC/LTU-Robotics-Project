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
        RobotPort robot;

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
            leftMotor.setObject(0.0);
            rightMotor = new GatedVariable();
            rightMotor.setObject(0.0);
            motorEnable = new GatedVariable();
            motorEnable.setObject(false);
            try
            {
                robot = RobotPort.getRobotPort("COM5", 9600, 30);
                robot.open();
            }
            catch(Exception e)
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS,
                    "Failed to start MotorControl with Exception : " + e.ToString());
                return false;
            }
            return base.startup();
        }

        public override void shutdown()
        {
            robot.close();

            base.shutdown();
        }

        public override void process()
        {
            leftMotor.shiftObject();
            rightMotor.shiftObject();
            motorEnable.shiftObject();
            double leftMotorSpeed = (double)leftMotor.getObject();
            double rightMotorSpeed = (double)rightMotor.getObject();
            bool motorEnabled = (bool)motorEnable.getObject();

            if(motorEnabled)
            {
                //this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, leftMotorSpeed.ToString() + " | " + rightMotorSpeed.ToString());
                //robot.sendCommand(new object[] { "LEFTMOTOR", "SET SPEED", leftMotorSpeed.ToString("N") });

                if(leftMotorSpeed >= 0 && rightMotorSpeed >= 0)
                {
                    if (leftMotorSpeed >= 1.9 * rightMotorSpeed)
                        robot.sendCommand(new object[] { "LEFTMOTOR", "RIGHT", leftMotorSpeed.ToString("N") });
                    else if (rightMotorSpeed >= 1.9 * leftMotorSpeed)
                        robot.sendCommand(new object[] { "LEFTMOTOR", "LEFT", leftMotorSpeed.ToString("N") });
                    else if (rightMotorSpeed < 0.0001 && leftMotorSpeed < 0.0001)
                        robot.sendCommand(new object[] { "LEFTMOTOR", "STOP" });
                    else
                        robot.sendCommand(new object[] { "LEFTMOTOR", "FORWARD", leftMotorSpeed.ToString("N") });
                }
                else
                {
                    robot.sendCommand(new object[] { "LEFTMOTOR", "REVERSE", leftMotorSpeed.ToString("N") });
                }

                //robot.sendCommand(new object[] { "RIGHTMOTOR", "SET SPEED", rightMotorSpeed.ToString("N") });
            }

            base.process();
        }
    }
}
