using cw_unpack.Properties;
using System;
using System.IO;

namespace cw_unpack
{
    class Program
    {
        static void Main(string[] args)
        {
            File.WriteAllBytes("CSWeb", Resources.CSWeb);
            File.WriteAllBytes("cwtl", Resources.cwtl);
            File.WriteAllBytes("cw-enmod", Resources.cw_enmod);
            File.WriteAllBytes("cw-dismod", Resources.cw_dismod);
        }
    }
}