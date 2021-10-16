using CwMan;
using System;

namespace cwtl
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Logger.Error("Cannot contuinue without arguments");
                return;
            }
            Constant.GoIn();
            switch (args[0].ToLower())
            {
                case "start":
                    Actions.Start();
                    break;
                case "stop":
                    Actions.Abort();
                    break;
                case "install":
                    Constant.GoOut();
                    Actions.Setup();
                    break;
                default:
                    Logger.Error("Unknown argument: " + args[0]);
                    break;
            }



            Constant.GoOut();
        }
    }
}
