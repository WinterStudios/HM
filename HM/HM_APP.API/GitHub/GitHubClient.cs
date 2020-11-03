using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using HM_App.API.Properties;

namespace HM_App.API.GitHub
{
    public class GitHubClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>https://api.github.com/</remarks>
        public static string GitHubUrl { get => "https://api.github.com/"; }
        public static string TOKEN { get => Settings._Settings.GITHUB_TOKEN; }

        public static Release GetReleaseLastet(string username, string repositoryName, string? token) => GetRelease(username, repositoryName, true, token).FirstOrDefault();

        public static IEnumerable<Release> GetRelease(string username, string repositoryName, string? token) => GetRelease(username, repositoryName, false, token);




        // Private fucntions
        private static IEnumerable<Release> GetRelease(string username, string repositoryName, bool lastRelease, string? token)
        {
            string url = string.Empty;
            if(lastRelease)
                url = string.Format("{0}repos/{1}/{2}/releases/latest",GitHubUrl,username,repositoryName);
            else
                url = string.Format("{0}repos/{1}/{2}/releases", GitHubUrl, username, repositoryName);


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "HM_App";
            request.ServicePoint.Expect100Continue = false;
            request.Accept = "application/vnd.github.v3.raw";

            if (!string.IsNullOrEmpty(token))
            {
                string credentials = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}", token);
                credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(credentials));
                request.Headers.Add(HttpRequestHeader.Authorization, string.Concat("token ", token));
            }

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

        /// <summary>
        /// Download Release.zip from Release
        /// </summary>
        /// <param name="release">GitHub Release</param>
        /// <param name="outputDirectory">Output Directory</param>
        /// <param name="token">Private Token, case repos is private</param>
        /// <returns></returns>
        public static object DownloadRelease(Release release, string outputDirectory, string token)
        {
            Assets asset = release.Assets.FirstOrDefault(x => x.Name == "Release.zip");
            string url = asset.URL;// + string.Format("?access_token={0}", token);
            Trace.WriteLine(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.UserAgent = "HM_APP";
            request.Accept = "application/octet-stream";
            request.ServicePoint.Expect100Continue = false;
            request.Method = "GET";
            request.Headers.Add(HttpRequestHeader.Authorization, string.Concat("token ", token));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            FileStream file = new FileStream(outputDirectory + "/" + asset.Name, FileMode.Create, System.IO.FileAccess.Write);
            byte[] buffer = new byte[8 * 1024];
            int read = 0;
            while((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                file.Write(buffer, 0, read);
            }
            file.Close();

            return null;
        }


        public static async Task<IEnumerable<Repository>> GetRepositories(string user)
        {
            string url = string.Format("{0}users/{1}/repos", GitHubUrl, user);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "HM_App";
            request.ServicePoint.Expect100Continue = false;
            request.Accept = "application/vnd.github.v3.raw";
            request.Headers.Add(HttpRequestHeader.Authorization, string.Concat("token ", TOKEN));

            HttpWebResponse response = new HttpWebResponse();
            try
            {
                response = (HttpWebResponse)await request.GetResponseAsync();
                
            }
            catch (WebException ex)
            {
                return new List<Repository>();
            }


            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            List<Repository> repositories = JsonSerializer.Deserialize<Repository[]>(json).ToList();
            
            return repositories;
        }


        [Obsolete]
        public GitHubClient() => new GitHubClient("");
        [Obsolete]
        public GitHubClient(string user)
        {
            string url = string.Format("{0}users/{1}", GitHubUrl, user);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "HM_App";
            request.ServicePoint.Expect100Continue = false;
            request.Accept = "application/vnd.github.v3.raw";

            HttpWebResponse response = new HttpWebResponse();
            try
            {
                response = (HttpWebResponse) request.GetResponse();

            }
            catch (WebException ex)
            {
                 
            }
           
        }

        public static Release[] GetReleases(Repository repository)
        {
            string url = string.Format("{0}repos/{1}/{2}/releases", GitHubUrl, repository.Owner.Name, repository.Name);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "HM_App";
            request.ServicePoint.Expect100Continue = false;
            request.Accept = "application/vnd.github.v3.raw";
            request.Headers.Add(HttpRequestHeader.Authorization, string.Concat("token ", TOKEN));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();

            List<Release> releases = JsonSerializer.Deserialize<Release[]>(json).ToList();

            return releases.ToArray();
        }


        [Obsolete]
        private static object Get(string url, string token, Type type)
        {
            url = string.Format("{0}", GitHubUrl);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.UserAgent = "HM_App";
            request.ServicePoint.Expect100Continue = false;
            request.Accept = "application/vnd.github.v3.raw";


            return null;
        }

    }
}
