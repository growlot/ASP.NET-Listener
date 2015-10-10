// //-----------------------------------------------------------------------
// // <copyright file="IntegrationTest.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Bootstrapper.Test
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using ApiService;
    using Communication;
    using Core;
    using Core.Ninject;
    using Domain.Listener.Transaction;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;

    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task OpenTransactionTest()
        {
            using (var server = TestServer.Create<Startup>())
            {
                //HttpResponseMessage response = await server.HttpClient.PostAsync("/open/InstallMeter", new InstallMeterRequestMessage());
                var di = ((NinjectDependencyInjectionAdapter)ApplicationIntegration.DependencyResolver);
                var transactionKeyBuilder = new Mock<ITransactionKeyBuilder>();
                string nextKey = Guid.NewGuid().ToString("D");
                transactionKeyBuilder.Setup(s => s.Create()).Returns(nextKey);

                string expectedMessage = $"{{\"Data\":{{\"Test\":\"A1-S2-D3\",\"UserName\":\"ListenerUser\"}},\"TransactionKey\":\"{nextKey}\",\"Header\":{{\"PrimaryCategory\":\"ElectricMeters\",\"PrimaryKey\":\"AA-11-XSE\",\"Operation\":\"Install\"}}}}";
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

                HttpResponseMessage response =
                    await
                        server.CreateRequest("api/transaction/ElectricMeters('AA-11-XSE')/Install()")
                            .And(
                                request =>
                                    request.Content =
                                        new ObjectContent(typeof(InstallMeterRequestMessage),
                                            new InstallMeterRequestMessage { Test = "A1-S2-D3" },
                                            new JsonMediaTypeFormatter(),
                                            new MediaTypeWithQualityHeaderValue("application/json")))
                            .AddHeader("AMS-Company", "CCD")
                            .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e")
                            .SendAsync("PUT");

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseMessage = JsonConvert.DeserializeObject<ApiResponseMessage>(responseBody);
                Assert.AreEqual(nextKey, responseMessage.Data);

                HttpResponseMessage processResponse =
                    await server.CreateRequest($"api/transaction/Transaction('{nextKey}')/Process()").AddHeader("AMS-Company", "CCD")
                        .AddHeader("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e").PostAsync();

                Assert.AreEqual(HttpStatusCode.OK, processResponse.StatusCode);
                communicationHandler.Verify(
                    s =>
                        s.Handle(
                            It.Is<TransactionDataReady>(
                                ready =>
                                    string.Compare(JsonConvert.SerializeObject(ready.Data), expectedMessage,
                                        StringComparison.InvariantCulture) == 0), It.IsAny<IConnectionConfiguration>()),
                    Times.Once, "Received unexpected {0}".FormatWith(JsonConvert.SerializeObject(receivedData)));
            }
        }

        private class InstallMeterRequestMessage
        {
            public string Test { get; set; }
            public string UserName { get; set; }
        }
    }
}