using System;
using System.Collections.Generic;
using System.Text;

namespace HM_App.API.GitHub
{
    public class GitHubExceptions : System.Exception
    {
        public enum StatusCode
        {
            Offline = 0,
            TokenIncorrect = 2,
            OK = 100
        }
    }
}
