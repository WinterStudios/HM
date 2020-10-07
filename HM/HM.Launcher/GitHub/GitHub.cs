using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace HM.Launcher.GitHub
{
    public class GitHub
    {
        public static string GitHub_API { get => "https://api.github.com/"; }
        public static string User { get => "WinterStudio"; }
        public static string ProjectName { get => "HM"; }

        public static string GetLastVersion()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GitHub_API + "repos/" + User + "/" + ProjectName + "/releases/lastet");
            request.Method = "GET";
            request.UserAgent = "Anything";
            request.ServicePoint.Expect100Continue = false;

            WebResponse response = request.GetResponse();


            return null;
        }
    }
}
