using IGVC_Controller.Code.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGVC_Controller.Code
{
    public partial class ModuleSelectionWindow : Form
    {
        bool wasActiveListLastChanged = false;

        public ModuleSelectionWindow()
        {
            InitializeComponent();
            updateList();
        }

        public void updateList()
        {
            this.inactiveModuleList.Items.Clear();
            this.inactiveModuleList.Items.AddRange(MainWindow.instance.inactiveModules.ToArray());

            this.activeModuleList.Items.Clear();
            this.activeModuleList.Items.AddRange(MainWindow.instance.activeModules.ToArray());
        }

        private void ModuleActivateButton_Click(object sender, EventArgs e)
        {
            string[] modulesToActivate = new string[inactiveModuleList.SelectedItems.Count];
            inactiveModuleList.SelectedItems.CopyTo(modulesToActivate, 0);

            foreach(string moduleName in modulesToActivate)
            {
                MainWindow.instance.inactiveModules.Remove(moduleName);
                MainWindow.instance.activeModules.Add(moduleName);

                MainWindow.instance.registry.register(MainWindow.instance.moduleDictionary[moduleName]);
            }

            updateList();
        }

        private void ModuleDeactivateButton_Click(object sender, EventArgs e)
        {
            string[] modulesToDeactivate = new string[activeModuleList.SelectedItems.Count];
            activeModuleList.SelectedItems.CopyTo(modulesToDeactivate, 0);

            foreach(string moduleName in modulesToDeactivate)
            {
                MainWindow.instance.inactiveModules.Add(moduleName);
                MainWindow.instance.activeModules.Remove(moduleName);

                MainWindow.instance.registry.unbindModule(MainWindow.instance.moduleDictionary[moduleName]);
            }

            updateList();
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            MainWindow.instance.SaveModuleSettings();
        }

        private void EditModuleProperties_Click(object sender, EventArgs e)
        {
            NoModulePropertiesWindow form = new NoModulePropertiesWindow();
            if (!wasActiveListLastChanged)
                ((IModuleEditor)form).setModule(MainWindow.instance.moduleDictionary[(string)inactiveModuleList.SelectedItem]);

            if (wasActiveListLastChanged)
                ((IModuleEditor)form).setModule(MainWindow.instance.moduleDictionary[(string)activeModuleList.SelectedItem]);
            form.Show();
        }

        private void activeModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.wasActiveListLastChanged = true;
        }

        private void inactiveModuleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.wasActiveListLastChanged = false;
        }
    }
}
