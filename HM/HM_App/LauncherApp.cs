using HM_App.API;
using HM_App.API.GitHub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace HM_App
{
    class LauncherApp
    {
        public static bool AllowUpdate { get => false; }
        public static SemVersion AppVersion { get; private set; }
        public static SemVersion OnlineVersion { get; private set; }
        private static string Token { get => "24124c08069e1e1c1f35e7bebfa9d5b179f49dc9"; }
        public static bool Debug { get; protected set; }
        public static void Initialize()
        {
            GetLocalVersion();
            GetOnlineVersion(null);
            Trace.WriteLine(AppVersion.ToString());
            Trace.WriteLine(OnlineVersion.ToString());
        }
        public static SemVersion GetLocalVersion()
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var fileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            AppVersion = SemVersion.GetVersionFromAssembly(fileVersion);
            
            return AppVersion;
        }
        /// <summary>
        /// Get the Online Version
        /// </summary>
        /// <param name="preRelease"></param>
        /// <remarks>
        /// <para>If true gets Pre-Release Version</para>
        /// <para>If false gets Development Version</para>
        /// <para>If Null gets Release Version</para>
        /// </remarks>
        public static SemVersion GetOnlineVersion(bool? preRelease)
        {
            Release lastRelease = GitHubClient.GetReleaseLastet("WinterStudios", "HM", Token);
            OnlineVersion = SemVersion.GetVersionFromGitHub(lastRelease.TagName);
            return OnlineVersion;
        }
    }
}
