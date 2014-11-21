namespace Service.Implementation.Unit.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Service.Implementation;

    [TestClass]
    public class TransformationsTests
    {
        [TestMethod]
        public void GetAsLeftAndGetAsFoundShouldUseMathematicalRounding()
        {
            IList<MeterTestResult> meterTestResults = new List<MeterTestResult>();
            MeterTestResult meterTestResult = new MeterTestResult()
            {
                AsLeft = (float)100.125,
                AsFound = (float)100.135,
                Element = 'S',
                TestType = "LL"
            };
            meterTestResults.Add(meterTestResult);
            float resultAsLeft = Transformations.GetAsLeft(meterTestResults, 'S', "LL");
            float resultAsFound = Transformations.GetAsFound(meterTestResults, 'S', "LL");

            Assert.AreEqual((float)100.13, resultAsLeft);
            Assert.AreEqual((float)100.14, resultAsFound);
        }
    }
}
