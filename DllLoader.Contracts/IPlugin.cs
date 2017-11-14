
namespace DllLoader.Contracts
{
    public interface IPlugin
    {
        PluginInfo GetPluginInfo();
        string Run();
    }
}
