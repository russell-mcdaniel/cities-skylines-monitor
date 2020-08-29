using ColossalFramework.Plugins;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class PluginManagerHandler
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(PluginManagerHandler));

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
            Logger.LogDebug($"EventLogMessage > MessageType: {type} | Message: {message}");
        }

        private void PluginsChanged()
        {
            Logger.LogDebug("PluginsChanged");

            ListPlugins("PluginsChanged");
        }

        private void PluginsStateChanged()
        {
            Logger.LogDebug("PluginsStateChanged");

            ListPlugins("PluginsStateChanged");
        }

        private void ListPlugins(string eventName)
        {
            var manager = PluginManager.instance;

            foreach (var info in manager.GetPluginsInfo())
            {
                Logger.LogDebug($"{eventName} ListPlugins > Name: {info.name} | IsBuiltIn: {info.isBuiltin} | IsEnabled: {info.isEnabled} | Assemblies ({info.assemblyCount}): {info.assembliesString}");
            }
        }
    }
}
