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

            manager.eventPluginsChanged += PluginsChanged;
            manager.eventPluginsStateChanged += PluginsStateChanged;

            PluginManager.eventLogMessage += EventLogMessage;
        }

        public void Unsubscribe()
        {
            var manager = PluginManager.instance;

            manager.eventPluginsChanged -= PluginsChanged;
            manager.eventPluginsStateChanged -= PluginsStateChanged;

            PluginManager.eventLogMessage -= EventLogMessage;
        }

        private void EventLogMessage(PluginManager.MessageType type, string message)
        {
            InsightsLogger.Log($"EventLogMessage > MessageType: {type} | Message: {message}");
        }

        private void PluginsChanged()
        {
            InsightsLogger.Log("PluginsChanged");

            ListPlugins("PluginsChanged");
        }

        private void PluginsStateChanged()
        {
            InsightsLogger.Log("PluginsStateChanged");

            ListPlugins("PluginsStateChanged");
        }

        private void ListPlugins(string eventName)
        {
            var manager = PluginManager.instance;

            foreach (var info in manager.GetPluginsInfo())
            {
                InsightsLogger.Log($"{eventName} ListPlugins > Name: {info.name} | IsBuiltIn: {info.isBuiltin} | IsEnabled: {info.isEnabled} | Assemblies ({info.assemblyCount}): {info.assembliesString}");
            }
        }
    }
}
