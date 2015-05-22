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
    class PathFinder : IModule
    {
        GatedVariable Nav_Mesh;
        GatedVariable GPSRelativeWaypoint;
        Point startPoint;

        public PathFinder() : base()
        {
            this.modulePriority = 61;
            startPoint = new Point();
            this.addSubscription(INTERMODULE_VARIABLE.NAV_MESH);
            this.addSubscription(INTERMODULE_VARIABLE.GPS_RELATIVE_POINT);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.NAV_MESH:
                    Nav_Mesh.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.GPS_RELATIVE_POINT:
                    GPSRelativeWaypoint.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            Nav_Mesh = new GatedVariable();
            GPSRelativeWaypoint = new GatedVariable();

            return base.startup();
        }

        public override void process()
        {
            NavMesh mesh = (NavMesh)Nav_Mesh.getObject();
            Point end = new Point();
            if (mesh != null && GPSRelativeWaypoint.getObject() != null)
            {
                end = (Point)GPSRelativeWaypoint.getObject();
                AStarPather pather = new AStarPather(mesh);
                Path path = pather.getPath(startPoint, end);
                if(path != null)
                {
                    this.sendDataToRegistry(INTERMODULE_VARIABLE.NAV_PATH, path);
                }
            }
            base.process();
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new PathFinderEditor();
        }
    }
}
