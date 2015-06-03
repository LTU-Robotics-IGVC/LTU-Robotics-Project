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
        float meterToPixel = 50;

        public PathFinder() : base()
        {
            this.modulePriority = 61;
            startPoint = new Point();
            this.addSubscription(INTERMODULE_VARIABLE.NAV_MESH);
            this.addSubscription(INTERMODULE_VARIABLE.GPS_RELATIVE_VECTOR2);
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.NAV_MESH:
                    Nav_Mesh.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.GPS_RELATIVE_VECTOR2:
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
            Point start = new Point(mesh.Width / 2, mesh.Height);
            
            if (mesh != null && GPSRelativeWaypoint.getObject() != null)
            {
                Vector2 p = (Vector2)GPSRelativeWaypoint.getObject();
                end = new Point((int)(p.X * meterToPixel) + start.X, start.Y - (int)(p.Y * meterToPixel));
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
