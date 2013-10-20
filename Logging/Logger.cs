namespace System
{
    /// <summary>
    /// Delegate for the Logged event, which provides the tag, message and format
    /// parts (if any)
    /// </summary>
    public delegate void LoggedArgs(LogLevels l, string tag, string message, object[] parts);

    /// <summary>
    /// Static class that provides access to logging to a global log channel
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Accesses the global log channel
        /// </summary>
        public static readonly LogChannel Global = new LogChannel();

        /// <summary>
        /// Gets or sets the current global channel's logging level. Set to "Production"
        /// by default.
        /// </summary>
        public static LogLevels Level
        {
            set { Global.Level = value; }
            get { return Global.Level; }
        }

        #region Quick setup
        /// <summary>
        /// Quickly sets up and attaches a <see cref="ConsoleLogger"/> and automatically
        /// sets the level to <see cref="LogLevels.All"/>
        /// </summary>
        /// <param name="level">Set a custom log level</param>
        public static void QuickSetup(LogLevels level = LogLevels.All)
        {
            Level = level;
            Attach( new ConsoleLogger() );
        }
        #endregion

        #region Logger methods
        /// <summary>
        /// Attaches a logger to the global log channel
        /// </summary>
        public static void Attach(ILogger logger)
        {
            Global.Attach(logger);
        }

        /// <summary>
        /// Detaches a logger from the global log channel
        /// </summary>
        public static void Detach(ILogger logger)
        {
            Global.Detach(logger);
        }

        /// <summary>
        /// Detaches all loggers from the global log channel
        /// </summary>
        public static void DetachAll(ILogger logger)
        {
            Global.DetachAll();
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
        { Global.emit(LogLevels.Fine, tag, message, parts); }

        /// <summary>
        /// Logs a debug level message, which is useful for minor functions such as
        /// opening a form or dialog
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Debug(string tag, string message, params object[] parts)
        { Global.emit(LogLevels.Debug, tag, message, parts); }

        /// <summary>
        /// Logs an informational level message, which is useful for reports made at an
        /// interval such as memory usage or for major state changes such as successful
        /// login
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Info(string tag, string message, params object[] parts)
        { Global.emit(LogLevels.Info, tag, message, parts); }

        /// <summary>
        /// Logs a warning level message, which is useful for when something goes wrong or
        /// unexpected data is received, but the program / assembly can carry on as normal
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Warn(string tag, string message, params object[] parts)
        { Global.emit(LogLevels.Warning, tag, message, parts); }

        /// <summary>
        /// Logs a severe level message, which is useful for exceptions and other critical
        /// errors where a thread or program is unable to continue
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public static void Severe(string tag, string message, params object[] parts)
        { Global.emit(LogLevels.Severe, tag, message, parts); } 
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
    }
}
