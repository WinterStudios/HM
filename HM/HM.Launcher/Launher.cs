using HM.Launcher.GitHub;
using System;
using System.Collections.Generic;
using System.Text;

namespace HM.Launcher
{
    public class Launher
    {
        public static void Main(string[] arg)
        {
            GitHub.GitHub.GetLastVersion();
        }
    }
}
