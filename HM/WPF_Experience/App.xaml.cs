using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Experience
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            window.Show();
            
            Thread thread = new Thread(() =>
            {
                
                for (int i = 0; i < 1000; i++)
                {
                    window.Dispatcher.Invoke(new Action(() => window.BR.Text = i.ToString()));
                    Thread.Sleep(100);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }
    }
}
