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
            pLoader.Execute();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
