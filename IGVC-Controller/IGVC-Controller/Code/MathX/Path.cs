using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    class Path
    {
        private Stack<Point> path;

        public int MapWidth { get; private set; }

        public int MapHeight { get; private set; }

        public Path(int mapWidth, int mapHeight)
        {
            path = new Stack<Point>();
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
        }

        public void AddNode(Point node)
        {
            this.path.Push(node);
        }

        public Point[] getPointArray()
        {
            return path.ToArray();
        }
    }
}
