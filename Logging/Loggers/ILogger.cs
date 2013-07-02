namespace System
{
    /// <summary>
    /// For implementing a class which handles log messages of all levels to any output,
    /// such as console or file. Can be paused.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Fired when the logger is paused; passes true or false for pause and unpause
        /// respectively
        /// </summary>
        event Action<bool> Pause;

        /// <summary>
        /// Gets or sets if this logger is paused
        /// </summary>
        bool Paused { get; set; }

        /// <summary>
        /// Called by Log when a log message is emitted that is allowed by its currently
        /// set Level
        /// </summary>
        void Emit(LogLevels level, string tag, string message, object[] args);
    }
}
