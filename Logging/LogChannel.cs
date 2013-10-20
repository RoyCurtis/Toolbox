using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Represents a channel in which log messages can be emitted from and loggers
    /// may be attached to
    /// </summary>
    public class LogChannel
    {
        List<ILogger> loggers = new List<ILogger>();

        object    mutex = new object();
        LogLevels level = LogLevels.Production;
        /// <summary>
        /// Gets or sets this channel's current logging level. Set to "Production" by
        /// default.
        /// </summary>
        public LogLevels Level
        {
            set { level = value; }
            get { return level;  }
        }

        /// <summary>
        /// Fires when a log is emitted through this channel but only if the channel's
        /// logging level allows for it
        /// </summary>
        public event LoggedArgs Logged;

        #region Logger methods
        /// <summary>
        /// Attaches a logger to this channel
        /// </summary>
        public void Attach(ILogger logger)
        {
            if ( !loggers.Contains(logger) )
                loggers.Add(logger);
        }

        /// <summary>
        /// Detaches a logger from this channel
        /// </summary>
        public void Detach(ILogger logger)
        {
            loggers.Remove(logger);
        }

        /// <summary>
        /// Detaches all loggers from this channel
        /// </summary>
        public void DetachAll()
        {
            loggers.Clear();
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
        public void Fine(string tag, string message, params object[] parts)
        { emit(LogLevels.Fine, tag, message, parts); }

        /// <summary>
        /// Logs a debug level message, which is useful for minor functions such as
        /// opening a form or dialog
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public void Debug(string tag, string message, params object[] parts)
        { emit(LogLevels.Debug, tag, message, parts); }

        /// <summary>
        /// Logs an informational level message, which is useful for reports made at an
        /// interval such as memory usage or for major state changes such as successful
        /// login
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public void Info(string tag, string message, params object[] parts)
        { emit(LogLevels.Info, tag, message, parts); }

        /// <summary>
        /// Logs a warning level message, which is useful for when something goes wrong or
        /// unexpected data is received, but the program / assembly can carry on as normal
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public void Warn(string tag, string message, params object[] parts)
        { emit(LogLevels.Warning, tag, message, parts); }

        /// <summary>
        /// Logs a severe level message, which is useful for exceptions and other critical
        /// errors where a thread or program is unable to continue
        /// </summary>
        /// <param name="tag">Name of class or section this log is from</param>
        /// <param name="message">Log message in formattable form</param>
        /// <param name="parts">Data for any formatting parameters in the message</param>
        public void Severe(string tag, string message, params object[] parts)
        { emit(LogLevels.Severe, tag, message, parts); } 
        #endregion

        #region Stack trace methods
        /// <summary>
        /// Outputs an exception's message and its stack trace through this channel
        /// as Severe level
        /// </summary>
        public void LogStackTrace(Exception e)
        {
            Severe("Exception", e.Message + e.StackTrace);
        }

        /// <summary>
        /// Recursively outputs an exception's message and its stack trace, along with
        /// that of inner exceptions, through this channel as Severe level
        /// </summary>
        public void LogFullStackTrace(Exception e)
        {
            var ex = e;
            while (true)
            {
                Severe("Exception", ex.Message + ex.StackTrace);
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
        internal void emit(LogLevels target, string tag, string message, object[] parts)
        {
            if ( (target & Level) != target )
                return;

            if (Logged != null)
                Logged(target, tag, message, parts);

            foreach (var logger in loggers)
                logger.Emit(this, target, tag, message, parts);
        } 
        #endregion
    }
}
