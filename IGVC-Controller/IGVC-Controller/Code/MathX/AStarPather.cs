using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    class AStarPather
    {
        private bool mustReachTarget = true;
        private NavMesh navMesh;

        public AStarPather(NavMesh navMesh)
        {
            this.navMesh = navMesh;
        }

        public void setMustReachTarget(bool mustReachTarget)
        {
            this.mustReachTarget = mustReachTarget;
        }

        public Path getPath(Point start, Point end)
        {
            //Prefilter NavMesh distanceRemaining
            for(int x = 0; x < navMesh.Width; x++)
            {
                for (int y = 0; y < navMesh.Height; y++)
                {
                    double diffX = end.X - x;
                    double diffY = end.Y - y;
                    navMesh.nodes[x + y * navMesh.Width].distanceRemaining =
                        (uint)Math.Sqrt(diffX * diffX + diffY * diffY);
                }
            }

            bool found = false;
            Point bestNode = new Point(-1, -1);
            while(!found)
            {
            }

            Path path = new Path();

            return path;
        }
    }
}
