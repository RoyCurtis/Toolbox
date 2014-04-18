using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TMathTests
    {
        [TestMethod]
        public void Clamp_intShrink()
        {
            int value    = 512;
            int expected = 256;
            int actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Clamp_intGrow()
        {
            int value    = 64;
            int expected = 128;
            int actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Clamp_shortShrink()
        {
            short value    = 512;
            short expected = 256;
            short actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Clamp_shortGrow()
        {
            short value    = 64;
            short expected = 128;
            short actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Clamp_intSame()
        {
            int value    = 200;
            int expected = 200;
            int actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Clamp_shortSame()
        {
            short value    = 200;
            short expected = 200;
            short actual   = value.Clamp(128, 256);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException( typeof(ArgumentException) )]
        public void Clamp_intError()
        {
            int value = 128;
            value.Clamp(256, 128);
        }

        [TestMethod]
        [ExpectedException( typeof(ArgumentException) )]
        public void Clamp_shortError()
        {
            short value = 128;
            value.Clamp(256, 128);
        }
    }
}
