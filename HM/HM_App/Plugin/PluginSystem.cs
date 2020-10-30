using HM_App.API.Plugins;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HM_App.Plugin
{
    public class PluginSystem
    {

        public static List<IPlugin> PluginsCollections { get; set; }

        public static int PluginsCounts { get; set; }

        public static void Initialize()
        {
            
        }
    }
}
