using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Example
{
    class ForcedDelay : IModule
    {
        int delay_ms = 1000;

        public ForcedDelay() : base()
        {
            this.modulePriority = 25;//this would pause data collect from data preprocess
        }

        public override void loadFromConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            this.delay_ms = config.Read<int>("delay", 1000);
            base.loadFromConfig(config);
        }

        public override void writeToConfig(IGVC_Controller.DataIO.SaveFile config)
        {
            config.Write<int>("delay", delay_ms);
            base.writeToConfig(config);
        }

        public override void process()
        {
            //Pauses the thread this module exists on
            Thread.Sleep(delay_ms);

            base.process();
        }
    }
}
