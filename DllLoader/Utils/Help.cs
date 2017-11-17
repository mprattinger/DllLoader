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
            var versionString = string.IsNullOrEmpty(Product.Version) ? string.Empty : $" ({Product.Version})";
            Reporter.Output.WriteLine(Product.LongName + versionString);
        }

        public static void PrintUsage(PluginLoader pLoader)
        {
            var sb = new StringBuilder();
            sb.Append($@"{Texts.Usage}: flintc [module] [arguments] [command-options]");
        }
    }
}
