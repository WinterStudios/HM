using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HM_App.API.GitHub
{
    public class Release
    {
        [JsonPropertyName("url")]
        public string URL { get; set; }
        [JsonPropertyName("id")]
        public long ID { get; set; }
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
