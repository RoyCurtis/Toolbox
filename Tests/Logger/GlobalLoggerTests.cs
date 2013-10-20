using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logger
{
    [TestClass]
    public class GlobalLoggerTests
    {
        [TestMethod]
        public void QuickSetup()
        {
            Log.QuickSetup();

            Assert.IsNotNull(Log.Global);
            Assert.AreEqual<LogLevels>(LogLevels.All, Log.Level);
            Assert.AreEqual<LogLevels>(LogLevels.All, Log.Global.Level);
        }

        [TestMethod]
        public void Emit_All()
        {
            var        fired = 0;
            LoggedArgs onlog = (lv, tag, msg, parts) => { fired++; };

            Log.Level          = LogLevels.All;
            Log.Global.Logged += onlog;

            Log.Fine("Test", "Fine test");
            Log.Debug("Test", "Debug test");
            Log.Info("Test", "Info test");
            Log.Warn("Test", "Warn test");
            Log.Severe("Test", "Severe test");
            Assert.AreEqual(fired, 5);

            Log.Global.Logged -= onlog;
        }

        [TestMethod]
        public void Emit_Production()
        {
            var        fired = 0;
            LoggedArgs onlog = (lv, tag, msg, parts) => { fired++; };

            Log.Level          = LogLevels.Production;
            Log.Global.Logged += onlog;

            Log.Fine("Test", "Fine test");
            Assert.AreEqual(fired, 0);
            Log.Debug("Test", "Debug test");
            Assert.AreEqual(fired, 0);
            Log.Info("Test", "Info test");
            Assert.AreEqual(fired, 1);
            Log.Warn("Test", "Warn test");
            Assert.AreEqual(fired, 2);
            Log.Severe("Test", "Severe test");
            Assert.AreEqual(fired, 3);

            Log.Global.Logged -= onlog;
        }

        [TestMethod]
        public void Emit_Debugging()
        {
            var        fired = 0;
            LoggedArgs onlog = (lv, tag, msg, parts) => { fired++; };

            Log.Level          = LogLevels.Debugging;
            Log.Global.Logged += onlog;

            Log.Fine("Test", "Fine test");
            Assert.AreEqual(fired, 0);
            Log.Debug("Test", "Debug test");
            Assert.AreEqual(fired, 1);
            Log.Info("Test", "Info test");
            Assert.AreEqual(fired, 2);
            Log.Warn("Test", "Warn test");
            Assert.AreEqual(fired, 3);
            Log.Severe("Test", "Severe test");
            Assert.AreEqual(fired, 4);

            Log.Global.Logged -= onlog;
        }
    }
}
