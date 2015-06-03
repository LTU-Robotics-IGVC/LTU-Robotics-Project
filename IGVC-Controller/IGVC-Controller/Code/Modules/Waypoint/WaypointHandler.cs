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
            GPSWaypoint waypoint = waypointQueue.Peek();
            if(waypoint.inRange(coord))
            {
                waypointQueue.Dequeue();
            }
            Vector2 relativePosition = waypoint.coordinate.getLinearConversionCoordinates() - coord.getLinearConversionCoordinates();

            //Todo
            //Orientation Correction

            this.sendDataToRegistry(INTERMODULE_VARIABLE.GPS_RELATIVE_VECTOR2, relativePosition);

            base.process();
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new WaypointHandlerEditor();
        }
    }
}
