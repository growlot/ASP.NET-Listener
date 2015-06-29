﻿//-----------------------------------------------------------------------
// <copyright file="PhysicalAddressTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="PhysicalAddress"/> class
    /// </summary>
    [TestClass]
    public class PhysicalAddressTests
    {
        /// <summary>
        /// US address format should be correct.
        /// </summary>
        [TestMethod]
        public void USAddressFormatShouldBeCorrect()
        {
            PhysicalAddress address = new PhysicalAddress("US", "CA", "San Jose", "455 Larkspur Dr.", string.Empty, "92926");
            Assert.AreEqual("455 Larkspur Dr., San Jose, CA 92926", address.GetAddressUSFormat());

            // test when first address line is po box and second is street address
            address = new PhysicalAddress("US", "CA", "San Jose", "P.O. Box 124", "455 Larkspur Dr.", "92926");
            Assert.AreEqual("455 Larkspur Dr., San Jose, CA 92926", address.GetAddressUSFormat());
        }
    }
}
