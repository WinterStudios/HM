using HM_App.API.GitHub;
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
        private static PluginManager PluginUI;
        public PluginManager()
        {
            InitializeComponent();
            PluginUI = this;
            //int plugins = 4;
            //for (int i = 0; i < plugins; i++)
            //{
            //    PluginInfo plugin = new PluginInfo { Name = "Plugin " + i };
            //    UC_ListBox_Plugins.Items.Add(plugin);
            //    //if (i < plugins)
            //        //UC_ListBox_Plugins.SelectedItem = plugin;
            //}

            //UC_ListBox_Plugins.Loaded += (sender, e) =>
            //{
            //    UC_ListBox_Plugins.SelectedIndex = 3;
            //    ListBoxItem lastItem = (ListBoxItem)UC_ListBox_Plugins.ItemContainerGenerator.ContainerFromIndex(UC_ListBox_Plugins.SelectedIndex);
            //    lastItem.Focus();
            //};

            for (int i = 0; i < PluginSystem.Plugins.Count; i++)
            {
                PluginInfo plugin = new PluginInfo();
                plugin.Name = PluginSystem.Plugins[i].Name;
                plugin.Version = PluginSystem.Plugins[i].Releases[0].TagName;
                plugin.Author = PluginSystem.Plugins[i].Repository.Owner.Name;

                UC_ListBox_Plugins.Items.Add(plugin);
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
                    PluginSystem.AddPlugin(window.Output);
                    //PluginInformation plugin = new PluginInformation();
                    //// plugin.Repository = window.Output;
                    //// plugin.SetInformation();
                    //UC_StackPanel_Plugins.Children.Add(plugin);
                }

            }

            //if (tagName == "Btn_AddPlugin_Add")
            //    PluginSystem.AddPlugin(UC_TextBox_AddPlugin_Username.Text, UC_TextBox_AddPlugin_Repository.Text);

        }

        public static void AddPlugin(Repository plugininfo)
        {
            
        }        

    }
}
