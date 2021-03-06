﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGVC_Controller.DataIO;

namespace IGVC_Controller.Code.Modules.Logger
{
    class Logger : IModule
    {
        public Logger() : base()
        {
            setLogTag("Logger");
            addSubscription(IModule.INTERMODULE_VARIABLE.LOG);
            this.modulePriority = 1000;
        }

        override public void recieveDataFromRegistry(INTERMODULE_VARIABLE tag, object data)
        {
            KeyValuePair<string, KeyValuePair<string, string>> values = (KeyValuePair<string, KeyValuePair<string, string>>)data;
            string thetag = values.Key;
            string severity = values.Value.Key;
            string message = values.Value.Value;
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.WriteLine("{0,-20} | {1,-10} | {2,-10}", thetag.ToUpper(), severity.ToUpper(), message);
            }
        }

        public override void writeToConfig(SaveFile config)
        {
            base.writeToConfig(config);
        }
    }
}
