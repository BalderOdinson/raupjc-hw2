using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixthAssignment;

namespace ThirdAssignment
{
    [TestClass]
    public class SixthAssignmentUnitTest
    {
        [TestMethod]
        public void FactorielTestMethod()
        {
            Assert.AreEqual(27, MathOperations.FactorialDigitSum(13).Result);
        }
    }
}
