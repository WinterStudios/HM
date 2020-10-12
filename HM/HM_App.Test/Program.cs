
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
                            string assemblyLocation = AppDomain.CurrentDomain.BaseDirectory + "HM_App.dll";
                            Console.WriteLine(assemblyLocation);
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
