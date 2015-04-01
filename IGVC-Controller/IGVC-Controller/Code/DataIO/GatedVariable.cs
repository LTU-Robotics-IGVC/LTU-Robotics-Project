using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.DataIO
{
    class GatedVariable
    {
        private object activeObject;
        private object inactiveObject;

        public void setObject(object nextObject)
        {
            this.inactiveObject = nextObject;
        }

        public void shiftObject()
        {
            this.activeObject = inactiveObject;
        }

        public object getObject()
        {
            return this.activeObject;
        }
    }
}
