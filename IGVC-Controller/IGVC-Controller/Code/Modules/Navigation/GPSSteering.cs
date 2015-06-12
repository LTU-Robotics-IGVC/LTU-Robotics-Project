using IGVC_Controller.Code.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Navigation
{
    class GPSSteering : IModule
    {
        GatedVariable waypointDistance;
        GatedVariable waypointHeading;
        GatedVariable compass;
        double baseSpeed = 1.0;

        public GPSSteering() : base()
        {
            this.modulePriority = 50;
            this.addSubscription(INTERMODULE_VARIABLE.WAYPOINT_DISTANCE);
            this.addSubscription(INTERMODULE_VARIABLE.WAYPOINT_HEADING);
            this.addSubscription(INTERMODULE_VARIABLE.COMPASS);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.COMPASS:
                    this.compass.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.WAYPOINT_HEADING:
                    this.waypointHeading.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.WAYPOINT_DISTANCE:
                    this.waypointDistance.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            compass = new GatedVariable();
            compass.setObject(0.0);
            waypointDistance = new GatedVariable();
            waypointHeading = new GatedVariable();

            return base.startup();
        }

        public override void process()
        {
            waypointDistance.shiftObject();
            waypointHeading.shiftObject();
            compass.shiftObject();

            if(waypointHeading.getObject() != null && waypointDistance.getObject() != null
                && compass.getObject() != null)
            {
                double distance = (double)waypointDistance.getObject();
                double targetHeading = (double)waypointHeading.getObject();
                double currentHeading = (double)compass.getObject();

                double differnceInAngle = currentHeading - targetHeading;
                if (differnceInAngle < -180.0)
                    differnceInAngle += 360.0;
                else if (differnceInAngle > 180.0)
                    differnceInAngle -= 360.0;

                double h = 50.0;
                double s = 1.0;
                if (differnceInAngle < h)
                    slightRight();
                else if (differnceInAngle < s)
                    slightRight();
                else if (differnceInAngle < -s)
                    forward();
                else if (differnceInAngle < -h)
                    slightLeft();
                else
                    slightLeft();

                this.sendDataToRegistry(INTERMODULE_VARIABLE.GPS_IS_STEERING, true);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.IS_AUTONOMOUS, true);
            }
            base.process();
        }

        private void slightLeft()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed * 0.5);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
        }

        private void slightRight()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed * 0.5);
        }

        private void hardLeft()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, 0.0);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
        }

        private void hardRight()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, 0.0);
        }

        private void forward()
        {
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, baseSpeed);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, baseSpeed);
        }
    }
}
