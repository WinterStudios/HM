using System;
using System.IO;

namespace Update
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].Contains("-update"))
                UpdateApp(args[1]);
            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);
            Console.ReadLine();
        }
        static void UpdateApp(string location)
        {
            Console.ReadLine();
        }
    }
}
