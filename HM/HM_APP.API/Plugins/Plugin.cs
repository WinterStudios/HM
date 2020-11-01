using System;
using System.Collections.Generic;
using System.Text;

namespace HM_App.API.Plugins
{
    public class Plugin
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Repository { get; set; }
        public bool AutomaticUpdate { get; set; }
    }
}
