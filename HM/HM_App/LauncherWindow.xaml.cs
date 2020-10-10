﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HM_App.API.GitHub;

namespace HM_App
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public LauncherWindow()
        {
            InitializeComponent();
            Task checkVersion = InitializeProcess();
        }
        private async Task InitializeProcess()
        {
            W_TextBlock_InfoProgress.Text = "Initialize App";
            await Task.Delay(1000);

            Release release = API.GitHub.GitHubClient.GetReleaseLastet("WinterStudios", "HM");
            Trace.WriteLine(Assembly.GetExecutingAssembly().FullName);
            Trace.WriteLine(release.TagName);


            //MainWindow main = new MainWindow();
            //main.InitializeComponent();
            ////while (!main.IsLoaded)
            ////{
                //W_TextBlock_InfoProgress.Text = "Loading Main Window";
                //await Task.Delay(1000);
            ////}
            //await Task.Delay(500);
            //W_TextBlock_InfoProgress.Text = "Loading Finish";
            //main.Show();
            //await Task.Delay(500);
            //this.Close();

        }
    }
}
