﻿// <copyright file="IntegrationTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
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
    using Communication;
    using Communication.Jms;
    using Core;
    using Core.Ninject.Test;
    using Domain;
    using Domain.Listener.Transaction;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Repository;
    using Repository.WNP;
    using Repository.WNP.Model;

    [TestClass]
    public class IntegrationTest
    {
        private static TestServer _server;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            ApplicationIntegration.SetDependencyInjectionResolver(new TestDependencyInjectionAdapter());
            _server = TestServer.Create<Startup>();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {

        }

        [TestInitialize()]
        public void Initialize()
        {

        }

        [TestCleanup()]
        public void Cleanup()
        {
            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;
            di.Reset();
            //di.Dispose();
            //_server.Dispose();
        }

        #region Test
        [TestMethod]
        public async Task OpenAndSucceedTransactionTest()
        {

            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(nextKey.ToString("D"));
            var entityKey = Guid.NewGuid().ToString("D");
            string expectedMessage = $"{{\"Data\":{{\"Test\":\"A1-S2-D3\",\"UserName\":\"ListenerUser\",\"EntityCategory\":\"ElectricMeters\",\"EntityKey\":\"{entityKey}\",\"OperationKey\":\"Install\"}}}}";
            object receivedData = string.Empty;

            //var cdi = ApplicationIntegration.DependencyResolver.WithScope(Guid.NewGuid().ToString("D"), "ApplicationScopeModule");
            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();
            //var transactionMessageDataRepository = new Mock<TransactionDataRepository> { CallBase = true };
            communicationHandlerMock.Setup(
                s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.Is<TransactionDataReady>(dr => dr.RecordKey == nextKey), It.IsAny<ProtocolConfiguration>()))
                //.Returns(Task.CompletedTask)
                .Callback((IConnectionConfiguration conn, TransactionDataReady data, IProtocolConfiguration pcfg) =>
                {
                    //var rData = (TransactionDataReady)data;
                    receivedData = data.Data;
                    //transactionMessageDataRepository.Object.SaveDataAsync(rData.RecordKey, rData.Data);
                });

            //\"RecordKey\":\"{nextKey}\"

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            //di.Rebind<ITransactionDataRepository>().ToConstant(transactionMessageDataRepository.Object);

            var responseMessage = await OpenTransaction(_server, entityKey);
            Assert.AreEqual(nextKey, responseMessage);

            await ProcessTransaction(_server, nextKey);
            communicationHandler.Verify(
                s =>
                    s.Handle(
                        It.Is<TransactionDataReady>(
                            ready =>
                                string.Compare(JsonConvert.SerializeObject(ready.Data), expectedMessage,
                                    StringComparison.InvariantCulture) == 0), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()),
                Times.Once, "Received unexpected {0}{1}Expected: {2}".FormatWith(JsonConvert.SerializeObject(receivedData), Environment.NewLine, expectedMessage));

            await SucceedTransaction(_server, nextKey);

            var transactionStatusId = await GetTransactionStatus(_server, nextKey);

            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId);

            var transactionMessageData = await GetTransactionData(_server, nextKey);
            Assert.AreEqual(expectedMessage, transactionMessageData);
            //transactionMessageDataRepository.Verify(s => s.SaveDataAsync(nextKey, It.Is<object>((obj) => string.CompareOrdinal(JsonConvert.SerializeObject(obj), expectedMessage) == 0)), Times.Once);

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

            var transactionRecordKey1 = await OpenTransaction(_server, entityKey);
            var transactionRecordKey2 = await OpenTransaction(_server, entityKey);

            await ProcessTransaction(_server, transactionRecordKey1);
            await SucceedTransaction(_server, transactionRecordKey1);

            await ProcessTransaction(_server, transactionRecordKey2);

            var transactionStatusId1 = await GetTransactionStatus(_server, transactionRecordKey1);
            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId1);

            var transactionStatusId2 = await GetTransactionStatus(_server, transactionRecordKey2);
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

            var recordKey = await OpenTransaction(_server, entityKey);

            await ProcessTransaction(_server, recordKey);

            await FailTransaction(_server, recordKey, "Test Failure Message", "Test Failure Details");

            var transactionStatusId1 = await GetTransactionStatus(_server, recordKey);
            Assert.AreEqual(TransactionStatusType.Failed, (TransactionStatusType)transactionStatusId1);

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Once);

        }

        [TestMethod]
        public async Task OpenAndSucceedBatchAsRoot()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;

            //var childId = ApplicationIntegration.DependencyResolver.WithScope(Guid.NewGuid().ToString("D"), "ApplicationScopeModule");

            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            var keyStack = new Queue<string>();
            keyStack.Enqueue(nextKey.ToString("D"));
            for (int i = 0; i < 100; i++)
            {
                keyStack.Enqueue(Guid.NewGuid().ToString("D"));
            }

            transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(keyStack.Dequeue);
            string expectedMessageTemplate = "{{\"Data\":{{\"UserName\":\"ListenerUser\",\"EntityCategory\":\"{0}\",\"EntityKey\":\"{1}\",\"OperationKey\":\"{2}\"}}}}";

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

            var responseMessage = await OpenBatchTransaction(_server);
            Assert.AreEqual(nextKey, responseMessage);

            await ProcessTransaction(_server, nextKey);

            Assert.AreEqual(10, receivedData.Count);

            List<string> messages =
                receivedData.Select(o => (o.Value as TransactionMessage).Data as IDictionary<string, object>)
                    .Select(
                        data =>
                            expectedMessageTemplate.FormatWith(
                                data["EntityCategory"],
                                data["EntityKey"],
                                data["OperationKey"]))
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

            await SucceedTransaction(_server, nextKey);

            var transactionStatusId = await GetTransactionStatus(_server, nextKey);

            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId);

            var childStatuses = await GetChildTransactionStatus(_server, nextKey);
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
            //var cdi = ApplicationIntegration.DependencyResolver.WithScope(Guid.NewGuid().ToString("D"), "ApplicationScopeModule");
            var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
            var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();

            communicationHandlerMock.Setup(s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.IsAny<TransactionDataReady>(), It.IsAny<ProtocolConfiguration>()));

            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");

            var transactionRecordKey1 = await OpenBatchTransaction(_server);
            Assert.AreEqual(nextKey, transactionRecordKey1);

            await FailTransaction(_server, transactionRecordKey1, "Batch Failure Message", "Batch failure details");

            var transactionStatusId1 = await GetTransactionStatus(_server, transactionRecordKey1);
            Assert.AreEqual(TransactionStatusType.Failed, (TransactionStatusType)transactionStatusId1);

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Never);

            var childStatuses = await GetChildTransactionStatus(_server, nextKey);
            Assert.IsTrue(childStatuses.All(c => (TransactionStatusType)c.Value == TransactionStatusType.Canceled));
        }

        [TestMethod]
        public async Task OpenAndFailBatchAsIndividual()
        {
            Assert.Inconclusive("Implement this");
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
            //var cdi = ApplicationIntegration.DependencyResolver.WithScope(Guid.NewGuid().ToString("D"), "ApplicationScopeModule");
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

            var transactionRecordKey1 = await OpenBatchTransaction(_server, entityKeys);
            Assert.AreEqual(nextKey, transactionRecordKey1);

            var transactionRecordKey2 = await OpenBatchTransaction(_server, entityKeys);
            Assert.AreNotEqual(nextKey, transactionRecordKey2);

            await ProcessTransaction(_server, transactionRecordKey1);
            await SucceedTransaction(_server, transactionRecordKey1);

            await ProcessTransaction(_server, transactionRecordKey2);

            var transactionStatusId1 = await GetTransactionStatus(_server, transactionRecordKey1);
            Assert.AreEqual(TransactionStatusType.Success, (TransactionStatusType)transactionStatusId1);

            var transactionStatusId2 = await GetTransactionStatus(_server, transactionRecordKey2);
            Assert.AreEqual(TransactionStatusType.Skipped, (TransactionStatusType)transactionStatusId2);

            communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Exactly(10));

            var childStatuses = await GetChildTransactionStatus(_server, transactionRecordKey2);
            Assert.IsTrue(childStatuses.All(c => (TransactionStatusType)c.Value == TransactionStatusType.Canceled));
        }

        [TestMethod]
        public async Task CreateWnpBatch()
        {
            var di = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
            var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
            var nextKey = Guid.NewGuid();
            //var keyStack = new Queue<string>();
            //keyStack.Enqueue(nextKey.ToString("D"));
            var seq = transactionRecordKeyBuilder.SetupSequence(s => s.Create()).Returns(nextKey.ToString("D"));
            for (int i = 0; i < 200; i++)
            {
                seq = seq.Returns(Guid.NewGuid().ToString("D"));
                //keyStack.Enqueue();
            }



            di.Rebind<IRecordKeyBuilder>(transactionRecordKeyBuilder.Object).InSingletonScope();
            var transactionRegistryMock = new Mock<TransactionRegistry>(di.ResolveType<IRecordKeyBuilder>(), di.ResolveType<ITransactionHashBuilder>(), di.ResolveType<ISummaryBuilder>()) { CallBase = true };

            var domainBuilderMock = new Mock<DomainBuilder> { CallBase = true };
            domainBuilderMock.Setup(s => s.Create<TransactionRegistry>()).Returns(transactionRegistryMock.Object);
            //var cdi = (TestDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver.WithScope(Guid.NewGuid().ToString("D"), "ApplicationScopeModule");
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

            var result = await OpenWnpBatchTransaction(_server, "B1");

            transactionRegistryMock.Verify(
                t => t.Create(It.IsAny<DateTime>(), It.IsAny<Dictionary<int, IEnumerable<FieldConfiguration>>>()),
                Times.Exactly(1));

        }

        #endregion

        #region Helper methods

        private static async Task SucceedTransaction(TestServer server, Guid nextKey)
        {
            HttpResponseMessage succeedResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry({nextKey})/AMSLLC.Listener.Succeed()")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

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
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, failResponse.StatusCode);
        }

        private static async Task<Guid> OpenTransaction(TestServer server, string entityKey)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));

            var requestMessage = new InstallMeterRequestMessage
            {
                Test = "A1-S2-D3",
                EntityCategory = "ElectricMeters",
                OperationKey = "Install",
                EntityKey = entityKey
            };
            HttpResponseMessage response =
                await
                    server.CreateRequest("listener/Open")
                        .And(
                            request =>
                                request.Content =
                                    new ObjectContent(
                                        typeof(InstallMeterRequestMessage),
                                        requestMessage,
                                        new JsonMediaTypeFormatter { SerializerSettings = settings },
                                        mediaType))
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e")
                        .PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(responseBody) as IDictionary<string, object>;
            var responseMessage = Guid.Parse(expando["value"].ToString());
            return responseMessage;
        }

        private static async Task<Guid> OpenWnpBatchTransaction(TestServer server, string batchKey)
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
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e")
                        .PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(responseBody) as IDictionary<string, object>;
            var responseMessage =Guid.Parse(expando["value"]?.ToString());
            return responseMessage;
        }

        private static async Task<Guid> OpenBatchTransaction(TestServer server, ICollection<string> entityKeys = null)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));

            var requestMessage = new BatchRequestMessage();

            Dictionary<string, List<string>> operations = new Dictionary<string, List<string>>
            {
                {
                    "ElectricMeters", new List<string>
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

            for (int i = 0; i < operations.Keys.Count; i++)
            {
                requestMessage.Body.Add(new BatchTransactionEntry
                {
                    EntityCategory = operations.Keys.ElementAt(i),
                    OperationKey = operations[operations.Keys.ElementAt(i)].First(),
                    EntityKey = entityKeys == null ? Guid.NewGuid().ToString() : entityKeys.ElementAt(requestMessage.Body.Count)
                });
                requestMessage.Body.Add(new BatchTransactionEntry
                {
                    EntityCategory = operations.Keys.ElementAt(operations.Keys.Count - i - 1),
                    OperationKey = operations[operations.Keys.ElementAt(operations.Keys.Count - i - 1)].First(),
                    EntityKey = entityKeys == null ? Guid.NewGuid().ToString() : entityKeys.ElementAt(requestMessage.Body.Count)
                });
            }

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
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e")
                        .PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            string responseBody = await response.Content.ReadAsStringAsync();
            var expando = JsonConvert.DeserializeObject<ExpandoObject>(responseBody) as IDictionary<string, object>;
            var responseMessage = Guid.Parse(expando["value"].ToString());
            return responseMessage;
        }

        private static async Task ProcessTransaction(TestServer server, Guid nextKey)
        {
            HttpResponseMessage processResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry({nextKey})/AMSLLC.Listener.Process()")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, processResponse.StatusCode);
        }

        private static async Task<long> GetTransactionStatus(TestServer server, Guid nextKey)
        {
            HttpResponseMessage registryEntry =
                await
                    server.CreateRequest($"listener/TransactionRegistry?$filter=RecordKey%20eq%20{nextKey}")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").GetAsync();
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
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").GetAsync();
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
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").GetAsync();
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
        private class
            InstallMeterRequestMessage
        {
            public string Test { get; set; }

            public string UserName { get; set; }

            public string EntityCategory { get; set; }

            public string EntityKey { get; set; }

            public string OperationKey { get; set; }
        }

        private class BatchRequestMessage
        {
            public List<BatchTransactionEntry> Body { get; } = new List<BatchTransactionEntry>();
        }

        private class BatchTransactionEntry
        {
            public string UserName { get; set; }

            public string EntityCategory { get; set; }

            public string EntityKey { get; set; }

            public object OperationKey { get; set; }
        }

        private class FailureData
        {
            public string Message { get; set; }

            public string Details { get; set; }
        }
        #endregion
    }
}