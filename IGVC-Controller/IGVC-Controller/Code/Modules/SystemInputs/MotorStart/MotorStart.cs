using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.SystemInputs.MotorStart
{
    class MotorStart : IModule
    {
        MotorStartForm form;

        delegate void delegateCloseForm();

        public MotorStart() : base()
        {
            this.modulePriority = 11;
        }

        public override bool startup()
        {
            form = new MotorStartForm();
            
            form.Show();
            return base.startup();
        }

        public override void process()
        {
            sendDataToRegistry(INTERMODULE_VARIABLE.DRIVING_ENABLED, form.CheckStatus());
            
            base.process();
        }

        public override void shutdown()
        {
            if (form.InvokeRequired)
            {
                delegateCloseForm del = this.closeForm;
                form.Invoke(del, null);
            }
            else
            {
                form.Close();
            }

            base.shutdown();
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new MotorStartEditor();
        }

        private void closeForm()
        {
            form.Close();
        }
    }
}
