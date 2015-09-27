// //-----------------------------------------------------------------------
// // <copyright file="IntegrationTest.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.Test
{
    using System.Threading.Tasks;
    using AMSLLC.Core;
    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.Domain.Listener.Transaction;
    using AMSLLC.Listener.Domain.Listener.Transaction.DomainEvent;
    using AMSLLC.Listener.Domain.Listener.Transaction.Endpoint;
    using AMSLLC.Listener.Repository;
    using ApplicationService;
    using ApplicationService.Impl;
    using Communication;
    using Core.Ninject;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public async Task TestEndpointFlow()
        {
            const string sourceApplicationId = "{77C79AAF-8F95-46FE-8873-6D7C291ED092}";
            const string targetApplicationid = "{D425852C-4600-49D6-9BF6-5640D1E66509}";
            const string operationKey = "SendDevice";
            const string transactionKey = "{70754941-6156-4C79-8F17-0C6BB5A85D9E}";

            var di = new NinjectDependencyInjectionAdapter();

            var transactionRepositoryMock = new Mock<ITransactionRepository>();

            var transactionExecutionDomain = new Mock<TransactionExecution> {CallBase = true};
            transactionExecutionDomain.As<IOriginator>();

            var memento = new TransactionExecutionMemento(sourceApplicationId, targetApplicationid, operationKey);
            memento.EndpointConfigurations.Add(new JmsEndpointConfiguration());

            //transactionExecutionDomain.Setup(s => s.Process(transactionKey)).CallBase();
            //transactionExecutionDomain.As<IOriginator>().Setup(s => s.SetMemento(It.IsAny<IMemento>()));

            transactionRepositoryMock.Setup(s => s.Get(sourceApplicationId, targetApplicationid, operationKey))
                .Returns(
                    (string saId, string taId, string opKey) => Task.FromResult(memento));


            var domainBuilderMock = new Mock<IDomainBuilder>();

            //domainBuilderMock.Setup(d => d.Create<TransactionExecution>(It.IsAny<IMemento>())).Returns(transactionExecutionDomain.Object);
            domainBuilderMock.Setup(d => d.Create<TransactionExecution>()).Returns(transactionExecutionDomain.Object);

            var jmsEndpointProcessorMock = new Mock<JmsEndpointProcessor>();
            jmsEndpointProcessorMock.Setup(
                e =>
                    e.Process(sourceApplicationId, targetApplicationid, operationKey, transactionKey,
                        It.IsAny<IIntegrationEndpointConfiguration>())).CallBase();

            di.Initialize(container =>
            {
                container.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();
                container.Bind<IApplicationServiceScope>().To<ApplicationServiceScope>();
                container.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
                container.Bind<IDomainBuilder>().ToConstant(domainBuilderMock.Object);
                container.Bind<IRepositoryManager>().To<RepositoryManager>();
                container.Bind<ITransactionRepository>().ToConstant(transactionRepositoryMock.Object);
                container.Bind<IEndpointProcessor>().ToConstant(jmsEndpointProcessorMock.Object).Named("endpoint-jms");
            });


            ApplicationIntegration.SetDependencyInjectionResolver(di);

            var handlerMock = new Mock<IDomainHandler>();
            var handlerMock2 = new Mock<IDomainHandler>();

            ApplicationEventManager.Instance.RegisterEvent(typeof(JmsDataReady), handlerMock.Object);
            ApplicationEventManager.Instance.RegisterEvent(typeof(JmsDataReady), handlerMock2.Object);
            EventsRegister.RegisterAsync<JmsDataReady>((d) => ApplicationEventManager.Instance.Handle(d));

            var service = di.ResolveType<ITransactionService>();

            await
                service.Process(new TransactionRequestMessage
                {
                    TransactionId = transactionKey,
                    DestinationApplicationId = targetApplicationid,
                    SourceApplicationId = sourceApplicationId,
                    OperationKey = operationKey
                });

            transactionExecutionDomain.Verify(foo => foo.Process(transactionKey), Times.Once());
            jmsEndpointProcessorMock.Verify(
                foo => foo.Process(sourceApplicationId, targetApplicationid, operationKey, transactionKey,
                    It.IsAny<IIntegrationEndpointConfiguration>()), Times.Once());
            handlerMock.Verify(foo => foo.Handle(It.IsAny<JmsDataReady>()), Times.Once());
            handlerMock2.Verify(foo => foo.Handle(It.IsAny<JmsDataReady>()), Times.Once());
        }
    }
}