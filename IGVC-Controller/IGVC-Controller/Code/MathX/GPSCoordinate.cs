using IGVC_Controller.Code.MathX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    [Serializable]
    public class GPSCoordinate
    {
        public float latitude;
        public float longitude;
        private const double E0 = 500;
        private const double N0 = 0;
        private const double k0 = 0.9996;
        private const double a = 6378.137;//equatorial radius
        private static double f;//flattening
        private static double n;
        private static double A;
        private static double[] alpha;

        public static float LinearConversion_X_LONG = 81.951347f * 1000f;
        public static float LinearConversion_Y_LAT = 109.962795f * 1000f;

        static GPSCoordinate()
        {
            f = 1.0 / 298.257223563;//flattening
            n = f / (2 - f);

            double A1 = a / (1 + n);
            double A2 = 1.0;
            for(int i = 1; i <= 2; i++)
            {
                A2 += (Math.Pow(n, (2 * i)) / (Math.Pow(2, (i + 1) * i)));
            }
            A = A1 * A2;

            double n2 = Math.Pow(n, 2);
            double n3 = Math.Pow(n, 3);
            alpha = new double[]{
                0.5*n-(2.0/3.0)*n2+(5.0/16.0)*n3,
                (13.0/48.0)*n2-(3.0/5.0)*n3,
                (61.0/240.0)*n3
            };
        }

        public GPSCoordinate(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public GPSCoordinate(float latDegrees, float latMinutes, float latSeconds, float longDegrees, float longMinutes, float longSeconds)
        {
            this.latitude = latDegrees + (latMinutes / 60.0f) + (latSeconds / 3600.0f);
            this.longitude = longDegrees + (longMinutes / 60.0f) + (longSeconds / 3600.0f);
        }

        public Vector2 getUTMCoordinates()
        {
            //Standard formula in km

            //Intermediaries
            double n_prime = 0.0;//uses tanh^-1 function --- ouch
            double e_prime = 0.0;

            double sumForE = 0;
            double sumForN = 0;
            for(int j = 1; j <= 3; j++)
            {
                sumForE += alpha[j-1]*Math.Cos(2*j*e_prime)*Math.Sinh(2*j*n_prime);
                sumForN += alpha[j-1]*Math.Sin(2*j*e_prime)*Math.Cosh(2*j*n_prime);
            }
            //Final Values
            double E = E0 + k0 * A * (n_prime + sumForE);
            double N = N0 + k0 * A * (e_prime + sumForN);

            return new Vector2((float)E, (float)N);
        }

        /// <summary>
        /// Returns x,y coordinates in meters
        /// <para>These values are only accurate in the area</para>
        /// <para>the conversion factors where measured in</para>
        /// </summary>
        /// <returns></returns>
        public Vector2 getLinearConversionCoordinates()
        {
            return new Vector2(longitude * LinearConversion_X_LONG, latitude * LinearConversion_Y_LAT);
        }

        public override string ToString()
        {
            return longitude.ToString("N6") + " || " + latitude.ToString("N6");
        }
    }
}