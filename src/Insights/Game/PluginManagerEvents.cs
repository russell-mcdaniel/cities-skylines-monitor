using ColossalFramework.Plugins;
using Insights.Logging;

namespace Insights.Game
{
    public class PluginManagerEvents
    {
        protected InsightsLogger<PluginManagerEvents> InsightsLogger { get; } = new InsightsLogger<PluginManagerEvents>();

        public void Subscribe()
        {
            var manager = PluginManager.instance;

            manager.eventPluginsChanged += PluginManagerPluginsChanged;
            manager.eventPluginsStateChanged += PluginManagerPluginsStateChanged;

            PluginManager.eventLogMessage += PluginManagerEventLogMessage;
        }

        public void Unsubscribe()
        {
            var manager = PluginManager.instance;

            manager.eventPluginsChanged -= PluginManagerPluginsChanged;
            manager.eventPluginsStateChanged -= PluginManagerPluginsStateChanged;

            PluginManager.eventLogMessage -= PluginManagerEventLogMessage;
        }

        private void PluginManagerEventLogMessage(PluginManager.MessageType type, string message)
        {
            InsightsLogger.Log($"PluginManagerEventLogMessage MessageType: {type} | Message: {message}");
        }

        private void PluginManagerPluginsChanged()
        {
            InsightsLogger.Log("PluginManagerPluginsChanged");

            ListPlugins("PluginManagerPluginsChanged");
        }

        private void PluginManagerPluginsStateChanged()
        {
            InsightsLogger.Log("PluginManagerPluginsStateChanged");

            ListPlugins("PluginManagerPluginsStateChanged");
        }

        private void ListPlugins(string eventName)
        {
            var manager = PluginManager.instance;

            foreach (var info in manager.GetPluginsInfo())
            {
                InsightsLogger.Log($"{eventName} ListPlugins Name: {info.name} | IsBuiltIn: {info.isBuiltin} | IsEnabled: {info.isEnabled} | Assemblies ({info.assemblyCount}): {info.assembliesString}");
            }
        }
    }
}
