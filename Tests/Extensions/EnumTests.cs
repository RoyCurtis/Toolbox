using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class TEnumsTests
    {
        [TestMethod]
        public void Parse()
        {
            var actualA = TEnums.Parse<TestEnum>("testEntry1", false);
            var actualB = TEnums.Parse<TestEnum>("testEntry2");
            var actualC = TEnums.Parse<TestEnum>("testEntry3", true);
            var actualD = TEnums.Parse<TestEnum>("SomethingElse");
            var actualE = TEnums.Parse<TestEnum>("nOtHiNG", true);

            Assert.AreEqual(TestEnum.testEntry1, actualA);
            Assert.AreEqual(TestEnum.tEsTeNtRy2, actualB);
            Assert.AreEqual(TestEnum.TESTENTRY3, actualC);
            Assert.AreEqual(TestEnum.somethingelse, actualD);
            Assert.AreEqual(TestEnum.Nothing, actualE);
        }
    }

    public enum TestEnum
    {
        testEntry1,
        tEsTeNtRy2,
        TESTENTRY3,
        somethingelse,
        Nothing
    }
}
