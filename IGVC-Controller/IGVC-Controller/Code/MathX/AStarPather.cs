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
        private bool diagonalPath = false;

        private NavMesh navMesh;

        public AStarPather(NavMesh navMesh)
        {
            this.navMesh = navMesh;
        }

        public void setMustReachTarget(bool mustReachTarget)
        {
            this.mustReachTarget = mustReachTarget;
        }
        
        public void setCanTravelDiagonally(bool diagonalPath)
        {
            this.diagonalPath = diagonalPath;
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
                        (int)Math.Sqrt(diffX * diffX + diffY * diffY);
                    navMesh.getNode(x, y).sourceIsNull = true;
                }
            }

            //Setup needed variables
            bool found = false;
            //Note that if bestNode is needed the whole map will have been searched
            int bestNode = -1;
            int bestDistance = int.MaxValue;
            //queue used to hold paths for evaluation
            SortedLinkQueue<int> queue = new SortedLinkQueue<int>();
            queue.Enqueue(navMesh.getIndex(ref start), (int)navMesh.getPathLength(ref start));
            Point evaluationPoint = new Point();

            while(!found && !queue.isEmpty())
            {
                int index = queue.Dequeue();
                Node evalNode = navMesh.nodes[index];
                if(evalNode.distanceRemaining < bestDistance)
                {
                    bestDistance = evalNode.distanceRemaining;
                    bestNode = index;
                }

                int x = navMesh.getXFromIndex(index);
                int y = navMesh.getYFromIndex(index);

                #region Search left, right, up, down
                //Check left
                evaluationPoint.X = x - 1;
                evaluationPoint.Y = y;
                if (isValidPoint(ref evaluationPoint))
                {
                    Node n = navMesh.getNode(ref evaluationPoint);
                    if(evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull && n.isPassable)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                //Check right
                evaluationPoint.X = x + 1;
                //evaluationPoint.Y is already p.Y
                if (isValidPoint(ref evaluationPoint))
                {
                    Node n = navMesh.getNode(ref evaluationPoint);
                    if (evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull && n.isPassable)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                //Check up
                evaluationPoint.X = x;
                evaluationPoint.Y = y - 1;
                if (isValidPoint(ref evaluationPoint))
                {
                    Node n = navMesh.getNode(ref evaluationPoint);
                    if (evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull && n.isPassable)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                //Check down
                //evaluationPoint.X is already p.X
                evaluationPoint.Y = y + 1;
                if (isValidPoint(ref evaluationPoint))
                {
                    Node n = navMesh.getNode(ref evaluationPoint);
                    if (evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull && n.isPassable)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = index;
                        n.sourceIsNull = false;
                        queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                #endregion

                #region Search ul, ur, ll, lr
                if(this.diagonalPath)
                {
                    //Check upperleft
                    evaluationPoint.X = x - 1;
                    evaluationPoint.Y = y - 1;
                    if (isValidPoint(ref evaluationPoint))
                    {
                        Node n = navMesh.getNode(ref evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull && n.isPassable)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                        }
                    }

                    //Check upperright
                    evaluationPoint.X = x + 1;
                    //evaluationPoint.Y is already p.Y - 1
                    if (isValidPoint(ref evaluationPoint))
                    {
                        Node n = navMesh.getNode(ref evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull && n.isPassable)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                        }
                    }

                    //Check lowerright
                    //evaluationPoint.X is already p.X + 1
                    evaluationPoint.Y = y + 1;
                    if (isValidPoint(ref evaluationPoint))
                    {
                        Node n = navMesh.getNode(ref evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull && n.isPassable)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                        }
                    }

                    //Check lowerleft
                    evaluationPoint.X = x - 1;
                    //evaluationPoint.Y is already p.Y + 1
                    if (isValidPoint(ref evaluationPoint))
                    {
                        Node n = navMesh.getNode(ref evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull && n.isPassable)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = index;
                            n.sourceIsNull = false;
                            queue.Enqueue(navMesh.getIndex(ref evaluationPoint), (int)(n.distanceTraveled + n.distanceRemaining));
                        }
                    }
                }
                #endregion
            }

            if(found)
            {
                Path path = new Path();
                Point p = new Point(end.X, end.Y);
                path.AddNode(p);
                while(p != start)
                {
                    Node n = navMesh.getNode(ref p);
                    p = navMesh.getPoint(n.source);
                    path.AddNode(p);
                }

                return path;
            }
            else if(!this.mustReachTarget && bestNode != -1)
            {
                Path path = new Path();
                Point p = navMesh.getPoint(bestNode);
                path.AddNode(p);
                while(p != start)
                {
                    Node n = navMesh.getNode(ref p);
                    p = navMesh.getPoint(n.source);
                    path.AddNode(p);
                }
                return path;
            }

            return null;
        }

        private bool isValidPoint(ref Point p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X < navMesh.Width && p.Y < navMesh.Height;
        }

        private bool isValidPoint(int x, int y)
        {
            return x >= 0 && y >= 0 && x < navMesh.Width && y < navMesh.Height;
        }
    }
}
