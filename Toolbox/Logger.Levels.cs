namespace System
{
    /// <summary>
    /// Flag enum that contains both specific levels and meta levels that enable multiple
    /// types of logging
    /// </summary>
    [Flags]
    public enum LogLevels
    {
        /// <summary>
        /// No logging
        /// </summary>
        None       = 0,
        /// <summary>
        /// Debugging loops and intricate micro-timed functions
        /// </summary>
        Fine       = 1,
        /// <summary>
        /// Debugging minor functions
        /// </summary>
        Debug      = 2,
        /// <summary>
        /// Reports at an interval, load messages
        /// </summary>
        Info       = 4,
        /// <summary>
        /// Something goes wrong or unexpected data, but not critical
        /// </summary>
        Warning    = 8,
        /// <summary>
        /// Critical error, crash or something stopping software / assembly
        /// </summary>
        Severe     = 16,

        /// <summary>
        /// All logging levels
        /// </summary>
        All        = Fine | Debug | Info | Warning | Severe,
        /// <summary>
        /// Quieter debug levels (no fine)
        /// </summary>
        Debugging  = Debug | Info | Warning | Severe,
        /// <summary>
        /// Production levels (no debug/fine)
        /// </summary>
        Production = Info | Warning | Severe
    }
}
