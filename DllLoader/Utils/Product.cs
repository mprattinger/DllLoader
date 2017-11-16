using System.Reflection;

namespace DllLoader.Utils
{
    public class Product
    {
        public static string LongName => "Flint Commandline Tools";
        public static readonly string Version = getProductVersion();
        public static readonly string Name = getProductName();

        private static string getProductVersion()
        {
            var attr = typeof(Product)
                .GetTypeInfo()
                .Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            return attr?.InformationalVersion;
        }

        private static string getProductName()
        {
            var attr = typeof(Product)
             .GetTypeInfo()
             .Assembly
             .GetName();
            return attr?.Name;
        }
    }
}
