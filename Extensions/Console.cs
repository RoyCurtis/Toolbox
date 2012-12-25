namespace System
{
    /// <summary>
    /// Provides additional thread-safe methods for writing to the console using colors
    /// </summary>
    public static class TBXConsole
    {
        static object lockkey = new object();

        /// <summary>
        /// Writes colored text to the console, resetting the foreground color to the
        /// previous value
        /// </summary>
        public static void WriteLineColored(ConsoleColor color, string message, params object[] args)
        {
            lock (lockkey)
            {
                var prevColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                Console.WriteLine(message, args);
                Console.ForegroundColor = prevColor;
            }
        }

        /// <summary>
        /// Writes highlighted text to the console, resetting the background color to the
        /// previous value
        /// </summary>
        public static void WriteLineHighlighted(ConsoleColor bgcolor, string message, params object[] args)
        {
            lock (lockkey)
            {
                var prevColor = Console.BackgroundColor;
                Console.BackgroundColor = bgcolor;
                Console.WriteLine(message, args);
                Console.BackgroundColor = prevColor;
            }
        }

        /// <summary>
        /// Writes both highlighted and colored text to the console, resetting both values
        /// to the previous
        /// </summary>
        public static void WriteLineColored(ConsoleColor bgcolor, ConsoleColor textcolor, string message, params object[] args)
        {
            lock (lockkey)
            {
                var prevBGColor = Console.BackgroundColor;
                var prevFGColor = Console.ForegroundColor;
                Console.BackgroundColor = bgcolor;
                Console.ForegroundColor = textcolor;
                Console.WriteLine(message, args);
                Console.BackgroundColor = prevBGColor;
                Console.ForegroundColor = prevFGColor;
            }
        }
    }
}
