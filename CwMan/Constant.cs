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
        public static string CSWebFile
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform switch
                {
                    PlatformID.Win32S => "CSWeb.exe",
                    PlatformID.Win32Windows => "CSWeb.exe",
                    PlatformID.Win32NT => "CSWeb.exe",
                    PlatformID.WinCE => "CSWeb.exe",
                    PlatformID.Unix => "CSWeb",
                    PlatformID.Xbox => "CSWeb.exe",
                    PlatformID.MacOSX => "CSWeb",
                    PlatformID.Other => "CSWeb",
                    _ => "CSWeb"
                };
            }
        }        
        public static string CWTLFile
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform switch
                {
                    PlatformID.Win32S => "cwtl.exe",
                    PlatformID.Win32Windows => "cwtl.exe",
                    PlatformID.Win32NT => "cwtl.exe",
                    PlatformID.WinCE => "cwtl.exe",
                    PlatformID.Unix => "cwtl",
                    PlatformID.Xbox => "cwtl.exe",
                    PlatformID.MacOSX => "cwtl",
                    PlatformID.Other => "cwtl",
                    _ => "cwtl"
                };
            }
        }        
        public static string CWEnModFile
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform switch
                {
                    PlatformID.Win32S => "cw-enmod.exe",
                    PlatformID.Win32Windows => "cw-enmod.exe",
                    PlatformID.Win32NT => "cw-enmod.exe",
                    PlatformID.WinCE => "cw-enmod.exe",
                    PlatformID.Unix => "cw-enmod",
                    PlatformID.Xbox => "cw-enmod.exe",
                    PlatformID.MacOSX => "cw-enmod",
                    PlatformID.Other => "cw-enmod",
                    _ => "cw-enmod"
                };
            }
        }        
        public static string CWDisModFile
        {
            get
            {
                var os = Environment.OSVersion;
                return os.Platform switch
                {
                    PlatformID.Win32S => "cw-dismod.exe",
                    PlatformID.Win32Windows => "cw-dismod.exe",
                    PlatformID.Win32NT => "cw-dismod.exe",
                    PlatformID.WinCE => "cw-dismod.exe",
                    PlatformID.Unix => "cw-dismod",
                    PlatformID.Xbox => "cw-dismod.exe",
                    PlatformID.MacOSX => "cw-dismod",
                    PlatformID.Other => "cw-dismod",
                    _ => "cw-dismod"
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
