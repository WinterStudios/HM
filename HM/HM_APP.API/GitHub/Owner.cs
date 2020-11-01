using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HM_App.API.GitHub
{
    public class Owner
    {
        [JsonPropertyName("login")]
        public string Name { get; set; }
    }
}
