using FlintCli.Contracts;

namespace DllLoader.Loader
{
    public class PluginStructure
    {
        public string UniqueName { get; set; }
        public PluginInfo Info { get; set; }
        public IPlugin Plugin { get; set; }
    }
}
