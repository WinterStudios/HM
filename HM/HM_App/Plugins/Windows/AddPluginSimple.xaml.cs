using HM_App.API.GitHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HM_App.Plugins.Windows
{
    /// <summary>
    /// Interaction logic for AddPluginSimple.xaml
    /// </summary>
    public partial class AddPluginSimple : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public Repository Output;

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

        private async void W_TextBox_UserSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();

            timer.Interval = new TimeSpan(0, 0, 2);
            timer.Start();
            timer.Tick += async (sender, e) =>
            {
                List<Repository> repositories = (List<Repository>)await GitHubClient.GetRepositories(W_TextBox_UserSearch.Text);
                W_ListBox_Repositories.ItemsSource = repositories;
                timer.Stop();
            };
        }

        private void W_ListBox_Repositories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Output = (Repository)W_ListBox_Repositories.SelectedItem;
            this.DialogResult = true;
        }
    }
}
