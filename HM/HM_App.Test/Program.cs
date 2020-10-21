
using HM_App.API;
using System;
using System.Runtime.InteropServices;
using System.Xml;

namespace HM_App.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { "-setVersion", "-development" };
            if (args.Length > 0)
                switch (args[0])
                {
                    case "-setVersion":
                        SetVersion(args[1]);
                        break;
                    case "-output":
                        switch (args[1])
                        {
                            case "-versionToGitHub":
                                string assemblyLocation = "D:\\a\\HM\\HM\\HM\\HM_App\\bin\\Debug\\netcoreapp3.1\\HM_App.dll";
                                string version = System.Diagnostics.FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
                                Console.WriteLine(string.Format("v{0}", API.SemVersion.GetVersionFromAssembly(version)));
                                break;
                            case "-versionToGitHubFinal":

                                break;
                        }
                        break;
                }
            //test.dsa();
            Console.WriteLine();
        }
        private static void SetVersion(string arg_1)
        {

            XmlDocument exe_XmlDoc = new XmlDocument();
            XmlDocument api_XmlDoc = new XmlDocument();
            string exeProjectFilePath = System.IO.Directory.GetCurrentDirectory() + "..\\..\\..\\..\\HM_App\\HM_App.csproj";
            string apiProjectFilePath = System.IO.Directory.GetCurrentDirectory() + "..\\..\\..\\..\\HM_App.API\\HM_App.API.csproj";

            exe_XmlDoc.Load(exeProjectFilePath);
            api_XmlDoc.Load(apiProjectFilePath);
            XmlNamespaceManager mgr = new XmlNamespaceManager(exe_XmlDoc.NameTable);
            XmlNamespaceManager api = new XmlNamespaceManager(api_XmlDoc.NameTable);

            mgr.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");
            api.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");

            XmlNode exe_assembly = exe_XmlDoc.ChildNodes[0].ChildNodes[0].SelectSingleNode("AssemblyVersion");
            XmlNode exe_file = exe_XmlDoc.ChildNodes[0].ChildNodes[0].SelectSingleNode("FileVersion");
            XmlNode api_assembly = api_XmlDoc.ChildNodes[0].ChildNodes[0].SelectSingleNode("AssemblyVersion");
            XmlNode api_file = api_XmlDoc.ChildNodes[0].ChildNodes[0].SelectSingleNode("FileVersion");

            SemVersion appVersion = SemVersion.GetVersionFromAssembly(exe_assembly.InnerText);

            if (arg_1 == "-preview")
            {
                if (appVersion.Patch.Release != SemVersion.PATCH.PreRelease.preview)
                {
                    appVersion.Patch.Release = SemVersion.PATCH.PreRelease.preview;
                    appVersion.Patch.Revision = 1;
                }
                else
                    appVersion.Patch.Revision++;
            }
            if(arg_1 == "-final")
                if (appVersion.Patch.Release != SemVersion.PATCH.PreRelease.r)
                {
                    appVersion.Patch.Release = SemVersion.PATCH.PreRelease.r;
                    appVersion.Patch.Revision = 1;
                }
                else
                    appVersion.Patch.Revision++;
            if (arg_1 == "-development")
                if (appVersion.Patch.Release != SemVersion.PATCH.PreRelease.development)
                {
                    appVersion.Patch.Release = SemVersion.PATCH.PreRelease.development;
                    appVersion.Patch.Revision = 1;
                }
                else
                    appVersion.Patch.Revision++;
            exe_assembly.InnerText = exe_file.InnerText = api_assembly.InnerText = api_file.InnerText = appVersion.ToAssemblyFormat();
            exe_XmlDoc.Save(exeProjectFilePath);
            api_XmlDoc.Save(apiProjectFilePath);
            Console.WriteLine(appVersion.ToString());
        }
    }
}
