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
        float cellScale = 0.020f; //How many meters per cell
        GatedVariable LIDAR;
        GatedVariable CollisionImage;
        Vector2 LIDARDestinationOrigin;
        Vector2 ImageSourceOrigin;
        Vector2 ImageDestinationOrigin;
        float imagePixelScale = 0.005f; //How many meters per pixel

        public DualVision_LIDAR_ObstacleMapBuilder() : base()
        {
            this.modulePriority = 60;
            this.addSubscription(INTERMODULE_VARIABLE.COLLISION_IMAGE);
            this.addSubscription(INTERMODULE_VARIABLE.LIDAR_RAW);

            LIDARDestinationOrigin = new Vector2(this.mapWidth / 2, this.mapHeight-1);
            ImageDestinationOrigin = new Vector2(this.mapWidth / 2, this.mapHeight);
            ImageSourceOrigin = new Vector2(500, 1000);//Currently a guess point
        }

        public override void recieveDataFromRegistry(IModule.INTERMODULE_VARIABLE tag, object data)
        {
            switch(tag)
            {
                case INTERMODULE_VARIABLE.COLLISION_IMAGE:
                    this.CollisionImage.setObject(data);
                    break;
                case INTERMODULE_VARIABLE.LIDAR_RAW:
                    this.LIDAR.setObject(data);
                    break;
            }
            base.recieveDataFromRegistry(tag, data);
        }

        public override bool startup()
        {
            LIDAR = new GatedVariable();
            CollisionImage = new GatedVariable();
            return base.startup();
        }

        public override void process()
        {
            LIDAR.shiftObject();
            CollisionImage.shiftObject();

            Image<Gray, byte> mapImage = imageBasedCalc();
            //mapImage = mapImage.PyrUp().PyrDown();
            mapImage = mapImage.Dilate(5);

            NavMesh mesh = new NavMesh(mapWidth, mapHeight);
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    byte val = mapImage.Data[y, x, 0];
                    if(val > 200)
                    {
                        mesh.getNode(x, y).traverseCost = NavMesh.impassable;
                    }
                    else if(val > 0)
                    {
                        mesh.getNode(x, y).traverseCost = val;
                    }
                }
            }
            //NavMesh map2 = new NavMesh(map.Width, map.Height);
            
            //for(int x = 1; x < map.Width-1; x++)
            //    for(int y = 1; y < map.Height-1; y++)
            //    {
            //        int count = 0;
            //        if (map.getNode(x + 1, y).isPassable)
            //            count++;
            //        if (map.getNode(x - 1, y).isPassable)
            //            count++;
            //        if (map.getNode(x, y + 1).isPassable)
            //            count++;
            //        if (map.getNode(x, y - 1).isPassable)
            //            count++;
            //        if (count >= 3)
            //            map2.getNode(x, y).traverseCost = 1;
            //        else
            //            map2.getNode(x, y).traverseCost = NavMesh.impassable;
            //    }
            //map = map2;
            //map2 = new NavMesh(map.Width, map.Height);
            //int dis = 10;
            //for (int x = 0; x < map.Width; x++)
            //    for (int y = 0; y < map.Height; y++)
            //    {
            //        bool keepGoing = true;
            //        for(int subX = Math.Max(0, x - dis); subX < Math.Min(map.Width, x + dis) && keepGoing; subX++)
            //            for (int subY = Math.Max(0, y - dis); subY < Math.Min(map.Height, y + dis) && keepGoing; subY++)
            //            {
            //                if (MathXHelper.getPointDis(x, y, subX, subY) < dis)
            //                {
            //                    if (!map.getNode(subX, subY).isPassable)
            //                    {
            //                        map2.getNode(x, y).traverseCost = NavMesh.impassable;
            //                        keepGoing = false;
            //                    }
            //                }
            //            }
            //    }

           this.sendDataToRegistry(INTERMODULE_VARIABLE.NAV_MESH, mesh);
        }

        private Image<Gray, byte> imageBasedCalc()
        {
            Image<Gray, byte> img = ((Image<Gray, byte>)CollisionImage.getObject());

            if (img == null)
                return null;

            //resize img based on scale at the defined origin

            Image<Gray, byte> scaledImage = img.Resize(imagePixelScale / cellScale, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

            //note that the img is not yet scaled to fit the map size
            //just that it is set so that there is a 1:1 pixel to cell scale setup

            Image<Gray, byte> mapImage = new Image<Gray, byte>(mapWidth, mapHeight);

            //get some info
            for(int x = 0; x < mapWidth; x++)
                for(int y = 0; y < mapHeight; y++)
                {
                    float xImage = x - ImageDestinationOrigin.X + ImageSourceOrigin.X*imagePixelScale/cellScale;
                    float yImage = y - ImageDestinationOrigin.Y + ImageSourceOrigin.Y * imagePixelScale / cellScale;

                    if(scaledImage.Data[(int)yImage, (int)xImage, 0] > 0)
                    {
                        //map.getNode(x, y).traverseCost = NavMesh.impassable;
                        mapImage.Data[y, x, 0] = 0;
                    }
                }

            //Now map the LIDAR data to the NavMesh
            //Note that processScaledLIDAR puts the data into the cell domain
            //already
            if (LIDAR.getObject() == null)
                return mapImage;

            List<long> distances = (List<long>)LIDAR.getObject();
            List<Vector2> points = processScaledLIDAR(distances);
            foreach(Vector2 point in points)
            {
                if(point.Within(0, 0, mapWidth, mapHeight))
                {
                    mapImage.Data[(int)point.Y, (int)point.X, 0] = 255;
                }
                //Node node = map.getNode((int)point.X, (int)point.Y);
                //if (node != null)
                //    node.traverseCost = NavMesh.impassable;
            }

            return mapImage;
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

                points.Add(new Vector2((float)xMeters, -(float)yMeters) / cellScale
                    + LIDARDestinationOrigin);
            }

            return points;
        }
    }
}
