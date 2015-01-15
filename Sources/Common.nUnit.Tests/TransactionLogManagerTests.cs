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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "transactionLogManager", Justification = "Exception expected so no need to use variable"), TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void TransactionLogManagerInitializationShouldThrowExceptionIfPersistenceManagerIsNotSpecified()
        {
            ITransactionManager transactionLogManager;

            transactionLogManager = new TransactionManager(null);
        }

        /// <summary>
        /// Transaction should be saved with long message.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Common.Unit.Tests.TransactionLogManagerTests.CheckTransactionSave(System.Int32,System.String,System.String)", Justification = "It's just a test"), TestMethod]
        public void TransactionShouldBeSavedWithLongMessage()
        {
            int returnCode = 1;
            string message = "Long long message (more than 1000 symbols). aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            string debugInfo = "Anything";

            CheckTransactionSave(returnCode, message, debugInfo);
        }

        /// <summary>
        /// Failed transaction should be saved.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Common.Unit.Tests.TransactionLogManagerTests.CheckTransactionSave(System.Int32,System.String,System.String)", Justification = "It's just a test"), TestMethod]
        public void FailedTransactionShouldBeSaved()
        {
            int returnCode = 1;
            string message = "Normal message";
            string debugInfo = "Anything";

            CheckTransactionSave(returnCode, message, debugInfo);
        }

        /// <summary>
        /// Successful transaction should be saved.
        /// </summary>
        [TestMethod]
        public void SuccessfulTransactionShouldBeSaved()
        {
            int returnCode = 0;
            CheckTransactionSave(returnCode, null, null);
        }

        /// <summary>
        /// Successful transaction should be saved.
        /// </summary>
        /// <param name="returnCode">The return code.</param>
        /// <param name="message">The message.</param>
        /// <param name="debugInfo">The debug information.</param>
        private static void CheckTransactionSave(int returnCode, string message, string debugInfo)
        {
            bool saveVisited = false;
            TransactionLog transactionLog = null;
            ITransactionManager transactionLogManager;
            int transactionId = 1;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1StringObject<TransactionLog>((propertyName, propertyValue) =>
                {
                    transactionLog = new TransactionLog(transactionId);
                    return transactionLog;
                });
                persistenceManager.SaveOf1M0<TransactionLog>((transactionLogObject) =>
                {
                    saveVisited = true;
                    if (transactionLog.Message != null)
                    {
                        Assert.IsTrue(transactionLog.Message.Length <= 1000);
                    }
                });
                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                transactionLogManager = new TransactionManager(persistenceController);

                transactionLogManager.UpdateTransactionStatus(transactionId, returnCode, message, debugInfo);
            }

            Assert.IsTrue(saveVisited);
        }
    }
}
