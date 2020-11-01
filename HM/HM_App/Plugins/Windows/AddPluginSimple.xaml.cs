using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HM_App.Plugins.Windows
{
    /// <summary>
    /// Interaction logic for AddPluginSimple.xaml
    /// </summary>
    public partial class AddPluginSimple : Window
    {
        public AddPluginSimple()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tagName = ((Button)sender).Tag.ToString();

            if (tagName == "Close")
                this.Close();
        }
    }
}
