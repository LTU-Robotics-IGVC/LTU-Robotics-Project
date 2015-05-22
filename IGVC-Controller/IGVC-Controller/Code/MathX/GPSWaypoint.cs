using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    [Serializable]
    class GPSWaypoint
    {
        public GPSCoordinate coordinate;
        public float range;//in meters

        public GPSWaypoint(float latitude, float longitude, float range)
        {
            this.coordinate = new GPSCoordinate(latitude, longitude);
            this.range = range;
        }

        public GPSWaypoint(GPSCoordinate coordinate, float range)
        {
            this.coordinate = coordinate;
            this.range = range;
        }

        public bool inRange(GPSCoordinate comparison)
        {
            Vector2 thisCoord = this.coordinate.getLinearConversionCoordinates();
            Vector2 otherCoord = comparison.getLinearConversionCoordinates();
            float dis = (thisCoord - otherCoord).Magnitude();
            return dis < range;
        }
    }
}
