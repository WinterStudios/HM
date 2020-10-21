using System;
using System.IO;
using System.Runtime.ExceptionServices;

namespace Update
{
    class Program
    {
        static void Main(string[] args)
        {
            //args = new string[] { "-update", "ds", "C:\\Users\\hugo_\\source\\repos\\WinterStudios\\HM\\HM\\HM_App\\bin\\Debug\\netcoreapp3.1\\" };
            Console.WriteLine(args[0]);
            if (args.Length > 0 && args[0].Contains("-update"))
            {
                UpdateApp(args[1], args[2]);
                Console.WriteLine(args[0]);
                Console.WriteLine(args[1]);
            }
            Console.ReadLine();
        }
        static void UpdateApp(string location, string descompressTo)
        {
            try
            {
                string s = descompressTo.Remove(descompressTo.LastIndexOf('\\'));
                s = s.Remove(s.LastIndexOf('\\') + 1);

                System.IO.Compression.ZipFile.ExtractToDirectory(location + "Release.zip", s,true);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.FileName = descompressTo + "HM_App.exe";
                startInfo.Arguments = "-updated";
                process.StartInfo = startInfo;
                process.Start();
                
            }
            catch (Exception ex)
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ex.txt", ex.Message);
            }    
        }
    }
}
