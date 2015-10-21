namespace AMSLLC.Listener.ODataService.Properties
{
    using Persistence;
    using ApplicationService;
    using ApplicationService.Impl;
    using ApplicationService.Validator;
    using Communication;
    using Communication.Jms;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using Persistence.Listener;
    using Repository;
    using Repository.Listener;
    using Services;
    using Services.FilterTransformer;
    using Services.Impl;
    using Services.Impl.FilterTransformer;

    public class ODataServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind<ListenerODataContext>().ToSelf().InRequestScope();

            this.Kernel.Bind<WNPDBContext>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionStringName", "WNPDatabase");

            this.Kernel.Bind<ODataServiceConfigurator>().ToSelf().InSingletonScope();
            this.Kernel.Bind<ApplicationServiceConfigurator>().ToSelf().InSingletonScope();

            this.Kernel.Bind<IEdmModelGenerator>().To<EdmModelGeneratorImpl>();
            this.Kernel.Bind<IODataRouteManager>().To<ODataRouteManagerImpl>();

            this.Kernel.Bind<IAutoConvertor>().To<AutoConvertorImpl>();
            this.Kernel.Bind<IFilterTransformer>().To<FilterTransformerImpl>();
            this.Kernel.Bind<IODataFunctionToSqlConvertor>().To<ODataFunctionToSqlConvertorSqlServerImpl>();

            this.Kernel.Bind<ListenerDbContext>().ToSelf().InRequestScope().WithConstructorArgument("connectionStringName", "ListenerDB");
            //Kernel.Bind<ListenerODataContext>().ToSelf().InRequestScope();

            this.Kernel.Bind<IPersistenceAdapter>().To<PocoCachedAdapter>().InRequestScope();

            this.Kernel.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();

            this.Kernel.Bind<IApplicationServiceScope>().To<ApplicationServiceScope>();
            this.Kernel.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
            this.Kernel.Bind<IDomainBuilder>().To<DomainBuilder>().InSingletonScope();
            this.Kernel.Bind<IRepositoryManager>().To<RepositoryManager>();
            this.Kernel.Bind<ITransactionRepository>().To<TransactionRepository>();
            this.Kernel.Bind<IRecordKeyBuilder>().To<RecordKeyBuilder>().InSingletonScope();
            this.Kernel.Bind<ITransactionKeyBuilder>().To<TransactionKeyBuilder>().InSingletonScope();
            this.Kernel.Bind<ISummaryBuilder>().To<SummaryBuilder>().InSingletonScope();
            this.Kernel.Bind<IEndpointDataProcessor>().To<DefaultEndpointDataProcessor>().InSingletonScope();
            this.Kernel.Bind<IConnectionConfigurationBuilder>().To<JmsConnectionConfigurationBuilder>().InSingletonScope().Named("connection-builder-jms");
            this.Kernel.Bind<ICommunicationHandler>().To<JmsDispatcher>().InSingletonScope().Named("communication-jms");

            this.Kernel.Bind<IUniqueHashValidator>().To<UniqueHashValidator>();
        }
    }
}