using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.MathX
{
    public class Vector2
    {
        public float X;
        public float Y;

        public Vector2()
        {

        }

        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public float Magnitude()
        {
            return (float)System.Math.Sqrt(X * X + Y * Y);
        }

        public float Angle()
        {
            return (float)Math.Atan2(Y, X);
        }

        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            return new Vector2(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }
    }
}
