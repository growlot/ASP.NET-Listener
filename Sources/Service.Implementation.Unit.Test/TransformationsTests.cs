//-----------------------------------------------------------------------
// <copyright file="TransformationsTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Service.Implementation.Unit.Test
{
    using System;
    using System.Collections.Generic;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Service.Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests transformations class
    /// </summary>
    [TestClass]
    public class TransformationsTests
    {
        /// <summary>
        /// Gets as left and get as found should use mathematical rounding.
        /// </summary>
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
