using System;
using System.Collections.Generic;
using System.Linq;
using FirstAssignment;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThirdAssignment
{
    [TestClass]
    public class FirstAssignmentUnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            Assert.IsTrue(new Student("pero","0036491634") == new Student("ivan","0036491634"));
            Assert.IsTrue(new Student("pero", "0036491634") != null);
        }
    }
}
