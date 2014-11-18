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
        public void ConfigManagerInitializationShouldThrowExceptionIfPersistenceManagerIsNotSpecified()
        {
            ITransactionManager transactionLogManager;

            transactionLogManager = new TransactionManager(null);
            transactionLogManager.ToString();
        }

        /// <summary>
        /// Tests if IsEnabled returns true.
        /// </summary>
        [TestMethod]
        public void AfterUpdateTransactionStateNewTransactionStateShouldBeReturned()
        {
            ITransactionManager transactionLogManager;

            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1StringObject<TransactionLog>((propertyName, propertyValue) =>
                {
                    TransactionLog transaction = new TransactionLog(1);
                    return transaction;
                });
                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                transactionLogManager = new TransactionManager(persistenceController);
                transactionLogManager.UpdateTransactionState(1, TransactionStateLookup.ClientEnd);

                // Assert.AreEqual(url, settings.GetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl));
            }
        }
    }
}
