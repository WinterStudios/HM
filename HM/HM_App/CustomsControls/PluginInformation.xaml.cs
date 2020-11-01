using HM_App.API.GitHub;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HM_App.CustomsControls
{
    /// <summary>
    /// Interaction logic for PluginInformation.xaml
    /// </summary>
    public partial class PluginInformation : UserControl
    {


        public string PluginName
        {
            get { return (string)GetValue(PluginNameProperty); }
            set { SetValue(PluginNameProperty, value); }
        }
        public string PluginOwner
        {
            get { return (string)GetValue(PluginOwnerProperty); }
            set { SetValue(PluginOwnerProperty, value); }
        }

        public Repository Repository { get; internal set; }


        // Using a DependencyProperty as the backing store for PluginName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PluginNameProperty =
            DependencyProperty.Register("PluginName", typeof(string), typeof(PluginInformation));

        public static readonly DependencyProperty PluginOwnerProperty =
            DependencyProperty.Register("PluginOwner", typeof(string), typeof(PluginInformation));


        public PluginInformation()
        {
            InitializeComponent();
        }
        public void SetInformation()
        {
            PluginName = Repository.Name;
            PluginOwner = Repository.Owner.Name;
        }
    }
}
