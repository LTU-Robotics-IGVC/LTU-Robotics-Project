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
using IGVC_Controller.Code.Modules.SystemInputs;
using IGVC_Controller.Code.Modules.Example;
using IGVC_Controller.Code.Modules.LIDAR;
using IGVC_Controller.Code.Modules.Visualizer;

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

            //Console.SetWindowPosition(0, 0);
            //Console.WindowLeft = 0;
            //Console.WindowTop = 0;
            //Console.SetWindowSize(50, 40);

            //this.SetDesktopBounds(3, 0, 500, 800);
            //this.SetDesktopLocation(0, 0);
            this.StartPosition = FormStartPosition.Manual;
            this.DesktopLocation = new Point(0, 0); 
        }

        private void setupModules()
        {
            config.BeginRead();

            this.setupModule("StereoVision", new StereoVision());
            this.setupModule("Logger", new Logger());
            this.setupModule("ConsoleLogger", new ConsoleLogger());
            this.setupModule("RandomDataGenerator", new RandomDataGenerator());
            this.setupModule("RandomDataListener", new RandomDataListener());
            this.setupModule("ForcedDelay", new ForcedDelay());
            this.setupModule("LIDAR_Interface", new LIDAR_Interface());
            this.setupModule("LIDAR_Visualizer", new LIDAR_Visualizer());

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
            config.itemPath = moduleName + "_";
            module.loadFromConfig(config);
            config.itemPath = "";

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
                config.itemPath = moduleNameDictionary[module] + "_";
                module.writeToConfig(config);
            }

            config.itemPath = "";
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(button2.Text == "start")
            {
                registry.start();
                button2.Text = "stop";
            }
            else
            {
                registry.stop();
                button2.Text = "start";
            }
        }
    }
}
