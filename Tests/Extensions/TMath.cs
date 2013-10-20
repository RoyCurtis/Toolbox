using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TMathTests
    {
        [TestMethod]
        public void Clamp_Max()
        {
            var value    = 512;
            var expected = 256;
            var actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Clamp_Min()
        {
            var value    = 0;
            var expected = 128;
            var actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Clamp_Same()
        {
            var value    = 200;
            var expected = 200;
            var actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException( typeof(ArgumentException) )]
        public void Clamp_Error()
        {
            var value = 128;
            value.Clamp(256, 128);
        }
    }
}
