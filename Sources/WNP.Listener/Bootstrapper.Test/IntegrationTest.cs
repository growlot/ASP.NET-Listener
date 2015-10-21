// //-----------------------------------------------------------------------
// // <copyright file="IntegrationTest.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

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
    using System.Text;
    using System.Threading.Tasks;
    using Communication;
    using Core;
    using Core.Ninject;
    using Domain.Listener;
    using Domain.Listener.Transaction;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    [TestClass]
    public class IntegrationTest
    {
        //[TestInitialize()]
        //public void Initialize() { }

        //[TestCleanup()]
        //public void Cleanup() { }

        [TestMethod]
        public async Task OpenAndSucceedTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var di = ((NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver);
                var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
                string nextKey = Guid.NewGuid().ToString("D");
                transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(nextKey);
                var entityKey = Guid.NewGuid().ToString("D");
                string expectedMessage = $"{{\"Data\":{{\"Test\":\"A1-S2-D3\",\"UserName\":\"ListenerUser\",\"EntityCategory\":\"ElectricMeters\",\"EntityKey\":\"{entityKey}\",\"OperationKey\":\"Install\"}},\"RecordKey\":\"{nextKey}\"}}";
                object receivedData = string.Empty;

                var communicationHandler = new Mock<ICommunicationHandler>();
                communicationHandler.Setup(
                    s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()))
                    .Returns(Task.CompletedTask)
                    .Callback(
                        (object data, IConnectionConfiguration conn) =>
                            receivedData = ((TransactionDataReady)data).Data);

                di.Kernel.Rebind<IRecordKeyBuilder>().ToConstant(transactionRecordKeyBuilder.Object).InSingletonScope();
                di.Kernel.Rebind<ICommunicationHandler>().ToConstant(communicationHandler.Object).InSingletonScope().Named("communication-jms");
                var responseMessage = await OpenTransaction(server, entityKey);
                Assert.AreEqual(nextKey, responseMessage);

                await ProcessTransaction(server, nextKey);
                communicationHandler.Verify(
                    s =>
                        s.Handle(
                            It.Is<TransactionDataReady>(
                                ready =>
                                    string.Compare(JsonConvert.SerializeObject(ready.Data), expectedMessage,
                                        StringComparison.InvariantCulture) == 0), It.IsAny<IConnectionConfiguration>()),
                    Times.Once, "Received unexpected {0}{1}Expected: {2}".FormatWith(JsonConvert.SerializeObject(receivedData), Environment.NewLine, expectedMessage));

                await SucceedTransaction(server, nextKey);

                var transactionStatusId = await GetTransactionStatus(server, nextKey);

                Assert.AreEqual(0L, transactionStatusId);
            }
        }

        [TestMethod]
        public async Task OpenAndSkipTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var di = ((NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver);
                var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
                transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(() => Guid.NewGuid().ToString("D"));
                var entityKey = Guid.NewGuid().ToString("D");
                var communicationHandler = new Mock<ICommunicationHandler>();
                communicationHandler.Setup(
                    s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()))
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

                communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()), Times.Once);
            }
        }

        [TestMethod]
        public async Task OpenAndFailTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var di = ((NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver);
                var transactionRecordKeyBuilder = new Mock<IRecordKeyBuilder>();
                transactionRecordKeyBuilder.Setup(s => s.Create()).Returns(() => Guid.NewGuid().ToString("D"));
                var entityKey = Guid.NewGuid().ToString("D");
                var communicationHandler = new Mock<ICommunicationHandler>();
                communicationHandler.Setup(
                    s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()))
                    .Returns(Task.CompletedTask);

                di.Kernel.Rebind<IRecordKeyBuilder>().ToConstant(transactionRecordKeyBuilder.Object).InSingletonScope();
                di.Kernel.Rebind<ICommunicationHandler>().ToConstant(communicationHandler.Object).InSingletonScope().Named("communication-jms");

                var recordKey = await OpenTransaction(server, entityKey);

                await ProcessTransaction(server, recordKey);

                await FailTransaction(server, recordKey, "Test Failure Message", "Test Failure Details");

                var transactionStatusId1 = await GetTransactionStatus(server, recordKey);
                Assert.AreEqual((long)TransactionStatusType.Failed, transactionStatusId1);

                communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()), Times.Once);
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

            HttpResponseMessage response =
                await
                    server.CreateRequest("listener/Open")
                        .And(
                            request =>
                                request.Content =
                                    new ObjectContent(typeof(InstallMeterRequestMessage),
                                        new InstallMeterRequestMessage
                                        {
                                            Test = "A1-S2-D3",
                                            EntityCategory = "ElectricMeters",
                                            OperationKey = "Install",
                                            EntityKey = entityKey
                                        },
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
                (long)(((ex["value"] as List<object>).Single() as IDictionary<string, object>)["TransactionStatusId"]);
            return transactionStatusId;
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