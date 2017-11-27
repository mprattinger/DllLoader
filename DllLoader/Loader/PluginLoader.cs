using FlintCli.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DllLoader.Loader
{
    public class PluginLoader
    {
        public List<PluginStructure> Plugins { get; set; }

        public PluginLoader()
        {
        }

        public void Init()
        {
            var dlls = getDllFiles();
            var assem = resolveAssemblies(dlls);
            Plugins = initPlugins(assem);
        }

        public bool IsPlugin(string candidate)
        {
            return Plugins.Any(p => (p.Info.ShortName == $"-{candidate}" || p.Info.LongName == $"--{candidate}"));
        }

        public void Execute()
        {
            Plugins.ForEach(p =>
            {
                p.Plugin.Run();
            });
        }

        private List<string> getDllFiles()
        {
            var pluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "plugins");
            return Directory.EnumerateFiles(pluginsPath).ToList();
        }

        private List<Assembly> resolveAssemblies(List<string> dlls)
        {
            var ret = new List<Assembly>();
            dlls.ForEach(p =>
            {
                using (var ar = new AssemblyResolver(p))
                {
                    ret.Add(ar.Assembly);
                }
            });
            return ret;
        }

        private List<PluginStructure> initPlugins(List<Assembly> assemblies)
        {
            var ret = new List<PluginStructure>();
            assemblies.ForEach(a => {
                try
                {
                    getType(a);
                    var type = getType(a);
                    var plugin = Activator.CreateInstance(type) as IPlugin; //Class-Type anhand des Namens ermitteln!!!!
                    var inf = plugin.GetPluginInfo();
                    ret.Add(new PluginStructure { UniqueName = inf.UniquePluginName, Info = inf, Plugin = plugin });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error initializing plugin {a.FullName}, {ex.Message}");
                }
            });
            return ret;
        }

        private Type getType(Assembly assembly)
        {
            var name = assembly.ManifestModule.Name;
            var className = name.Substring(0, name.IndexOf("Plugin"));
            return assembly.GetTypes().Where(t => t.Name.Contains(className)).FirstOrDefault();
        }
    }
}
