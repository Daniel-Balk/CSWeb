using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        }
        public static void Start()
        {
            Thread t = new(new ThreadStart(() =>
            {
                Process p = new()
                {
                    StartInfo = new()
                    {
                        FileName = Constant.CSWebFile,
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
    }
}
