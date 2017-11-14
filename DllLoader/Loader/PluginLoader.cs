using DllLoader.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DllLoader.Loader
{
    public class PluginLoader
    {
        public List<string> PluginDlls { get; set; }
        public List<Assembly> Assemblies { get; set; }
        public List<PluginStructure> Plugins { get; set; }

        public PluginLoader()
        {
            Assemblies = new List<Assembly>();
            Plugins = new List<PluginStructure>();
        }

        public void Init()
        {
            var pluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "plugins");
            PluginDlls = Directory.EnumerateFiles(pluginsPath).ToList();

            PluginDlls.ForEach(p => {
                using(var ar = new AssemblyResolver(p))
                {
                    Assemblies.Add(ar.Assembly);
                    var plugin = Activator.CreateInstance(ar.Assembly.GetTypes().First()) as IPlugin; //Class-Type anhand des Namens ermitteln!!!!
                    var inf = plugin.GetPluginInfo();
                    Plugins.Add(new PluginStructure { UniqueName = inf.UniquePluginName, Info = inf, Plugin = plugin });
                }
            });
        }

        public void Execute()
        {
            Assemblies.ForEach(a => {
                var plugin = Activator.CreateInstance(a.GetTypes().First()) as IPlugin;
                plugin.Run();
            });
        }
    }
}
