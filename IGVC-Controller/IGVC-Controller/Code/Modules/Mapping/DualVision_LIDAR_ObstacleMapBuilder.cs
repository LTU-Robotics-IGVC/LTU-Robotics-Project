using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.Code.DataIO;
using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Mapping
{
    class DualVision_LIDAR_ObstacleMapBuilder : IModule
    {
        int mapWidth = 200;
        int mapHeight = 200;
        float cellScale = 0.040f; //How many meters per cell
        GatedVariable LIDAR;
        GatedVariable CollisionImage;
        Vector2 LIDARorigin;

        public DualVision_LIDAR_ObstacleMapBuilder() : base()
        {
            this.modulePriority = 84;
            this.addSubscription(INTERMODULE_VARIABLE.COLLISION_IMAGE);
            this.addSubscription(INTERMODULE_VARIABLE.LIDAR_RAW);

            LIDARorigin = new Vector2(this.mapWidth / 2, this.mapHeight * 0.9f);
        }

        public override void process()
        {
            NavMesh map = new NavMesh(mapWidth, mapHeight);

            //Load lidar map data
            List<long> distances = (List<long>)LIDAR.getObject();
            List<Vector2> points = processScaledLIDAR(distances);
            foreach(Vector2 point in points)
            {
                Node node = map.getNode((int)point.X, (int)point.Y);
                node.traverseCost = NavMesh.impassable;
            }

            Image<Gray, byte> collisionMap = (Image<Gray, byte>)CollisionImage.getObject();
            //Need some scaling factors for this one
        }

        //returns data in meters
        private List<Vector2> processScaledLIDAR(List<long> distances)
        {
            List<Vector2> points = new List<Vector2>();
            int c = distances.Count;
            for (int i = 0; i < c; i++)
            {
                double valInMeters = (double)distances[i] / 1000.0;
                if (valInMeters == 0 || valInMeters >= 30.0)
                    continue;

                double angle = /*angle ratio*/ ((double)(1080 - i) / (double)c) * /*angle range*/ (135.0 * 2)
                    - /*angle offset*/ 135.0;

                //to radians
                angle = (angle / 180.0) * Math.PI;
                //angle 0degrees = up

                //y in meters
                double yMeters = (Math.Cos(angle) * valInMeters);

                //x in meters
                double xMeters = (Math.Sin(angle) * valInMeters);

                points.Add(new Vector2((float)xMeters, (float)yMeters) * cellScale + LIDARorigin);
            }

            return points;
        }
    }
}
