using DllLoader.Loader;
using DllLoader.Utils.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DllLoader.Utils
{
    class Help
    {
        public static void PrintVersionHeader()
        {
            Reporter.Output.WriteLine();
            var versionString = string.IsNullOrEmpty(Product.Version) ? string.Empty : $" ({Product.Version})";
            Reporter.Output.WriteLine(Product.LongName + versionString);
        }

        public static void PrintUsage(PluginLoader pLoader)
        {
            var sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine($@"{Texts.Usage}: flintc [tool-options] [plugin] [plugin-options]");
            sb.AppendLine("");
            sb.AppendLine($"{Texts.Plugins}:");
            pLoader.Plugins.ForEach(p => {
                sb.AppendLine($"\t{p.Info.PluginCommand}\t{p.UniqueName}");
            });
            sb.AppendLine("");
            sb.AppendLine($"{Texts.ToolOptions}:");
            sb.AppendLine($"\t-h|--help\t{Texts.HelpText}");
            sb.AppendLine($"\t--version\t{Texts.VersionText}");
            sb.AppendLine("");
            Reporter.Output.Write(sb.ToString());
        }
    }
}
