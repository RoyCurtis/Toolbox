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

        /// <summary>
        /// Sets whether to automatically print all log entries queued up whilst this
        /// logger is paused. If true, this logger will buffer entries in memory, else
        /// messages get dropped.
        /// </summary>
        public bool AutoPrintBacklog;

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
                lock (lockkey)
                {
                    paused = value;
                    Pause(value);
                    if (value || !AutoPrintBacklog) return;

                    var origCount = buffer.Count;
                    while (buffer.Count != 0)
                    {
                        var queued = buffer.Dequeue();
                        OnLog(queued.Level, queued.Tag, queued.Message, queued.Args);
                    }

                    if (origCount > 0)
                        Console.WriteLine("*** End backlog ***");
                }
            }
        }

        /// <summary>
        /// Gets the number of log messages currently in the pause queue
        /// </summary>
        public int Count { get { return buffer.Count; } }

        Queue<QueuedLog> buffer = new Queue<QueuedLog>();
        object lockkey = new object();

        #region Constructors
        /// <summary>
        /// Sets up a new console logger, automatically attaching to the Log.Logged event
        /// </summary>
        public ConsoleLogger() { Log.Logged += OnLog; }
        ~ConsoleLogger() { Log.Logged -= OnLog; }
        #endregion

        /// <summary>
        /// Prints log messages to console with appropriate color or queues messages
        /// to buffer if paused
        /// </summary>
        public void OnLog(LogLevels l, string tag, string msg, object[] args)
        {
            lock (lockkey)
            {
                if (paused)
                {
                    buffer.Enqueue(new QueuedLog
                    {
                        Level = l,
                        Tag = tag,
                        Message = msg,
                        Args = args
                    });

                    return;
                }

                msg = "[" + tag + "] " + msg;
                switch (l)
                {
                    case LogLevels.Fine:
                        coloredMessage(ConsoleColor.DarkGray, msg, args);
                        return;
                    case LogLevels.Debug:
                        coloredMessage(ConsoleColor.Gray, msg, args);
                        return;
                    case LogLevels.Info:
                        coloredMessage(ConsoleColor.White, msg, args);
                        return;
                    case LogLevels.Warning:
                        coloredMessage(ConsoleColor.Yellow, msg, args);
                        return;
                    case LogLevels.Severe:
                        coloredMessage(ConsoleColor.Red, msg, args);
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
            public string Tag;
            public string Message;
            public object[] Args;
        }
    }
}
