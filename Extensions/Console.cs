namespace System
{
    /// <summary>
    /// Provides additional thread-safe methods for writing to the console using colors
    /// and other fancy formatting
    /// </summary>
    public static class TConsole
    {
        static object mutex = new object();

        /// <summary>
        /// Writes text to console with a pre- and post-fix newline. Thread-safe and
        /// formattable.
        /// </summary>
        public static void WriteBlock(string line, params object[] args)
        {
            lock (mutex)
                Console.WriteLine("\n{0}\n", line.LFormat(args));
        }

        /// <summary>
        /// Writes colored text to console with a pre- and post-fix newline, resetting
        /// the foreground color to the previous value. Thread-safe and formattable.
        /// </summary>
        public static void WriteBlockColored(ConsoleColor color, string line, params object[] args)
        {
            lock (mutex)
                WriteLineColored(color, "\n{0}\n", line.LFormat(args));
        }

         /// <summary>
        /// Writes colored text to the console, resetting the foreground color to the
        /// previous value. Thread-safe and formattable.
        /// </summary>
        public static void WriteColored(ConsoleColor color, string message, params object[] args)
        {
            lock (mutex)
            {
                var prevColor = Console.ForegroundColor;

                Console.ForegroundColor = color;
                Console.Write(message, args);
                Console.ForegroundColor = prevColor;
            }
        }

        /// <summary>
        /// Writes colored text lines to the console, resetting the foreground color to
        /// the previous value. Thread-safe and formattable.
        /// </summary>
        public static void WriteLineColored(ConsoleColor color, string message, params object[] args)
        {
            lock (mutex)
            {
                var prevColor = Console.ForegroundColor;

                Console.ForegroundColor = color;
                Console.WriteLine(message, args);
                Console.ForegroundColor = prevColor;
            }
        }

        /// <summary>
        /// Writes highlighted text to the console, resetting the background color to the
        /// previous value. Thread-safe and formattable.
        /// </summary>
        public static void WriteHighlighted(ConsoleColor bgcolor, string message, params object[] args)
        {
            lock (mutex)
            {
                var prevColor = Console.BackgroundColor;

                Console.BackgroundColor = bgcolor;
                Console.Write(message, args);
                Console.BackgroundColor = prevColor;
            }
        }

        /// <summary>
        /// Writes highlighted text lines to the console, resetting the background color
        /// to the previous value. Thread-safe and formattable.
        /// </summary>
        public static void WriteLineHighlighted(ConsoleColor bgcolor, string message, params object[] args)
        {
            lock (mutex)
            {
                var prevColor = Console.BackgroundColor;

                Console.BackgroundColor = bgcolor;
                Console.WriteLine(message, args);
                Console.BackgroundColor = prevColor;
            }
        }

        /// <summary>
        /// Writes both highlighted and colored text to the console, resetting both values
        /// to the previous. Thread-safe and formattable.
        /// </summary>
        public static void WriteColored(ConsoleColor bgcolor, ConsoleColor textcolor, string message, params object[] args)
        {
            lock (mutex)
            {
                var prevBGColor = Console.BackgroundColor;
                var prevFGColor = Console.ForegroundColor;

                Console.BackgroundColor = bgcolor;
                Console.ForegroundColor = textcolor;
                Console.Write(message, args);
                Console.BackgroundColor = prevBGColor;
                Console.ForegroundColor = prevFGColor;
            }
        }

        /// <summary>
        /// Writes both highlighted and colored text lines to the console, resetting both
        /// values to the previous. Thread-safe and formattable.
        /// </summary>
        public static void WriteLineColored(ConsoleColor bgcolor, ConsoleColor textcolor, string message, params object[] args)
        {
            lock (mutex)
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
