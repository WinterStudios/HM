using HM_App.API.GitHub;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace HM_App.API
{
    public class SemVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public PATCH Patch { get; set; }
        private bool IsInvalid { get; set; }

        public class PATCH
        {
            public enum PreRelease
            {
                alpha = 1,
                beta = 2,
                preview = 3,
                rc = 4,
                r = 5
            }
            public PreRelease? Release { get; set; }
            public int? Revision { get; set; }

            public PATCH() { }

            public PATCH(PreRelease? release, int? revision)
            {
                Release = release;
                if (revision != null)
                    Revision = revision;
                else
                    Revision = null;
            }

            public PATCH GetFinal()
            {
                return GetFinal(null);
            }
            public PATCH GetFinal(int? revision)
            {
                return new PATCH(PreRelease.r, revision);
            }


            public static PATCH Parse(string patch)
            {
                PATCH _patch = new PATCH();
                if (patch.Any(char.IsDigit))
                {
                    if (patch[0].ToString().IndexOfAny(new char[] { '1', '2', '3', '4', '5' }) >= 0 && patch.Length > 1)
                    {
                        string[] split = new string[] { patch[0].ToString(), patch.Substring(1) };
                        PreRelease preRelease = (PreRelease)int.Parse(split[0]);
                        _patch.Release = preRelease;
                        _patch.Revision = int.Parse(split[1]);
                    }
                    if (patch[0].ToString().IndexOfAny(new char[] { '1', '2', '3', '4', '5' }) >= 0 && patch.Length == 1)
                    {
                        PreRelease preRelease = (PreRelease)int.Parse(patch[0].ToString());
                        _patch.Release = preRelease;
                        _patch.Revision = null;
                    }
                }

                return _patch;
            }

            public override string ToString()
            {
                if (Revision != null && Release != null)
                    return string.Format("-{0}.{1}", Enum.GetName(typeof(PreRelease), Release), Revision);
                if (Release != null)
                    return string.Format("-{0}", Enum.GetName(typeof(PreRelease), Release));
                else
                    return string.Empty;
            }
        }

        public SemVersion() => new SemVersion(0, 0, 0);

        public SemVersion(int major, int minor, int build) => new SemVersion(major, minor, build, null);

        public SemVersion(int major, int minor, int build, PATCH patch)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Patch = patch;
        }

        

        public static SemVersion GetVersionFromAssembly(string version)
        {
            SemVersion semSystem = new SemVersion();
            string[] versions = version.Split('.');
            semSystem.Major = int.Parse(versions[0]);
            semSystem.Minor = int.Parse(versions[1]);
            semSystem.Build = int.Parse(versions[2]);
            semSystem.Patch = PATCH.Parse(versions[3]);

            return semSystem;
        }

        public static SemVersion GetVersionFromGitHub(string version) => SemVersion.Parse(version.Replace("v",""));



        public static bool Compare(SemVersion v1, SemVersion v2)
        {
            return false;
        }

        public static SemVersion Parse(string semVersion)
        {
            if (!semVersion.Contains('.') || semVersion.Where(x => x == '.').ToArray().Length < 2)
                return Invalid();

            string[] number = semVersion.Split('.', 3);

            if (!number[0].Any(char.IsDigit) || !number[1].Any(char.IsDigit) ||  !char.IsDigit(number[2][0]))
                return SemVersion.Invalid();

            SemVersion version = new SemVersion();
            version.Major = int.Parse(number[0]);
            version.Minor = int.Parse(number[1]);
            version.Patch = PATCH.Parse("0");
            if (number[2].Contains('-'))
            {
                string[] build = number[2].Split('-');
                version.Build = int.Parse(build[0]);
                string[] patch = new string[] { };
                patch = build[1].Split('.');
                if (Enum.GetNames(typeof(PATCH.PreRelease)).Contains(patch[0]))
                    version.Patch.Release = (PATCH.PreRelease)Enum.Parse(typeof(PATCH.PreRelease), patch[0]);
                if (build[1].Contains('.'))
                {
                    version.Patch.Revision = int.Parse(patch[1]);
                }
            }
            else
                version.Build = int.Parse(number[2]);
            return version;
        }

        public static SemVersion Invalid() => new SemVersion { IsInvalid = true };

        #region Override


        public override string ToString()
        {
            if (IsInvalid)
                return "#.#.#.#-INVALID";
            return string.Format("{0}.{1}.{2}{3}", Major, Minor, Build, Patch.ToString());
        }

        #endregion

    }
}
