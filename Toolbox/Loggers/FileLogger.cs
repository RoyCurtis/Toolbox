using System.IO;
using System.Text;

namespace System.Loggers
{
    /// <summary>
    /// Thread-safe logger that appends to file. Can be paused.
    /// </summary>
    public class FileLogger : ILogger
    {
        /// <summary>
        /// Fired when this file logger is paused; passes true or false for pause and
        /// unpause respectively
        /// </summary>
        public event Action<bool> Pause;

        #region Public fields
        /// <summary>
        /// Read-only file path that this logger is currently writing to
        /// </summary>
        public readonly string File;

        /// <summary>
        /// Gets or sets if this logger writes timestamps next to log entries
        /// </summary>
        public bool WriteTimestamp;

        bool paused = true;
        /// <summary>
        /// Gets or sets pause state; if paused, the logger flushes and closes the stream
        /// and silently discards messages, else re-establishes stream and resumes writing
        /// </summary>
        public bool Paused
        {
            get { return paused; }
            set
            {
                lock (lockkey)
                {
                    if (value)
                    {
                        stream.Flush();
                        stream.Close();
                        stream = null;
                    }
                    else
                        stream = new StreamWriter(File, true, Encoding.UTF8);

                    paused = value;
                    Pause(value);
                }
            }
        } 
        #endregion

        #region Private
        StreamWriter stream;
        object lockkey = new object(); 
        #endregion

        #region (De)constructors
        /// <summary>
        /// Creates a file logger that appends to the specified relative or absolute
        /// path. To begin logging, set the Paused property to false.
        /// </summary>
        public FileLogger(string path)
        {
            File = Path.GetFullPath(path);
            Log.Logged += OnLog;
        }

        /// <summary>
        /// Automatic pause and deregistration of logged event upon deconstruction
        /// </summary>
        ~FileLogger()
        {   
            Paused = true;
            Log.Logged -= OnLog;
        } 
        #endregion

        #region Log handler
        /// <summary>
        /// Appends log messages to file with timestamp, level and message
        /// </summary>
        public void OnLog(LogLevels level, string tag, string message, object[] args)
        {
            lock (lockkey)
            {
                if (paused) return;

                var msg = string.Format(message, args);
                stream.WriteLine("{0:g} | [{1}] {2}", DateTime.Now, tag.PadLeft(16, ' '), msg);
            }
        } 
        #endregion
    }
}
