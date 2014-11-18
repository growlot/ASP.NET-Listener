namespace Client.Implementation.Unit.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common;
    using NHibernate.Criterion;
    using AMSLLC.Listener.Client.Implementation;
    using AMSLLC.Listener.Client.Implementation.Messages;

    [TestClass]
    public class ListenerWebServiceClientTests
    {
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

        [TestMethod]
        public void SuccessfullTransactionShouldBeSaved()
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

        private class ListenerWebServiceClientTester : ListenerWebServiceClient
        {
            public ListenerWebServiceClientTester(IPersistenceManager persistenceManager)
                : base(persistenceManager)
            { 
            }

            public new void FinishTransaction(ClientResponse response, int transactionId)
            {
                base.FinishTransaction(response, transactionId);
            }
        }
    }
}
