﻿// //-----------------------------------------------------------------------
// // <copyright file="UnitTest1.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace ApplicationService.Test
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AMSLLC.Core;
    using AMSLLC.Core.Ninject;
    using AMSLLC.Listener.ApplicationService;
    using AMSLLC.Listener.ApplicationService.Impl;
    using AMSLLC.Listener.Communication;
    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.Domain.Listener.Transaction;
    using AMSLLC.Listener.Repository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;

    [TestClass]
    public class ApplicationServiceTest
    {
        [TestMethod]
        public async Task TestEndpointFlow()
        {
            const string transactionKey = "{70754941-6156-4C79-8F17-0C6BB5A85D9E}";

            var testMessageData = new TestMessageData()
            {
                Value = 987,
                ArrayProperty =
                    new[]
                    {
                        new TestSubData
                        {
                            AnotherValue = "ABC",
                            NestedData =
                                new TestSubData
                                {
                                    AnotherValue = "EFD",
                                    NestedArray =
                                        new[]
                                        { new TestMessageData { Value = 159 }, new TestMessageData { Value = 9713 } }
                                }
                        },
                        new TestSubData { AnotherValue = "F-1" }
                    },
                ComplexProperty =
                    new TestSubData
                    {
                        AnotherValue = "Hello, World!",
                        NestedData = new TestSubData { AnotherValue = "Hey!" }
                    }
            };

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            //var testJson = JsonConvert.SerializeObject(testMessageData, settings);

            var di = new NinjectDependencyInjectionAdapter();

            var transactionRepositoryMock = new Mock<ITransactionRepository>();

            var transactionExecutionDomain = new Mock<TransactionExecution> { CallBase = true };
            transactionExecutionDomain.As<IOriginator>();
            transactionExecutionDomain.As<IWithDomainBuilder>();
            var integrationEndpointConfiguration = new Mock<IntegrationEndpointConfiguration>();

            Dictionary<string, object> integerMap = new Dictionary<string, object>
            {
                { "159", 159000 },
                { "9713", 9713000 }
            };
            Dictionary<string, object> stringMap = new Dictionary<string, object>
            {
                { "Hey!", "Hi, Bob!" },
                { "Hi.", "Bye!" }
            };

            var fieldConfigurations = new List<FieldConfigurationMemento>();
            fieldConfigurations.Add(new FieldConfigurationMemento("Value", "Value1", null));
            fieldConfigurations.Add(new FieldConfigurationMemento("ComplexProperty.AnotherValue",
                "ComplexProperty.CorrectValue", null));
            fieldConfigurations.Add(new FieldConfigurationMemento("ComplexProperty.NestedData.AnotherValue", "Flatten",
                stringMap));
            fieldConfigurations.Add(new FieldConfigurationMemento("ArrayProperty.AnotherValue",
                "ArrayProperty[].SimpleArrayProperty", null));
            fieldConfigurations.Add(new FieldConfigurationMemento("ArrayProperty.NestedData.AnotherValue",
                "ArrayProperty[].NestedData.NestedArrayProperty", null));
            fieldConfigurations.Add(new FieldConfigurationMemento("ArrayProperty.NestedData.NestedArray.Value",
                "ArrayProperty[].NestedData.NestedArray[].DeepValue", integerMap));

            var memento = new TransactionExecutionMemento(transactionKey,
                new[]
                {
                    new IntegrationEndpointConfigurationMemento("jms", "", EndpointTriggerType.Always,
                        fieldConfigurations)
                });


            transactionRepositoryMock.Setup(s => s.GetExecutionContext(transactionKey))
                .Returns(
                    (string taId) => Task.FromResult((IMemento)memento));

            transactionRepositoryMock.Setup(s => s.GetTransactionData(transactionKey))
                .Returns(Task.FromResult(JsonConvert.SerializeObject(testMessageData)));


            var domainBuilderMock = new Mock<DomainBuilder>() { CallBase = true };
            transactionExecutionDomain.Setup(t => t.DomainBuilder).Returns(domainBuilderMock.Object);

            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<IntegrationEndpointConfiguration>())
                .Returns(integrationEndpointConfiguration.Object);

            var jmsEndpointProcessorMock = new Mock<DefaultEndpointDataProcessor>();
            jmsEndpointProcessorMock.Setup(
                e =>
                    e.Process(It.IsAny<object>(),
                        It.IsAny<IntegrationEndpointConfiguration>())).CallBase();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();


            var communicationHandler = new Mock<ICommunicationHandler>();
            object commHandlerData = null;
            communicationHandler.Setup(s => s.Handle(It.IsAny<object>(), It.IsAny<IConnectionConfiguration>()))
                .Callback(
                    (object data, IConnectionConfiguration conn) => commHandlerData = ((TransactionDataReady)data).Data)
                .Returns(Task.CompletedTask);

            di.Initialize(container =>
            {
                container.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();
                container.Bind<IApplicationServiceScope>().To<ApplicationServiceScope>();
                container.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
                container.Bind<IDomainBuilder>().ToConstant(domainBuilderMock.Object);
                container.Bind<IRepositoryManager>().To<RepositoryManager>();
                container.Bind<ITransactionRepository>().ToConstant(transactionRepositoryMock.Object);
                container.Bind<IEndpointDataProcessor>().ToConstant(jmsEndpointProcessorMock.Object);
                container.Bind<IConnectionConfigurationBuilder>()
                    .ToConstant(jmsConnectionBuilder.Object)
                    .InSingletonScope()
                    .Named("connection-builder-jms");
                container.Bind<ICommunicationHandler>()
                    .ToConstant(communicationHandler.Object)
                    .InSingletonScope()
                    .Named("communication-jms");
            });


            ApplicationIntegration.SetDependencyInjectionResolver(di);

            var service = di.ResolveType<ITransactionService>();

            await
                service.Process(new ProcessTransactionRequestMessage
                {
                    TransactionKey = transactionKey
                    //Data = testMessageData
                });

            transactionExecutionDomain.Verify(
                foo => foo.Process(It.IsAny<object>()), Times.Once());
            jmsConnectionBuilder.Verify(f => f.Create(It.Is<IMemento>(data => data != null)), Times.Once);
            jmsEndpointProcessorMock.Verify(
                f =>
                    f.Process(It.IsAny<object>(),
                        It.Is<IntegrationEndpointConfiguration>(data => data.Protocol == "jms")), Times.Once);
            const string expectedMessageBodyJson =
                "{\"ComplexProperty\":{\"NestedData\":{\"NestedData\":null,\"NestedArray\":null},\"NestedArray\":null,\"CorrectValue\":\"Hello, World!\"},\"ArrayProperty\":[{\"NestedData\":{\"NestedData\":null,\"NestedArray\":[{\"ComplexProperty\":null,\"ArrayProperty\":null,\"DeepValue\":159000},{\"ComplexProperty\":null,\"ArrayProperty\":null,\"DeepValue\":9713000}],\"NestedArrayProperty\":\"EFD\"},\"NestedArray\":null,\"SimpleArrayProperty\":\"ABC\"},{\"NestedData\":null,\"NestedArray\":null,\"SimpleArrayProperty\":\"F-1\"}],\"Value1\":987,\"Flatten\":\"Hi, Bob!\"}";

            communicationHandler.Verify(
                foo =>
                    foo.Handle(
                        It.Is<object>(
                            data =>
                                string.Compare(
                                    JsonConvert.SerializeObject(((TransactionDataReady)data).Data, settings),
                                    expectedMessageBodyJson,
                                    StringComparison.InvariantCulture) == 0),
                        It.IsAny<IConnectionConfiguration>()), Times.Once(),
                "Handler invoked with unexpected data: {0}".FormatWith(JsonConvert.SerializeObject(commHandlerData)));
        }

        [TestMethod]
        public void DefaultValueMapperTest()
        {
            var testMessageData = new TestMessageData()
            {
                Value = 987,
                ArrayProperty =
                    new[]
                    {
                        new TestSubData
                        {
                            AnotherValue = "ABC",
                            NestedData =
                                new TestSubData
                                {
                                    AnotherValue = "EFD",
                                    NestedArray =
                                        new[]
                                        { new TestMessageData { Value = 159 }, new TestMessageData { Value = 9713 } }
                                }
                        },
                        new TestSubData { AnotherValue = "F-1" }
                    },
                ComplexProperty =
                    new TestSubData
                    {
                        AnotherValue = "Hello, World!",
                        NestedData = new TestSubData { AnotherValue = "Hey!" }
                    }
            };
            //var t = JsonConvert.SerializeObject(testMessageData);
            var di = new NinjectDependencyInjectionAdapter();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();
            di.Initialize(container =>
            {
                container.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
                container.Bind<IConnectionConfigurationBuilder>()
                    .ToConstant(jmsConnectionBuilder.Object)
                    .InSingletonScope()
                    .Named("connection-builder-jms");
            });
            ApplicationIntegration.SetDependencyInjectionResolver(di);

            Dictionary<string, object> integerMap = new Dictionary<string, object>
            {
                { "159", 159000 },
                { "9713", 9713000 }
            };
            Dictionary<string, object> stringMap = new Dictionary<string, object>
            {
                { "Hey!", "Hi, Bob!" },
                { "Hi.", "Bye!" }
            };

            var fieldConfigurations = new List<FieldConfigurationMemento>();
            fieldConfigurations.Add(new FieldConfigurationMemento("ComplexProperty.NestedData.AnotherValue", null,
                stringMap));
            fieldConfigurations.Add(new FieldConfigurationMemento("ArrayProperty.NestedData.NestedArray.Value", null,
                integerMap));

            DefaultEndpointDataProcessor p = new DefaultEndpointDataProcessor();
            IntegrationEndpointConfiguration cfg = new IntegrationEndpointConfiguration();
            ((IOriginator)cfg).SetMemento(new IntegrationEndpointConfigurationMemento("jms", "",
                EndpointTriggerType.Undefined, fieldConfigurations));
            var d = p.Process(testMessageData, cfg);
            const string expectedJson =
                "{\"Value\":987,\"ComplexProperty\":{\"AnotherValue\":\"Hello, World!\",\"NestedData\":{\"AnotherValue\":\"Hi, Bob!\",\"NestedData\":null,\"NestedArray\":null},\"NestedArray\":null},\"ArrayProperty\":[{\"AnotherValue\":\"ABC\",\"NestedData\":{\"AnotherValue\":\"EFD\",\"NestedData\":null,\"NestedArray\":[{\"Value\":159000,\"ComplexProperty\":null,\"ArrayProperty\":null},{\"Value\":9713000,\"ComplexProperty\":null,\"ArrayProperty\":null}]},\"NestedArray\":null},{\"AnotherValue\":\"F-1\",\"NestedData\":null,\"NestedArray\":null}]}";
            Assert.AreEqual(expectedJson, JsonConvert.SerializeObject(d.Data));
        }

        private class TestMessageData
        {
            public int Value { get; set; }
            public TestSubData ComplexProperty { get; set; }
            public TestSubData[] ArrayProperty { get; set; }
        }

        private class TestSubData
        {
            public string AnotherValue { get; set; }
            public TestSubData NestedData { get; set; }
            public TestMessageData[] NestedArray { get; set; }
        }
    }
}