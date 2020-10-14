using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HM_App.API.GitHub
{
    public class Assets
    {
        [JsonPropertyName ("url")]
        public string URL { get; set; }
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName ("name")]
        public string Name { get; set; }
        
        [JsonPropertyName ("size")]
        public long Size { get; set; }

        [JsonPropertyName ("browser_download_url")]
        public string DownloadUrl { get; set; }
    }
}
