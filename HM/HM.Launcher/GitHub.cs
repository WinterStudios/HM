using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HM.Launcher.GitHub
{
    public class GitHub
    {
        public static string GitHub_API { get => "https://api.github.com/"; }
        public static string User { get => "winterstudios"; }
        public static string ProjectName { get => "HM"; }

        public static string GetLastVersion()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GitHub_API + "repos/" + User + "/" + ProjectName + "/releases/latest");
            request.Method = "GET";
            request.UserAgent = "Anything";
            request.ServicePoint.Expect100Continue = false;
            try
            {
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string jsonString = reader.ReadToEnd();
                Console.WriteLine(jsonString);
            }
            catch(WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            return null;
        }
    }
}
