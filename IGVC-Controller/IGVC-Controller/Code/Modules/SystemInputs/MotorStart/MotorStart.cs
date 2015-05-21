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

        /// <summary>
        /// Enables dynamic drive (true) or three state drive (false) 
        /// </summary>
        public bool dynamic_drive = false;

        delegate void delegateCloseForm();

        public MotorStart() : base()
        {
            this.modulePriority = 11;
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.dynamic_drive = config.Read<bool>("dynamic_drive", false);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<bool>("dynamic_drive", this.dynamic_drive);

            base.writeToConfig(config);
        }

        public override bool startup()
        {
           sendDataToRegistry(INTERMODULE_VARIABLE.DYNAMIC_DRIVE_ENABLED, dynamic_drive);
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
