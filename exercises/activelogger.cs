// ActivityLoggerProgram.cs
// -------------------------------------------
// A simple, self-contained C# console app that:
// 1. Checks whether a specified log file exists.
// 2. If not, creates the containing folder and the log file.
// 3. Provides easy helper methods to log activity entries at different levels.
// 4. Persists entries with timestamps so you can review later or commit to GitHub.
//
// Usage:
//   dotnet new console -n ActivityLoggerDemo
//   (Replace the generated Program.cs contents with this file's contents.)
//   dotnet run --project ActivityLoggerDemo            // uses default folder + activity.log
//   dotnet run --project ActivityLoggerDemo -- "C:/Logs/MyApp" "mylog.txt"   // custom path/file
//
// After running, check the console output for the full path to the created/used log file.
//
// Keep the comments if you plan to upload to GitHub; they're beginner-friendly but concise.
// -------------------------------------------

using System;
using System.IO;
using System.Threading;

namespace ActivityLoggerDemo
{
    /// <summary>
    /// Simple set of log levels you can expand as needed.
    /// </summary>
    public enum ActivityLevel
    {
        Info,
        Warning,
        Error,
        Debug,
        Critical
    }

    /// <summary>
    /// Lightweight activity logger.
    /// * Ensures folder + file exist on first use.
    /// * Thread-safe (synchronized writes).
    /// * Adds UTC-style timestamp + level tag.
    /// * Optional exception message capture.
    /// </summary>
    public static class ActivityLogger
    {
        private static readonly object _sync = new object();

        /// <summary>Directory containing the log file (set during Initialize).</summary>
        public static string LogDirectory { get; private set; } = string.Empty;

        /// <summary>Full path to the log file (set during Initialize).</summary>
        public static string LogFilePath { get; private set; } = string.Empty;

        private static bool _isInitialized = false;

        /// <summary>
        /// Initialize the logger, optionally specifying a directory and log file name.
        /// If omitted, uses: %LOCALAPPDATA%/ActivityLoggerDemo/activity.log on Windows,
        /// or the platform equivalent LocalApplicationData folder on Linux/macOS.
        /// </summary>
        public static void Initialize(string? directory = null, string? fileName = null)
        {
            if (string.IsNullOrWhiteSpace(directory))
            {
                directory = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "ActivityLoggerDemo");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "activity.log";
            }

            LogDirectory = directory;
            LogFilePath = Path.Combine(LogDirectory, fileName);

            EnsureLogFile();
            _isInitialized = true;
        }

        /// <summary>
        /// Make sure the folder exists; if not, create it. Also ensure the log file exists.
        /// Writes a creation header line the first time the log is created.
        /// </summary>
        private static void EnsureLogFile()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            if (!File.Exists(LogFilePath))
            {
                using (var sw = File.CreateText(LogFilePath))
                {
                    sw.WriteLine($"# Activity Log created {DateTimeOffset.Now:u}");
                }
            }
        }

        /// <summary>
        /// Generic log method. Automatically initializes with defaults if not already done.
        /// </summary>
        public static void Log(ActivityLevel level, string message, Exception? ex = null)
        {
            if (!_isInitialized)
            {
                Initialize(); // use defaults
            }

            string line = FormatLine(level, message, ex);
            WriteLineWithRetry(line);
        }

        /// <summary>
        /// Format: 2025-07-21 12:34:56Z | Info     | message | EX: System.Exception: ...
        /// Uses universal sortable datetime format "u" for consistency across locales.
        /// </summary>
        private static string FormatLine(ActivityLevel level, string message, Exception? ex)
        {
            var timestamp = DateTimeOffset.Now.ToString("u"); // e.g., 2025-07-21 10:15:30Z
            string exceptionText = ex == null ? string.Empty : $" | EX: {ex.GetType().FullName}: {ex.Message}";
            return $"{timestamp} | {level,-8} | {message}{exceptionText}";
        }

        /// <summary>
        /// Actually write to disk. Retries a few times in case of transient IO locks.
        /// </summary>
        private static void WriteLineWithRetry(string line, int maxAttempts = 3, int delayMs = 100)
        {
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    lock (_sync) // sync multi-thread writes
                    {
                        File.AppendAllText(LogFilePath, line + Environment.NewLine);
                    }
                    return; // success
                }
                catch (IOException) when (attempt < maxAttempts)
                {
                    Thread.Sleep(delayMs);
                }
            }
        }

        // Convenience wrappers ----------------------------------------------
        public static void LogInfo(string message) => Log(ActivityLevel.Info, message);
        public static void LogWarning(string message) => Log(ActivityLevel.Warning, message);
        public static void LogError(string message, Exception? ex = null) => Log(ActivityLevel.Error, message, ex);
        public static void LogDebug(string message) => Log(ActivityLevel.Debug, message);
        public static void LogCritical(string message, Exception? ex = null) => Log(ActivityLevel.Critical, message, ex);
    }

    /// <summary>
    /// Demo console program that writes some sample log entries.
    /// Pass optional args: [0] directory, [1] filename
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Accept optional directory + filename from command line.
            string? dirArg = args.Length > 0 ? args[0] : null;
            string? fileArg = args.Length > 1 ? args[1] : null;

            ActivityLogger.Initialize(dirArg, fileArg);

            ActivityLogger.LogInfo("Application started.");
            ActivityLogger.LogDebug("Debug: starting DoWork() simulation.");

            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                // In real apps you might log and rethrow; here we just log.
                ActivityLogger.LogCritical("Unhandled exception in Main.", ex);
            }

            ActivityLogger.LogInfo("Application finished cleanly.");

            Console.WriteLine($"Logs written to: {ActivityLogger.LogFilePath}");
            Console.WriteLine("Sample entries:");
            Console.WriteLine("------------------------------------------------------------");
            if (File.Exists(ActivityLogger.LogFilePath))
            {
                foreach (var line in File.ReadLines(ActivityLogger.LogFilePath))
                {
                    Console.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Simulated work that writes various log levels and throws a handled exception.
        /// </summary>
        static void DoWork()
        {
            ActivityLogger.LogInfo("Work step 1: Starting.");
            // Simulate a warning condition (replace with real conditions in your app)
            ActivityLogger.LogWarning("Low disk space (simulated warning).");

            try
            {
                // Simulate an error
                throw new InvalidOperationException("Simulated operation failure");
            }
            catch (Exception ex)
            {
                ActivityLogger.LogError("Handled exception in DoWork.", ex);
            }

            ActivityLogger.LogInfo("Work step 1: Completed.");
        }
    }
}

/* ------------------------------------------------------------
   QUICK NOTES (include in README if you like)
   ------------------------------------------------------------
   • What it does: Guarantees the log folder + file exist, then appends timestamped lines.
   • Where logs go (default): LocalApplicationData/ActivityLoggerDemo/activity.log
     - Windows example: C:\\Users\\<You>\\AppData\\Local\\ActivityLoggerDemo\\activity.log
     - Linux/macOS: ~/.local/share/ActivityLoggerDemo/activity.log (varies by platform)
   • Customize path: pass directory + file name as args.
   • Levels: Info, Warning, Error, Debug, Critical (extend as needed).
   • Thread-safe: Writes are locked so multi-thread apps don't interleave lines.
   • Retry-on-IO: Tries again briefly if file temporarily locked.

   NEXT STEPS IDEAS
   ----------------
   - Add log rotation (size-based or daily).
   - Write JSON lines for structured logging.
   - Integrate with Serilog / NLog later if you outgrow this.
   - Add async logging queue if extremely chatty.
*/
