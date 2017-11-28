using DllLoader.Loader;
using DllLoader.Utils;
using DllLoader.Utils.Localization;
using System;
using System.Reflection;

namespace DllLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = new Main();
            main.Init();
            main.ProcessArgs(args);
        }

        public static int ProcessArgs(string[] args)
        {
            var lastArg = 0;
            for (; lastArg < args.Length; lastArg++)
            {
                if (IsArg(args[lastArg], "h", "help"))
                {

                    return 0;
                }
                else if (IsArg(args[lastArg], "version"))
                {
                    printVersion();
                    return 0;
                }
                else if (IsArg(args[lastArg], "info"))
                {
                    printInfo();
                    return 0;
                }
            }
            return 1;
        }

        private static void printVersion()
        {
            Console.WriteLine(Product.Version);
        }

        private static void printInfo()
        {
            Console.WriteLine(Product.Version);
        }

        private static bool IsArg(string candidate, string longName)
        {
            return IsArg(candidate, shortName: null, longName: longName);
        }

        private static bool IsArg(string candidate, string shortName, string longName)
        {
            return (shortName != null && candidate.Equals("-" + shortName)) || (longName != null && candidate.Equals("--" + longName));
        }
    }
}
