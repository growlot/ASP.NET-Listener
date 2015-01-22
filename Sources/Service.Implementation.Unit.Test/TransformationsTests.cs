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
                AsLeft = 100.125M,
                AsFound = 100.135M,
                Element = 'S',
                TestType = "LL"
            };
            meterTestResults.Add(meterTestResult);
            decimal resultAsLeft = Transformations.GetAsLeft(meterTestResults, 'S', "LL");
            decimal resultAsFound = Transformations.GetAsFound(meterTestResults, 'S', "LL");

            Assert.AreEqual(100.13M, resultAsLeft);
            Assert.AreEqual(100.14M, resultAsFound);
        }
    }
}
