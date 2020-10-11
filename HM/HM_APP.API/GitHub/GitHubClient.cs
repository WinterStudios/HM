using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace HM_App.API.GitHub
{
    public class GitHubClient
    {
        public static string GitHubUrl { get => "https://api.github.com/"; }

        public static Release GetReleaseLastet(string username, string repositoryName) => GetRelease(username, repositoryName, true).FirstOrDefault();

        public static IEnumerable<Release> GetRelease(string username, string repositoryName) => GetRelease(username, repositoryName, false);




        // Private fucntions
        private static IEnumerable<Release> GetRelease(string username, string repositoryName, bool lastRelease)
        {
            string url = string.Empty;
            if(lastRelease)
                url = string.Format("{0}repos/{1}/{2}/releases/latest",GitHubUrl,username,repositoryName);
            else
                url = string.Format("{0}repos/{1}/{2}/releases", GitHubUrl, username, repositoryName);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "Anything";
            request.ServicePoint.Expect100Continue = false;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string json = reader.ReadToEnd();

            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            

            if (lastRelease)
            { 
                Release release = JsonSerializer.Deserialize<Release>(json, options);
                List<Release> releases = new List<Release>();
                releases.Add(release);
                return releases;
            }
            else
            {
                List<Release> releases = new List<Release>();
                releases = JsonSerializer.Deserialize<List<Release>>(json, options);
                return releases;
            }
                

            
            
        }

    }
}
