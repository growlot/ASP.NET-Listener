// <copyright file="ActionValueTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.Unit.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for <see cref="ActionValue"/> value object.
    /// </summary>
    [TestClass]
    public class ActionValueTest
    {
        /// <summary>
        /// Action value code is case sensitive, and incorrectly cased code should not be recognized.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "value", Justification = "Justified")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ActionValueCodeIsCaseSensitive()
        {
            var value = new ActionValue("r");
            value = null;
        }

        /// <summary>
        /// Wrong action code should not work.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "AMSLLC.Listener.Domain.WNP.ActionValue", Justification = "Justified")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WrongActionCodeShouldNotWork()
        {
            new ActionValue("B");
        }

        /// <summary>
        /// NULL, empty string or whitespace characters should not work
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "AMSLLC.Listener.Domain.WNP.ActionValue", Justification = "Justified")]
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NullOrEmptyActionCodeShouldNotWork()
        {
            new ActionValue(null);
            new ActionValue(string.Empty);
            new ActionValue("  ");
        }

        /// <summary>
        /// Action values created from static methods should match created from code.
        /// </summary>
        [TestMethod]
        public void ActionValuesCreatedFromStaticMethodsShouldMatchCreatedFromCode()
        {
            var actionValue = new ActionValue("R");
            Assert.AreEqual(ActionValue.Required, actionValue);

            actionValue = new ActionValue("D");
            Assert.AreEqual(ActionValue.Disabled, actionValue);

            actionValue = new ActionValue("E");
            Assert.AreEqual(ActionValue.Enabled, actionValue);

            actionValue = new ActionValue("C");
            Assert.AreEqual(ActionValue.Clear, actionValue);
        }
    }
}
