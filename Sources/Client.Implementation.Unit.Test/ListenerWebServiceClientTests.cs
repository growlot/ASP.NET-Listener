//-----------------------------------------------------------------------
// <copyright file="ListenerWebServiceClientTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Client.Implementation.Unit.Test
{
    using System;
    using AMSLLC.Listener.Client.Implementation;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NHibernate.Criterion;

    /// <summary>
    /// Tests Listener web service client
    /// </summary>
    [TestClass]
    public class ListenerWebServiceClientTests
    {
        /// <summary>
        /// Failed transaction should be saved with long message.
        /// </summary>
        [TestMethod]
        public void FailedTransactionShouldBeSavedWithLongMessage()
        {
            bool saveVisited = false;
            TransactionLog transactionLog = null;
            int transactionId = 1;
            ClientResponse response = new ClientResponse()
            {
                ReturnCode = -1,
                Message = "Long long message (more thatn 1000 symbols). aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                DebugInfo = "Anything"
            };

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
                    Assert.IsTrue(transactionLog.Message.Length <= 1000);
                });

                ListenerWebServiceClientTester webServiceClient = new ListenerWebServiceClientTester(persistenceManager);
                webServiceClient.FinishTransaction(response, transactionId);
            }

            Assert.IsTrue(saveVisited);
        }

        /// <summary>
        /// Failed transaction should be saved.
        /// </summary>
        [TestMethod]
        public void FailedTransactionShouldBeSaved()
        {
            bool saveVisited = false;
            TransactionLog transactionLog = null;
            int transactionId = 1;
            ClientResponse response = new ClientResponse()
            {
                ReturnCode = -1,
                Message = "Normal message",
                DebugInfo = "Anything"
            };

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
                });

                ListenerWebServiceClientTester webServiceClient = new ListenerWebServiceClientTester(persistenceManager);
                webServiceClient.FinishTransaction(response, transactionId);

                Assert.IsTrue(saveVisited);
            }
        }

        /// <summary>
        /// Successful transaction should be saved.
        /// </summary>
        [TestMethod]
        public void SuccessfulTransactionShouldBeSaved()
        {
            bool saveVisited = false;
            TransactionLog transactionLog = null;
            int transactionId = 1;
            ClientResponse response = new ClientResponse()
            {
                ReturnCode = 0
            };

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
                });

                ListenerWebServiceClientTester webServiceClient = new ListenerWebServiceClientTester(persistenceManager);
                webServiceClient.FinishTransaction(response, transactionId);
            }

            Assert.IsTrue(saveVisited);
        }

        /// <summary>
        /// Exposes protected methods for testing
        /// </summary>
        private class ListenerWebServiceClientTester : ListenerWebServiceClient
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ListenerWebServiceClientTester"/> class.
            /// </summary>
            /// <param name="persistenceManager">The persistence manager.</param>
            public ListenerWebServiceClientTester(IPersistenceManager persistenceManager)
                : base(persistenceManager)
            { 
            }

            /// <summary>
            /// Finishes the transaction by setting transaction status and last transaction state.
            /// </summary>
            /// <param name="response">The response.</param>
            /// <param name="transactionId">The transaction identifier.</param>
            public new void FinishTransaction(ClientResponse response, int transactionId)
            {
                base.FinishTransaction(response, transactionId);
            }
        }
    }
}
