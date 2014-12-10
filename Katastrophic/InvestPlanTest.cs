using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Katastrophic
{
    [TestClass]
    public class InvestPlanTest
    {
        [TestMethod]
        public void WhenInputIsNullOrEmptyThenThrowException()
        {
            try
            {
                InvestPlan.Input(string.Empty).Output();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("index", ex.ParamName);
                Assert.IsTrue(true);
            }

            try
            {
                InvestPlan.Input(null).Output();
            }
            catch (NullReferenceException)
            {
                Assert.IsTrue(true);
            }  
        }

        [TestMethod]
        public void WhenInputIsOnlyZeroThenEmpty()
        {
            var result = InvestPlan.Input("0").Output();
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void WhenInputIsTwoZeroThenImpossible()
        {
            var result = InvestPlan.Input(@"0
0
1 1 1 1 1 1 1 1 1 1 1 1").Output();
            Assert.AreEqual("Case 1: IMPOSSIBLE", result);
        }

        [TestMethod]
        public void WhenInputIsOneThenImpossible()
        {
            var result = InvestPlan.Input(@"0
1
1 1 1 1 1 1 1 1 1 1 1 1").Output();
            Assert.AreEqual("Case 1: IMPOSSIBLE", result);
        }

        [TestMethod]
        public void WhenInputIs10ThenEmpty()
        {
            var result = InvestPlan.Input(@"0
10
1 2 3 1 1 1 1 1 1 1 1 1").Output();
            Assert.AreEqual("Case 1: 1 3 20", result);
        }

        [TestMethod]
        public void WhenMultipleInputsThenMultipleResults()
        {
            var result = InvestPlan.Input(@"0
10
1 2 3 1 1 1 1 1 1 1 1 1
20
1 5 10 2 1 6 1 1 2 20 1 1").Output();
            Assert.AreEqual("Case 1: 1 3 20\nCase 2: 1 10 380", result);
        }



    }
}
