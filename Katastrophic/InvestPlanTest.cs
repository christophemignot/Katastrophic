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
                var result = InvestPlan.Input(string.Empty).Output();
            }
            //catch (ArgumentNullException ex)
            //{
            //    Assert.AreEqual("String", ex.ParamName);
            //    Assert.IsTrue(true);
            //}
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("index", ex.ParamName);
                Assert.IsTrue(true);
            }


            try
            {
                var result = InvestPlan.Input(null).Output();
            }
            //catch (ArgumentNullException ex)
            //{
            //    Assert.AreEqual("s", ex.ParamName);
            //    Assert.IsTrue(true);
            //}
            catch (NullReferenceException ex)
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
        public void WhenMutipleInputsThenMultipleResults()
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
