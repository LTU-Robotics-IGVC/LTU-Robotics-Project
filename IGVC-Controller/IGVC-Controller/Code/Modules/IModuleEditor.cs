using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code.Modules
{
    public interface IModuleEditor
    {
        void setModule(IModule module);

        void loadDataFromModule();

        void setDataToModule();
    }
}
