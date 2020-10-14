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
        public static Settings _Settings { get; set; }
        public bool DEBUG_DEVELOPMENT_MODE { get; set; }
        public bool ALLOW_PRE_RELEASE { get; set; }
        public bool ALLOW_UPDATE { get; set; }

        public static void Load()
        {
            if (!File.Exists(Paths.SettingsPath))
            {
                LoadDefault();
                Save();
                return;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            StreamReader stream = new StreamReader(Paths.SettingsPath);
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

            StreamWriter stream = new StreamWriter(Paths.SettingsPath);

            serializer.Serialize(stream, settings);
            stream.Close();
        }

    }
}
