namespace System
{
    /// <summary>
    /// Provides useful extensions to DateTime
    /// </summary>
    public static class TBXDateTime
    {
        /// <summary>
        /// Returns an integer amount of seconds difference between DateTime.Now and
        /// this DateTime value
        /// </summary>
        public static int SecondsToNow(this DateTime time)
        {
            return (int) DateTime.Now.Subtract(time).TotalSeconds;
        }
    }
}
