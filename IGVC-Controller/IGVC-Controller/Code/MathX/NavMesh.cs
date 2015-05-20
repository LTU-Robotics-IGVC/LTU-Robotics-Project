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

        public Node getNode(Point p)
        {
            return nodes[p.X + p.Y * this.Width];
        }

        public int getPathLength(Point p)
        {
            int index = p.X + p.Y * this.Width;
            return nodes[index].distanceRemaining + nodes[index].distanceTraveled;
        }
    }

    class Node
    {
        public Point source;
        public int distanceTraveled;
        public int distanceRemaining;
        public int traverseCost;
        public bool sourceIsNull;

        public Node()
        {
            source = new Point(-1, -1);
            this.sourceIsNull = true;
            this.distanceTraveled = 0;
            this.distanceRemaining = int.MaxValue;
            this.traverseCost = 1;
        }
    }
}
