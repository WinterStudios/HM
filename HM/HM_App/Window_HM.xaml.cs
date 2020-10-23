using HM_App.API.Properties;
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
