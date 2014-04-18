using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Threading;

namespace Tests
{
    [TestClass]
    public class TDateTimeTests
    {
        [TestMethod]
        public void UnixTimestamp()
        {
            // 1396310400 == 1st April 2014
            // Inaccurate test - more of a sanity test
            Assert.IsTrue(TDateTime.UnixTimestamp > 1396310400);
        }

        [TestMethod]
        public void SecondsToNow()
        {
            var now = DateTime.Now;
            Thread.Sleep(2000);

            Assert.AreEqual(2, now.SecondsToNow());
        }

        [TestMethod]
        public void UnixSeconds()
        {
            var testA = new DateTime(1970, 1, 1, 0, 1, 0);
            var testB = new DateTime(1979, 3, 5, 8, 41, 35);
            var testC = new DateTime(1990, 1, 1, 11, 22, 33);

            Assert.AreEqual(60, testA.UnixSeconds());
            Assert.AreEqual(289471295, testB.UnixSeconds());
            Assert.AreEqual(631192953, testC.UnixSeconds());
        }
    }
}
