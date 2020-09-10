using System;
using Insights.Game.Events;
using Insights.Utilities;

namespace Insights.Logging
{
    /// <summary>
    /// Provides logging services.
    /// </summary>
    public sealed class InsightsLogger
    {
        private const string LoggerErrorMessage = "The logger encountered an error.";

#if DEBUG
        private static LogFileManager _gameLog = new LogFileManager(LogFileType.Game, RolloverInterval.Day);
#else
        private static LogFileManager _gameLog = new LogFileManager(LogFileType.Game, RolloverInterval.Minute);
#endif

        private static LogFileManager _modLog = new LogFileManager(LogFileType.Mod, RolloverInterval.Day);

        private readonly string _loggerTypeName;

        public InsightsLogger(Type callerType)
        {
            if (callerType == null)
                throw new ArgumentNullException(nameof(callerType));

            // Configure the logging context.
            _loggerTypeName = callerType.Name;
        }

        public void LogDebug(string message)
        {
            LogToModLog(message, LogLevel.Debug);
        }

        public void LogInfo(string message)
        {
            LogToModLog(message, LogLevel.Information);
        }

        public void LogError(string message, Exception ex = null)
        {
            LogToModLog(message, LogLevel.Error);
        }

        public void LogEvent(GameEvent @event)
        {
            LogToGameLog(@event);
        }

        public void LogWarn(string message)
        {
            LogToModLog(message, LogLevel.Warning);
        }

        /// <summary>
        /// Logs a message to the game log.
        /// </summary>
        /// <param name="message"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The logger should not throw in order to avoid disrupting the game.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Instance member is more intuitive design. No performance concern.")]
        private void LogToGameLog(GameEvent @event)
        {
            try
            {
                var eventText = XmlHelper.ToXml(@event);

                _gameLog.WriteLine(@event.SessionTime, eventText);
            }
            catch (Exception ex)
            {
                InternalLogger.Log(LoggerErrorMessage, ex);
            }
        }

        /// <summary>
        /// Logs a message to the mod log.
        /// </summary>
        /// <param name="message"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The logger should not throw in order to avoid disrupting the game.")]
        private void LogToModLog(string message, LogLevel level)
        {
            try
            {
                // The time is used for the log entry timestamp and for rollover detection.
                var timestamp = DateTimeOffset.Now;
                var logLevelText = GetLogLevelText(level);

                // Format and log the message.
                var messageText = $"{timestamp:O} [{logLevelText}] {_loggerTypeName} {message}";

                _modLog.WriteLine(timestamp, messageText);
            }
            catch (Exception ex)
            {
                InternalLogger.Log(LoggerErrorMessage, ex);
            }
        }

        /// <summary>
        /// Reset the logger so that it can be reinitialized.
        /// </summary>
        /// <remarks>
        /// This method is provided primarily to support hot reloads of mods.
        /// 
        /// See remarks on the LogFileManager.
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The logger should not throw in order to avoid disrupting the game.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Instance member is more intuitive design. No performance concern.")]
        public void Reset()
        {
            try
            {
                _gameLog.Reset();
            }
            catch (Exception ex)
            {
                InternalLogger.Log(LoggerErrorMessage, ex);
            }

            try
            {
                _modLog.Reset();
            }
            catch (Exception ex)
            {
                InternalLogger.Log(LoggerErrorMessage, ex);
            }
        }

        private static string GetLogLevelText(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return "DBG";

                case LogLevel.Information:
                    return "INF";

                case LogLevel.Warning:
                    return "WRN";

                case LogLevel.Error:
                    return "ERR";

                default:
                    throw new ArgumentOutOfRangeException(nameof(level), $"The level {level} is not recognized.");
            }
        }
    }
}
