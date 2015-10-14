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
                var transactionKeyBuilder = new Mock<ITransactionKeyBuilder>();
                string nextKey = Guid.NewGuid().ToString("D");
                transactionKeyBuilder.Setup(s => s.Create()).Returns(nextKey);
                var entityKey = Guid.NewGuid().ToString("D");
                string expectedMessage = $"{{\"Data\":{{\"Test\":\"A1-S2-D3\",\"UserName\":\"ListenerUser\",\"EntityCategory\":\"ElectricMeters\",\"EntityKey\":\"{entityKey}\",\"OperationKey\":\"Install\"}},\"TransactionKey\":\"{nextKey}\",\"Header\":{{\"PrimaryCategory\":\"ElectricMeters\",\"PrimaryKey\":\"{entityKey}\",\"Operation\":\"Install\"}}}}";
                object receivedData = string.Empty;

                var communicationHandler = new Mock<ICommunicationHandler>();
                communicationHandler.Setup(
                    s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()))
                    .Returns(Task.CompletedTask)
                    .Callback(
                        (object data, IConnectionConfiguration conn) =>
                            receivedData = ((TransactionDataReady)data).Data);

                di.Kernel.Rebind<ITransactionKeyBuilder>().ToConstant(transactionKeyBuilder.Object).InSingletonScope();
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

        private static async Task SucceedTransaction(TestServer server, string nextKey)
        {
            HttpResponseMessage succeedResponse =
                await
                    server.CreateRequest($"listener/TransactionRegistry('{nextKey}')/AMSLLC.Listener.Succeed()")
                        .AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

            Assert.AreEqual(HttpStatusCode.OK, succeedResponse.StatusCode);
        }

        [TestMethod]
        public async Task OpenAndSkipTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var di = ((NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver);
                var transactionKeyBuilder = new Mock<ITransactionKeyBuilder>();
                transactionKeyBuilder.Setup(s => s.Create()).Returns(() => Guid.NewGuid().ToString("D"));
                var entityKey = Guid.NewGuid().ToString("D");
                var communicationHandler = new Mock<ICommunicationHandler>();
                communicationHandler.Setup(
                    s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()))
                    .Returns(Task.CompletedTask);

                di.Kernel.Rebind<ITransactionKeyBuilder>().ToConstant(transactionKeyBuilder.Object).InSingletonScope();
                di.Kernel.Rebind<ICommunicationHandler>().ToConstant(communicationHandler.Object).InSingletonScope().Named("communication-jms");

                var transactionKey1 = await OpenTransaction(server, entityKey);
                var transactionKey2 = await OpenTransaction(server, entityKey);

                await ProcessTransaction(server, transactionKey1);
                await ProcessTransaction(server, transactionKey2);

                await SucceedTransaction(server, transactionKey1);

                var transactionStatusId1 = await GetTransactionStatus(server, transactionKey1);
                Assert.AreEqual(0L, transactionStatusId1);

                var transactionStatusId2 = await GetTransactionStatus(server, transactionKey2);
                Assert.AreEqual(3L, transactionStatusId2);

                communicationHandler.Verify(s => s.Handle(It.IsAny<TransactionDataReady>(), It.IsAny<IConnectionConfiguration>()), Times.Once);
            }
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
                    server.CreateRequest($"listener/TransactionRegistry?$filter=Key%20eq%20%27{nextKey}%27")
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
    }
}