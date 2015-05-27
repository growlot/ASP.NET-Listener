//-----------------------------------------------------------------------
// <copyright file="UtilitiesTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Unit.Test
{
    using System;
    using System.ComponentModel;
    using AMSLLC.Listener.Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="Utilities"/> class.
    /// </summary>
    [TestClass]
    public class UtilitiesTests
    {
        /// <summary>
        /// Enumeration used for tests
        /// </summary>
        private enum TestEnum
        {
            /// <summary>
            /// Enumeration with description
            /// </summary>
            [System.ComponentModel.Description("description")]
            WithDescription,

            /// <summary>
            /// Enumeration with empty description
            /// </summary>
            [System.ComponentModel.Description]
            EmptyDescription,

            /// <summary>
            /// Enumeration without description
            /// </summary>
            NoDescription
        }

        /// <summary>
        /// Should get enumeration description.
        /// </summary>
        [TestMethod]
        public void ShouldGetEnumDescription()
        {
            string description = Utilities.GetEnumDescription(TestEnum.WithDescription);
            Assert.AreEqual("description", description);
        }

        /// <summary>
        /// Should throw exception if enumeration does not have description.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void ShouldThrowExceptionIfEnumDoesNotHaveDescription()
        {
            Utilities.GetEnumDescription(TestEnum.NoDescription);
        }

        /// <summary>
        /// Should throw exception if enumeration description is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void ShouldThrowExceptionIfEnumDescriptionIsEmpty()
        {
            Utilities.GetEnumDescription(TestEnum.EmptyDescription);
        }

        /// <summary>
        /// Should throw exception if enumeration not specified.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ShouldThrowExceptionIfEnumNotSpecified()
        {
            Utilities.GetEnumDescription(null);
        }
    }
}
