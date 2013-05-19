namespace System
{
    /// <summary>
    /// Provides useful extensions to DateTime
    /// </summary>
    public static class TDateTime
    {
        /// <summary>
        /// The UNIX epoch at Jan 1st 1970
        /// </summary>
        public static DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// Returns number of seconds since the UNIX epoch, typically known as a
        /// "timestamp" value
        /// </summary>
        public static long UnixTimestamp
        {
            get { return (long) DateTime.Now.Subtract(UnixEpoch).TotalSeconds; }
        }

        /// <summary>
        /// Returns an integer amount of seconds difference between DateTime.Now and
        /// this DateTime value
        /// </summary>
        public static int SecondsToNow(this DateTime time)
        {
            return (int) DateTime.Now.Subtract(time).TotalSeconds;
        }

        /// <summary>
        /// Returns a long amount of seconds ticks between the UNIX epoch and this
        /// DateTime value
        /// </summary>
        public static long UnixSeconds(this DateTime time)
        {
            return (long) time.Subtract(UnixEpoch).TotalSeconds;
        }
    }
}
