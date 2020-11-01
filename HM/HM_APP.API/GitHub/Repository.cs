using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HM_App.API.GitHub
{
    public class Repository
    {
        public long ID { get; set; }
        public string Name { get; set; }
    }
}
