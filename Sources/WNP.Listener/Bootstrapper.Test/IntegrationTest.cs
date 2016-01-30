// <copyright file="IntegrationTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Test
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ApplicationService;
    using ApplicationService.Test;
    using Communication.Jms;
    using Core;
    using Core.Ninject.Test;
    using Domain;
    using Domain.Listener;
    using Domain.Listener.Transaction;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Persistence.Listener;
    using Persistence.Listener.Models;
    using Repository;
    using Repository.Listener;
    using Repository.WNP;
    using Repository.WNP.Model;
    using Shared;

    [TestClass]
    public class IntegrationTest
    {
        private const string CompanyCode = "PPL";
        private const string CompanyId = "0";
        private const string ApplicationCode = "dde3ff6d-e368-4427-b75e-6ec47183f88e";
        private static TestServer server;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            ApplicationIntegration.SetDependencyInjectionResolver(new TestDependencyInjectionAdapter());
            server = TestServer.Create<Startup>();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;
            di.Reset();
        }

        #region Test
        [TestMethod]
        public async Task OpenAndSucceedTransactionWithoutAutoSucceedTest()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(nextKey.ToString("D"));
            var entityKey = Guid.NewGuid().ToString("D");
            string expectedMessage = $"{{\"Data\":{{\"Test\":\"A1-S2-D3\",\"UserName\":\"ListenerUser\",\"EntityCategory\":\"EM\",\"EntityKey\":\"{entityKey}\",\"OperationKey\":\"Add\"}}}}";
            object receivedData = string.Empty;

            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            communicationHandlerMock.Setup(
                s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.Is<TransactionDataReady>(dr => dr.RecordKey == nextKey), It.IsAny<ProtocolConfiguration>()))
                .Callback((IConnectionConfiguration conn, TransactionDataReady data, IProtocolConfiguration pcfg) =>
                {
                    receivedData = data.Data;
                });

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var responseMessage = await OpenTransaction(server, entityKey, "EM", "Add");
            Assert.AreEqual(1, responseMessage.Length);
            Assert.AreEqual(nextKey, responseMessage.Single());

            await ProcessTransaction(server, nextKey);
            communicationHandler.Verify(
                s =>
                    s.Handle(
                        It.Is<TransactionDataReady>(
                            ready =>
                                string.Compare(JsonConvert.SerializeObject(ready.Data), expectedMessage,
                                    StringComparison.InvariantCulture) == 0), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()),
                Times.Once, "Received unexpected {0}{1}Expected: {2}".FormatWith(JsonConvert.SerializeObject(receivedData), Environment.NewLine, expectedMessage));

            var transactionStatusId = await GetTransactionStatus(server, nextKey);

            Assert.AreEqual(TransactionStatusType.Processing, (TransactionStatusType)transactionStatusId);

            await SucceedTransaction(server, nextKey);

            transactionStatusId = await GetTransactionStatus(server, nextKey);

            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId);

            var transactionMessageData = await GetTransactionData(server, nextKey);
            Assert.AreEqual(expectedMessage, transactionMessageData);
        }


        [TestMethod]
        public async Task OpenAndAutoSucceedTransactionTest()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(nextKey.ToString("D"));
            var entityKey = Guid.NewGuid().ToString("D");
            string expectedMessage = $"{{\"Data\":{{\"Test\":\"A1-S2-D3\",\"UserName\":\"ListenerUser\",\"EntityCategory\":\"EM\",\"EntityKey\":\"{entityKey}\",\"OperationKey\":\"Install\"}}}}";
            object receivedData = string.Empty;

            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            communicationHandlerMock.Setup(
                s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.Is<TransactionDataReady>(dr => dr.RecordKey == nextKey), It.IsAny<ProtocolConfiguration>()))
                .Callback((IConnectionConfiguration conn, TransactionDataReady data, IProtocolConfiguration pcfg) =>
                {
                    receivedData = data.Data;
                });

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var responseMessage = await OpenTransaction(server, entityKey, "EM", "Install");
            Assert.AreEqual(1, responseMessage.Length);
            Assert.AreEqual(nextKey, responseMessage.Single());

            await ProcessTransaction(server, nextKey);
            communicationHandler.Verify(
                s =>
                    s.Handle(
                        It.Is<TransactionDataReady>(
                            ready =>
                                string.Compare(JsonConvert.SerializeObject(ready.Data), expectedMessage,
                                    StringComparison.InvariantCulture) == 0), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()),
                Times.Once, "Received unexpected {0}{1}Expected: {2}".FormatWith(JsonConvert.SerializeObject(receivedData), Environment.NewLine, expectedMessage));

            var transactionStatusId = await GetTransactionStatus(server, nextKey);

            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId);

            var transactionMessageData = await GetTransactionData(server, nextKey);
            Assert.AreEqual(expectedMessage, transactionMessageData);
        }

        [TestMethod]
        public async Task OpenAndSkipTransactionTest()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(() => Guid.NewGuid().ToString("D"));
            var entityKey = Guid.NewGuid().ToString("D");
            var communicationHandler = new Mock<ICommunicationHandler>();
            communicationHandler.Setup(
                s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()))
                .Returns(Task.CompletedTask);

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var transactionRecordKeys1 = await OpenTransaction(server, entityKey, "EM", "Add");
            var transactionRecordKeys2 = await OpenTransaction(server, entityKey, "EM", "Add");
            Assert.AreEqual(1, transactionRecordKeys1.Length);
            Assert.AreEqual(1, transactionRecordKeys2.Length);
            await ProcessTransaction(server, transactionRecordKeys1.Single());
            await SucceedTransaction(server, transactionRecordKeys1.Single());

            await ProcessTransaction(server, transactionRecordKeys2.Single());

            var transactionStatusId1 = await GetTransactionStatus(server, transactionRecordKeys1.Single());
            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId1);

            var transactionStatusId2 = await GetTransactionStatus(server, transactionRecordKeys2.Single());
            Assert.AreEqual(TransactionStatusType.Skipped, (TransactionStatusType)transactionStatusId2);

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Once);
        }

        [TestMethod]
        public async Task OpenAndFailTransactionTest()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(() => Guid.NewGuid().ToString("D"));
            var entityKey = Guid.NewGuid().ToString("D");
            var communicationHandler = new Mock<ICommunicationHandler>();
            communicationHandler.Setup(
                s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()))
                .Returns(Task.CompletedTask);

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var recordKeys = await OpenTransaction(server, entityKey);
            Assert.AreEqual(1, recordKeys.Length);
            await ProcessTransaction(server, recordKeys.Single());

            await FailTransaction(server, recordKeys.Single(), "Test Failure Message", "Test Failure Details");

            var transactionStatusId1 = await GetTransactionStatus(server, recordKeys.Single());
            Assert.AreEqual(TransactionStatusType.Failed, (TransactionStatusType)transactionStatusId1);

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Once);
        }

        [TestMethod]
        public async Task OpenAndSucceedBatchAsRoot()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;

            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var keyStack = new Queue<string>();
            keyStack.Enqueue(nextKey.ToString("D"));
            for (int i = 0; i < 100; i++)
            {
                keyStack.Enqueue(Guid.NewGuid().ToString("D"));
            }

            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(keyStack.Dequeue);
            string expectedMessageTemplate = "{{\"Data\":{{\"UserName\":\"ListenerUser\",\"EntityCategory\":\"{0}\",\"EntityKey\":\"{1}\",\"OperationKey\":\"{2}\",\"Priority\":{3}}}}}";

            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            ConcurrentDictionary<Guid, object> receivedData = new ConcurrentDictionary<Guid, object>();

            communicationHandlerMock.Setup(s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.IsAny<TransactionDataReady>(), It.IsAny<ProtocolConfiguration>()))
                .Callback((IConnectionConfiguration conn, TransactionDataReady data, IProtocolConfiguration pcfg) =>
                {
                    while (!receivedData.TryAdd(data.RecordKey, data.Data))
                    {
                        Thread.Sleep(100);
                    }
                });

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var responseMessageReceived = await OpenBatchTransaction(server);
            Assert.AreEqual(1, responseMessageReceived.Length);
            Assert.AreEqual(nextKey, responseMessageReceived.Single());

            await ProcessTransaction(server, nextKey);

            Assert.AreEqual(10, receivedData.Count);

            List<string> messages =
                receivedData.Select(o => (o.Value as TransactionMessage).Data as IDictionary<string, object>)
                    .Select(
                        data =>
                            expectedMessageTemplate.FormatWith(
                                data["EntityCategory"],
                                data["EntityKey"],
                                data["OperationKey"],
                                data["Priority"] ?? "null"))
                    .ToList();

            communicationHandler.Verify(
                s =>
                    s.Handle(
                        It.Is<TransactionDataReady>(
                            ready => messages.Count(m =>
                                string.Compare(JsonConvert.SerializeObject(ready.Data), m,
                                    StringComparison.InvariantCulture) == 0) == 1), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()),
                Times.AtLeastOnce, "Received unexpected message. Should be in: {0}".FormatWith(string.Join(Environment.NewLine, messages)));

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Exactly(10));

            await SucceedTransaction(server, nextKey);

            var transactionStatusId = await GetTransactionStatus(server, nextKey);

            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId);

            var childStatuses = await GetChildTransactionStatus(server, nextKey);
            Assert.IsTrue(childStatuses.All(c => (TransactionStatusType)c.Value == TransactionStatusType.Success));
        }

        [TestMethod]
        public async Task OpenAndFailBatchAsRoot()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var keyStack = new Queue<string>();
            keyStack.Enqueue(nextKey.ToString("D"));
            for (int i = 0; i < 100; i++)
            {
                keyStack.Enqueue(Guid.NewGuid().ToString("D"));
            }

            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(keyStack.Dequeue);
            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            communicationHandlerMock.Setup(s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.IsAny<TransactionDataReady>(), It.IsAny<ProtocolConfiguration>()));

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var transactionRecordKey1Received = await OpenBatchTransaction(server);
            Assert.AreEqual(1, transactionRecordKey1Received.Length);
            var transactionRecordKey1 = transactionRecordKey1Received.Single();
            Assert.AreEqual(nextKey, transactionRecordKey1);

            await FailTransaction(server, transactionRecordKey1, "Batch Failure Message", "Batch failure details");

            var transactionStatusId1 = await GetTransactionStatus(server, transactionRecordKey1);
            Assert.AreEqual(TransactionStatusType.Failed, (TransactionStatusType)transactionStatusId1);

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Never);

            var childStatuses = await GetChildTransactionStatus(server, nextKey);
            Assert.IsTrue(childStatuses.All(c => (TransactionStatusType)c.Value == TransactionStatusType.Canceled));
        }

        [TestMethod]
        public async Task OpenAndFailBatchAsIndividualNoPriority()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var keyStack = new Queue<string>();
            keyStack.Enqueue(nextKey.ToString("D"));
            for (int i = 0; i < 100; i++)
            {
                keyStack.Enqueue(Guid.NewGuid().ToString("D"));
            }

            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(keyStack.Dequeue);
            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            communicationHandlerMock.Setup(s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.IsAny<TransactionDataReady>(), It.IsAny<ProtocolConfiguration>())).Throws<NotImplementedException>();

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var transactionRecordKey1Received = await OpenBatchTransaction(server);
            Assert.AreEqual(1, transactionRecordKey1Received.Length);
            var transactionRecordKey1 = transactionRecordKey1Received.Single();
            Assert.AreEqual(nextKey, transactionRecordKey1);

            await ProcessTransaction(server, nextKey, HttpStatusCode.InternalServerError);

            var transactionStatusId1 = await GetTransactionStatus(server, transactionRecordKey1);

            Assert.AreEqual(TransactionStatusType.Failed, (TransactionStatusType)transactionStatusId1);

            var childStatuses = await GetChildTransactionStatus(server, nextKey);
            Assert.IsTrue(childStatuses.Count > 2);
            Assert.IsTrue(childStatuses.All(c => (TransactionStatusType)c.Value == TransactionStatusType.Failed));
            // Assert.AreEqual(1, childStatuses.Count(c => (TransactionStatusType)c.Value == TransactionStatusType.Failed));
            //Assert.AreEqual(childStatuses.Count - 1, childStatuses.Count(c => (TransactionStatusType)c.Value == TransactionStatusType.Canceled));

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Exactly(childStatuses.Count));
        }

        [TestMethod]
        public async Task OpenAndFailBatchAsIndividualWithPriority()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var keyStack = new Queue<string>();
            keyStack.Enqueue(nextKey.ToString("D"));
            for (int i = 0; i < 100; i++)
            {
                keyStack.Enqueue(Guid.NewGuid().ToString("D"));
            }

            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(keyStack.Dequeue);
            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            communicationHandlerMock.Setup(s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.IsAny<TransactionDataReady>(), It.IsAny<ProtocolConfiguration>())).Throws<NotImplementedException>();

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var priorities = Enumerable.Repeat(2, 100).Cast<int?>().ToList();
            priorities[0] = 1;
            priorities[1] = 1;
            priorities.Reverse();
            var pStack = new Stack<int?>(priorities);

            var transactionRecordKey1Received = await OpenBatchTransaction(server, priorities: pStack);
            Assert.AreEqual(1, transactionRecordKey1Received.Length);
            var transactionRecordKey1 = transactionRecordKey1Received.Single();
            Assert.AreEqual(nextKey, transactionRecordKey1);

            await ProcessTransaction(server, nextKey, HttpStatusCode.InternalServerError);

            var transactionStatusId1 = await GetTransactionStatus(server, transactionRecordKey1);
            Assert.AreEqual(TransactionStatusType.Failed, (TransactionStatusType)transactionStatusId1);

            var childStatuses = await GetChildTransactionStatus(server, nextKey);
            Assert.IsTrue(childStatuses.Count > 2);
            //Assert.IsTrue(childStatuses.All(c => (TransactionStatusType)c.Value == TransactionStatusType.Failed));
            Assert.AreEqual(2, childStatuses.Count(c => (TransactionStatusType)c.Value == TransactionStatusType.Failed));
            Assert.AreEqual(childStatuses.Count - 2, childStatuses.Count(c => (TransactionStatusType)c.Value == TransactionStatusType.Canceled));

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Exactly(2));
        }

        [TestMethod]
        public async Task OpenAndSkipBatchAsRoot()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var keyStack = new Queue<string>();
            keyStack.Enqueue(nextKey.ToString("D"));
            for (int i = 0; i < 100; i++)
            {
                keyStack.Enqueue(Guid.NewGuid().ToString("D"));
            }

            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(keyStack.Dequeue);
            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            communicationHandlerMock.Setup(s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.IsAny<TransactionDataReady>(), It.IsAny<ProtocolConfiguration>()));

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            List<string> entityKeys = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                entityKeys.Add(Guid.NewGuid().ToString("D"));
            }

            var transactionRecordKey1Received = await OpenBatchTransaction(server, entityKeys);
            Assert.AreEqual(1, transactionRecordKey1Received.Length);
            var transactionRecordKey1 = transactionRecordKey1Received.Single();
            Assert.AreEqual(nextKey, transactionRecordKey1);

            var transactionRecordKey2Received = await OpenBatchTransaction(server, entityKeys);
            Assert.AreEqual(1, transactionRecordKey2Received.Length);
            var transactionRecordKey2 = transactionRecordKey2Received.Single();
            Assert.AreNotEqual(nextKey, transactionRecordKey2);

            await ProcessTransaction(server, transactionRecordKey1);
            await SucceedTransaction(server, transactionRecordKey1);

            await ProcessTransaction(server, transactionRecordKey2);

            var transactionStatusId1 = await GetTransactionStatus(server, transactionRecordKey1);
            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId1);

            var transactionStatusId2 = await GetTransactionStatus(server, transactionRecordKey2);
            Assert.AreEqual(TransactionStatusType.Skipped, (TransactionStatusType)transactionStatusId2);

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Exactly(10));

            var childStatuses = await GetChildTransactionStatus(server, transactionRecordKey2);
            Assert.IsTrue(childStatuses.All(c => (TransactionStatusType)c.Value == TransactionStatusType.Canceled));
        }

        [TestMethod]
        public async Task CreateWnpBatch()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var seq = transactionRecordKeyBuilder.SetupSequence(s => s.Create()).Returns(nextKey.ToString("D"));
            for (int i = 0; i < 20; i++)
            {
                seq = seq.Returns(Guid.NewGuid().ToString("D"));
            }

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            var transactionRegistryMock = new Mock<TransactionRegistry>(di.ResolveType<IRecordKeyBuilder>(), di.ResolveType<ITransactionHashBuilder>(), di.ResolveType<ISummaryBuilder>()) { CallBase = true };

            var domainBuilderMock = new Mock<DomainBuilder> { CallBase = true };
            domainBuilderMock.Setup(s => s.Create<TransactionRegistry>()).Returns(transactionRegistryMock.Object);
            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            var batchRepository = new Mock<IWnpBatchRepository>();
            batchRepository.Setup(s => s.GetMeterTestBatchAsync(It.IsAny<string>())).ReturnsAsync(
                new List<Meter>
                {
                    new Meter
                    {
                        EquipmentNumber = "EQ1",
                        Owner = 0,
                        Tests =
                        {
                            new MeterTest
                            {
                                StartDate = DateTime.Now.AddDays(-50)
                            }, new MeterTest
                            {
                                StartDate = DateTime.Now.AddDays(-51)
                            }
                        }
                    },
                    new Meter
                    {
                        EquipmentNumber = "EQ2",
                        Owner = 0,
                        Tests =
                        {
                            new MeterTest
                            {
                                StartDate = DateTime.Now.AddDays(-30)
                            },
                            new MeterTest
                            {
                                StartDate = DateTime.Now.AddDays(-31)
                            }
                        }
                    },
                    new Meter
                    {
                        EquipmentNumber = "EQ3",
                        Owner = 0,
                        Tests =
                        {
                            new MeterTest
                            {
                                StartDate = DateTime.Now.AddDays(-10)
                            },
                            new MeterTest
                            {
                                StartDate = DateTime.Now.AddDays(-11)
                            }
                        }
                    }
                });
            di.Rebind<IWnpBatchRepository>(batchRepository.Object);
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var result = await OpenWnpBatchTransaction(server, "B1");

            transactionRegistryMock.Verify(
                t => t.Create(It.IsAny<DateTime>(), It.IsAny<Dictionary<int, IEnumerable<FieldConfiguration>>>()),
                Times.Exactly(1));

            di.Kernel.Release(transactionRecordKeyBuilder.Object);
        }

        [TestMethod]
        public async Task EndpointFailureAs500()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            di.Kernel.Bind<IRecordKeyBuilder>().ToConstant((IRecordKeyBuilder)null);
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(nextKey.ToString("D"));
            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            var entityKey = Guid.NewGuid().ToString("D");
            var transactionRegistryMock = new Mock<TransactionRegistry>(di.ResolveType<IRecordKeyBuilder>(), di.ResolveType<ITransactionHashBuilder>(), di.ResolveType<ISummaryBuilder>()) { CallBase = true };

            var domainBuilderMock = new Mock<DomainBuilder> { CallBase = true };
            domainBuilderMock.Setup(s => s.Create<TransactionRegistry>()).Returns(transactionRegistryMock.Object);

            di.Rebind<ICommunicationHandler>(new DummyCommunicationHandler()).Named("communication-jms");
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);

            var responseMessage = await OpenTransaction(server, entityKey);
            Assert.AreEqual(1, responseMessage.Length);
            Assert.AreEqual(nextKey, responseMessage.Single(), $"Expected {nextKey}, got {responseMessage} with entity key as {entityKey}");

            await ProcessTransaction(server, nextKey, HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task MultipleEntityCategoryOperationsTransactionOpenAndIndividualSucceedTest()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var nextKey1 = Guid.NewGuid();
            var keyStack = new Queue<string>();
            keyStack.Enqueue(nextKey.ToString("D"));
            keyStack.Enqueue(nextKey1.ToString("D"));

            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(keyStack.Dequeue);
            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();

            var entityKey = Guid.NewGuid().ToString("D");

            var communicationHandlerMock = new Mock<ICommunicationHandler>();
            di.Rebind<ICommunicationHandler>(communicationHandlerMock.Object).Named("communication-jms");

            var domainBuilderMock = new Mock<DomainBuilder> { CallBase = true };
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);

            var responseMessage = await OpenTransaction(server, entityKey, "EM", "Test");
            Assert.AreEqual(2, responseMessage.Length);
            Assert.IsTrue(responseMessage.Contains(nextKey));
            Assert.IsTrue(responseMessage.Contains(nextKey1));

            await ProcessTransaction(server, nextKey);
            await ProcessTransaction(server, nextKey1);

            await SucceedTransaction(server, nextKey);
            await SucceedTransaction(server, nextKey1);

            var transactionStatusId = await GetTransactionStatus(server, nextKey);

            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId);

            transactionStatusId = await GetTransactionStatus(server, nextKey1);

            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId);
        }

        #endregion

        #region Helper methods

        private static async Task SucceedTransaction(TestServer server, Guid nextKey)
        {
            HttpResponseMessage succeedResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry({nextKey})/AMSLLC.Listener.Succeed()")
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-CompanyId", CompanyId)
                        .AddHeader("AMS-Application", ApplicationCode).PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, succeedResponse.StatusCode);
        }

        private static async Task FailTransaction(TestServer server, Guid nextKey, string message, string details)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));

            HttpResponseMessage failResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry({nextKey})/AMSLLC.Listener.Fail()").And(
                            request =>
                                request.Content =
                                    new ObjectContent(
                                        typeof(FailureData),
                                        new FailureData
                                        {
                                            Message = message,
                                            Details = details
                                        },
                                        new JsonMediaTypeFormatter { SerializerSettings = settings },
                                        mediaType))
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-CompanyId", CompanyId)
                        .AddHeader("AMS-Application", ApplicationCode).PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, failResponse.StatusCode);
        }

        private static async Task<Guid[]> OpenTransaction(TestServer server, string entityKey, string entityCategory = "EM", string operationKey = "Install")
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));

            var requestMessage = new OpenTransactionWrapper<InstallMeterRequestMessage>(new InstallMeterRequestMessage
            {
                Test = "A1-S2-D3",
                EntityCategory = entityCategory,
                OperationKey = operationKey,
                EntityKey = entityKey
            });
            HttpResponseMessage response =
                await
                    server.CreateRequest("listener/Open")
                        .And(
                            request =>
                                request.Content =
                                    new ObjectContent(
                                        typeof(OpenTransactionWrapper<InstallMeterRequestMessage>),
                                        requestMessage,
                                        new JsonMediaTypeFormatter { SerializerSettings = settings },
                                        mediaType))
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-CompanyId", CompanyId)
                        .AddHeader("AMS-Application", ApplicationCode)
                        .PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(responseBody) as IDictionary<string, object>;
            var responseMessage = ((List<Object>)expando["value"]).Select(s => Guid.Parse(s.ToString()));
            return responseMessage.ToArray();
        }

        private static async Task<Guid[]> OpenWnpBatchTransaction(TestServer server, string batchKey)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            //var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            //mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));

            HttpResponseMessage response =
                await
                    server.CreateRequest($"listener/BuildBatch").And(request =>
                    request.Content = new StringContent($"={batchKey}", Encoding.UTF8, "application/x-www-form-urlencoded"))
                        //request.Content = new ObjectContent(
                        //         typeof(string),
                        //         $"={batchKey}",
                        //         new JsonMediaTypeFormatter { SerializerSettings = settings },
                        //         mediaType))
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-CompanyId", CompanyId)
                        .AddHeader("AMS-Application", ApplicationCode)
                        .PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(responseBody) as IDictionary<string, object>;
            var responseMessage = ((List<Object>)expando["value"]).Select(s => Guid.Parse(s.ToString()));
            return responseMessage.ToArray();
        }

        private static async Task<Guid[]> OpenBatchTransaction(TestServer server, ICollection<string> entityKeys = null, Stack<int?> priorities = null)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));

            var requestMessage = new BatchRequestMessage { BatchNumber = "TB1" };

            Dictionary<string, List<string>> operations = new Dictionary<string, List<string>>
            {
                {
                    "EM", new List<string>
                    {
                        "Install"
                    }
                },
                {
                    "Circuits", new List<string>
                    {
                        "Add"
                    }
                },
                {
                    "Sites", new List<string>
                    {
                        "Add"
                    }
                },
                {
                    "Users", new List<string>
                    {
                        "Add"
                    }
                },
                {
                    "Vehicles", new List<string>
                    {
                        "Add"
                    }
                }
            };
            var p = priorities ?? new Stack<int?>(Enumerable.Repeat((int?)null, 100));
            List<BatchTransactionEntry> body = new List<BatchTransactionEntry>();
            for (int i = 0; i < operations.Keys.Count; i++)
            {
                body.Add(new BatchTransactionEntry
                {
                    EntityCategory = operations.Keys.ElementAt(i),
                    OperationKey = operations[operations.Keys.ElementAt(i)].First(),
                    EntityKey = entityKeys == null ? Guid.NewGuid().ToString() : entityKeys.ElementAt(body.Count),
                    Priority = p.Pop()
                });
                body.Add(new BatchTransactionEntry
                {
                    EntityCategory = operations.Keys.ElementAt(operations.Keys.Count - i - 1),
                    OperationKey = operations[operations.Keys.ElementAt(operations.Keys.Count - i - 1)].First(),
                    EntityKey = entityKeys == null ? Guid.NewGuid().ToString() : entityKeys.ElementAt(body.Count),
                    Priority = p.Pop()
                });
            }

            requestMessage.Body = JsonConvert.SerializeObject(body);

            HttpResponseMessage response =
                await
                    server.CreateRequest("listener/Batch")
                        .And(
                            request =>
                                request.Content =
                                    new ObjectContent(
                                        typeof(BatchRequestMessage),
                                        requestMessage,
                                        new JsonMediaTypeFormatter { SerializerSettings = settings },
                                        mediaType))
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-CompanyId", CompanyId)
                        .AddHeader("AMS-Application", ApplicationCode)
                        .PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(responseBody) as IDictionary<string, object>;
            var responseMessage = ((List<Object>)expando["value"]).Select(s => Guid.Parse(s.ToString()));
            return responseMessage.ToArray();
        }

        private static async Task ProcessTransaction(TestServer server, Guid nextKey, HttpStatusCode expectedCode = HttpStatusCode.OK)
        {
            HttpResponseMessage processResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry({nextKey})/AMSLLC.Listener.Process()")
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-Application", ApplicationCode).PostAsync();

            Assert.AreEqual(expectedCode, processResponse.StatusCode);
        }

        private static async Task<long> GetTransactionStatus(TestServer server, Guid nextKey)
        {
            HttpResponseMessage registryEntry =
                await
                    server.CreateRequest($"listener/TransactionRegistry?$filter=RecordKey%20eq%20{nextKey}")
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-CompanyId", CompanyId)
                        .AddHeader("AMS-Application", ApplicationCode).GetAsync();
            string rstr = await registryEntry.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, registryEntry.StatusCode, rstr);

            var ex = JsonConvert.DeserializeObject<ExpandoObject>(rstr) as IDictionary<string, object>;
            long transactionStatusId =
                (long)((ex["value"] as List<object>).Single() as IDictionary<string, object>)["TransactionStatusId"];
            return transactionStatusId;
        }

        private static async Task<Dictionary<Guid, long>> GetChildTransactionStatus(TestServer server, Guid nextKey)
        {
            Dictionary<Guid, long> returnValue = new Dictionary<Guid, long>();
            HttpResponseMessage registryEntry =
                await
                    server.CreateRequest($"listener/TransactionRegistry?$filter=BatchKey%20eq%20{nextKey}")
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-Application", ApplicationCode).GetAsync();
            string rstr = await registryEntry.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, registryEntry.StatusCode, rstr);

            var ex = JsonConvert.DeserializeObject<ExpandoObject>(rstr) as IDictionary<string, object>;
            var items = ex["value"] as List<object>;
            foreach (var e in items)
            {
                long transactionStatusId = (long)(e as IDictionary<string, object>)["TransactionStatusId"];
                Guid recordKey = Guid.Parse((string)(e as IDictionary<string, object>)["RecordKey"]);
                returnValue.Add(recordKey, transactionStatusId);
            }

            return returnValue;
        }

        private static async Task<string> GetTransactionData(TestServer server, Guid nextKey)
        {
            HttpResponseMessage registryEntry =
                await
                    server.CreateRequest($"listener/TransactionMessageData?$filter=RecordKey%20eq%20{nextKey}")
                        .AddHeader("AMS-Company", CompanyCode)
                        .AddHeader("AMS-CompanyId", CompanyId)
                        .AddHeader("AMS-Application", ApplicationCode).GetAsync();
            string rstr = await registryEntry.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, registryEntry.StatusCode, rstr);

            var ex = JsonConvert.DeserializeObject<ExpandoObject>(rstr) as IDictionary<string, object>;
            var recordKey = Guid.Parse((string)((ex["value"] as List<object>).Single() as IDictionary<string, object>)["RecordKey"]);
            Assert.AreEqual(nextKey, recordKey);
            string returnValue = (string)((ex["value"] as List<object>).Single() as IDictionary<string, object>)["MessageData"];
            return returnValue;
        }
        #endregion

        #region Test models

        private class OpenTransactionWrapper<T>
            where T : IOpenTransactionData
        {
            public string EntityCategory { get; set; }

            public string OperationKey { get; set; }

            public string Body { get; set; }

            public OpenTransactionWrapper(T data)
            {
                this.EntityCategory = data.EntityCategory;
                this.OperationKey = data.OperationKey;
                this.Body = JsonConvert.SerializeObject(data);
            }
        }

        private interface IOpenTransactionData
        {
            string EntityCategory { get; }

            string OperationKey { get; }
        }

        private class
            InstallMeterRequestMessage : IOpenTransactionData
        {
            public string Test { get; set; }

            public string UserName { get; set; }

            public string EntityCategory { get; set; }

            public string EntityKey { get; set; }

            public string OperationKey { get; set; }
        }

        private class BatchRequestMessage
        {
            public string Body { get; set; }

            public string BatchNumber { get; set; }
        }

        private class BatchTransactionEntry
        {
            public string UserName { get; set; }

            public string EntityCategory { get; set; }

            public string EntityKey { get; set; }

            public object OperationKey { get; set; }

            public int? Priority { get; set; }
        }

        private class FailureData
        {
            public string Message { get; set; }

            public string Details { get; set; }
        }
        #endregion
    }
}