using System;
using ColossalFramework.Plugins;
using UnityEngine;

namespace Insights.Logging
{
    // TODO: Log levels (info, warn, error, etc.).
    // TODO: Log targets (Unity game log file, in-game debug output panel).
    // TODO: Include mod name in messages since the log targets are not mod-specific.

    internal static class InternalLogger
    {
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void Log(string message, Exception ex)
        {
            Debug.Log(message);
            Debug.LogException(ex);
        }

        //private static void LogScratch()
        //{
        //    // NOTE: This is scratch code to show how to use the in-game debug output panel.
        //    DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, "messageText");
        //}
    }
}
