using DllLoader.Contracts;
using DllLoader.Loader;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
