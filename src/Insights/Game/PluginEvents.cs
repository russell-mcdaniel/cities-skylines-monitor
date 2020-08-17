using ColossalFramework.Plugins;
using Insights.Logging;

namespace Insights.Game
{
    public class PluginEvents
    {
        protected InsightsLogger<PluginEvents> InsightsLogger { get; } = new InsightsLogger<PluginEvents>();

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

            ListPlugins();
        }

        private void PluginManagerPluginsStateChanged()
        {
            InsightsLogger.Log("PluginManagerPluginsStateChanged");

            ListPlugins();
        }

        private void ListPlugins()
        {
            var manager = PluginManager.instance;

            foreach (var info in manager.GetPluginsInfo())
            {
                InsightsLogger.Log($"PluginManagerPluginsChanged Name: {info.name} | IsBuiltIn: {info.isBuiltin} | IsEnabled: {info.isEnabled}");
            }
        }
    }
}
