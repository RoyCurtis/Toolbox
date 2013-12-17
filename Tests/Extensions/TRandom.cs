using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class TRandomTests
    {
        const int MinLimitInt = -25;
        const int MaxLimitInt = 25;

        const float MaxLimitF = 23.331f;

        [TestMethod]
        public void Next_MaxExclusive()
        {
            for (var i = 0; i < 1000; i++)
                if (TRandom.Next(MaxLimitInt) >= MaxLimitInt)
                    Assert.Fail("Exclusive maximum limit was one of the random results");
        }

        [TestMethod]
        public void Next_MaxInclusive()
        {
            for (var i = 0; i < 1000; i++)
            {
                var value = TRandom.Next(MaxLimitInt);

                if (value == 0)
                    return;

                if (value < 0)
                    Assert.Fail("Random result was less than inclusive minimum limit");
            }

            Assert.Fail("Inclusive minimum limit never hit");
        }

        [TestMethod]
        public void Next_MinMaxBounds()
        {
            for (var i = 0; i < 1000; i++)
            {
                var value = TRandom.Next(MinLimitInt, MaxLimitInt);

                if (value >= MaxLimitInt || value < MinLimitInt)
                    Assert.Fail("Random result was out of bounds");
            }
        }

        [TestMethod]
        public void Next_MinMaxNegatives()
        {
            for (var i = 0; i < 1000; i++)
                if (TRandom.Next(MinLimitInt, MaxLimitInt) < 0)
                    return;

            Assert.Fail("Negative result never hit");
        }

        [TestMethod]
        public void NextFloat()
        {
            for (var i = 0; i < 1000; i++)
            {
                var value = TRandom.NextFloat(MaxLimitF);

                if (value >= MaxLimitF || value < 0)
                    Assert.Fail("Random result was out of bounds");
            }
        }

        [TestMethod]
        public void NextBytes()
        {
            var buffer = new byte[32];
            TRandom.NextBytes(buffer);

            foreach (var value in buffer)
                if (value != 0)
                    return;

            Assert.Fail("Empty byte buffer");
        }

        [TestMethod]
        public void NextBool()
        {
            var hitTrue  = false;
            var hitFalse = false;

            for (var i = 0; i < 1000; i++)
            {
                if ( TRandom.NextBool() )
                    hitTrue  = true;
                else
                    hitFalse = true;

                if (hitTrue && hitFalse)
                    return;
            }

            Assert.Fail("All results were the same");
        }

        [TestMethod]
        public void NextBool_Favor()
        {
            for (var i = 0; i < 500; i++)
                if ( !TRandom.NextBool(100) )
                    Assert.Fail("Hit a false result on 100% favor");

            for (var i = 0; i < 500; i++)
                if ( TRandom.NextBool(0) )
                    Assert.Fail("Hit a true result on 0% favor");
        }

        [TestMethod]
        public void NextElement_String()
        {
            var list   = new[] { "hello", "my", "name", "is", "duke", "nukem" };
            var actual = new HashSet<string>();

            for (var i = 0; i < 200; i++)
                actual.Add( TRandom.NextElement(list) );

            CollectionAssert.AreEquivalent( list, actual.ToArray() );
        }

        [TestMethod]
        public void NextElement_Char()
        {
            var list   = new[] { 'j', 'a', 'r', 'e', 'd' };
            var actual = new HashSet<char>();

            for (var i = 0; i < 200; i++)
                actual.Add( TRandom.NextElement(list) );

            CollectionAssert.AreEquivalent( list, actual.ToArray() );
        }

        [TestMethod]
        public void NextLetter()
        {
            var test   = "dolypartn";
            var actual = new HashSet<char>();

            for (var i = 0; i < 200; i++)
                actual.Add( TRandom.NextLetter(test) );

            Assert.AreEqual(test.Length, actual.Count);
        }
    }
}
