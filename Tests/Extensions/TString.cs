using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class TStringTests
    {
        [TestMethod]
        public void TerseSplit_String()
        {
            var test     = "I am a string; I am am going to be split";
            var expected = new[] { "I", "a string; I", "going to be split" };
            var actual   = test.TerseSplit("am");

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TerseSplit_Char()
        {
            var test     = "I am a string; I am going to be split by char;  ;like so  ";
            var expected = new[] { "I am a string", "I am going to be split by char", "like so" };
            var actual   = test.TerseSplit(';');

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IEquals()
        {
            var testA = "!t£e$s%t^s&t*r(i)n#g";
            var testB = "!t£E$S%t^s&T*r(I)n#G";
            var testC = "!S£o$M%e^s&t*r(i)n#g";

            Assert.IsTrue( testA.IEquals(testB) );
            Assert.IsFalse( testA.IEquals(testC) );
            Assert.IsFalse( testB.IEquals(testC) );
        }

        [TestMethod]
        public void LFormat()
        {
            var expected = "This is a formatting test";
            var actual   = "This {0} a {1} {2}".LFormat("is", "formatting", "test");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Mapping()
        {
            var map      = new[] { @"\bI\b", "you" };
            var test     = "I am a string. i have some words and characters I should replace";
            var expected = "you am a string. i have some words and characters you should replace";
            var actual   = test.Map(map);

            Assert.AreEqual(expected, actual, "String not map-replaced correctly");
        }

        [TestMethod]
        public void Mapping_IgnoreCase()
        {
            var map      = new[] { @"\bI\b", "you" };
            var test     = "I am a string. i have some words and characters I should replace";
            var expected = "you am a string. you have some words and characters you should replace";
            var actual   = test.Map(true, map);

            Assert.AreEqual(expected, actual, "String not map-replaced correctly");
        }

        [TestMethod]
        public void Mapping_DoNothing()
        {
            var map      = new[] { "nothing", "something" };
            var expected = "There should be no change in this string";
            var actual   = expected.Map(map);

            Assert.AreEqual(expected, actual, "String not map-replaced correctly");
        }

        [TestMethod]
        [ExpectedException( typeof(ArgumentException) )]
        public void Mapping_OddMap()
        {
            var map    = new[] { "1", "2", "3" };
            var actual = "hello".Map(true, map);
        }
    }
}
