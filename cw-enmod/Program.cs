using CwMan;
using System.Diagnostics;

namespace cw_enmod
{
    class Program
    {
        static void Main(string[] args)
        {
            Constant.GoIn();
            var i = Process.GetProcessesByName("CSWeb").Length;
            Actions.Abort();
            Actions.EnMod(args);
            if (i != 0)
                Actions.Start();
            Constant.GoOut();
        }
    }
}
