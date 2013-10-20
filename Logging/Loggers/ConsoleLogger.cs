using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Thread-safe logger that writes to console using colors for each log level.
    /// Can be paused; queues messages up optionally
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        static readonly object mutex = new object();
        
        /// <summary>
        /// Fired when this console logger is paused. Passes the new pause value.
        /// </summary>
        public event Action<bool> Pause;

        #region Level colors
        /// <summary>
        /// Gets or sets text color for fine-level logs. Default is DarkGray.
        /// </summary>
        public ConsoleColor FineColor = ConsoleColor.DarkGray;
        /// <summary>
        /// Gets or sets text color for debug-level logs. Default is Gray.
        /// </summary>
        public ConsoleColor DebugColor = ConsoleColor.Gray;
        /// <summary>
        /// Gets or sets text color for info-level logs. Default is White.
        /// </summary>
        public ConsoleColor InfoColor = ConsoleColor.White;
        /// <summary>
        /// Gets or sets text color for warn-level logs. Default is Yellow.
        /// </summary>
        public ConsoleColor WarnColor = ConsoleColor.Yellow;
        /// <summary>
        /// Gets or sets text color for severe-level logs. Default is Red.
        /// </summary>
        public ConsoleColor SevereColor = ConsoleColor.Red; 
        #endregion

        #region Public fields
        /// <summary>
        /// Sets whether to automatically print all log entries queued up whilst this
        /// logger is paused. If true, this logger will buffer entries in memory, else
        /// messages get dropped.
        /// </summary>
        public bool AutoPrintBacklog;

        /// <summary>
        /// Gets or sets the tag padding. If set to than zero, this logger will pad the
        /// tags of logs with spaces for enhanced readability.
        /// </summary>
        public int TagPadding = 0;

        /// <summary>
        /// Gets or sets the format of each log entry's message. Must at least include:
        /// - "{0}" for the tag
        /// - "{1}" for the message
        /// </summary>
        /// <example>Default is "{0} {1}"</example>
        public string MessageFormat = "[{0}] {1}";

        bool paused;
        /// <summary>
        /// Gets or sets pause state
        /// </summary>
        /// <seealso cref="AutoPrintBacklog"/>
        public bool Paused
        {
            get
            { 
                lock (mutex)
                    return paused;
            }

            set
            {
                lock (mutex)
                {
                    paused = value;

                    // Auto-print backlog on unpause, else clear backbuffer (just in case)
                    if (!value)
                    {
                        if (AutoPrintBacklog)
                            printBacklog();
                        else
                            queue.Clear();
                    }

                    if (Pause != null)
                        Pause(value);
                }
            }
        }
        #endregion

        #region Privates
        Queue<QueuedLog> queue  = new Queue<QueuedLog>();

        /// <summary>
        /// Goes through the backlog buffer and logs every entry, clearing the buffer
        /// as it does
        /// </summary>
        void printBacklog()
        {
            var origCount = queue.Count;

            if (origCount == 0)
                return;

            coloredMessage(ConsoleColor.Cyan, "### Printing logger backlog ###");

            while (queue.Count > 0)
            {
                var queued = queue.Dequeue();
                Emit(queued.Source, queued.Level, queued.Tag, queued.Message, queued.Args);
            }

            coloredMessage(ConsoleColor.Cyan, "### End of logger backlog ###");
        }
        #endregion

        #region Log handler
        /// <summary>
        /// Prints log messages to console with appropriate color or queues messages
        /// to buffer if paused
        /// </summary>
        public void Emit(LogChannel source, LogLevels level, string tag, string msg, object[] args)
        {
            lock (mutex)
            {
                if (paused)
                {
                    queue.Enqueue(new QueuedLog
                    {
                        Source  = source,
                        Level   = level,
                        Tag     = tag,
                        Message = msg,
                        Args    = args,
                    });

                    return;
                }

                string finalTag = tag.PadRight(TagPadding);
                string finalMsg = MessageFormat.LFormat(finalTag, msg);

                switch (level)
                {
                    case LogLevels.Fine:
                        coloredMessage(FineColor, finalMsg, args);
                        return;
                    case LogLevels.Debug:
                        coloredMessage(DebugColor, finalMsg, args);
                        return;
                    case LogLevels.Info:
                        coloredMessage(InfoColor, finalMsg, args);
                        return;
                    case LogLevels.Warning:
                        coloredMessage(WarnColor, finalMsg, args);
                        return;
                    case LogLevels.Severe:
                        coloredMessage(SevereColor, finalMsg, args);
                        return;
                }
            }
        }

        void coloredMessage(ConsoleColor col, string msg, params object[] args)
        {
            Console.ForegroundColor = col;
            Console.WriteLine(msg, args);
            Console.ForegroundColor = ConsoleColor.White;
        }

        struct QueuedLog
        {
            public LogChannel Source;
            public LogLevels  Level;
            public string     Tag;
            public string     Message;
            public object[]   Args;
        }
        #endregion
    }
}
