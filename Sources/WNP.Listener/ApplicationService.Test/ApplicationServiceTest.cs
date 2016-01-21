// <copyright file="ApplicationServiceTest.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Test
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bus;
    using Commands;
    using Communication;
    using Core;
    using Core.Ninject.Test;
    using Domain;
    using Domain.Listener.Transaction;
    using Implementations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Newtonsoft.Json;
    using ODataService;
    using Repository.Listener;
    using Serilog;
    using Shared;

    [TestClass]
    public class ApplicationServiceTest
    {
        #region Setup

        [ClassInitialize]
        public static void ClassInit(
            TestContext context)
        {
            var di = new TestDependencyInjectionAdapter();
            var container = di.Kernel;
            ApplicationIntegration.SetDependencyInjectionResolver(di);
            container.Bind<ITransactionHashBuilder>().To<TransactionHashBuilder>().InSingletonScope();
            container.Bind<IRecordKeyBuilder>().To<RecordKeyBuilder>().InSingletonScope();
            container.Bind<ISummaryBuilder>().To<SummaryBuilder>().InSingletonScope();
            container.Bind<ITransactionService>().To<TransactionService>();
            container.Bind<IApplicationServiceScope>().To<ApplicationServiceScope>();
            container.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
            container.Bind<ApplicationServiceConfigurator>().ToSelf();
            container.Bind<IDependencyInjectionModule>()
                .To<TestScopeContainerInitializer>()
                .Named("ApplicationScopeModule");

            var log = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            Log.Logger = log;
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
        }
        #endregion

        #region Tests

        [TestMethod]
        public async Task TestEndpointFlow()
        {
            const string recordKey = "{70754941-6156-4C79-8F17-0C6BB5A85D9E}";

            var testMessageData = new TestMessageData()
            {
                Value = 987,
                ArrayProperty = new[]
                    {
                        new TestSubData
                        {
                            AnotherValue = "ABC",
                        NestedData = new TestSubData
                                {
                                    AnotherValue = "EFD",
                            NestedArray = new[]
                            {
                                new TestMessageData
                                {
                                    Value = 159
                                },
                                new TestMessageData
                                {
                                    Value = 9713
                                }
                            }
                        }
                        },
                    new TestSubData
                    {
                        AnotherValue = "F-1"
                    }
                },
                ComplexProperty = new TestSubData
                {
                    AnotherValue = "Hello, World!",
                    NestedData = new TestSubData
                    {
                        AnotherValue = "Hey!"
                    }
                }
            };

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            // var testJson = JsonConvert.SerializeObject(testMessageData, settings);
            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;

            var transactionRepositoryMock = new Mock<ITransactionRepository>();

            var busImpl = new InMemoryBus();
            InMemoryBus.Reset();

            var hashBuilder = new Mock<ITransactionHashBuilder>();

            var transactionExecutionDomain = new Mock<TransactionExecution>(busImpl, hashBuilder.Object)
            {
                CallBase = true
            };
            transactionExecutionDomain.As<IOriginator>();
            transactionExecutionDomain.As<IWithDomainBuilder>();
            var integrationEndpointConfiguration = new Mock<IntegrationEndpointConfiguration>();

            Dictionary<string, object> integerMap = new Dictionary<string, object>
            {
                {
                    "159", 159000
                },
                {
                    "9713", 9713000
                }
            };
            Dictionary<string, object> stringMap = new Dictionary<string, object>
            {
                {
                    "Hey!", "Hi, Bob!"
                },
                {
                    "Hi.", "Bye!"
                }
            };

            var fieldConfigurations = new List<FieldConfigurationMemento>();
            fieldConfigurations.Add(
                new FieldConfigurationMemento("Value", "Value1", null, null, null, 0, null, false));
            fieldConfigurations.Add(
                new FieldConfigurationMemento(
                "ComplexProperty.AnotherValue",
                "ComplexProperty.CorrectValue",
                null,
                null,
                    null,
                    0,
                    null,
                    false));
            fieldConfigurations.Add(
                new FieldConfigurationMemento(
                "ComplexProperty.NestedData.AnotherValue",
                "Flatten",
                null,
                null,
                    stringMap,
                    0,
                    null,
                    false));
            fieldConfigurations.Add(
                new FieldConfigurationMemento(
                "ArrayProperty.AnotherValue",
                "ArrayProperty[].SimpleArrayProperty",
                null,
                null,
                    null,
                    0,
                    null,
                    false));
            fieldConfigurations.Add(
                new FieldConfigurationMemento(
                "ArrayProperty.NestedData.AnotherValue",
                "ArrayProperty[].NestedData.NestedArrayProperty",
                null,
                null,
                    null,
                    0,
                    null,
                    false));
            fieldConfigurations.Add(
                new FieldConfigurationMemento(
                "ArrayProperty.NestedData.NestedArray.Value",
                "ArrayProperty[].NestedData.NestedArray[].DeepValue",
                null,
                null,
                    integerMap,
                    0,
                    null,
                    false));

            var memento = new TransactionExecutionMemento(
                1,
                Guid.Parse(recordKey),
                1,
                new[]
                {
                    new IntegrationEndpointConfigurationMemento(
                        "jms",
                        string.Empty,
                        string.Empty,
                        EndpointTriggerType.Always)
                },
                fieldConfigurations,
                null,
                JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(testMessageData)),
                new List<Guid>(),
                TransactionStatusType.Pending,
                null);

            transactionRepositoryMock.Setup(s => s.GetExecutionContextAsync(Guid.Parse(recordKey)))
                .Returns((Guid taId) => Task.FromResult((IMemento)memento));

            transactionRepositoryMock.Setup(s => s.GetHashCountAsync(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(
                    (int i,
                        string h) => Task.FromResult(0));

            transactionRepositoryMock.Setup(s => s.GetRegistryEntry(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionRegistryMemento(
                        0,
                        Guid.Parse(recordKey),
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        TransactionStatusType.Processing,
                        string.Empty,
                        DateTime.Now,
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        0,
                        new List<TransactionRegistryMemento>()));

            // var dn = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(testMessageData));
            //transactionRepositoryMock.Setup(s => s.GetTransactionDataAsync(Guid.Parse(recordKey)))
            //    .Returns(Task.FromResult(JsonConvert.SerializeObject(testMessageData)));

            var domainBuilderMock = new Mock<DomainBuilder>()
            {
                CallBase = true
            };
            transactionExecutionDomain.Setup(t => t.DomainBuilder).Returns(domainBuilderMock.Object);

            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<IntegrationEndpointConfiguration>())
                .Returns(integrationEndpointConfiguration.Object);

            var jmsEndpointProcessorMock = new Mock<DefaultEndpointDataProcessor>();
            jmsEndpointProcessorMock.Setup(e => e.Process(It.IsAny<object>(), It.IsAny<IList<FieldConfiguration>>()))
                .CallBase();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();

            var communicationHandler = new Mock<ICommunicationHandler>();
            object commHandlerData = null;
            Guid? actualKey = null;
            communicationHandler.Setup(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>())).Callback(
                            (object data,
                                IConnectionConfiguration conn,
                                IProtocolConfiguration pcfg) =>
                {
                    commHandlerData = ((TransactionDataReady)data).Data;
                    actualKey = ((TransactionDataReady)data).RecordKey;
                }).Returns(Task.CompletedTask);

            var builderMock = new Mock<IProtocolConfigurationBuilder>();

            di.Rebind<IDependencyInjectionModule>(
                new AppServiceTestContainerInitializer(transactionRepositoryMock.Object))
                .Named("ApplicationScopeModule");

            di.Rebind<ICommandBus>(busImpl);

            di.Rebind<IDomainEventBus>(busImpl);
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);
            // di.Rebind<ITransactionRepository>(transactionRepositoryMock.Object);
            di.Rebind<IEndpointDataProcessor>(jmsEndpointProcessorMock.Object);
            di.Rebind<IConnectionConfigurationBuilder>(jmsConnectionBuilder.Object)
                .InSingletonScope()
                .Named("connection-builder-jms");
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");
            di.Rebind<IProtocolConfigurationBuilder>(builderMock.Object).Named("protocol-builder-jms");

            var configurator = di.ResolveType<ApplicationServiceConfigurator>();
            configurator.RegisterCommandHandlers();
            configurator.RegisterSagaHandlers();

            var service = di.ResolveType<ITransactionService>();

            await service.Process(
                new ProcessTransactionCommand
                {
                    RecordKey = Guid.Parse(recordKey)
                });

            transactionExecutionDomain.Verify(foo => foo.Process(), Times.Once());
            jmsConnectionBuilder.Verify(f => f.Create(It.Is<IMemento>(data => data != null)), Times.Once);
            jmsEndpointProcessorMock.Verify(
                f => f.Process(It.IsAny<object>(), It.IsAny<IList<FieldConfiguration>>()),
                Times.Once);
            string expectedMessageBodyJson =
                $"{{\"Data\":{{\"ComplexProperty\":{{\"NestedData\":{{\"NestedData\":null,\"NestedArray\":null}},\"NestedArray\":null,\"CorrectValue\":\"Hello, World!\"}},\"ArrayProperty\":[{{\"NestedData\":{{\"NestedData\":null,\"NestedArray\":[{{\"ComplexProperty\":null,\"ArrayProperty\":null,\"DeepValue\":159000}},{{\"ComplexProperty\":null,\"ArrayProperty\":null,\"DeepValue\":9713000}}],\"NestedArrayProperty\":\"EFD\"}},\"NestedArray\":null,\"SimpleArrayProperty\":\"ABC\"}},{{\"NestedData\":null,\"NestedArray\":null,\"SimpleArrayProperty\":\"F-1\"}}],\"Value1\":987,\"Flatten\":\"Hi, Bob!\"}}}}";

            communicationHandler.Verify(
                foo =>
                    foo.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>()),
                Times.Once());

            communicationHandler.Verify(
                 foo =>
                     foo.Handle(
                         It.Is<object>(
                             data =>
                                 string.Compare(
                                     JsonConvert.SerializeObject(((TransactionDataReady)data).Data, settings),
                                     expectedMessageBodyJson,
                                    StringComparison.InvariantCulture) == 0),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>()),
                Times.Once(),
                "Handler invoked with unexpected data: {0}{1}Expected:{2}{1}".FormatWith(
                    JsonConvert.SerializeObject(commHandlerData, settings),
                    Environment.NewLine,
                    expectedMessageBodyJson));

            communicationHandler.Verify(
                 foo =>
                     foo.Handle(
                        It.Is<object>(data => ((TransactionDataReady)data).RecordKey == Guid.Parse(recordKey)),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>()),
                Times.Once(),
                 "Handler invoked with unexpected data: expected/actual key:{0}/{1}".FormatWith(recordKey, actualKey));

            transactionRepositoryMock.Verify(s => s.UpdateHashAsync(It.Is<Dictionary<Guid, string>>(d => d.Count() == 1)), Times.Once);
        }

        [TestMethod]
        public void DefaultValueMapperTest()
        {
            var testMessageData = new TestMessageData()
            {
                Value = 987,
                ArrayProperty = new[]
                    {
                        new TestSubData
                        {
                            AnotherValue = "ABC",
                        NestedData = new TestSubData
                                {
                                    AnotherValue = "EFD",
                            NestedArray = new[]
                            {
                                new TestMessageData
                                {
                                    Value = 159
                                },
                                new TestMessageData
                                {
                                    Value = 9713
                                }
                            }
                        }
                        },
                    new TestSubData
                    {
                        AnotherValue = "F-1"
                    }
                },
                ComplexProperty = new TestSubData
                {
                    AnotherValue = "Hello, World!",
                    NestedData = new TestSubData
                    {
                        AnotherValue = "Hey!"
                    }
                }
            };

            // var t = JsonConvert.SerializeObject(testMessageData);
            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;

            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();
            var builderMock = new Mock<IProtocolConfigurationBuilder>();

            di.Rebind<IConnectionConfigurationBuilder>(jmsConnectionBuilder.Object)
                .InSingletonScope()
                .Named("connection-builder-jms");

            di.Rebind<IProtocolConfigurationBuilder>(builderMock.Object).Named("protocol-builder-jms");

            Dictionary<string, object> integerMap = new Dictionary<string, object>
            {
                {
                    "159", 159000
                },
                {
                    "9713", 9713000
                }
            };
            Dictionary<string, object> stringMap = new Dictionary<string, object>
            {
                {
                    "Hey!", "Hi, Bob!"
                },
                {
                    "Hi.", "Bye!"
                }
            };

            var fieldConfigurations = new List<FieldConfigurationMemento>();
            fieldConfigurations.Add(
                new FieldConfigurationMemento(
                "ComplexProperty.NestedData.AnotherValue",
                null,
                null,
                null,
                    stringMap,
                    0,
                    null,
                    false));
            fieldConfigurations.Add(
                new FieldConfigurationMemento(
                "ArrayProperty.NestedData.NestedArray.Value",
                null,
                null,
                null,
                    integerMap,
                    0,
                    null,
                    false));

            DefaultEndpointDataProcessor p = new DefaultEndpointDataProcessor();
            IntegrationEndpointConfiguration cfg = new IntegrationEndpointConfiguration();
            ((IOriginator)cfg).SetMemento(
                new IntegrationEndpointConfigurationMemento(
                    "jms",
                    string.Empty,
                    string.Empty,
                    EndpointTriggerType.Undefined));

            var fieldConfiguration = fieldConfigurations.Select(
                s =>
            {
                var itm = new FieldConfiguration();
                ((IOriginator)itm).SetMemento(s);
                return itm;
            }).ToList();

            var d = p.Process(testMessageData, fieldConfiguration);
            const string expectedJson =
                "{\"Value\":987,\"ComplexProperty\":{\"AnotherValue\":\"Hello, World!\",\"NestedData\":{\"AnotherValue\":\"Hi, Bob!\",\"NestedData\":null,\"NestedArray\":null},\"NestedArray\":null},\"ArrayProperty\":[{\"AnotherValue\":\"ABC\",\"NestedData\":{\"AnotherValue\":\"EFD\",\"NestedData\":null,\"NestedArray\":[{\"Value\":159000,\"ComplexProperty\":null,\"ArrayProperty\":null},{\"Value\":9713000,\"ComplexProperty\":null,\"ArrayProperty\":null}]},\"NestedArray\":null},{\"AnotherValue\":\"F-1\",\"NestedData\":null,\"NestedArray\":null}]}";
            Assert.AreEqual(expectedJson, JsonConvert.SerializeObject(d.Data));
        }

        [TestMethod]
        public async Task BatchPriorityTest()
        {
            var recordKey = Guid.NewGuid().ToString("D");

            var busImpl = new InMemoryBus();
            InMemoryBus.Reset();
            var domainBuilderMock = new Mock<DomainBuilder>()
            {
                CallBase = true
            };
            var jmsEndpointProcessorMock = new Mock<IEndpointDataProcessor>();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();
            var communicationHandler = new Mock<ICommunicationHandler>();
            var builderMock = new Mock<IProtocolConfigurationBuilder>();
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var hashBuilder = new Mock<ITransactionHashBuilder>();
            var integrationEndpointConfiguration = new Mock<IntegrationEndpointConfiguration>()
            {
                CallBase = true
            };

            var transactionExecutionDomain = new Mock<TransactionExecution>(busImpl, hashBuilder.Object)
            {
                CallBase = true
            };
            transactionExecutionDomain.As<IOriginator>();
            transactionExecutionDomain.As<IWithDomainBuilder>();
            transactionExecutionDomain.Setup(t => t.DomainBuilder).Returns(domainBuilderMock.Object);

            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<IntegrationEndpointConfiguration>())
                .Returns(integrationEndpointConfiguration.Object);

            ConcurrentStack<Guid> handledTransactions = new ConcurrentStack<Guid>();

            communicationHandler.Setup(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>())).Callback(
                            (object obj,
                                IConnectionConfiguration ccfg,
                                IProtocolConfiguration pcfg) =>
                            {
                                TransactionDataReady dataReady = (TransactionDataReady)obj;
                                handledTransactions.Push(dataReady.RecordKey);
                            }).Returns(Task.CompletedTask);

            Dictionary<Guid, int> priorities = new Dictionary<Guid, int>
            {
                {
                    Guid.NewGuid(), 4
                },
                {
                    Guid.NewGuid(), 5
                },
                {
                    Guid.NewGuid(), 1
                },
                {
                    Guid.NewGuid(), 2
                },
                {
                    Guid.NewGuid(), 2
                },
                {
                    Guid.NewGuid(), 1
                },
                {
                    Guid.NewGuid(), 1
                },
                {
                    Guid.NewGuid(), 4
                },
                {
                    Guid.NewGuid(), 5
                },
                {
                    Guid.NewGuid(), 3
                }
            };

            var batch =
                priorities.Select(keyValuePair => this.CreateBatchChild(keyValuePair.Key, keyValuePair.Value))
                    .ToList();

            transactionRepositoryMock.Setup(s => s.GetExecutionContextAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionExecutionMemento(
                        0,
                        Guid.Parse(recordKey),
                        0,
                        new[]
                        {
                            new IntegrationEndpointConfigurationMemento(
                                "jms",
                                string.Empty,
                                string.Empty,
                                EndpointTriggerType.Always)
                        },
                        new List<FieldConfigurationMemento>(),
                        batch,
                        new object(),
                        new List<Guid>(),
                        TransactionStatusType.Pending,
                        null));

            transactionRepositoryMock.Setup(s => s.GetRegistryEntry(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionRegistryMemento(
                        0,
                        Guid.Parse(recordKey),
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        TransactionStatusType.Processing,
                        string.Empty,
                        DateTime.Now,
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        0,
                        new List<TransactionRegistryMemento>()));

            jmsEndpointProcessorMock.Setup(s => s.Process(It.IsAny<object>(), It.IsAny<IList<FieldConfiguration>>()))
                .Returns(
                    new EndpointDataProcessorResult
                    {
                        Data = new object(),
                        Hash = string.Empty
                    });

            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;

            di.Rebind<IDependencyInjectionModule>(
                new AppServiceTestContainerInitializer(transactionRepositoryMock.Object))
                .Named("ApplicationScopeModule");

            di.Rebind<ICommandBus>(busImpl);
            di.Rebind<IDomainEventBus>(busImpl);
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);
            // di.Rebind<ITransactionRepository>(transactionRepositoryMock.Object);
            di.Rebind<IEndpointDataProcessor>(jmsEndpointProcessorMock.Object);
            di.Rebind<IConnectionConfigurationBuilder>(jmsConnectionBuilder.Object).Named("connection-builder-jms");
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");
            di.Rebind<IProtocolConfigurationBuilder>(builderMock.Object).Named("protocol-builder-jms");

            var configurator = di.ResolveType<ApplicationServiceConfigurator>();
            configurator.RegisterCommandHandlers();
            configurator.RegisterSagaHandlers();

            var service = di.ResolveType<ITransactionService>();

            await service.Process(
                new ProcessTransactionCommand
                {
                    RecordKey = Guid.Parse(recordKey)
                });

            communicationHandler.Verify(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>()),
                Times.Exactly(priorities.Count + 1)); // +1 for root transaction

            Guid currentTransaction;
            int lastPriority = int.MaxValue;
            bool rootFound = false;
            while (!handledTransactions.IsEmpty)
            {
                handledTransactions.TryPop(out currentTransaction);
                if (!priorities.ContainsKey(currentTransaction))
                {
                    Assert.AreEqual(1, lastPriority);
                    Assert.IsFalse(rootFound);
                    rootFound = true;
                    continue;
                }

                Assert.IsTrue(priorities[currentTransaction] <= lastPriority);
                lastPriority = priorities[currentTransaction];
            }

            transactionRepositoryMock.Verify(s => s.UpdateHashAsync(It.Is<Dictionary<Guid, string>>(d => d.Count() == priorities.Count + 1)), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public async Task EndpointExceptionPropagationTest()
        {
            var recordKey = Guid.NewGuid().ToString("D");

            var busImpl = new InMemoryBus();
            InMemoryBus.Reset();
            var domainBuilderMock = new Mock<DomainBuilder>()
            {
                CallBase = true
            };
            var jmsEndpointProcessorMock = new Mock<IEndpointDataProcessor>();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();
            var communicationHandler = new Mock<ICommunicationHandler>();
            var builderMock = new Mock<IProtocolConfigurationBuilder>();
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var hashBuilder = new Mock<ITransactionHashBuilder>();
            var integrationEndpointConfiguration = new Mock<IntegrationEndpointConfiguration>()
            {
                CallBase = true
            };

            var transactionExecutionDomain = new Mock<TransactionExecution>(busImpl, hashBuilder.Object)
            {
                CallBase = true
            };
            transactionExecutionDomain.As<IOriginator>();
            transactionExecutionDomain.As<IWithDomainBuilder>();
            transactionExecutionDomain.Setup(t => t.DomainBuilder).Returns(domainBuilderMock.Object);

            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<IntegrationEndpointConfiguration>())
                .Returns(integrationEndpointConfiguration.Object);


            communicationHandler.Setup(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>())).Throws<NotImplementedException>();



            transactionRepositoryMock.Setup(s => s.GetExecutionContextAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionExecutionMemento(
                        0,
                        Guid.Parse(recordKey),
                        0,
                        new[]
                        {
                            new IntegrationEndpointConfigurationMemento(
                                "jms",
                                string.Empty,
                                string.Empty,
                                EndpointTriggerType.Always)
                        },
                        new List<FieldConfigurationMemento>(),
                        new List<TransactionExecutionMemento>(),
                        new object(),
                        new List<Guid>(),
                        TransactionStatusType.Pending,
                        null));

            transactionRepositoryMock.Setup(s => s.GetRegistryEntry(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionRegistryMemento(
                        0,
                        Guid.Parse(recordKey),
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        TransactionStatusType.Processing,
                        string.Empty,
                        DateTime.Now,
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        0,
                        new List<TransactionRegistryMemento>()));

            jmsEndpointProcessorMock.Setup(s => s.Process(It.IsAny<object>(), It.IsAny<IList<FieldConfiguration>>()))
                .Returns(
                    new EndpointDataProcessorResult
                    {
                        Data = new object(),
                        Hash = string.Empty
                    });

            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;

            di.Rebind<IDependencyInjectionModule>(
                new AppServiceTestContainerInitializer(transactionRepositoryMock.Object))
                .Named("ApplicationScopeModule");

            di.Rebind<ICommandBus>(busImpl);
            di.Rebind<IDomainEventBus>(busImpl);
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);
            // di.Rebind<ITransactionRepository>(transactionRepositoryMock.Object);
            di.Rebind<IEndpointDataProcessor>(jmsEndpointProcessorMock.Object);
            di.Rebind<IConnectionConfigurationBuilder>(jmsConnectionBuilder.Object).Named("connection-builder-jms");
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");
            di.Rebind<IProtocolConfigurationBuilder>(builderMock.Object).Named("protocol-builder-jms");

            var configurator = di.ResolveType<ApplicationServiceConfigurator>();
            configurator.RegisterCommandHandlers();
            configurator.RegisterSagaHandlers();

            var service = di.ResolveType<ITransactionService>();

            await service.Process(
                new ProcessTransactionCommand
                {
                    RecordKey = Guid.Parse(recordKey)
                });

            Assert.Fail("Shouldn't have reached this");
        }

        [TestMethod]
        public async Task RetryBatch()
        {
            var recordKey = Guid.NewGuid().ToString("D");

            var busImpl = new InMemoryBus();
            InMemoryBus.Reset();
            var domainBuilderMock = new Mock<DomainBuilder>()
            {
                CallBase = true
            };
            var jmsEndpointProcessorMock = new Mock<IEndpointDataProcessor>();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();
            var communicationHandler = new Mock<ICommunicationHandler>();
            var builderMock = new Mock<IProtocolConfigurationBuilder>();
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var hashBuilder = new Mock<ITransactionHashBuilder>();
            var integrationEndpointConfiguration = new Mock<IntegrationEndpointConfiguration>()
            {
                CallBase = true
            };

            var transactionExecutionDomain = new Mock<TransactionExecution>(busImpl, hashBuilder.Object)
            {
                CallBase = true
            };
            transactionExecutionDomain.As<IOriginator>();
            transactionExecutionDomain.As<IWithDomainBuilder>();
            transactionExecutionDomain.Setup(t => t.DomainBuilder).Returns(domainBuilderMock.Object);

            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<IntegrationEndpointConfiguration>())
                .Returns(integrationEndpointConfiguration.Object);

            ConcurrentStack<Guid> handledTransactions = new ConcurrentStack<Guid>();

            communicationHandler.Setup(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>())).Callback(
                            (object obj,
                                IConnectionConfiguration ccfg,
                                IProtocolConfiguration pcfg) =>
                            {
                                TransactionDataReady dataReady = (TransactionDataReady)obj;
                                handledTransactions.Push(dataReady.RecordKey);
                            }).Returns(Task.CompletedTask);

            Dictionary<Guid, int> priorities = new Dictionary<Guid, int>
            {
                {
                    Guid.NewGuid(), 0
                },
                {
                    Guid.NewGuid(), 0
                },
                {
                    Guid.NewGuid(), 1
                },
                {
                    Guid.NewGuid(), 1
                },
                {
                    Guid.NewGuid(), 2
                },
                {
                    Guid.NewGuid(), 2
                },
                {
                    Guid.NewGuid(), 3
                },
                {
                    Guid.NewGuid(), 3
                },
                {
                    Guid.NewGuid(), 4
                },
                {
                    Guid.NewGuid(), 4
                },
                {
                    Guid.NewGuid(), 5
                },
                {
                    Guid.NewGuid(), 5
                },
                {
                    Guid.NewGuid(), 6
                },
                {
                    Guid.NewGuid(), 6
                }
            };

            var batch =
                priorities.Select(keyValuePair => this.CreateBatchChild(keyValuePair.Key, keyValuePair.Value))
                    .ToList();

            foreach (TransactionExecutionMemento transactionExecutionMemento in batch)
            {
                transactionExecutionMemento.Status = (TransactionStatusType)(transactionExecutionMemento.Priority ?? 0);
            }

            var expectedCount =
                batch.Count(
                    s => s.Status == TransactionStatusType.Failed || s.Status == TransactionStatusType.Pending || s.Status == TransactionStatusType.Canceled);
            Assert.AreNotEqual(0, expectedCount);


            transactionRepositoryMock.Setup(s => s.GetExecutionContextAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionExecutionMemento(
                        0,
                        Guid.Parse(recordKey),
                        0,
                        new[]
                        {
                            new IntegrationEndpointConfigurationMemento(
                                "jms",
                                string.Empty,
                                string.Empty,
                                EndpointTriggerType.Always)
                        },
                        new List<FieldConfigurationMemento>(),
                        batch,
                        new object(),
                        new List<Guid>(),
                        TransactionStatusType.Pending,
                        null));

            transactionRepositoryMock.Setup(s => s.GetRegistryEntry(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionRegistryMemento(
                        0,
                        Guid.Parse(recordKey),
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        TransactionStatusType.Processing,
                        string.Empty,
                        DateTime.Now,
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        0,
                        new List<TransactionRegistryMemento>()));

            transactionRepositoryMock.Setup(s => s.UpdateHashAsync(It.IsAny<Dictionary<Guid, string>>()))
                .Callback((Dictionary<Guid, string> d) => Log.Logger.Information("Received number of child hash: {0}", d.Count())).Returns(Task.CompletedTask);

            jmsEndpointProcessorMock.Setup(s => s.Process(It.IsAny<object>(), It.IsAny<IList<FieldConfiguration>>()))
                .Returns(
                    new EndpointDataProcessorResult
                    {
                        Data = new object(),
                        Hash = string.Empty
                    });

            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;

            di.Rebind<IDependencyInjectionModule>(
                new AppServiceTestContainerInitializer(transactionRepositoryMock.Object))
                .Named("ApplicationScopeModule");

            di.Rebind<ICommandBus>(busImpl);
            di.Rebind<IDomainEventBus>(busImpl);
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);
            di.Rebind<IEndpointDataProcessor>(jmsEndpointProcessorMock.Object);
            di.Rebind<IConnectionConfigurationBuilder>(jmsConnectionBuilder.Object).Named("connection-builder-jms");
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");
            di.Rebind<IProtocolConfigurationBuilder>(builderMock.Object).Named("protocol-builder-jms");

            var configurator = di.ResolveType<ApplicationServiceConfigurator>();
            configurator.RegisterCommandHandlers();
            configurator.RegisterSagaHandlers();

            var service = di.ResolveType<ITransactionService>();

            await service.Process(
                new ProcessTransactionCommand
                {
                    RecordKey = Guid.Parse(recordKey),
                    RetryPolicy = RetryPolicyType.Retry
                });

            // +1 for root transaction
            communicationHandler.Verify(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>()),
                Times.Exactly(expectedCount + 1));

            transactionRepositoryMock.Verify(s => s.UpdateHashAsync(It.Is<Dictionary<Guid, string>>(d => d.Count() == expectedCount + 1)), Times.Once);
        }

        [TestMethod]
        public async Task ForceRetryBatch()
        {
            var recordKey = Guid.NewGuid().ToString("D");

            var busImpl = new InMemoryBus();
            InMemoryBus.Reset();
            var domainBuilderMock = new Mock<DomainBuilder>()
            {
                CallBase = true
            };
            var jmsEndpointProcessorMock = new Mock<IEndpointDataProcessor>();
            var jmsConnectionBuilder = new Mock<IConnectionConfigurationBuilder>();
            var communicationHandler = new Mock<ICommunicationHandler>();
            var builderMock = new Mock<IProtocolConfigurationBuilder>();
            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            var hashBuilder = new Mock<ITransactionHashBuilder>();
            var integrationEndpointConfiguration = new Mock<IntegrationEndpointConfiguration>()
            {
                CallBase = true
            };

            var transactionExecutionDomain = new Mock<TransactionExecution>(busImpl, hashBuilder.Object)
            {
                CallBase = true
            };
            transactionExecutionDomain.As<IOriginator>();
            transactionExecutionDomain.As<IWithDomainBuilder>();
            transactionExecutionDomain.Setup(t => t.DomainBuilder).Returns(domainBuilderMock.Object);

            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<IntegrationEndpointConfiguration>())
                .Returns(integrationEndpointConfiguration.Object);

            ConcurrentStack<Guid> handledTransactions = new ConcurrentStack<Guid>();

            communicationHandler.Setup(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>())).Callback(
                            (object obj,
                                IConnectionConfiguration ccfg,
                                IProtocolConfiguration pcfg) =>
                            {
                                TransactionDataReady dataReady = (TransactionDataReady)obj;
                                handledTransactions.Push(dataReady.RecordKey);
                            }).Returns(Task.CompletedTask);

            Dictionary<Guid, int> priorities = new Dictionary<Guid, int>
            {
                {
                    Guid.NewGuid(), 0
                },
                {
                    Guid.NewGuid(), 0
                },
                {
                    Guid.NewGuid(), 1
                },
                {
                    Guid.NewGuid(), 1
                },
                {
                    Guid.NewGuid(), 2
                },
                {
                    Guid.NewGuid(), 2
                },
                {
                    Guid.NewGuid(), 3
                },
                {
                    Guid.NewGuid(), 3
                },
                {
                    Guid.NewGuid(), 4
                },
                {
                    Guid.NewGuid(), 4
                },
                {
                    Guid.NewGuid(), 5
                },
                {
                    Guid.NewGuid(), 5
                },
                {
                    Guid.NewGuid(), 6
                },
                {
                    Guid.NewGuid(), 6
                }
            };

            var batch =
                priorities.Select(keyValuePair => this.CreateBatchChild(keyValuePair.Key, keyValuePair.Value))
                    .ToList();

            foreach (TransactionExecutionMemento transactionExecutionMemento in batch)
            {
                transactionExecutionMemento.Status = (TransactionStatusType)(transactionExecutionMemento.Priority ?? 0);
            }

            //foreach (TransactionExecutionMemento transactionExecutionMemento in batch.Where(s => s.Priority == 5))
            //{
            //    transactionExecutionMemento.Status = TransactionStatusType.Skipped;
            //}

            //foreach (TransactionExecutionMemento transactionExecutionMemento in batch.Where(s => s.Priority == 2))
            //{
            //    transactionExecutionMemento.Status = TransactionStatusType.Success;
            //}

            var expectedCount =
                  batch.Count(
                      s =>
                          s.Status == TransactionStatusType.Canceled || s.Status == TransactionStatusType.Failed ||
                          s.Status == TransactionStatusType.Pending || s.Status == TransactionStatusType.Invalid ||
                          s.Status == TransactionStatusType.Skipped || s.Status == TransactionStatusType.Processing);
            Assert.AreNotEqual(0, expectedCount);


            transactionRepositoryMock.Setup(s => s.GetExecutionContextAsync(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionExecutionMemento(
                        0,
                        Guid.Parse(recordKey),
                        0,
                        new[]
                        {
                            new IntegrationEndpointConfigurationMemento(
                                "jms",
                                string.Empty,
                                string.Empty,
                                EndpointTriggerType.Always)
                        },
                        new List<FieldConfigurationMemento>(),
                        batch,
                        new object(),
                        new List<Guid>(),
                        TransactionStatusType.Pending,
                        null));

            transactionRepositoryMock.Setup(s => s.GetRegistryEntry(It.IsAny<Guid>()))
                .ReturnsAsync(
                    new TransactionRegistryMemento(
                        0,
                        Guid.Parse(recordKey),
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        TransactionStatusType.Processing,
                        string.Empty,
                        DateTime.Now,
                        null,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        0,
                        new List<TransactionRegistryMemento>()));

            transactionRepositoryMock.Setup(s => s.UpdateHashAsync(It.IsAny<Dictionary<Guid, string>>()))
                .Callback((Dictionary<Guid, string> d) => Log.Logger.Information("Received number of child hash: {0}", d.Count())).Returns(Task.CompletedTask);

            jmsEndpointProcessorMock.Setup(s => s.Process(It.IsAny<object>(), It.IsAny<IList<FieldConfiguration>>()))
                .Returns(
                    new EndpointDataProcessorResult
                    {
                        Data = new object(),
                        Hash = string.Empty
                    });

            var di = ApplicationIntegration.DependencyResolver as TestDependencyInjectionAdapter;

            di.Rebind<IDependencyInjectionModule>(
                new AppServiceTestContainerInitializer(transactionRepositoryMock.Object))
                .Named("ApplicationScopeModule");

            di.Rebind<ICommandBus>(busImpl);
            di.Rebind<IDomainEventBus>(busImpl);
            di.Rebind<IDomainBuilder>(domainBuilderMock.Object);
            di.Rebind<IEndpointDataProcessor>(jmsEndpointProcessorMock.Object);
            di.Rebind<IConnectionConfigurationBuilder>(jmsConnectionBuilder.Object).Named("connection-builder-jms");
            di.Rebind<ICommunicationHandler>(communicationHandler.Object).Named("communication-jms");
            di.Rebind<IProtocolConfigurationBuilder>(builderMock.Object).Named("protocol-builder-jms");

            var configurator = di.ResolveType<ApplicationServiceConfigurator>();
            configurator.RegisterCommandHandlers();
            configurator.RegisterSagaHandlers();

            var service = di.ResolveType<ITransactionService>();

            await service.Process(
                new ProcessTransactionCommand
                {
                    RecordKey = Guid.Parse(recordKey),
                    RetryPolicy = RetryPolicyType.Force
                });

            // +1 for root transaction
            communicationHandler.Verify(
                s =>
                    s.Handle(
                        It.IsAny<object>(),
                        It.IsAny<IConnectionConfiguration>(),
                        It.IsAny<IProtocolConfiguration>()),
                Times.Exactly(expectedCount + 1));

            transactionRepositoryMock.Verify(s => s.UpdateHashAsync(It.Is<Dictionary<Guid, string>>(d => d.Count() == expectedCount + 1)), Times.Once);
        }

        #endregion

        #region Helper methods
        private TransactionExecutionMemento CreateBatchChild(
            Guid recordKey,
            int priority)
        {
            return new TransactionExecutionMemento(
                0,
                recordKey,
                0,
                new[]
                {
                    new IntegrationEndpointConfigurationMemento(
                        "jms",
                        string.Empty,
                        string.Empty,
                        EndpointTriggerType.Always)
                },
                new List<FieldConfigurationMemento>(),
                new List<TransactionExecutionMemento>(),
                new object(),
                new List<Guid>(),
                TransactionStatusType.Pending,
                priority);
        }
        #endregion

        #region Test classes
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
        #endregion
    }
}