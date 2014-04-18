using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class TConsoleTests
    {
        TextWriter   originalOut;
        StringWriter captureOut;

        [TestInitialize]
        public void Init()
        {
            originalOut = Console.Out;
            captureOut  = new StringWriter();

            Console.SetOut(captureOut);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Console.OpenStandardOutput();
            captureOut.Dispose();
        }

        [TestMethod]
        public void WriteBlock()
        {
            TConsole.WriteBlock("Hello, world!");
            TConsole.WriteBlock("Test {0} {2} {1}", "Alpha", "Beta", "Gamma");

            var actual   = captureOut.ToString();
            var expected = "\nHello, world!\n{0}\nTest Alpha Gamma Beta\n{0}".LFormat(Environment.NewLine);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteBlockColored()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            TConsole.WriteBlockColored(ConsoleColor.Red, "Hello, world!");
            Assert.AreEqual(ConsoleColor.Yellow, Console.ForegroundColor);
            TConsole.WriteBlockColored(ConsoleColor.Blue, "Test {0} {2} {1}", "Alpha", "Beta", "Gamma");
            Assert.AreEqual(ConsoleColor.Yellow, Console.ForegroundColor);

            var actual   = captureOut.ToString();
            var expected = "\nHello, world!\n{0}\nTest Alpha Gamma Beta\n{0}".LFormat(Environment.NewLine);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteColored()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            TConsole.WriteColored(ConsoleColor.Red, "Hello, world!");
            Assert.AreEqual(ConsoleColor.Green, Console.ForegroundColor);
            TConsole.WriteColored(ConsoleColor.Blue, "Test {0} {2} {1}", "Alpha", "Beta", "Gamma");
            Assert.AreEqual(ConsoleColor.Green, Console.ForegroundColor);

            var actual   = captureOut.ToString();
            var expected = "Hello, world!Test Alpha Gamma Beta";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteColored_Both()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.White;
            TConsole.WriteColored(ConsoleColor.Red, ConsoleColor.Green, "Hello, world!");
            Assert.AreEqual(ConsoleColor.Green, Console.ForegroundColor);
            Assert.AreEqual(ConsoleColor.White, Console.BackgroundColor);
            TConsole.WriteColored(ConsoleColor.Blue, ConsoleColor.Yellow, "Test {0} {2} {1}", "Alpha", "Beta", "Gamma");
            Assert.AreEqual(ConsoleColor.Green, Console.ForegroundColor);
            Assert.AreEqual(ConsoleColor.White, Console.BackgroundColor);

            var actual   = captureOut.ToString();
            var expected = "Hello, world!Test Alpha Gamma Beta";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteHighlighted()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            TConsole.WriteColored(ConsoleColor.Red, "Hello, world!");
            Assert.AreEqual(ConsoleColor.Gray, Console.BackgroundColor);
            TConsole.WriteColored(ConsoleColor.Blue, "Test {0} {2} {1}", "Alpha", "Beta", "Gamma");
            Assert.AreEqual(ConsoleColor.Gray, Console.BackgroundColor);

            var actual   = captureOut.ToString();
            var expected = "Hello, world!Test Alpha Gamma Beta";
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteLineColored()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            TConsole.WriteLineColored(ConsoleColor.Red, "Hello, world!");
            Assert.AreEqual(ConsoleColor.DarkBlue, Console.ForegroundColor);
            TConsole.WriteLineColored(ConsoleColor.Blue, "Test {0} {2} {1}", "Alpha", "Beta", "Gamma");
            Assert.AreEqual(ConsoleColor.DarkBlue, Console.ForegroundColor);

            var actual   = captureOut.ToString();
            var expected = "Hello, world!{0}Test Alpha Gamma Beta{0}".LFormat(Environment.NewLine);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteLineColored_Both()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.Cyan;
            TConsole.WriteLineColored(ConsoleColor.Red, ConsoleColor.DarkBlue, "Hello, world!");
            Assert.AreEqual(ConsoleColor.DarkBlue, Console.ForegroundColor);
            Assert.AreEqual(ConsoleColor.Cyan, Console.BackgroundColor);
            TConsole.WriteLineColored(ConsoleColor.Blue, ConsoleColor.Yellow, "Test {0} {2} {1}", "Alpha", "Beta", "Gamma");
            Assert.AreEqual(ConsoleColor.DarkBlue, Console.ForegroundColor);
            Assert.AreEqual(ConsoleColor.Cyan, Console.BackgroundColor);

            var actual   = captureOut.ToString();
            var expected = "Hello, world!{0}Test Alpha Gamma Beta{0}".LFormat(Environment.NewLine);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteLineHighlighted()
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            TConsole.WriteLineHighlighted(ConsoleColor.Red, "Hello, world!");
            Assert.AreEqual(ConsoleColor.DarkMagenta, Console.BackgroundColor);
            TConsole.WriteLineHighlighted(ConsoleColor.Blue, "Test {0} {2} {1}", "Alpha", "Beta", "Gamma");
            Assert.AreEqual(ConsoleColor.DarkMagenta, Console.BackgroundColor);

            var actual   = captureOut.ToString();
            var expected = "Hello, world!{0}Test Alpha Gamma Beta{0}".LFormat(Environment.NewLine);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Mutex()
        {
            var tasks = new Task[]
            {
                new Task(() => TConsole.WriteBlock("{0}{1}", "Code Of", "Honor")),
                new Task(() => TConsole.WriteBlockColored(ConsoleColor.DarkYellow, "{0}{1}", "The Best", "Of Both Worlds")),
                new Task(() => TConsole.WriteColored(ConsoleColor.Blue, "{0}{1}", "The Master", "piece Society")),
                new Task(() => TConsole.WriteColored(ConsoleColor.Red, ConsoleColor.Green, "{0}{1}", "Encounter At", "Farpoint")),
                new Task(() => TConsole.WriteLineColored(ConsoleColor.Cyan, "{0}{1}", "Q", "Who?")),
                new Task(() => TConsole.WriteLineColored(ConsoleColor.Black, ConsoleColor.DarkGray, "{0}{1}", "The Measure", "Of A Man")),
                new Task(() => TConsole.WriteBlock("{0}{1}", "Code of", "Honor")),
            };

            
        }
    }
}
