using System;
using ColossalFramework.Plugins;
using UnityEngine;

namespace Insights.Logging
{
    internal static class InternalLogger
    {
        public static void Log(string message)
        {
            var messageText = $"{DateTimeOffset.Now:O} InsightsMod: {message}";

            DebugOutputPanel.AddMessage(PluginManager.MessageType.Message, messageText);

            Debug.Log(messageText);
        }

        public static void Log(string message, Exception ex)
        {
            var messageText = $"{DateTimeOffset.Now:O} InsightsMod: {message}";

            DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, messageText);

            Debug.LogError(messageText);
            Debug.LogException(ex);
        }
    }
}
