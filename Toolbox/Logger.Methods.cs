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
        const string LOG_LOGGER = "Logger";

        static LogLevels logLevel;
        /// <summary>
        /// Gets or sets the current logging level, automatically logging the change in
        /// level to Info
        /// </summary>
        public static LogLevels Level
        {
            set
            {
                Info(LOG_LOGGER, "Logging level set to {0}", value);
                logLevel = value;
            }

            get { return logLevel; }
        }

        /// <summary>
        /// Fires when a log is made but only if the current logging level allows for it
        /// </summary>
        public static event LoggedArgs Logged;

        static bool log(LogLevels l, string tag, string message, object[] parts)
        {
            if (Logged != null && (l & Level) == l)
                Logged(l, tag, message, parts);

            return true;
        }

        /// <summary>
        /// Logs a fine level message, which is useful for debugging loops or functions
        /// called within seconds of other fuctions
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        /// <returns></returns>
        public static bool Fine(string tag, string message, params object[] parts)
        { return log(LogLevels.Fine, tag, message, parts); }

        /// <summary>
        /// Logs a debug level message, which is useful for minor functions such as
        /// opening a form or dialog
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        /// <returns></returns>
        public static bool Debug(string tag, string message, params object[] parts)
        { return log(LogLevels.Debug, tag, message, parts); }

        /// <summary>
        /// Logs an informational level message, which is useful for reports made at an
        /// interval such as memory usage or for major state changes such as successful
        /// login
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        /// <returns></returns>
        public static bool Info(string tag, string message, params object[] parts)
        { return log(LogLevels.Info, tag, message, parts); }

        /// <summary>
        /// Logs a warning level message, which is useful for when something goes wrong or
        /// unexpected data is received, but the program / assembly can carry on as normal
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        /// <returns></returns>
        public static bool Warn(string tag, string message, params object[] parts)
        { return log(LogLevels.Warning, tag, message, parts); }

        /// <summary>
        /// Logs a severe level message, which is useful for exceptions and other critical
        /// errors where a thread or program is unable to continue
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        /// <returns></returns>
        public static bool Severe(string tag, string message, params object[] parts)
        { return log(LogLevels.Severe, tag, message, parts); }

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
                if (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    continue;
                }
                else break;
            }
        }
    }
}
