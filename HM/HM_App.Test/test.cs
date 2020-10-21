using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace HM_App.Test
{
    class test
    {
        public static void dsa()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("..\\..\\..\\HM_App.Test.csproj");

            XmlNamespaceManager mgr = new XmlNamespaceManager(xmldoc.NameTable);
            mgr.AddNamespace("x", "http://schemas.microsoft.com/developer/msbuild/2003");
            XmlNode node = xmldoc.ChildNodes[0].ChildNodes[0].ChildNodes[2];
            HM_App.API.SemVersion App_Version = HM_App.API.SemVersion.GetVersionFromAssembly(node.InnerText);
            bool preview = true;
            if(preview)
            {
                App_Version.Patch.Release = API.SemVersion.PATCH.PreRelease.preview;
                App_Version.Patch.Revision = 1;
            }
            node.InnerText = App_Version.ToString();
            xmldoc.Save("..\\..\\..\\HM_App.Test.csproj");
            foreach (XmlNode item in xmldoc.ChildNodes[0].ChildNodes[0].ChildNodes[2])
            {
                string test = item.InnerText.ToString();
                Console.WriteLine(test);
            }

        }
    }
}
