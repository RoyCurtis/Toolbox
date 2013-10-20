using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Logger
{
    [TestClass]
    public class ILoggerTests
    {
        TestLogger logger = new TestLogger();

        [TestInitialize]
        public void Init()
        {
            Log.Attach(logger);
        }

        [TestCleanup]
        public void Cleanup()
        {
            Log.Detach(logger);
        }

        [TestMethod]
        public void EmitAll()
        {
            Log.Level = LogLevels.All;

            Log.Fine("TestA", "Fine test");
            Assert.AreEqual(logger.LastTag, "TestA");
            Assert.AreEqual(logger.LastLevel, LogLevels.Fine);
            Assert.AreEqual(logger.LastArgs.Length, 0);

            Log.Debug("TestB", "Debug test", "hello");
            Assert.AreEqual(logger.LastTag, "TestB");
            Assert.AreEqual(logger.LastLevel, LogLevels.Debug);
            CollectionAssert.Contains(logger.LastArgs, "hello");

            Log.Info("TestC", "Info test");
            Assert.AreEqual(logger.LastTag, "TestC");
            Assert.AreEqual(logger.LastLevel, LogLevels.Info);
            Assert.AreEqual(logger.LastArgs.Length, 0);

            Log.Warn("TestD", "Warn test");
            Assert.AreEqual(logger.LastSource, Log.Global);
            Assert.AreEqual(logger.LastLevel, LogLevels.Warning);
            Assert.AreEqual(logger.LastArgs.Length, 0);

            Log.Severe("TestE", "Severe test");
            Assert.AreEqual(logger.LastMessage, "Severe test");
            Assert.AreEqual(logger.LastLevel, LogLevels.Severe);
            Assert.AreEqual(logger.LastArgs.Length, 0);
        }

        [TestMethod]
        public void Pause()
        {
            var testmessageA   = "This should not go through";
            var testmessageB   = "This should go through";
            var pausetriggered = false;
            logger.Pause  += (p) => { pausetriggered = true; };
            logger.Paused  = true;

            Assert.IsTrue(pausetriggered);
            Assert.IsTrue(logger.Paused);

            Log.Info("Test", testmessageA);
            Assert.AreNotEqual(logger.LastMessage, testmessageA);

            logger.Paused = false;
            Assert.IsFalse(logger.Paused);
            Log.Info("Test", testmessageB);
            Assert.AreEqual(logger.LastMessage, testmessageB);
        }
    }
}
