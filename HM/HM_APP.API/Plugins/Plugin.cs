using HM_App.API.GitHub;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM_App.API.Plugins
{
    public class Plugin
    {
        public string Name { get; set; }
        public Repository Repository { get; set; }
        public Release[] Releases { get; set; }
        public SemVersion CurrentVersion { get; set; }
        public bool AutomaticUpdate { get; set; }

    }
}
