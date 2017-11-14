using DllLoader.Loader;
using System;

namespace DllLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var pLoader = new PluginLoader();
            pLoader.Init();
            //pLoader.Execute();

            Console.WriteLine("DllLoader v1.0.0-alpha (c) MPrattinger 2017");
            Console.WriteLine("===========================================");
            Console.WriteLine("");
            pLoader.Plugins.ForEach(p => {
                Console.WriteLine($"{p.Info.LongName} / {p.Info.ShortName} => {p.Info.UniquePluginName}");
            });

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
