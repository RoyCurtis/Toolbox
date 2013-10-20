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

        #region Public properties and files
        string file;
        /// <summary>
        /// Gets or sets the absolute or relative target file path. Setting it will
        /// close any previous stream
        /// </summary>
        public string File
        {
            get { return file; }
            set
            {
                lock (mutex)
                {
                    flushAndClose();
                    file = Path.GetFullPath(value);
                }
            }
        } 

        bool paused = false;
        /// <summary>
        /// Gets or sets pause state
        /// </summary>
        public bool Paused
        {
            get { return paused; }
            set
            {
                lock (mutex)
                {
                    paused = value;
                    Pause(value);
                }
            }
        } 

        /// <summary>
        /// Gets or sets if this logger writes timestamps next to log entries
        /// </summary>
        public bool WriteTimestamp;
        #endregion

        #region Private fields
        StreamWriter stream;
        object mutex = new object(); 
        #endregion

        #region (De)constructors
        /// <summary>
        /// Creates a file logger that appends to the specified relative or absolute
        /// path. To begin logging, set the Paused property to false.
        /// </summary>
        public FileLogger(string path)
        {
            File = path;
        }

        /// <summary>
        /// Automatic flush and close of the file upon deconstruction
        /// </summary>
        ~FileLogger()
        {   
            flushAndClose();
        } 
        #endregion

        #region Log handler
        /// <summary>
        /// Appends log messages to file with timestamp, level and message
        /// </summary>
        public void Emit(LogChannel source, LogLevels level, string tag, string message, object[] args)
        {
            lock (mutex)
            {
                if (paused)
                    return;

                beginStream();

                var msg = string.Format(message, args);
                stream.WriteLine("{0:g} | [{1}] {2}", DateTime.Now, tag.PadLeft(16, ' '), msg);
            }
        } 
        #endregion

        #region Private methods
        /// <summary>
        /// Sets up a new file stream from the current set path
        /// </summary>
        void beginStream()
        {
            lock (mutex)
                if (stream == null)
                    stream = new StreamWriter(File, true, Encoding.UTF8);
        }

        /// <summary>
        /// Finalizes the file stream and sets it to null for a new one
        /// </summary>
        void flushAndClose()
        {
            lock (mutex)
            {
                if (stream == null)
                    return;

                stream.Flush();
                stream.Close();
                stream = null;
            }
        }
        #endregion
    }
}
