using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HM_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LauncherWindow LauncherWindows { get; set; }
        static Thread startUpThread { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LauncherWindows = new LauncherWindow();
            LauncherWindows.Show();


            startUpThread = new Thread(() =>
            {
                LauncherApp.LauncherWindow = LauncherWindows;
                LauncherApp.Initialize();
            });
            startUpThread.SetApartmentState(ApartmentState.STA);
            startUpThread.Start();


        }

        public static void LoadResources()
        {
            ResourceDictionary Colores = new ResourceDictionary() { Source = new Uri("/HM_App;component/Themes/Light.xaml", UriKind.RelativeOrAbsolute) };
            App.Current.Resources.MergedDictionaries.Add(Colores);

            //ResourceDictionary ButtonStyles = new ResourceDictionary() { Source = new Uri("/HM_App;component/Styles/Buttons.xaml", UriKind.RelativeOrAbsolute) };
            //App.Current.Resources.MergedDictionaries.Add(ButtonStyles);
        }

        public static void StartLoadMainWindow()
        {
            //LoadResources();
            Window_HM window = new Window_HM();
            
            window.Show();
            LauncherWindows.Close();

        }
    }
}
