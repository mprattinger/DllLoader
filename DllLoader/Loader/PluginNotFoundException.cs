using System;
using System.Collections.Generic;
using System.Text;

namespace DllLoader.Loader
{
    public class PluginNotFoundException : Exception
    {
        public PluginNotFoundException(string pluginName, Exception inner) : base($"Plugin with name {pluginName} not found!", inner)
        {
           
        }

        public PluginNotFoundException(string pluginName) : this(pluginName, null)
        {

        }
    }
}
