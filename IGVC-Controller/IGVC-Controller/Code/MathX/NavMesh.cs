using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    class NavMesh
    {
        public Node[] nodes;
        public int Size;
        public int Width;
        public int Height;

        public const uint unpassable = uint.MaxValue;

        public NavMesh(int width, int height)
        {
            //Access is nodes[x + y * width]
            nodes = new Node[width * height];
            this.Size = width * height;
            this.Width = width;
            this.Height = height;
        }

        public int getIndex(int x, int y)
        {
            return x + y * Width;
        }

        public Point getPoint(int index)
        {
            return new Point(index % Width, index / Width);
        }
    }

    struct Node
    {
        public Node source;
        public uint distanceTraveled;
        public uint distanceRemaining;
        public uint traverseCost;
        bool sourceIsNull;

        public Node()
        {
            this.sourceIsNull = true;
            this.distanceTraveled = 0;
            this.distanceRemaining = uint.MaxValue;
            this.traverseCost = 1;
        }
    }
}
