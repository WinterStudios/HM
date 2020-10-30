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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tagName = ((Button)sender).Tag.ToString();

            switch (tagName)
            {
                case "ADD_PLUGIN":
                    Window addPluginWindow = new Window();
                    addPluginWindow.ShowDialog();
                    break;
            }
        }
    }
}
