using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TCollectionTests
    {
        [TestMethod]
        public void GetOrNull_Value()
        {
            var def  = default(bool);
            var test = new Dictionary<string, bool> {
                { "a", true },
                { "b", true },
            };

            Assert.IsTrue( test.GetOrNullV("a") );
            Assert.IsTrue( test.GetOrNullV("b") );
            Assert.AreEqual(def,  test.GetOrNullV("c") );
        }

        [TestMethod]
        public void GetOrNull_Reference()
        {
            var test = new Dictionary<string, object> {
                { "a", new object() },
                { "b", new object() },
            };

            Assert.IsNotNull( test.GetOrNullR("a") );
            Assert.IsNotNull( test.GetOrNullR("b") );
            Assert.IsNull( test.GetOrNullR("c") );
        }

        [TestMethod]
        public void AddRange()
        {
            var actual   = new SortedSet<string>();
            var expected = new[] {
                "hello", "i", "am", "a", "set"
            };

            actual.AddRange(expected);

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void IContains()
        {
            var test = new[] {
                "Hello", "I", "AM", "a", "sEt"
            };

            Assert.IsTrue( test.IContains("hello") );
            Assert.IsTrue( test.IContains("I") );
            Assert.IsTrue( test.IContains("am") );
            Assert.IsTrue( test.IContains("A") );
            Assert.IsTrue( test.IContains("SeT") );
        }
    }
}
