using HM_App.API.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HM_App
{
    /// <summary>
    /// Interaction logic for Window_HM.xaml
    /// </summary>
    public partial class Window_HM : Window
    {
        public Window_HM()
        {
            InitializeComponent();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((MenuItem)sender).Tag.ToString();
            switch (tag)
            {
                case "AppLocalData":
                    string path = string.Format("{0}", Paths.LocalApplicationData.Replace("\\", "/"));
                    path = path.Remove(path.LastIndexOf('/')).Replace("/", @"\");
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() { UseShellExecute = true, FileName = "explorer.exe", Arguments = path });
                    break;

                case "Exit":
                    Application.Current.Shutdown();
                    break;
                case "PluginManager":
                    Pages.Settings.PluginManager plugin = new Pages.Settings.PluginManager();
                    TabItem tab = new TabItem();
                    tab.Content = plugin;
                    tab.Header = "Plugin Manager";
                    W_TabControl.Items.Add(tab);
                    W_TabControl.SelectedItem = tab;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            switch (tag)
            {
                case "Exit":
                    Application.Current.Shutdown();
                    break;
            }
        }
    }
}
