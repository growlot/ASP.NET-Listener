// <copyright file="ApplicationServiceTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Test
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApplicationService;
    using Bus;
    using Commands;
    using Communication;
    using Core;
    using Core.Ninject;
    using Domain;
    using Domain.Listener.Transaction;
    using Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using Repository;

    [TestClass]
    public class ApplicationServiceTest
    {
        [TestMethod]
        public async Task TestEndpointFlow()
        {
            const string recordKey = "{70754941-6156-4C79-8F17-0C6BB5A85D9E}";

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

            // var testJson = JsonConvert.SerializeObject(testMessageData, settings);
            var di = new NinjectDependencyInjectionAdapter();

            var transactionRepositoryMock = new Mock<ITransactionRepository>();

            var busImpl = new InMemoryBus();
            var domainBusImpl = busImpl as IDomainEventBus;
            var domainEventBus = new Mock<IDomainEventBus>();

            domainEventBus.Setup(s => s.Subscribe(It.IsAny<Action<IDomainEvent>>())).Callback((Action<IDomainEvent> evt) => domainBusImpl.Subscribe(evt));
            domainEventBus.Setup(s => s.SubscribeAsync(It.IsAny<Func<IDomainEvent, Task>>())).Callback((Func<IDomainEvent, Task> evt) => domainBusImpl.SubscribeAsync(evt));

            domainEventBus.Setup(s => s.Publish(It.IsAny<IDomainEvent>())).Callback((IDomainEvent evt) => domainBusImpl.Publish(evt));
            domainEventBus.Setup(s => s.PublishAsync(It.IsAny<IDomainEvent>())).Callback((IDomainEvent evt) => domainBusImpl.PublishAsync(evt)).Returns(new Task[0]);
            domainEventBus.Setup(s => s.PublishBulk(It.IsAny<ICollection<IDomainEvent>>())).Callback((ICollection<IDomainEvent> evt) => domainBusImpl.PublishBulk(evt)).Returns(Task.CompletedTask);


            var hashBuilder = new Mock<ITransactionHashBuilder>();

            var transactionExecutionDomain = new Mock<TransactionExecution>(domainEventBus.Object, hashBuilder.Object) { CallBase = true };
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
            fieldConfigurations.Add(new FieldConfigurationMemento("Value", "Value1", null, null, null, 0, null, false));
            fieldConfigurations.Add(new FieldConfigurationMemento(
                "ComplexProperty.AnotherValue",
                "ComplexProperty.CorrectValue",
                null,
                null,
                null, 0, null, false));
            fieldConfigurations.Add(new FieldConfigurationMemento(
                "ComplexProperty.NestedData.AnotherValue",
                "Flatten",
                null,
                null,
                stringMap, 0, null, false));
            fieldConfigurations.Add(new FieldConfigurationMemento(
                "ArrayProperty.AnotherValue",
                "ArrayProperty[].SimpleArrayProperty",
                null,
                null,
                null, 0, null, false));
            fieldConfigurations.Add(new FieldConfigurationMemento(
                "ArrayProperty.NestedData.AnotherValue",
                "ArrayProperty[].NestedData.NestedArrayProperty",
                null,
                null,
                null, 0, null, false));
            fieldConfigurations.Add(new FieldConfigurationMemento(
                "ArrayProperty.NestedData.NestedArray.Value",
                "ArrayProperty[].NestedData.NestedArray[].DeepValue",
                null,
                null,
                integerMap, 0, null, false));

            var memento = new TransactionExecutionMemento(
                1,
                Guid.Parse(recordKey),
                1,
                new[] { new IntegrationEndpointConfigurationMemento("jms", string.Empty, string.Empty, EndpointTriggerType.Always) },
                fieldConfigurations, null, JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(testMessageData)), new List<Guid>(), TransactionStatusType.Pending);

            transactionRepositoryMock.Setup(s => s.GetExecutionContextAsync(Guid.Parse(recordKey)))
                .Returns(
                    (Guid taId) => Task.FromResult((IMemento)memento));

            transactionRepositoryMock.Setup(s => s.GetHashCountAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(
                    (int i, string h) => Task.FromResult(0));

            // var dn = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(testMessageData));
            //transactionRepositoryMock.Setup(s => s.GetTransactionDataAsync(Guid.Parse(recordKey)))
            //    .Returns(Task.FromResult(JsonConvert.SerializeObject(testMessageData)));

            var domainBuilderMock = new Mock<DomainBuilder>() { CallBase = true };
            transactionExecutionDomain.Setup(t => t.DomainBuilder).Returns(domainBuilderMock.Object);

            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<IntegrationEndpointConfiguration>())
                .Returns(integrationEndpointConfiguration.Object);

            var jmsEndpointProcessorMock = new Mock<DefaultEndpointDataProcessor>();
            jmsEndpointProcessorMock.Setup(
                e =>
                    e.Process(
                        It.IsAny<object>(),
                        It.IsAny<IList<FieldConfiguration>>())).CallBase();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();

            var communicationHandler = new Mock<ICommunicationHandler>();
            object commHandlerData = null;
            Guid? actualKey = null;
            communicationHandler.Setup(s => s.Handle(It.IsAny<object>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()))
                .Callback((object data, IConnectionConfiguration conn, IProtocolConfiguration pcfg) =>
                {
                    commHandlerData = ((TransactionDataReady)data).Data;
                    actualKey = ((TransactionDataReady)data).RecordKey;
                })
                .Returns(Task.CompletedTask);

            var builderMock = new Mock<IProtocolConfigurationBuilder>();

            di.Initialize(container =>
            {
                container.Bind<ITransactionHashBuilder>().To<TransactionHashBuilder>().InSingletonScope();
                container.Bind<ICommandBus>().ToConstant(busImpl).InSingletonScope();
                container.Bind<IDomainEventBus>().ToConstant(busImpl).InSingletonScope();
                container.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();
                container.Bind<IApplicationServiceScope>().To<ApplicationServiceScope>();
                container.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
                container.Bind<IDomainBuilder>().ToConstant(domainBuilderMock.Object);
                container.Bind<IRepositoryManager>().To<RepositoryManager>();
                container.Bind<ITransactionRepository>().ToConstant(transactionRepositoryMock.Object);
                container.Bind<IEndpointDataProcessor>().ToConstant(jmsEndpointProcessorMock.Object);
                container.Bind<IConnectionConfigurationBuilder>().ToConstant(jmsConnectionBuilder.Object).InSingletonScope().Named("connection-builder-jms");
                container.Bind<ICommunicationHandler>().ToConstant(communicationHandler.Object).InSingletonScope().Named("communication-jms");
                container.Bind<IProtocolConfigurationBuilder>().ToConstant(builderMock.Object).Named("protocol-builder-jms");
                container.Bind<ApplicationServiceConfigurator>().ToSelf().InSingletonScope();
            });

            ApplicationIntegration.SetDependencyInjectionResolver(di);

            var configurator = di.ResolveType<ApplicationServiceConfigurator>();
            configurator.RegisterCommandHandlers();
            configurator.RegisterSagaHandlers();

            var service = di.ResolveType<ITransactionService>();

            await
                service.Process(new ProcessTransactionCommand
                {
                    RecordKey = Guid.Parse(recordKey)
                });

            transactionExecutionDomain.Verify(
                foo => foo.Process(), Times.Once());
            jmsConnectionBuilder.Verify(f => f.Create(It.Is<IMemento>(data => data != null)), Times.Once);
            jmsEndpointProcessorMock.Verify(
                f =>
                    f.Process(
                        It.IsAny<object>(),
                        It.IsAny<IList<FieldConfiguration>>()), Times.Once);
            string expectedMessageBodyJson =
                $"{{\"Data\":{{\"ComplexProperty\":{{\"NestedData\":{{\"NestedData\":null,\"NestedArray\":null}},\"NestedArray\":null,\"CorrectValue\":\"Hello, World!\"}},\"ArrayProperty\":[{{\"NestedData\":{{\"NestedData\":null,\"NestedArray\":[{{\"ComplexProperty\":null,\"ArrayProperty\":null,\"DeepValue\":159000}},{{\"ComplexProperty\":null,\"ArrayProperty\":null,\"DeepValue\":9713000}}],\"NestedArrayProperty\":\"EFD\"}},\"NestedArray\":null,\"SimpleArrayProperty\":\"ABC\"}},{{\"NestedData\":null,\"NestedArray\":null,\"SimpleArrayProperty\":\"F-1\"}}],\"Value1\":987,\"Flatten\":\"Hi, Bob!\"}}}}";

            communicationHandler.Verify(
                foo => foo.Handle(It.IsAny<object>(), It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()),
                Times.Once());

            communicationHandler.Verify(
                 foo =>
                     foo.Handle(
                         It.Is<object>(
                             data =>
                                 string.Compare(
                                     JsonConvert.SerializeObject(((TransactionDataReady)data).Data, settings),
                                     expectedMessageBodyJson,
                                     StringComparison.InvariantCulture) == 0
                                     ),
                         It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Once(),
                 "Handler invoked with unexpected data: {0}{1}Expected:{2}{1}".FormatWith(JsonConvert.SerializeObject(commHandlerData, settings), Environment.NewLine, expectedMessageBodyJson));

            communicationHandler.Verify(
                 foo =>
                     foo.Handle(
                         It.Is<object>(
                             data => ((TransactionDataReady)data).RecordKey == Guid.Parse(recordKey)),
                         It.IsAny<IConnectionConfiguration>(), It.IsAny<IProtocolConfiguration>()), Times.Once(),
                 "Handler invoked with unexpected data: expected/actual key:{0}/{1}".FormatWith(recordKey, actualKey));
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

            // var t = JsonConvert.SerializeObject(testMessageData);
            var di = new NinjectDependencyInjectionAdapter();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();
            var builderMock = new Mock<IProtocolConfigurationBuilder>();
            di.Initialize(container =>
            {
                container.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
                container.Bind<IConnectionConfigurationBuilder>()
                    .ToConstant(jmsConnectionBuilder.Object)
                    .InSingletonScope()
                    .Named("connection-builder-jms");

                container.Bind<IProtocolConfigurationBuilder>().ToConstant(builderMock.Object).Named("protocol-builder-jms");
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
            fieldConfigurations.Add(new FieldConfigurationMemento(
                "ComplexProperty.NestedData.AnotherValue",
                null,
                null,
                null,
                stringMap, 0, null, false));
            fieldConfigurations.Add(new FieldConfigurationMemento(
                "ArrayProperty.NestedData.NestedArray.Value",
                null,
                null,
                null,
                integerMap, 0, null, false));

            DefaultEndpointDataProcessor p = new DefaultEndpointDataProcessor();
            IntegrationEndpointConfiguration cfg = new IntegrationEndpointConfiguration();
            ((IOriginator)cfg).SetMemento(new IntegrationEndpointConfigurationMemento("jms", string.Empty, string.Empty, EndpointTriggerType.Undefined));

            var fieldConfiguration = fieldConfigurations.Select(s =>
            {
                var itm = new FieldConfiguration();
                ((IOriginator)itm).SetMemento(s);
                return itm;
            }).ToList();

            var d = p.Process(
                testMessageData,
                fieldConfiguration);
            const string expectedJson =
                "{\"Value\":987,\"ComplexProperty\":{\"AnotherValue\":\"Hello, World!\",\"NestedData\":{\"AnotherValue\":\"Hi, Bob!\",\"NestedData\":null,\"NestedArray\":null},\"NestedArray\":null},\"ArrayProperty\":[{\"AnotherValue\":\"ABC\",\"NestedData\":{\"AnotherValue\":\"EFD\",\"NestedData\":null,\"NestedArray\":[{\"Value\":159000,\"ComplexProperty\":null,\"ArrayProperty\":null},{\"Value\":9713000,\"ComplexProperty\":null,\"ArrayProperty\":null}]},\"NestedArray\":null},{\"AnotherValue\":\"F-1\",\"NestedData\":null,\"NestedArray\":null}]}";
            Assert.AreEqual(expectedJson, JsonConvert.SerializeObject(d.Data));
        }

        public class TestMessageData
        {
            public int Value { get; set; }

            public TestSubData ComplexProperty { get; set; }

            public TestSubData[] ArrayProperty { get; set; }
        }

        public class TestSubData
        {
            public string AnotherValue { get; set; }

            public TestSubData NestedData { get; set; }

            public TestMessageData[] NestedArray { get; set; }
        }
    }
}