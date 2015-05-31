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
        Vector2 LIDARDestinationOrigin;
        Vector2 ImageSourceOrigin;
        Vector2 ImageDestinationOrigin;
        float imagePixelScale = 0.001f; //How many meters per pixel

        public DualVision_LIDAR_ObstacleMapBuilder() : base()
        {
            this.modulePriority = 84;
            this.addSubscription(INTERMODULE_VARIABLE.COLLISION_IMAGE);
            this.addSubscription(INTERMODULE_VARIABLE.LIDAR_RAW);

            LIDARDestinationOrigin = new Vector2(this.mapWidth / 2, this.mapHeight * 0.9f);
            ImageDestinationOrigin = new Vector2(this.mapWidth / 2, this.mapHeight * 0.9f);
            ImageSourceOrigin = new Vector2(500, 500);//Currently a guess point
        }

        public override void process()
        {
            LIDAR.shiftObject();
            CollisionImage.shiftObject();

            NavMesh map = imageBasedCalc();

            this.sendDataToRegistry(INTERMODULE_VARIABLE.NAV_MESH, map);
        }

        private NavMesh imageBasedCalc()
        {
            NavMesh map = new NavMesh(mapWidth, mapHeight);

            Image<Gray, byte> img = ((Image<Gray, byte>)CollisionImage.getObject());
            
            //resize img based on scale at the defined origin

            Image<Gray, byte> scaledImage = img.Resize(imagePixelScale / cellScale, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

            //note that the img is not yet scaled to fit the map size
            //just that it is set so that there is a 1:1 pixel to cell scale setup

            //get some info
            for(int x = 0; x < mapWidth; x++)
                for(int y = 0; y < mapHeight; y++)
                {
                    float xImage = x - ImageDestinationOrigin.X + ImageSourceOrigin.X*imagePixelScale/cellScale;
                    float yImage = y - ImageDestinationOrigin.Y + ImageSourceOrigin.Y * imagePixelScale / cellScale;

                    if(scaledImage.Data[(int)yImage, (int)xImage, 0] > 0)
                    {
                        map.getNode(x, y).traverseCost = NavMesh.impassable;
                    }
                }

            //Now map the LIDAR data to the NavMesh
            //Note that processScaledLIDAR puts the data into the cell domain
            //already

            List<long> distances = (List<long>)LIDAR.getObject();
            List<Vector2> points = processScaledLIDAR(distances);
            foreach(Vector2 point in points)
            {
                Node node = map.getNode((int)point.X, (int)point.Y);
                if (node != null)
                    node.traverseCost = NavMesh.impassable;
            }

            return map;
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

                double angle = /*angle ratio*/ ((double)(1080 - i) / (double)c)
                    * /*angle range*/ (135.0 * 2)
                    - /*angle offset*/ 135.0;

                //to radians
                angle = (angle / 180.0) * Math.PI;
                //angle 0degrees = up

                //y in meters
                double yMeters = (Math.Cos(angle) * valInMeters);

                //x in meters
                double xMeters = (Math.Sin(angle) * valInMeters);

                points.Add(new Vector2((float)xMeters, (float)yMeters) * cellScale 
                    + LIDARDestinationOrigin);
            }

            return points;
        }
    }
}
