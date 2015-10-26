// <copyright file="DependencyWiring.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System;
    using ApplicationService;
    using ApplicationService.Implementations;
    using ApplicationService.Validator;
    using Bus;
    using Communication;
    using Communication.Jms;
    using Core;
    using Domain;
    using Domain.Listener.Transaction;
    using MetadataService;
    using MetadataService.Implementations;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using ODataService;
    using ODataService.Services;
    using ODataService.Services.FilterTransformer;
    using ODataService.Services.Impl;
    using ODataService.Services.Impl.FilterTransformer;
    using Persistence;
    using Persistence.Listener;
    using Repository;
    using Serilog;

    /// <summary>
    /// Creates application configuration using dependency injection.
    /// </summary>
    public class DependencyWiring : NinjectModule
    {
        /// <inheritdoc/>
        public override void Load()
        {
            // -------------------------
            // Core bindings
            // -------------------------
            this.Kernel.Bind<IPersistenceAdapter>().To<PocoCachedAdapter>().InRequestScope();
            this.Kernel.Bind<IDateTimeProvider>().To<UtcDateTimeProvider>().InSingletonScope();

            // -------------------------
            // Domain bindings
            // -------------------------
            this.Kernel.Bind<IDomainEventBus>().To<InMemoryBus>().InSingletonScope();
            this.Kernel.Bind<IDomainBuilder>().To<DomainBuilder>().InSingletonScope();

            // -------------------------
            // Domain.Listener bindings
            // -------------------------
            this.Kernel.Bind<IRecordKeyBuilder>().To<RecordKeyBuilder>().InSingletonScope();
            this.Kernel.Bind<ITransactionKeyBuilder>().To<TransactionKeyBuilder>().InSingletonScope();
            this.Kernel.Bind<ISummaryBuilder>().To<SummaryBuilder>().InSingletonScope();
            this.Kernel.Bind<IEndpointDataProcessor>().To<DefaultEndpointDataProcessor>().InSingletonScope();
            this.Kernel.Bind<IConnectionConfigurationBuilder>().To<JmsConnectionConfigurationBuilder>().InSingletonScope().Named("connection-builder-jms");
            this.Kernel.Bind<IProtocolConfigurationBuilder>().To<Communication.Jms.ProtocolConfigurationBuilder>().InSingletonScope().Named("protocol-builder-jms");
            this.Kernel.Bind<IUniqueHashValidator>().To<UniqueHashValidator>();

            // -------------------------
            // Communication bindings
            // -------------------------
            this.Kernel.Bind<ICommunicationHandler>().To<JmsDispatcher>().InSingletonScope().Named("communication-jms");

            // -------------------------
            // Metadata service bindings
            // -------------------------
            this.Kernel.Bind<IMetadataProvider>().To<MetadataProvider>().InTransientScope();
            this.Kernel.Bind<IActionConfigurator>().To<ActionConfigurator>().InSingletonScope();

            try
            {
                Mappers.Register(typeof(WNPMetadataEntry), new WNPMetadataMapping());
            }
            catch (ArgumentException)
            {
                Log.Error("Mapper Error (possible duplicate registration). No Rethrow");
            }

            // -------------------------
            // Application service bindings
            // -------------------------
            this.Kernel.Bind<ICommandBus>().To<InMemoryBus>().InSingletonScope();
            this.Kernel.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();
            this.Kernel.Bind<IApplicationServiceScope>().To<ApplicationServiceScope>();
            this.Kernel.Bind<ApplicationServiceConfigurator>().ToSelf().InSingletonScope();

            // -------------------------
            // Repository bindings
            // -------------------------
            this.Kernel.Bind<IRepositoryManager>().To<RepositoryManager>();
            this.Kernel.Bind<ITransactionRepository>().To<TransactionRepository>();
            this.Kernel.Bind<ITransactionDataRepository>().To<TransactionDataRepository>();

            // -------------------------
            // Persistence bindings
            // -------------------------
            this.Kernel.Bind<WNPDBContext>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionStringName", "WNPDatabase");

            // -------------------------
            // Persistence.Listener bindings
            // -------------------------
            this.Kernel.Bind<ListenerDbContext>().ToSelf().InRequestScope().WithConstructorArgument("connectionStringName", "ListenerDB");

            // -------------------------
            // OData service bindings
            // -------------------------
            this.Kernel.Bind<ListenerODataContext>().ToSelf().InRequestScope();
            this.Kernel.Bind<ODataServiceConfigurator>().ToSelf().InSingletonScope();
            this.Kernel.Bind<IEdmModelGenerator>().To<EdmModelGeneratorImpl>();
            this.Kernel.Bind<IODataRouteManager>().To<ODataRouteManagerImpl>();
            this.Kernel.Bind<IAutoConvertor>().To<AutoConvertorImpl>();
            this.Kernel.Bind<IFilterTransformer>().To<FilterTransformerImpl>();
            this.Kernel.Bind<IODataFunctionToSqlConvertor>().To<ODataFunctionToSqlConvertorSqlServerImpl>();
        }
    }
}