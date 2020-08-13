using System;
using UnityEngine;
using ColossalFramework.Plugins;

namespace Insights.Diagnostics
{
    internal static class Tracer
    {
        private static string ModName = "Insights";
        private static string TimestampFormat = "yyyy-MM-dd HH:mm:ss.ffffff";

        // TODO: Implement target filters (game log file, game output panel, telemetry file?).
        // TODO: Implement trace levels (info, warn, error, etc.).
        // TODO: Expand message template to include method, method, and data.
        // TODO: Migrate to universal logger for game telemetry and program logging.
        internal static void Trace(string message)
        {
            var timestamp = DateTime.Now.ToString(TimestampFormat);
            var traceText = $"{timestamp} {ModName}: {message}";

            // Log to game log file.
            try
            {
                Debug.Log(traceText);
            }
            catch
            {
            }

            // Log to in-game debug panel.
            try
            {
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, traceText);
            }
            catch
            {
            }
        }
    }
}
