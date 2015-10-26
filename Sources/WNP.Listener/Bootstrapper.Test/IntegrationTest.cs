﻿// <copyright file="IntegrationTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Test
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Communication;
    using Communication.Jms;
    using Core;
    using Core.Ninject;
    using Core.Ninject.Test;
    using Domain.Listener.Transaction;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Repository;
    using Repository.Listener;

    [TestClass]
    public class IntegrationTest
    {
        // [TestInitialize()]
        // public void Initialize() { }

        // [TestCleanup()]
        // public void Cleanup() { }
        [TestMethod]
        public async Task OpenAndSucceedTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var di = (NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
                var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
                string nextKey = Guid.NewGuid().ToString("D");
                transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(nextKey);
                var entityKey = Guid.NewGuid().ToString("D");
                string expectedMessage = $"{{\"Data\":{{\"Test\":\"A1-S2-D3\",\"UserName\":\"ListenerUser\",\"EntityCategory\":\"ElectricMeters\",\"EntityKey\":\"{entityKey}\",\"OperationKey\":\"Install\"}}}}";
                object receivedData = string.Empty;

                var communicationHandlerMock = new Mock<JmsDispatcher>(di.ResolveType<ITransactionDataRepository>()) { CallBase = true };
                var communicationHandler = communicationHandlerMock.As<ICommunicationHandler>();
                //var transactionMessageDataRepository = new Mock<TransactionDataRepository> { CallBase = true };
                communicationHandlerMock.Setup(
                    s => s.PutMessage(It.IsAny<JmsConnectionConfiguration>(), It.Is<TransactionDataReady>(dr => string.CompareOrdinal(dr.RecordKey, nextKey) == 0), It.IsAny<ProtocolConfiguration>()))
                    //.Returns(Task.CompletedTask)
                    .Callback((IConnectionConfiguration conn, TransactionDataReady data, IProtocolConfiguration pcfg) =>
                    {
                        //var rData = (TransactionDataReady)data;
                        receivedData = data.Data;
                        //transactionMessageDataRepository.Object.SaveDataAsync(rData.RecordKey, rData.Data);
                    });

                //\"RecordKey\":\"{nextKey}\"

                di.Kernel.Rebind<IRecordKeyBuilder>().ToConstant(transactionRecordKeyBuilder.Object).InSingletonScope();
                di.Kernel.Rebind<ICommunicationHandler>().ToConstant(communicationHandler.Object).InSingletonScope().Named("communication-jms");
                //di.Kernel.Rebind<ITransactionDataRepository>().ToConstant(transactionMessageDataRepository.Object);

                var responseMessage = await OpenTransaction(server, entityKey);
                Assert.AreEqual(nextKey, responseMessage);

                await ProcessTransaction(server, nextKey);
                communicationHandler.Verify(
                    s =>
                        s.Handle(
                            It.Is<TransactionDataReady>(
                                ready =>
                                    string.Compare(JsonConvert.SerializeObject(ready.Data), expectedMessage,
                                        StringComparison.InvariantCulture) == 0), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()),
                    Times.Once, "Received unexpected {0}{1}Expected: {2}".FormatWith(JsonConvert.SerializeObject(receivedData), Environment.NewLine, expectedMessage));

                await SucceedTransaction(server, nextKey);

                var transactionStatusId = await GetTransactionStatus(server, nextKey);

                Assert.AreEqual(0L, transactionStatusId);

                var transactionMessageData = await GetTransactionData(server, nextKey);
                Assert.AreEqual(expectedMessage, transactionMessageData);
                //transactionMessageDataRepository.Verify(s => s.SaveDataAsync(nextKey, It.Is<object>((obj) => string.CompareOrdinal(JsonConvert.SerializeObject(obj), expectedMessage) == 0)), Times.Once);
            }
        }

        [TestMethod]
        public async Task OpenAndSkipTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var di = (NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
                var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
                transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(() => Guid.NewGuid().ToString("D"));
                var entityKey = Guid.NewGuid().ToString("D");
                var communicationHandler = new Mock<ICommunicationHandler>();
                communicationHandler.Setup(
                    s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()))
                    .Returns(Task.CompletedTask);

                di.Kernel.Rebind<IRecordKeyBuilder>().ToConstant(transactionRecordKeyBuilder.Object).InSingletonScope();
                di.Kernel.Rebind<ICommunicationHandler>().ToConstant(communicationHandler.Object).InSingletonScope().Named("communication-jms");

                var transactionRecordKey1 = await OpenTransaction(server, entityKey);
                var transactionRecordKey2 = await OpenTransaction(server, entityKey);

                await ProcessTransaction(server, transactionRecordKey1);
                await ProcessTransaction(server, transactionRecordKey2);

                await SucceedTransaction(server, transactionRecordKey1);

                var transactionStatusId1 = await GetTransactionStatus(server, transactionRecordKey1);
                Assert.AreEqual((long)TransactionStatusType.Success, transactionStatusId1);

                var transactionStatusId2 = await GetTransactionStatus(server, transactionRecordKey2);
                Assert.AreEqual((long)TransactionStatusType.Skipped, transactionStatusId2);

                communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Once);
            }
        }

        [TestMethod]
        public async Task OpenAndFailTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var di = (NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver;
                var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
                transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(() => Guid.NewGuid().ToString("D"));
                var entityKey = Guid.NewGuid().ToString("D");
                var communicationHandler = new Mock<ICommunicationHandler>();
                communicationHandler.Setup(
                    s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()))
                    .Returns(Task.CompletedTask);

                di.Kernel.Rebind<IRecordKeyBuilder>().ToConstant(transactionRecordKeyBuilder.Object).InSingletonScope();
                di.Kernel.Rebind<ICommunicationHandler>().ToConstant(communicationHandler.Object).InSingletonScope().Named("communication-jms");

                var recordKey = await OpenTransaction(server, entityKey);

                await ProcessTransaction(server, recordKey);

                await FailTransaction(server, recordKey, "Test Failure Message", "Test Failure Details");

                var transactionStatusId1 = await GetTransactionStatus(server, recordKey);
                Assert.AreEqual((long)TransactionStatusType.Failed, transactionStatusId1);

                communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Once);
            }
        }

        private static async Task SucceedTransaction(TestServer server, string nextKey)
        {
            HttpResponseMessage succeedResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry('{nextKey}')/AMSLLC.Listener.Succeed()")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, succeedResponse.StatusCode);
        }

        private static async Task FailTransaction(TestServer server, string nextKey, string message, string details)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };

            var mediaType = new MediaTypeWithQualityHeaderValue("application/json");
            mediaType.Parameters.Add(new NameValueHeaderValue("odata", "verbose"));

            HttpResponseMessage failResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry('{nextKey}')/AMSLLC.Listener.Fail()").And(
                            request =>
                                request.Content =
                                    new ObjectContent(typeof(FailureData),
                                        new FailureData()
                                        {
                                            Message = "Test Failure Message",
                                            Details = "Test Failure Details"
                                        },
                                        new JsonMediaTypeFormatter { SerializerSettings = settings },
                                        mediaType))
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, failResponse.StatusCode);
        }

        private static async Task<string> OpenTransaction(TestServer server, string entityKey)
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
            var responseMessage = expando["value"].ToString();
            return responseMessage;
        }

        private static async Task ProcessTransaction(TestServer server, string nextKey)
        {
            HttpResponseMessage processResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry('{nextKey}')/AMSLLC.Listener.Process()")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, processResponse.StatusCode);
        }

        private static async Task<long> GetTransactionStatus(TestServer server, string nextKey)
        {
            HttpResponseMessage registryEntry =
                await
                    server.CreateRequest($"listener/TransactionRegistry?$filter=RecordKey%20eq%20%27{nextKey}%27")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").GetAsync();
            string rstr = await registryEntry.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, registryEntry.StatusCode, rstr);

            var ex = JsonConvert.DeserializeObject<ExpandoObject>(rstr) as IDictionary<string, object>;
            long transactionStatusId =
                (long)((ex["value"] as List<object>).Single() as IDictionary<string, object>)["TransactionStatusId"];
            return transactionStatusId;
        }

        private static async Task<string> GetTransactionData(TestServer server, string nextKey)
        {
            HttpResponseMessage registryEntry =
                await
                    server.CreateRequest($"listener/TransactionMessageData?$filter=RecordKey%20eq%20%27{nextKey}%27")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").GetAsync();
            string rstr = await registryEntry.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, registryEntry.StatusCode, rstr);

            var ex = JsonConvert.DeserializeObject<ExpandoObject>(rstr) as IDictionary<string, object>;
            string recordKey = (string)((ex["value"] as List<object>).Single() as IDictionary<string, object>)["RecordKey"];
            Assert.AreEqual(nextKey, recordKey);
            string returnValue = (string)((ex["value"] as List<object>).Single() as IDictionary<string, object>)["MessageData"];
            return returnValue;
        }

        private class
            InstallMeterRequestMessage
        {
            public string Test { get; set; }

            public string UserName { get; set; }

            public string EntityCategory { get; set; }

            public string EntityKey { get; set; }

            public string OperationKey { get; set; }
        }

        private class FailureData
        {
            public string Message { get; set; }

            public string Details { get; set; }
        }
    }
}