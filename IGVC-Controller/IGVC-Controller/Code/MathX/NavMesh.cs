using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    class NavMesh
    {
        private Node[] nodes;

        private uint unpassable = uint.MaxValue;

        public NavMesh(int width, int height)
        {
            //Access is nodes[x + y * width]
            nodes = new Node[width * height];
        }
    }

    struct Node
    {
        public Node source;
        public uint distanceTraveled;
        public uint distanceRemaining;
        public uint traverseCost = 1;
    }
}
