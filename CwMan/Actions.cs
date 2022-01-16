using CwMan.Properties;
using Logging.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CwMan
{
    public static class Actions
    {
        public static void Setup()
        {
            InstallDefaultConfiguration();
            string[] files = new string[]
            {
                Constant.CSWebFile,
                Constant.CWDisModFile,
                Constant.CWEnModFile,
                Constant.CWTLFile,
            };
            files.ForEach((f) =>
            {
                Logger.Warn("Installing " + f);
                var nf = Constant.ApplicationPath + f;
                try
                {
                    if (File.Exists(nf))
                        File.Delete(nf);
                    File.Copy(f, nf);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            });
        }
        public static void InstallDefaultConfiguration()
        {
            File.WriteAllBytes("conf.zip", Resources.defaultconfigs);
            try
            {
                if (Directory.Exists(Constant.ApplicationPath))
                    Directory.Delete(Constant.ApplicationPath, true);
            }
            catch (Exception)
            {
                try
                {
                    if (Directory.Exists(Constant.ApplicationPath))
                        Directory.Delete(Constant.ApplicationPath);
                }
                catch (Exception)
                {

                }
            }
            Directory.CreateDirectory(Constant.ApplicationPath);
            ZipFile.ExtractToDirectory("conf.zip", Constant.ApplicationPath);
            File.Delete("conf.zip");
        }
        public static void Start()
        {
            Thread t = new(new ThreadStart(() =>
            {
                Process p = new()
                {
                    StartInfo = new()
                    {
                        FileName = Constant.ApplicationPath + Constant.CSWebFile,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        RedirectStandardError = true
                    },
                };
                p.Start();
            }));
            t.Start();
        }
        public static void Abort()
        {
            Process.GetProcessesByName("CSWeb").ForEach((e) =>
            {
                e.Kill();
            });
        }
        public static void ForEach<T>(this T[] ts, Action<T> e)
        {
            ts.ToList().ForEach(e);
        }
        public static void DisMod(string[] args)
        {
            if (!Directory.Exists("disxom"))
                Directory.CreateDirectory("disxom");
            if (!Directory.Exists("xom"))
                Directory.CreateDirectory("xom");
            foreach (var v in args)
            {
                if (File.Exists(Constant.ApplicationPath + "xom/" + v + ".dll"))
                    File.Move(Constant.ApplicationPath + "xom/" + v + ".dll", Constant.ApplicationPath + "disxom/" + v + ".dll");
                if (File.Exists(Constant.ApplicationPath + "xom/" + v))
                    File.Move(Constant.ApplicationPath + "xom/" + v, Constant.ApplicationPath + "disxom/" + v);
            }
        }
        public static void EnMod(string[] args)
        {
            if (!Directory.Exists("disxom"))
                Directory.CreateDirectory("disxom");
            if (!Directory.Exists("xom"))
                Directory.CreateDirectory("xom");
            foreach (var v in args)
            {
                if (File.Exists(Constant.ApplicationPath + "disxom/" + v + ".dll"))
                    File.Move(Constant.ApplicationPath + "disxom/" + v + ".dll", Constant.ApplicationPath + "xom/" + v + ".dll");
                if (File.Exists(Constant.ApplicationPath + "disxom/" + v))
                    File.Move(Constant.ApplicationPath + "disxom/" + v, Constant.ApplicationPath + "xom/" + v);
            }
        }
    }
}