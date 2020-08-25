using System;
using System.IO;
using Insights.Utilities;
using UnityEngine;

namespace Insights.Logging
{
    /// <summary>
    /// Provides file services for the logger.
    /// </summary>
    /// <remarks>
    /// Performance relies on the built-in buffering of the StreamWriter.
    /// 
    /// Thready safety is achieved through explicit locking code. The initial implementation
    /// used TextWriter.Synchronized() to create a thread-safe version of the StreamWriter,
    /// but the remaining buffer contents were never flushed on Dispose().
    /// </remarks>
    public class LogFileManager
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

        private string _fileName;

        private object _syncRoot { get; } = new object();

        private long _timestampResolution { get; }

        /// <summary>
        /// The time used for the log file name. It is updated each interval to the whole interval value.
        /// </summary>
        private DateTimeOffset _timestampStep;

        /// <summary>
        /// The StreamWriter used to write to the log file.
        /// </summary>
        private StreamWriter _writer;

        public LogFileManager(string fileName, RolloverInterval interval)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));

            _fileName = fileName;
            _timestampResolution = GetTimestampInterval(interval);
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
        /// the log file is still open causing an IOException (sharing violation) when the
        /// mod attempts to reopen the log file.
        /// </remarks>
        public void Reset()
        {
            // Flush and release the log file.
            _writer?.Dispose();

            // Ensure that the next log call will initialize the writer.
            _timestampStep = DateTimeOffset.MinValue;
        }

        public void WriteLine(DateTimeOffset timestamp, string text)
        {
            var timestampStep = timestamp.Truncate(_timestampResolution);

            // Use double-checked locking pattern. The safety of this approach for all situations
            // is debated, but it seems better than doing nothing. Open to improvement here.
            if (timestampStep != _timestampStep)
            {
                lock (_syncRoot)
                {
                    if (timestampStep != _timestampStep)
                    {
                        _timestampStep = timestampStep;

                        CreateLogFile(timestampStep);
                    }
                }
            }

            lock (_syncRoot)
            {
                _writer.WriteLine(text);
            }
        }

        /// <summary>
        /// Creates a log file based on the specified timestamp. If the log file already exists, it will be opened.
        /// </summary>
        /// <param name="timestamp">The timestamp truncated to the target log file interval.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "The log writer lifecycle is managed at the class scope.")]
        private void CreateLogFile(DateTimeOffset timestamp)
        {
            // Set the log file name.
            var logFileName = $"{_fileName}-{timestamp:yyyyMMdd}-{timestamp:HHmmss}.log";

            // Set the log file path.
            var logFilePath = Path.Combine(
                Application.dataPath,
                Path.Combine(
                    "Mods/Insights",
                    logFileName));

            // Create directory. No action if the directory already exists.
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

            // Dispose the existing writer. This forces a flush.
            _writer?.Dispose();

            // Create a new writer. Use append to ensure a log file is not overwritten
            // if a session is restarted within the same rollover period.
            _writer = File.AppendText(logFilePath);
            _writer.AutoFlush = LogFileAutoFlush;
        }

        private static long GetTimestampInterval(RolloverInterval interval)
        {
            switch (interval)
            {
                case RolloverInterval.Day:
                    return TimeSpan.TicksPerDay;

                case RolloverInterval.Hour:
                    return TimeSpan.TicksPerHour;

                case RolloverInterval.Minute:
                    return TimeSpan.TicksPerMinute;

                default:
                    return TimeSpan.TicksPerMinute;
            }
        }
    }
}
