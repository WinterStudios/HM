using HM_App.API;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM_App.Plugins
{
    public class PluginInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string File { get; set; }
        public string Author { get; set; }
        public SemVersion Version { get; set; }
        public bool Enable { get; set; }
    }
}
