using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Delegate for the Logged event, which provides the tag, message and format
    /// parts (if any)
    /// </summary>
    public delegate void LoggedArgs(LogLevels l, string tag, string message, object[] parts);

    /// <summary>
    /// Global static class for the logging system, which exposes methods to log messages
    /// of varying levels and tags  
    /// </summary>
    public static class Log
    {
        const string tag = "Logger";

        static LogLevels level = LogLevels.Production;
        /// <summary>
        /// Gets or sets the current logging level. Set to "Production" by default
        /// </summary>
        public static LogLevels Level
        {
            set { level = value; }
            get { return level; }
        }

        /// <summary>
        /// Fires when a log is made but only if the current logging level allows for it.
        /// Useful for external code to hook into logging events
        /// </summary>
        public static event LoggedArgs Logged;

        /// <summary>
        /// List of loggers attached to the system; log messages get emitted to each one
        /// </summary>
        public static List<ILogger> Loggers = new List<ILogger>();

        #region Quick setup
        /// <summary>
        /// Quickly sets up and attaches a console logger and automatically sets the level
        /// to "All"
        /// </summary>
        /// <param name="level">Set a custom log level</param>
        public static void QuickSetup(LogLevels level = LogLevels.All)
        {
            Level = level;
            Loggers.Add( new ConsoleLogger() );
        }
        #endregion

        #region Logging methods
        /// <summary>
        /// Logs a fine level message, which is useful for debugging loops or functions
        /// called within seconds of other fuctions
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Fine(string tag, string message, params object[] parts)
        { emit(LogLevels.Fine, tag, message, parts); }

        /// <summary>
        /// Logs a debug level message, which is useful for minor functions such as
        /// opening a form or dialog
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Debug(string tag, string message, params object[] parts)
        { emit(LogLevels.Debug, tag, message, parts); }

        /// <summary>
        /// Logs an informational level message, which is useful for reports made at an
        /// interval such as memory usage or for major state changes such as successful
        /// login
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Info(string tag, string message, params object[] parts)
        { emit(LogLevels.Info, tag, message, parts); }

        /// <summary>
        /// Logs a warning level message, which is useful for when something goes wrong or
        /// unexpected data is received, but the program / assembly can carry on as normal
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Warn(string tag, string message, params object[] parts)
        { emit(LogLevels.Warning, tag, message, parts); }

        /// <summary>
        /// Logs a severe level message, which is useful for exceptions and other critical
        /// errors where a thread or program is unable to continue
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Severe(string tag, string message, params object[] parts)
        { emit(LogLevels.Severe, tag, message, parts); } 
        #endregion

        #region Stack trace methods
        /// <summary>
        /// Extension method that outputs an exception's message and its stack trace
        /// to the logger as Severe level
        /// </summary>
        public static void LogStackTrace(this Exception e)
        {
            Log.Severe("Exception", e.Message + e.StackTrace);
        }

        /// <summary>
        /// Extension method that recurses through inner exceptions, log all stack
        /// traces
        /// </summary>
        public static void LogFullStackTrace(this Exception e)
        {
            var ex = e;
            while (true)
            {
                Log.Severe("Exception", ex.Message + ex.StackTrace);
                if ( ex.InnerException != null )
                {
                    ex = ex.InnerException;
                    continue;
                }
                else
                    break;
            }
        } 
        #endregion

        #region Private methods
        static void emit(LogLevels target, string tag, string message, object[] parts)
        {
            if ( (target & Level) != target )
                return;

            if (Logged != null)
                Logged(target, tag, message, parts);

            foreach (var logger in Loggers)
                logger.Emit(target, tag, message, parts);
        } 
        #endregion
    }
}
