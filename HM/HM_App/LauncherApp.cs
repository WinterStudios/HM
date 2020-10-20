using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static Thread MainWindowThread { get; set; }

        public static void Initialize()
        {
            Settings.Load();
            GetLocalVersion();
            bool update = Environment.GetCommandLineArgs().Contains("-updated");
            if (!update)
                CheckForUpdate();

            // Load Plugins or check for them
            LoadWindow();

        }

        private static void CheckForUpdate()
        {
            if(Settings._Settings.ALLOW_PRE_RELEASE)
            {
                Release preRelease = GitHubClient.GetRelease("WinterStudios", "HM", Token).FirstOrDefault(x => x.PreRelease == true && x.Branch == API.GitHub.Internal.Branch.preview);
                OnlineVersion = SemVersion.GetVersionFromGitHub(preRelease.TagName);
                bool updateAvalable = SemVersion.Compare(AppVersion, OnlineVersion);
                if (updateAvalable && Settings._Settings.ALLOW_AUTOMATIC_UPDATE)
                {
                    GitHubClient.DownloadRelease(preRelease, Paths.LocalApplicagionDataDownloads, Token);

                    UpdateApp();
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

            File.Copy(updateExe, Paths.LocalApplicationDataUpdate + "Update.exe", true);
            File.Copy(updateDll, Paths.LocalApplicationDataUpdate + "Update.dll", true);

            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Paths.LocalApplicationDataUpdate + "Update.exe";
            startInfo.Arguments = string.Format("-{0} {1} {2}", "update", Paths.LocalApplicagionDataDownloads, AppDomain.CurrentDomain.BaseDirectory);
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

        private static void LoadWindow()
        {
            
            MainWindowThread = new Thread(() =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            })
            { IsBackground = true };
            MainWindowThread.SetApartmentState(ApartmentState.STA);
            MainWindowThread.Start();

        }
    }
}
