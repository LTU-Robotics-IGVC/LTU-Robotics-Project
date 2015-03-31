using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using IGVC_Controller.RobotInterface.Simulator;
using IGVC_Controller.Code.Registries;
using IGVC_Controller.Code.Modules.Logger;
using IGVC_Controller.Code.Modules.Vision;
using IGVC_Controller.DataIO;
using IGVC_Controller.Code.Modules;
using IGVC_Controller.Code;

namespace IGVC_Controller
{
    public partial class MainWindow : Form
    {
        public Registry registry = new Registry();
        public Dictionary<string, IModule> moduleDictionary = new Dictionary<string, IModule>();
        public Dictionary<IModule, string> moduleNameDictionary = new Dictionary<IModule, string>();
        public List<string> activeModules = new List<string>();
        public List<string> inactiveModules = new List<string>();
        public List<string> moduleNames = new List<string>();
        public static SaveFile config = new SaveFile("config");

        public Form moduleSelectionWindow;// = new ModuleSelectionWindow();

        public static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();
            //capture = new Capture();
            //capture.ImageGrabbed += capture_ImageGrabbed;
            //capture.ImageGrabbed += process_FPS;
            //capture.Start();

            instance = this;

            this.FormClosing += Form1_FormClosing;

            this.setupModules();
        }

        private void setupModules()
        {
            config.BeginRead();

            this.setupModule("StereoVision", new StereoVision());
            this.setupModule("Logger", new Logger());

            activeModules = config.Read<List<string>>("Active_Modules", new List<string>());
            foreach(string moduleName in moduleNames)
            {
                if(activeModules.Contains(moduleName))
                {
                    registry.register(moduleDictionary[moduleName]);
                }
                else
                {
                    inactiveModules.Add(moduleName);
                }
            }

            config.EndRead();
        }

        private void setupModule(string moduleName, IModule module)
        {
            module.loadFromConfig(config);
            moduleDictionary.Add(moduleName, module);
            moduleNameDictionary.Add(module, moduleName);
            moduleNames.Add(moduleName);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        public void SaveModuleSettings()
        {
            config.BeginWrite();

            config.Write<List<string>>("Active_Modules", activeModules);

            foreach (IModule module in moduleDictionary.Values)
            {
                module.writeToConfig(config);
            }

            config.EndWrite();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (moduleSelectionWindow != null && moduleSelectionWindow.Visible)
                return;
            Type x = typeof(ModuleSelectionWindow);
            moduleSelectionWindow = (Form)x.GetConstructor(new Type[0]).Invoke(new object[0]);

            moduleSelectionWindow.Show();
            moduleSelectionWindow.Focus();
        }
    }
}
