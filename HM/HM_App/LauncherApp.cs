using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using HM_App.API;
using HM_App.API.GitHub;
using HM_App.API.Properties;

namespace HM_App
{
    class LauncherApp
    {
        public static bool AllowUpdate { get => false; }
        public static SemVersion AppVersion { get; private set; }
        public static SemVersion OnlineVersion { get; private set; }
        private static string Token { get => "58221a498d9af2d31783e71eb563494968cd62bc"; }
        public static bool Debug { get; protected set; }
        public static void Initialize()
        {
            Settings.Load();
            GetLocalVersion();
            if(Settings._Settings.ALLOW_UPDATE)
                CheckForUpdate();
            
            //if (Settings._Settings.ALLOW_UPDATE)
            //    GitHubClient.DownloadRelease(GitHubClient.GetReleaseLastet("WinterStudios", "HM", Token), AppDomain.CurrentDomain.BaseDirectory, Token);
        }

        private static void CheckForUpdate()
        {
            if(Settings._Settings.ALLOW_PRE_RELEASE)
            {
                Release preRelease = GitHubClient.GetRelease("WinterStudios", "HM", Token).FirstOrDefault(x => x.PreRelease == true && x.Branch == API.GitHub.Internal.Branch.development);
                OnlineVersion = SemVersion.GetVersionFromGitHub(preRelease.TagName);
                bool updateAvalable = SemVersion.Compare(AppVersion, OnlineVersion);
                if(Settings._Settings.ALLOW_UPDATE)
                {
                    bool newUpdate = SemVersion.Compare(AppVersion, OnlineVersion);
                    if(newUpdate || Settings._Settings.DEBUG_DEVELOPMENT_MODE)
                    {
                        if(Settings._Settings.ALLOW_AUTOMATIC_UPDATE)
                        {
                            try
                            {
                                GitHubClient.DownloadRelease(preRelease, Paths.LocalApplicagionDataDownloads, Token);
                            }
                            catch (GitHubExceptions ex)
                            {
                                
                            }
                            
                        }
                        else
                        {
                            // Add to Notification QUEUE that is a new Update
                        }
                    }

                } 

            }
        }

        public static SemVersion GetLocalVersion()
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var fileVersion = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            AppVersion = SemVersion.GetVersionFromAssembly(fileVersion);
            
            return AppVersion;
        }

        /// private static 

        /// <summary>
        /// Get the Online Version
        /// </summary>
        /// <param name="preRelease"></param>
        /// <remarks>
        /// <para>If true gets Pre-Release Version</para>
        /// <para>If false gets Development Version</para>
        /// <para>If Null gets Last Release Version</para>
        /// </remarks>
        /// public static Release GetOnlineVersion(bool? preRelease)
        /// {
        ///     switch (preRelease)
        ///     {
        ///         case null:
        ///             break;
        ///     }
        ///     Release lastRelease = GitHubClient.GetReleaseLastet("WinterStudios", "HM", Token);
        ///     OnlineVersion = SemVersion.GetVersionFromGitHub(lastRelease.TagName);
        ///     return OnlineVersion;
        /// }
    }
}
