using HM_App.API.GitHub;
using HM_App.API.Plugins;
using HM_App.API.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;


namespace HM_App.Plugins
{
    public class PluginSystem
    {

        public static List<IPlugin> PluginsCollections { get; set; }

        public static int PluginsCounts { get; set; }

        public static List<Plugin> Plugins { get; set; }

        public static void Initialize()
        {
            Plugins = LoadPluginLibary();
        }

        public static void AddPlugin(Repository repository) 
        {
            Plugin plugin = new Plugin();
            plugin.Repository = repository;
            plugin.Name = repository.Name;
            plugin.Releases = GitHubClient.GetReleases(repository);


            if (Plugins == null)
                Plugins = new List<Plugin>();
            Plugins.Add(plugin);
            SavePluginLibary();
        }

        private static List<Plugin> LoadPluginLibary()
        {
            if (!File.Exists(Paths.LocalApplicationDataPlugin))
                return new List<Plugin>();

            List<Plugin> plugins = new List<Plugin>();
            string json = File.ReadAllText(Paths.LocalApplicationDataPlugin);
            plugins = JsonSerializer.Deserialize<Plugin[]>(json).ToList();

            return plugins;
        }

        private static void SavePluginLibary()
        {
            string jsonString = JsonSerializer.Serialize<Plugin[]>(Plugins.ToArray(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Paths.LocalApplicationDataPlugin, jsonString);
        }

    }
}
