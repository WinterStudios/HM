using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HM_App.API.Properties
{
    public static class Paths
    {
        /// <summary>
        /// Get Local Application Data
        /// </summary>
        /// <remarks>C:\Users\[user]\AppData\Local\HM\</remarks>
        public static string LocalApplicationData
        {
            get
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HM\\"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HM\\");
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HM\\";
            }
        }

        /// <summary>
        /// Get Local Application Data for downloads
        /// </summary>
        /// <remarks>C:\Users\[user]\AppData\Local\HM\Downloads\</remarks>
        public static string LocalApplicagionDataDownloads { get
            {
                if (!Directory.Exists(LocalApplicationData + "Downloads\\"))
                    Directory.CreateDirectory(LocalApplicationData + "Downloads\\");
                return LocalApplicationData + "Downloads\\";
            } }

        public static string LocalApplicationDataUpdate { get
            {
                if (!Directory.Exists(LocalApplicationData + "Update\\"))
                    Directory.CreateDirectory(LocalApplicationData + "Update\\");
                return LocalApplicationData + "Update\\";
            } } 

        public static string SettingsPath { get => LocalApplicationData + "settings.xml"; }

        /// <summary>
        /// Get Plugin Path
        /// </summary>
        /// <remarks>C:\Users\[user]\AppData\Local\HM\Plugins\</remarks>
        public static string PluginsPath { get {
                if (!Directory.Exists(LocalApplicationData + "Plugins\\"))
                    Directory.CreateDirectory(LocalApplicationData + "Plugins\\");
                return LocalApplicationData + "Plugins\\";
            } }
    }
}
