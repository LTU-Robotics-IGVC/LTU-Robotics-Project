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
        double LeftOrg;
        double RightOrg;

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
            LeftOrg = 0.0;
            RightOrg = 0.0;

            leftMotor = new GatedVariable();
            leftMotor.setObject(0.0);
            rightMotor = new GatedVariable();
            rightMotor.setObject(0.0);
            motorEnable = new GatedVariable();
            motorEnable.setObject(false);
            try
            {
                robot = RobotPort.getRobotPort("COM4", 9600, 30);
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
            leftMotorSpeed = Math.Max(leftMotorSpeed, 0);
            rightMotorSpeed = Math.Max(rightMotorSpeed, 0);
            if(motorEnabled)
            {
                //this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, leftMotorSpeed.ToString() + " | " + rightMotorSpeed.ToString());
                //robot.sendCommand(new object[] { "LEFTMOTOR", "SET SPEED", leftMotorSpeed.ToString("N") });
                if (LeftOrg != leftMotorSpeed || RightOrg != rightMotorSpeed )//|| true)
                {
                    //if (leftMotorSpeed >= 0.0 && rightMotorSpeed >= 0.0)
                    //{
                    //    if (rightMotorSpeed < 0.0001 && leftMotorSpeed < 0.0001)
                    //        robot.sendCommandWithResponse(new object[] { "LEFTMOTOR_R", "STOP" });
                    //    else if (leftMotorSpeed >= 1.9 * rightMotorSpeed)
                    //        robot.sendCommandWithResponse(new object[] { "LEFTMOTOR_R", "RIGHT", leftMotorSpeed.ToString("N") });
                    //    else if (rightMotorSpeed >= 1.9 * leftMotorSpeed)
                    //        robot.sendCommandWithResponse(new object[] { "LEFTMOTOR_R", "LEFT", rightMotorSpeed.ToString("N") });
                    //    else
                    //        robot.sendCommandWithResponse(new object[] { "LEFTMOTOR_R", "FORWARD", leftMotorSpeed.ToString("N") });
                    //}
                    //else
                    //{
                    //    robot.sendCommandWithResponse(new object[] { "LEFTMOTOR_R", "REVERSE", leftMotorSpeed.ToString("N") });
                    //}

                    LeftOrg = leftMotorSpeed;
                    RightOrg = rightMotorSpeed;

                
                    //robot.sendCommand(new object[] { "LEFTMOTOR_R", "SETM", leftMotorSpeed.ToString("N"), rightMotorSpeed.ToString("N") });

                    robot.sendCommand(new object[] { "SETM", leftMotorSpeed.ToString("N"), rightMotorSpeed.ToString("N") });
                    this.sendDataToRegistry(INTERMODULE_VARIABLE.STATUS, leftMotorSpeed.ToString("N") + " : " + rightMotorSpeed.ToString("N"));
                }
                //robot.sendCommand(new object[] { "RIGHTMOTOR", "SET SPEED", rightMotorSpeed.ToString("N") });
            }

            base.process();
        }
    }
}
