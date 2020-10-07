using MeioMundo.Editor.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HM.API.GitHub
{
    public class GitHub
    {

        #region Static Zone
        public static string User { get { return "WinterStudios"; } }
        public static string Repository { get { return "MeioMundo"; } }
        public static string GitHubUrl { get { return "https://api.github.com/repos/"; } }
        /// <summary>
        /// Get the Last Version of the Reposity
        /// </summary>
        public static string GetRelease()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/WinterStudios/MeioMundo/releases/latest");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;

            WebResponse response = webRequest.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }
        /// <summary>
        /// Get the Last Version of the Reposity
        /// </summary>
        /// <param name="user">Username</param>
        /// <param name="reposity">Reposity Name</param>
        public static void GetRelease(string user, string reposity)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://api.github.com/repos/"+ user +"/" + reposity + "/releases/latest");
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;

            WebResponse response = webRequest.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            Console.WriteLine(reader.ReadToEnd());
        }


        // ------------------------------- Zone Finish --------------------------------------------------------------------------------------------------
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <value>"https://api.github.com/"</value>
        public string GitHubApiUrl { get => "https://api.github.com/"; }
        public string Username { get; set; }
        public string RepositoryName { get; set; }
        public IEnumerable<Release> Releases { get; private set; }

        public string ReleaseURL { get; protected set; }
         

        private HttpWebRequest webRequest { get; set; }

        public GitHub()
        {
            
        }
        /// <summary>
        /// Create a GitHub Client with all the stuff
        /// </summary>
        /// <param name="username">User</param>
        /// <param name="reposities">Repository Name</param>
        public GitHub(string username, string reposities)
        {
            if (!string.IsNullOrEmpty(Username) || !string.IsNullOrEmpty(RepositoryName))
                return;

            Releases = GetReleases(username, reposities).ToList();
        }

        public IEnumerable<Release> GetReleases(string user, string repos)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(GitHubApiUrl + "repos/" + string.Format("{0}/{1}/releases",user,repos)));
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;
            WebResponse response;
            response = webRequest.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonString = reader.ReadToEnd();

            var releasesObj = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

            List<object> list_releases = new List<object>((IEnumerable<object>)releasesObj);
            List<Release> releases = new List<Release>();
            for (int i = 0; i < list_releases.Count; i++)
            {
                Release release = Newtonsoft.Json.JsonConvert.DeserializeObject<Release>(list_releases[i].ToString());
                releases.Add(release);
                
            }
            return releases;
        }



    }
}
