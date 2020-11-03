using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HM_App.API.GitHub
{
    public class Repository
    {
        [JsonPropertyName("id")]
        public long ID { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("owner")]
        public Owner Owner { get; set; }
        public Release[] Releases { get; set; }
    }
}
