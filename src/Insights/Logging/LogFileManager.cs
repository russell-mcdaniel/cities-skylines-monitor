using System;
using System.IO;
using UnityEngine;

namespace Insights.Logging
{
    internal static class LogFileManager
    {
        /// <summary>
        /// Gets a value indicating whether the writer flushes its buffer after every write.
        /// </summary>
        /// <remarks>
        /// This lowers the performance of the logger. Use only for development and debugging.
        /// </remarks>
#if DEBUG
        private const bool LogFileAutoFlush = true;
#else
        private const bool LogFileAutoFlush = false;
#endif

        public static object LogFileSyncRoot { get; } = new object();

        /// <summary>
        /// The time used for the current log file name. It is updated each minute to the whole minute.
        /// </summary>
        public static DateTime LogFileTime { get; set; }

        /// <summary>
        /// The StreamWriter used to write to the log file.
        /// </summary>
        public static StreamWriter LogFileWriter { get; private set; }

        /// <summary>
        /// Creates a log file based on the specified timestamp. If the log file already exists, it will be opened.
        /// </summary>
        /// <param name="timestamp">The timestamp truncated to the target log file interval.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "The log writer lifecycle is managed at the class scope.")]
        public static void CreateLogFile(DateTime timestamp)
        {
            // Set the log file name.
            var logFileName = $"CitiesSkylinesInsights-{timestamp:yyyyMMdd}-{timestamp:HHmm}.log";

            // Set the log file path.
            var logFilePath = Path.Combine(
                Application.dataPath,
                Path.Combine(
                    "Mods/Insights",
                    logFileName));

            // Create directory. No action if the directory already exists.
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

            // Dispose the existing writer. This forces a flush.
            LogFileWriter?.Dispose();

            // Create a new writer. Use append to ensure a log file is not overwritten
            // if a session is restarted within the same rollover period.
            LogFileWriter = File.AppendText(logFilePath);
            LogFileWriter.AutoFlush = LogFileAutoFlush;
        }

        /// <summary>
        /// Reset the log file manager.
        /// </summary>
        /// <remarks>
        /// This method is provided primarily to support hot reloads of mods. The game does
        /// not release resources held by mods when they are unloaded and recreated, so it
        /// is up to the mod to handle this.
        /// 
        /// In this case, the StreamWriter remains open, but when the game reloads the mod,
        /// the log file is still open causing a IOException (sharing violation) when the
        /// mod attempts to reopen the log file.
        /// </remarks>
        public static void Reset()
        {
            // Flush and release the log file.
            LogFileWriter?.Dispose();

            // Ensure that the next log call will initialize the writer.
            LogFileTime = DateTime.MinValue;
        }
    }
}
