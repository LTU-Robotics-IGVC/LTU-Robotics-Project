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
                }
            }

            //Setup needed variables
            bool found = false;
            //best checks are not used yet
            Point bestNode = new Point(-1, -1);
            int bestDistance = int.MaxValue;
            //queue used to hold paths for evaluation
            SortedQueue<Point> queue = new SortedQueue<Point>();
            queue.Enqueue(start, (int)navMesh.getPathLength(start));
            Point evaluationPoint = new Point();

            while(!found && !queue.isEmpty())
            {
                Point p = queue.Dequeue();
                Node evalNode = navMesh.getNode(p);

                #region Search left, right, up, down
                //Check left
                evaluationPoint.X = p.X - 1;
                evaluationPoint.Y = p.Y;
                if (isValidPoint(evaluationPoint))
                {
                    Node n = navMesh.getNode(evaluationPoint);
                    if(evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                //Check right
                evaluationPoint.X = p.X + 1;
                //evaluationPoint.Y is already p.Y
                if (isValidPoint(evaluationPoint))
                {
                    Node n = navMesh.getNode(evaluationPoint);
                    if (evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                //Check up
                evaluationPoint.X = p.X;
                evaluationPoint.Y = p.Y - 1;
                if (isValidPoint(evaluationPoint))
                {
                    Node n = navMesh.getNode(evaluationPoint);
                    if (evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                //Check down
                //evaluationPoint.X is already p.X
                evaluationPoint.Y = p.Y + 1;
                if (isValidPoint(evaluationPoint))
                {
                    Node n = navMesh.getNode(evaluationPoint);
                    if (evaluationPoint == end)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        found = true;
                        continue;
                    }
                    if (n.sourceIsNull)
                    {
                        n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                        n.source = p;
                        queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
                    }
                }

                #endregion

                #region Search ul, ur, ll, lr
                if(this.diagonalPath)
                {
                    //Check upperleft
                    evaluationPoint.X = p.X - 1;
                    evaluationPoint.Y = p.Y - 1;
                    if (isValidPoint(evaluationPoint))
                    {
                        Node n = navMesh.getNode(evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
                        }
                    }

                    //Check upperright
                    evaluationPoint.X = p.X + 1;
                    //evaluationPoint.Y is already p.Y - 1
                    if (isValidPoint(evaluationPoint))
                    {
                        Node n = navMesh.getNode(evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
                        }
                    }

                    //Check lowerright
                    //evaluationPoint.X is already p.X + 1
                    evaluationPoint.Y = p.Y + 1;
                    if (isValidPoint(evaluationPoint))
                    {
                        Node n = navMesh.getNode(evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
                        }
                    }

                    //Check lowerleft
                    evaluationPoint.X = p.X - 1;
                    //evaluationPoint.Y is already p.Y + 1
                    if (isValidPoint(evaluationPoint))
                    {
                        Node n = navMesh.getNode(evaluationPoint);
                        if (evaluationPoint == end)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            found = true;
                            continue;
                        }
                        if (n.sourceIsNull)
                        {
                            n.distanceTraveled = n.traverseCost + evalNode.distanceTraveled;
                            n.source = p;
                            queue.Enqueue(new Point(evaluationPoint.X, evaluationPoint.Y), (int)(n.distanceTraveled + n.distanceRemaining));
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
                    Node n = navMesh.getNode(p);
                    p = n.source;
                    path.AddNode(p);
                }

                return path;
            }

            return null;
        }

        private bool isValidPoint(Point p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X < navMesh.Width && p.Y < navMesh.Height;
        }
    }
}
