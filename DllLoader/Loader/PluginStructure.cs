using DllLoader.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DllLoader.Loader
{
    public struct PluginStructure
    {
        public string UniqueName { get; set; }
        public PluginInfo Info { get; set; }
        public IPlugin Plugin { get; set; }
    }
}
