using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Xml.Serialization;

namespace HM_App.API.Properties
{
    public class Settings
    {
        public static string SettingPath { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/HM/Settings.xml"; }
        public static string SettingFolder { get => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/HM/"; }
        public static Settings _Settings { get; set; }
        public bool DEBUG_DEVELOPMENT_MODE { get; set; }
        public bool ALLOW_PRE_RELEASE { get; set; }

        public static void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            StreamReader stream = new StreamReader(SettingPath);
            _Settings = (Settings)serializer.Deserialize(stream);
            stream.Close();
        }
        public static void LoadDefault()
        {
            _Settings = new Settings();
            _Settings.DEBUG_DEVELOPMENT_MODE = true;
            _Settings.ALLOW_PRE_RELEASE = true;

        }
        public static void Save()
        {
            Settings settings = new Settings();
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));

            if (!Directory.Exists(SettingFolder))
                Directory.CreateDirectory(SettingFolder);
            StreamWriter stream = new StreamWriter(SettingPath);

            serializer.Serialize(stream, settings);
            stream.Close();
        }

    }
}
