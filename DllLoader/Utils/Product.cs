using System.Reflection;

namespace DllLoader.Utils
{
    public class Product
    {
        //public static string LongName => LocalizableStrings.DotNetCommandLineTools;
        public static readonly string Version = GetProductVersion();

        private static string GetProductVersion()
        {
            var attr = typeof(Product)
                .GetTypeInfo()
                .Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            return attr?.InformationalVersion;
        }
    }
}
