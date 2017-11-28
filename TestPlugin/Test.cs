using FlintCli.Contracts;
using System;

namespace TestPlugin
{
    public class Test : IPlugin
    {
        public string Execute(string[] args)
        {
            Console.WriteLine("Hello From Test");
            Console.WriteLine("\tFolgende Argumente wurden übergeben:");
            foreach (var arg in args)
            {
                Console.WriteLine($"\t\t{arg}");
            }
            return "";
        }

        public PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                UniquePluginName = "Test",
                PluginCommand = "test"
            };
        }
    }
}
