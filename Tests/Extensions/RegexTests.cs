using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Tests
{
    [TestClass]
    public class TRegexTests
    {
        [TestMethod]
        public void ToArray()
        {
            var test     = "My number is 010 123-4567";
            var expected = new[] { "010 123-4567", "010", "123-4567" };
            var actual   = Regex.Match(test, "([0-9]{3,}) ([0-9]{3,3}-[0-9]{4,})").ToArray();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void IsMatch()
        {
            var test   = "ReGeX reGgaE";
            var actual = TRegex.IsMatch(test, "regex");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void TryMatch()
        {
            string[] actual;
            var test     = "Testing out tryMatch out";
            var expected = new[] { "out" };

            Assert.IsTrue( TRegex.TryMatch(test, "out", out actual) );
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TryMatch_Fail()
        {
            string[] actual;
            var test = "Testing out tryMatch out";

            Assert.IsFalse( TRegex.TryMatch(test, "fail", out actual) );
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void TryMatch_Extension()
        {
            string[] actual;
            var regex    = new Regex("out");
            var test     = "Testing out tryMatch out";
            var expected = new[] { "out" };

            Assert.IsTrue( regex.TryMatch(test, out actual) );
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
