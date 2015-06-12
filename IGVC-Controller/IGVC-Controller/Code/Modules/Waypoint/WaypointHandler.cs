using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Waypoint
{
    class WaypointHandler : IModule
    {
        public List<GPSWaypoint> waypoints = new List<GPSWaypoint>();
        private Queue<GPSWaypoint> waypointQueue;
        GatedVariable currentCoords;

        public WaypointHandler() : base()
        {
            this.modulePriority = 26;
            this.addSubscription(INTERMODULE_VARIABLE.GPS_COORDS);

            //set for test
            //loadTestCoord();
            //loadQualificationCoords();
            loadBasicCoords(true);
        }

        private void loadTestCoord()
        {
            //P1
            this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.54420f, 83f, 11f, 42.08071f), 1.0f));

            //P2
            this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.97258f, 83f, 11f, 42.08261f), 1.0f));

            //P3
            this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.89970f, 83f, 11f, 41.48335f), 1.0f));

            //P4
            this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.61944f, 83f, 11f, 41.51440f), 1.0f));
        }

        private void loadQualificationCoords()
        {
            this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 40.68822f, 83f, 11f, 43.31415f), 2.0f));

            this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.56625f, 83f, 11f, 43.66609f), 2.0f));

            this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.61944f, 83f, 11f, 41.51440f), 1.0f));
        }

        private void loadBasicCoords(bool startNorth)
        {
            if(startNorth)
            {
                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 44.35053f, 83f, 11f, 41.88869f), 1.0f));

                //Sudo Waypoint
                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 44.35053f, 83f, 11f, 42.3f), 1.0f));

                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 43.83808f, 83f, 11f, 41.87927f), 1.0f));

                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.61944f, 83f, 11f, 41.51440f), 1.0f));
            }
            else
            {
                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 43.83808f, 83f, 11f, 41.87927f), 1.0f));

                //Sudo Waypoint
                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 44.35053f, 83f, 11f, 42.3f), 1.0f));

                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 44.35053f, 83f, 11f, 41.88869f), 1.0f));

                this.waypoints.Add(new GPSWaypoint(new GPSCoordinate(42f, 40f, 41.61944f, 83f, 11f, 41.51440f), 1.0f));
            }
        }
        
        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            //waypoints = config.Read<List<GPSWaypoint>>("waypoints", waypoints);
            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            //config.Write<List<GPSWaypoint>>("waypoints", waypoints);
            base.writeToConfig(config);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.GPS_COORDS:
                    currentCoords.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            currentCoords = new GatedVariable();
            waypointQueue = new Queue<GPSWaypoint>();
            for (int i = 0; i < waypoints.Count; i++)
            {
                waypointQueue.Enqueue(waypoints[i]);
            }
            return base.startup();
        }

        public override void process()
        {
            currentCoords.shiftObject();
            GPSCoordinate coord = (GPSCoordinate)currentCoords.getObject();
            if (coord == null)
                return;
            GPSWaypoint waypoint = waypointQueue.Peek();
            if(waypoint.inRange(coord))
            {
                waypointQueue.Dequeue();
            }
            Vector2 relativePosition = waypoint.coordinate.getLinearConversionCoordinates() - coord.getLinearConversionCoordinates();
            relativePosition = relativePosition + new Vector2(0.0f, -3.0f);

            //Todo
            //Orientation Correction

            this.sendDataToRegistry(INTERMODULE_VARIABLE.GPS_RELATIVE_VECTOR2, relativePosition);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.WAYPOINT_DISTANCE, (double)relativePosition.Magnitude());
            //long++ North
            //lat++ West
            double heading = relativePosition.Angle() / Math.PI * 180.0;
            heading -= 90.0;
            if (heading < 0.0)
                heading += 360.0;
            this.sendDataToRegistry(INTERMODULE_VARIABLE.WAYPOINT_HEADING, heading);
            this.sendDataToRegistry(INTERMODULE_VARIABLE.CURRENT_WAYPOINT, waypoint);

            base.process();
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new WaypointHandlerEditor();
        }
    }
}
