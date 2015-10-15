namespace AMSLLC.Listener.ODataService.Properties
{
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
            Kernel.Bind<ListenerODataContext>().ToSelf().InRequestScope();

            Kernel.Bind<WNPDBContext>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionStringName", "WNPDatabase");

            Kernel.Bind<ODataServiceConfigurator>().ToSelf().InSingletonScope();
            Kernel.Bind<ApplicationServiceConfigurator>().ToSelf().InSingletonScope();

            Kernel.Bind<IEdmModelGenerator>().To<EdmModelGeneratorImpl>();            
            Kernel.Bind<IODataRouteManager>().To<ODataRouteManagerImpl>();

            Kernel.Bind<IAutoConvertor>().To<AutoConvertorImpl>();
            Kernel.Bind<IFilterTransformer>().To<FilterTransformerImpl>();
            Kernel.Bind<IODataFunctionToSqlConvertor>().To<ODataFunctionToSqlConvertorSqlServerImpl>();


            Kernel.Bind<ListenerDbContext>().ToSelf().InRequestScope().WithConstructorArgument("connectionStringName", "ListenerDB");
            //Kernel.Bind<ListenerODataContext>().ToSelf().InRequestScope();

            Kernel.Bind<IPersistenceAdapter>().To<PocoCachedAdapter>().InRequestScope();

            this.Kernel.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();
            

            this.Kernel.Bind<IApplicationServiceScope>().To<ApplicationServiceScope>();
            this.Kernel.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();
            this.Kernel.Bind<IDomainBuilder>().To<DomainBuilder>().InSingletonScope();
            this.Kernel.Bind<IRepositoryManager>().To<RepositoryManager>();
            this.Kernel.Bind<ITransactionRepository>().To<TransactionRepository>();
            this.Kernel.Bind<ITransactionKeyBuilder>().To<TransactionKeyBuilder>().InSingletonScope();
            this.Kernel.Bind<IEndpointDataProcessor>().To<DefaultEndpointDataProcessor>().InSingletonScope();
            this.Kernel.Bind<IConnectionConfigurationBuilder>().To<JmsConnectionConfigurationBuilder>().InSingletonScope().Named("connection-builder-jms");
            this.Kernel.Bind<ICommunicationHandler>().To<JmsDispatcher>().InSingletonScope().Named("communication-jms");


            this.Kernel.Bind<IUniqueHashValidator>().To<UniqueHashValidator>();
        }
    }
}