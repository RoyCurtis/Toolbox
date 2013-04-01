using System.Collections.Generic;

namespace System
{
    /// <summary>
    /// Thread-safe logger that writes to console using colors for each log level.
    /// Can be paused; queues messages up optionally
    /// </summary>
    public class ConsoleLogger : ILogger
    {
        /// <summary>
        /// Fired when this console logger is paused; passes true or false for pause and
        /// unpause respectively
        /// </summary>
        public event Action<bool> Pause;

        #region Level colors
        /// <summary>
        /// Gets or sets text color for fine-level logs; default is DarkGray
        /// </summary>
        public ConsoleColor FineColor = ConsoleColor.DarkGray;
        /// <summary>
        /// Gets or sets text color for debug-level logs; default is Gray
        /// </summary>
        public ConsoleColor DebugColor = ConsoleColor.Gray;
        /// <summary>
        /// Gets or sets text color for info-level logs; default is White
        /// </summary>
        public ConsoleColor InfoColor = ConsoleColor.White;
        /// <summary>
        /// Gets or sets text color for warn-level logs; default is Yellow
        /// </summary>
        public ConsoleColor WarnColor = ConsoleColor.Yellow;
        /// <summary>
        /// Gets or sets text color for severe-level logs; default is Red
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
        /// If more than zero, will pad the tag names of log entries with spaces for
        /// readability
        /// </summary>
        public int TagPadding = 0;

        /// <summary>
        /// Gets or sets the format of each log entry's tag. Must at least include {0}
        /// for the tag name. This will get padded with spaces depending on TagPadding
        /// </summary>
        /// <example>Default is "[{0}]"</example>
        public string TagFormat = "[{0}]";

        /// <summary>
        /// Gets or sets the format of each log entry's message. Must at least include:
        /// - {0} for the tag
        /// - {1} for the message
        /// </summary>
        /// <example>Default is "{0} {1}"</example>
        public string MessageFormat = "{0} {1}";

        /// <summary>
        /// If true, successive messages from the same tag are visually grouped
        /// </summary>
        public bool GroupSimilar = true;

               bool paused;
        /// <summary>
        /// Sets pause state; if paused and AutoPrintBacklog is true, logged messages
        /// get added to a backlog and then if unpaused, it will print the backlog to
        /// console.
        /// </summary>
        public bool Paused
        {
            get { return paused; }
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
                            buffer.Clear();
                    }

                    // Clear lastTag if grouping similar, so that the tag re-appears on
                    // backlog to prevent confusion
                    if (value && GroupSimilar)
                        lastTag = null;

                    if (Pause != null)
                        Pause(value);
                }
            }
        }

        /// <summary>
        /// Gets the number of log messages currently in the pause queue
        /// </summary>
        public int Count { get { return buffer.Count; } } 
        #endregion

        #region Private
        Queue<QueuedLog> buffer  = new Queue<QueuedLog>();
        object           mutex   = new object();
        string           lastTag = "";

        /// <summary>
        /// Goes through the backlog buffer and logs every entry, clearing the buffer
        /// as it does
        /// </summary>
        void printBacklog()
        {
            var origCount = buffer.Count;

            if (origCount == 0)
                return;

            coloredMessage(ConsoleColor.Cyan, "### Printing logger backlog ###");

            while (buffer.Count > 0)
            {
                var queued = buffer.Dequeue();
                OnLog(queued.Level, queued.Tag, queued.Message, queued.Args);
            }

            coloredMessage(ConsoleColor.Cyan, "### End of logger backlog ###");
        }
        #endregion

        #region (De)constructors
        /// <summary>
        /// Sets up a new console logger, automatically attaching to the Log.Logged event
        /// </summary>
        public ConsoleLogger() {
            Log.Logged += OnLog;
        }

        /// <summary>
        /// Automatic deregistration of logged event upon deconstruction
        /// </summary>
        ~ConsoleLogger() {
            Log.Logged -= OnLog;
        }
        #endregion

        #region Log handler
        /// <summary>
        /// Prints log messages to console with appropriate color or queues messages
        /// to buffer if paused
        /// </summary>
        public void OnLog(LogLevels l, string tag, string msg, object[] args)
        {
            lock (mutex)
            {
                if (paused)
                {
                    buffer.Enqueue(new QueuedLog
                    {
                        Level   = l,
                        Tag     = tag,
                        Message = msg,
                        Args    = args
                    });

                    return;
                }

                string finalTag = "";
                string finalMsg = "";

                // Blank tags if grouping similar
                if (GroupSimilar && tag == lastTag)
                {
                    var padLength = Math.Max(TagPadding, lastTag.Length);
                        finalTag  = "".PadRight(padLength);
                }
                else
                    finalTag = TagFormat.LFormat(tag).PadRight(TagPadding);

                // Divide groups of tags with newlines
                if (GroupSimilar && tag != lastTag)
                    finalMsg = '\n' + MessageFormat.LFormat(finalTag, msg);
                else
                    finalMsg = MessageFormat.LFormat(finalTag, msg);

                lastTag      = tag;
                switch (l)
                {
                    case LogLevels.Fine:
                        coloredMessage(FineColor,   finalMsg, args);
                        return;
                    case LogLevels.Debug:
                        coloredMessage(DebugColor,  finalMsg, args);
                        return;
                    case LogLevels.Info:
                        coloredMessage(InfoColor,   finalMsg, args);
                        return;
                    case LogLevels.Warning:
                        coloredMessage(WarnColor,   finalMsg, args);
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
            public LogLevels Level;
            public string    Tag;
            public string    Message;
            public object[]  Args;
        }
        #endregion
    }
}
