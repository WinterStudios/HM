using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HM_App.API.Properties
{
    public static class Paths
    {
        public static string LocalApplicationData
        {
            get
            {
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/HM/"))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/HM/");
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/HM/";
            }
        }
    }
}
