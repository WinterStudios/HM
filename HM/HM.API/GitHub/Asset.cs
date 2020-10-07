using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.API.GitHub
{
    public class Asset
    {
        public string URL { get; set; }
        public int ID { get; set; }
        public string Node_ID { get; set; }
        public string Name { get; set; }
        //public object label { get; set; }
        //public Uploader uploader { get; set; }
        //public string content_type { get; set; }
        //public string state { get; set; }
        public int Size { get; set; }
        //public int download_count { get; set; }
        //public DateTime created_at { get; set; }
        //public DateTime updated_at { get; set; }
        public string DonwloadUrl { get; set; }

        /// <summary>
        /// Create a Asset
        /// </summary>
        /// <param name="url">Url</param>
        /// <param name="id">Id of the Asset</param>
        /// <param name="node_id"></param>
        /// <param name="name">Name of the Asset</param>
        /// <param name="size">Asset Size (in bytes)</param>
        /// <param name="browser_download_url">Url that can be access from a brower, that can download it</param>
        public Asset(string url, int id, string node_id, string name, int size, string browser_download_url)
        {
            URL = url;
            ID = id;
            Node_ID = node_id;
            Name = name;
            Size = size;
            DonwloadUrl = browser_download_url;
        }
    }
}
