using System;
using System.IO;

namespace CwMan
{
    public struct Constant
    {
        public static string ApplicationPath
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform switch
                {
                    PlatformID.Win32S => Ctl("C:/csweb/"),
                    PlatformID.Win32Windows => Ctl("C:/csweb/"),
                    PlatformID.Win32NT => Ctl("C:/csweb/"),
                    PlatformID.WinCE => Ctl("C:/csweb/"),
                    PlatformID.Unix => Ctl("/etc/csweb/"),
                    PlatformID.Xbox => Ctl("C:/csweb/"),
                    PlatformID.MacOSX => Ctl("/etc/csweb/"),
                    PlatformID.Other => Ctl("/etc/csweb/"),
                    _ => Ctl("/etc/csweb/"),
                };
            }
        }

        private static string Ctl(string v)
        {
            if (!Directory.Exists(v))
                Directory.CreateDirectory(v);

            return v;
        }

        public static void GoIn()
        {
            temDir = Environment.CurrentDirectory;
            Environment.CurrentDirectory = ApplicationPath;
        }
        static string temDir = "";
        public static void GoOut()
        {
            Environment.CurrentDirectory = temDir;
        }
    }
}
