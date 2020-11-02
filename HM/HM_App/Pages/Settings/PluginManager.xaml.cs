using HM_App.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HM_App.Pages.Settings
{
    /// <summary>
    /// Interaction logic for PluginManager.xaml
    /// </summary>
    public partial class PluginManager : UserControl
    {
        public PluginManager()
        {
            InitializeComponent();
            int plugins = 4;
            for (int i = 0; i < plugins; i++)
            {
                //PluginInformation information = new PluginInformation();
                //information.PluginName = "MeioMundo.Editor.Ferramentas";
                //UC_StackPanel_Plugins.Children.Add(information);

            }
        }

        private void UC_Button_AddPlugin_Click(object sender, RoutedEventArgs e)
        {
            string tagName = ((Button)sender).Tag.ToString();
            if(tagName == "Btn_AddPlugin_Simple")
            {
                // UC_PopUp_AddPlugin.IsOpen = true;
                Plugins.Windows.AddPluginSimple window = new Plugins.Windows.AddPluginSimple();
                if(window.ShowDialog() == true)
                {
                    //PluginInformation plugin = new PluginInformation();
                    //// plugin.Repository = window.Output;
                    //// plugin.SetInformation();
                    //UC_StackPanel_Plugins.Children.Add(plugin);
                }

            }

            if (tagName == "Btn_AddPlugin_Add")
                PluginSystem.AddPlugin(UC_TextBox_AddPlugin_Username.Text, UC_TextBox_AddPlugin_Repository.Text);

        }

    }
}
