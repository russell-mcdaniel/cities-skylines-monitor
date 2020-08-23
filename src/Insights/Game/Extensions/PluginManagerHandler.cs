using ColossalFramework.Plugins;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class PluginManagerHandler
    {
        protected InsightsLogger<PluginManagerHandler> Logger { get; } = new InsightsLogger<PluginManagerHandler>();

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
            Logger.Log($"EventLogMessage > MessageType: {type} | Message: {message}");
        }

        private void PluginsChanged()
        {
            Logger.Log("PluginsChanged");

            ListPlugins("PluginsChanged");
        }

        private void PluginsStateChanged()
        {
            Logger.Log("PluginsStateChanged");

            ListPlugins("PluginsStateChanged");
        }

        private void ListPlugins(string eventName)
        {
            var manager = PluginManager.instance;

            foreach (var info in manager.GetPluginsInfo())
            {
                Logger.Log($"{eventName} ListPlugins > Name: {info.name} | IsBuiltIn: {info.isBuiltin} | IsEnabled: {info.isEnabled} | Assemblies ({info.assemblyCount}): {info.assembliesString}");
            }
        }
    }
}
