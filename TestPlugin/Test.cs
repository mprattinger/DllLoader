using DllLoader.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPlugin
{
    public class Test : IPlugin
    {
        public PluginInfo GetPluginInfo()
        {
            return new PluginInfo
            {
                UniquePluginName = "Test",
                LongName = "test",
                ShortName = "t"
            };
        }

        public string Run()
        {
            Console.WriteLine("Hello From Test");
            return "";
        }
    }
}
