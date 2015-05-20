using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.MathX
{
    class Path
    {
        private List<Node> path;

        public Path()
        {

        }

        public void AddNode(Node node)
        {
            this.path.Add(node);
        }
    }
}
