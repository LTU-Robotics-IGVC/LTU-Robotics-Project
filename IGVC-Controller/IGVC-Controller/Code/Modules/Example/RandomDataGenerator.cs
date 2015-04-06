using IGVC_Controller.DataIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.SystemInputs
{
    class RandomDataGenerator : IModule
    {
        Random rand = new Random();
        double maxVal = 1;
        double minVal = 0;

        public RandomDataGenerator()
        {
            this.setLogTag("random_data_generator");
            this.modulePriority = 0;
        }

        public override void loadFromConfig(SaveFile config)
        {
            maxVal = config.Read<double>("maxVal", 1);
            minVal = config.Read<double>("minVal", 0);

            base.loadFromConfig(config);
        }

        public override void writeToConfig(SaveFile config)
        {
            config.Write<double>("maxVal", maxVal);
            config.Write<double>("minVal", minVal);

            base.writeToConfig(config);
        }

        public override void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            throw new Exception("Module should not receive data");
        }

        public override void process()
        {
            double x = rand.NextDouble();
            x = (maxVal - minVal) * x + minVal;
            this.sendDataToRegistry(INTERMODULE_VARIABLE.EXAMPLE, x);
            base.process();
        }
    }
}
