
using System;

namespace HM_App.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            switch (args[0])
            {
                case "-output":
                    switch (args[1])
                    {
                        case "-versionToGitHub":
                            string assemblyLocation = "C:\\Users\\hugo_\\source\\repos\\WinterStudios\\HM\\HM\\HM_App\\bin\\Debug\\netcoreapp3.1\\HM_App.dll"; //"D:\\a\\HM\\HM\\HM\\HM_App\\bin\\Debug\\netcoreapp3.1\\HM_App.dll";
                            string version = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
                            Console.WriteLine(string.Format("v{0}", API.SemVersion.GetVersionFromAssembly(version)));
                            break;
                    }
                    break;

            }
            Console.WriteLine();
        }
    }
}
