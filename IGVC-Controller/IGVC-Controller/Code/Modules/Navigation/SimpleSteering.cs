using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Navigation
{
    class SimpleSteering : IModule
    {
        GatedVariable NavPath;
        int evalLength = 100;//process the first 25 points
        double baseSpeed = 1.0;

        public SimpleSteering() : base()
        {
            this.modulePriority = 62;
            this.addSubscription(INTERMODULE_VARIABLE.NAV_PATH);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.NAV_PATH:
                    NavPath.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            NavPath = new GatedVariable();
            return base.startup();
        }

        public override void process()
        {
            NavPath.shiftObject();
            Path path = (Path)NavPath.getObject();
            if(path != null)
            {
                Point[] points = path.getPointArray();
                int count = Math.Min(evalLength, points.Length);
                int runningX = 0;
                for(int i = 0; i < count; i++)
                {
                    runningX += points[i].X;
                }
                runningX /= count;
                double steering = (runningX / ((double)path.MapWidth / 2.0)) - 1.0;
                if (steering < -0.1)
                    hardLeft();
                else if (steering < -0.01)
                    slightLeft();
                else if (steering < 0.01)
                    forward();
                else if (steering < 0.1)
                    slightRight();
                else
                    hardRight();
            }
            else
            {
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_LEFT, 0.0);
                this.sendDataToRegistry(INTERMODULE_VARIABLE.MOTOR_SPEED_RIGHT, 0.0);
            }
            this.sendDataToRegistry(INTERMODULE_VARIABLE.IS_AUTONOMOUS, true);
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

        public override void shutdown()
        {
            base.shutdown();
        }
    }
}
