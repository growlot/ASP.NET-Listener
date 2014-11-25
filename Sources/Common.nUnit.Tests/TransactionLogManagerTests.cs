//-----------------------------------------------------------------------
// <copyright file="TransactionLogManagerTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Common.Unit.Tests
{
    using System;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="TransactionManager"/> class.
    /// </summary>
    [TestClass]
    public class TransactionLogManagerTests
    {
        /// <summary>
        /// Tests if transaction log manager throws exception if it is initialized without persistence controller
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void TransactionLogManagerInitializationShouldThrowExceptionIfPersistenceManagerIsNotSpecified()
        {
            ITransactionManager transactionLogManager;

            transactionLogManager = new TransactionManager(null);
            transactionLogManager.ToString();
        }
    }
}
