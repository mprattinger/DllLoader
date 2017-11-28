using DllLoader.Loader;
using DllLoader.Utils;
using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace DllLoader
{
    public class Main
    {
        public PluginLoader PluginLoader { get; set; }
               
        public Main()
        {
            PluginLoader = new PluginLoader();
        }

        public void Init()
        {
            try
            {
                PluginLoader.Init();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public int ProcessArgs(string[] args)
        {
            var lastArg = 0;
            for (; lastArg < args.Length; lastArg++)
            {
                if (IsArg(args[lastArg], "h", "help"))
                {
                    printHelp();
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
                else
                {
                    if (PluginLoader.IsPlugin(args[lastArg]))
                    {
                        //Execute Plugin
                        var command = args[lastArg];
                        var newFirst = lastArg + 1;
                        var a = args.Skip(newFirst).Take(args.Length - newFirst).ToArray();
                        PluginLoader.ExecutePlugin(command, a);
                        return 0;
                    }
                }
                printHelp();
            }
            return 1;
        }

        private void printVersion()
        {
            Reporter.Output.WriteLine(Product.Version);
        }

        private void printInfo()
        {
            Help.PrintVersionHeader();
            Reporter.Output.WriteLine();
            Reporter.Output.WriteLine("Product Information:");
            Reporter.Output.WriteLine($" Version:            {Product.Version}");
            Reporter.Output.WriteLine();
            Reporter.Output.WriteLine("Runtime Environment:");
            Reporter.Output.WriteLine($" OS Name:     {Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment.OperatingSystem}");
            Reporter.Output.WriteLine($" OS Version:  {Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment.OperatingSystemVersion}");
            Reporter.Output.WriteLine($" OS Platform: {Microsoft.DotNet.PlatformAbstractions.RuntimeEnvironment.OperatingSystemPlatform}");
            Reporter.Output.WriteLine($" Base Path:   {ApplicationEnvironment.ApplicationBasePath}");
        }

        private void printHelp()
        {
            Help.PrintVersionHeader();
            Help.PrintUsage(PluginLoader);
        }

        private bool IsArg(string candidate, string longName)
        {
            return IsArg(candidate, shortName: null, longName: longName);
        }

        private bool IsArg(string candidate, string shortName, string longName)
        {
            return (shortName != null && candidate.Equals("-" + shortName)) || (longName != null && candidate.Equals("--" + longName));
        }
    }
}
