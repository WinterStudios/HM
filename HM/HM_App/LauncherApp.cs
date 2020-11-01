using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using HM_App.API;
using HM_App.API.GitHub;
using HM_App.API.Properties;
using HM_App.Plugins;

namespace HM_App
{
    class LauncherApp
    {

        public static SemVersion AppVersion { get; private set; }
        public static SemVersion OnlineVersion { get; private set; }
        private static string Token { get => "63fc270ed63c11dd6d077e4ebf4e23c1373cb943"; }
        public static bool Debug { get; protected set; }
        
        public static LauncherWindow LauncherWindow { get; set; }
        
        public static void Initialize()
        {
            GetLocalVersion();
            LauncherWindow.Dispatcher.Invoke(new Action(() => LauncherWindow.W_TextBlock_Version.Text = AppVersion.ToString()));
            Thread.Sleep(500);
            LauncherWindow.Dispatcher.Invoke(new Action(() => LauncherWindow.W_TextBlock_InfoProgress.Text = "Loading Settings"));
            Settings.Load();
            Thread.Sleep(1000);
            LauncherWindow.Dispatcher.Invoke(new Action(() => LauncherWindow.W_TextBlock_InfoProgress.Text = "Settigns Load"));
            Thread.Sleep(500);
            bool update = Environment.GetCommandLineArgs().Contains("-updated");
            LauncherWindow.Dispatcher.Invoke(new Action(() => LauncherWindow.W_TextBlock_InfoProgress.Text = "Check For Updates"));
            if (!update)
                CheckForUpdate();

            LauncherWindow.Dispatcher.Invoke(new Action(() => LauncherWindow.W_TextBlock_InfoProgress.Text = "Loading..."));
            App.Current.Dispatcher.Invoke(new Action(() => PluginSystem.Initialize()));
            App.Current.Dispatcher.Invoke(new Action(() => App.StartLoadMainWindow()));
        }

        private static void CheckForUpdate()
        {
            if(Settings._Settings.ALLOW_PRE_RELEASE)
            {
                Release preRelease = GitHubClient.GetRelease("WinterStudios", "HM", "").FirstOrDefault(x => x.PreRelease == true && x.Branch == API.GitHub.Internal.Branch.preview);
                OnlineVersion = SemVersion.GetVersionFromGitHub(preRelease.TagName);
                bool updateAvalable = SemVersion.Compare(AppVersion, OnlineVersion);
                if (updateAvalable && Settings._Settings.ALLOW_AUTOMATIC_UPDATE)
                {
                    LauncherWindow.Dispatcher.Invoke(new Action(() => LauncherWindow.W_TextBlock_InfoProgress.Text = "Download Update"));
                    GitHubClient.DownloadRelease(preRelease, Paths.LocalApplicagionDataDownloads, Token);
                    UpdateApp();
                    App.Current.Shutdown();
                    // reset
                    // Add to notification System
                }
                // if(updateAvalable)
                    // add to notification System
            }
            else
            {
                Release release = GitHubClient.GetRelease("WinterStudios", "HM", Token).FirstOrDefault(x => x.PreRelease == false && x.Branch == API.GitHub.Internal.Branch.main);
                OnlineVersion = SemVersion.GetVersionFromGitHub(release.TagName);
                bool updateAvalable = SemVersion.Compare(AppVersion, OnlineVersion);
                if (updateAvalable && Settings._Settings.ALLOW_AUTOMATIC_UPDATE)
                {
                    GitHubClient.DownloadRelease(release, Paths.LocalApplicagionDataDownloads, Token);
                    UpdateApp();
                    App.Current.Shutdown();
                    // reset
                    // Add to notification System
                }
                // if(updateAvalable)
                // add to notification System
            }
        }

        private static void UpdateApp()
        {
            string updateExe = AppDomain.CurrentDomain.BaseDirectory + "Update.exe";
            string updateDll = AppDomain.CurrentDomain.BaseDirectory + "Update.dll";
            string updateJsonRuntime = AppDomain.CurrentDomain.BaseDirectory + "Update.runtimeconfig.json";
            
            File.Copy(updateExe, Paths.LocalApplicationDataUpdate + "Update.exe", true);
            File.Copy(updateDll, Paths.LocalApplicationDataUpdate + "Update.dll", true);
            File.Copy(updateJsonRuntime, Paths.LocalApplicationDataUpdate + "Update.runtimeconfig.json", true);
            
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Paths.LocalApplicationDataUpdate + "Update.exe";
            startInfo.Arguments = string.Format("{0} {1} {2}", "-update", Paths.LocalApplicagionDataDownloads, AppDomain.CurrentDomain.BaseDirectory);
            process.StartInfo = startInfo;
            process.Start();
        }

        public static SemVersion GetLocalVersion()
        {
            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var fileVersion = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            AppVersion = SemVersion.GetVersionFromAssembly(fileVersion);
            
            return AppVersion;
        }

    }
}
