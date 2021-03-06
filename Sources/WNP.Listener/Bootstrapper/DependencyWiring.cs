﻿// <copyright file="DependencyWiring.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System;
    using ApplicationService;
    using ApplicationService.BatchBuilder;
    using ApplicationService.Implementations;
    using Bus;
    using Communication.Jms;
    using Core;
    using Domain;
    using Domain.Listener;
    using Domain.Listener.Transaction;
    using MetadataService;
    using MetadataService.Implementations;
    using Ninject.Modules;
    using Ninject.Web.Common;
    using ODataService;
    using ODataService.Services;
    using ODataService.Services.Filter;
    using ODataService.Services.Implementations;
    using ODataService.Services.Implementations.Filter;
    using ODataService.Services.Implementations.Query;
    using ODataService.Services.Query;
    using Persistence.Listener;
    using Persistence.Poco;
    using Persistence.WNP;
    using Persistence.WNPAsync;
    using Repository;
    using Repository.Listener;
    using Repository.WNP;
    using Serilog;

    /// <summary>
    /// Creates application configuration using dependency injection.
    /// </summary>
    public class DependencyWiring : NinjectModule
    {
        /// <inheritdoc/>
        public override string Name => "DependencyWiring";

        /// <inheritdoc/>
        public override void Load()
        {
            // -------------------------
            // Core bindings
            // -------------------------
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
            this.Kernel.Bind<ITransactionHashBuilder>().To<TransactionHashBuilder>().InSingletonScope();
            this.Kernel.Bind<ISummaryBuilder>().To<SummaryBuilder>().InSingletonScope();
            this.Kernel.Bind<IEndpointDataProcessor>().To<DefaultEndpointDataProcessor>().InSingletonScope();
            this.Kernel.Bind<IConnectionConfigurationBuilder>().To<JmsConnectionConfigurationBuilder>().InSingletonScope().Named("connection-builder-jms");
            this.Kernel.Bind<IProtocolConfigurationBuilder>().To<Communication.Jms.ProtocolConfigurationBuilder>().InSingletonScope().Named("protocol-builder-jms");

            // -------------------------
            // Communication bindings
            // -------------------------
            this.Kernel.Bind<ICommunicationHandler>().To<JmsDispatcher>().Named("communication-jms");

            // -------------------------
            // Metadata service bindings
            // -------------------------
            this.Kernel.Bind<IMetadataProvider>().To<MetadataProvider>().InTransientScope();
            this.Kernel.Bind<IActionConfigurator>().To<ActionConfigurator>().InSingletonScope();
            this.Kernel.Bind<IEntityConfigurator>().To<EntityConfigurator>().InSingletonScope();

            try
            {
                Mappers.Register(typeof(WNPMetadataEntry), new WNPMetadataMapping());
                Mappers.Register(typeof(MeterTestResultsEntity), new AsyncMapper());
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
            this.Kernel.Bind<IBatchBuilder>().To<MeterTestResultBatchBuilder>();
            this.Kernel.Bind<ICurrentRequestScope>().To<CurrentRequestScope>().InRequestScope();

            // -------------------------
            // Repository bindings
            // -------------------------
            this.Kernel.Bind<IPersistenceAdapter>().To<ListenerPersistenceAdapter>().WhenClassHas<WithinListenerContextAttribute>();
            this.Kernel.Bind<IPersistenceAdapter>().To<WnpPersistenceAdapter>().WhenClassHas<WithinWnpContextAttribute>();
            this.Kernel.Bind<IWnpIntegrationService>().To<WnpIntegrationService>().InSingletonScope();
            this.Kernel.Bind<ITransactionDataRepository>().To<TransactionDataRepository>();

            // -------------------------
            // Repository.WNP bindings
            // -------------------------
            this.Kernel.Bind<IWNPUnitOfWork>().To<WNPUnitOfWork>().InRequestScope();
            this.Kernel.Bind<IWnpBatchRepository>().To<WnpRepository>();

            // -------------------------
            // Persistence.Listener bindings
            // -------------------------
            this.Kernel.Bind<ListenerDbContext>().ToSelf().WithConstructorArgument("connectionStringName", "ListenerDB");

            // -------------------------
            // Persistence.WNP bindings
            // -------------------------
            this.Kernel.Bind<WNPDBContext>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument("connectionStringName", "WNPDatabase");
            this.Kernel.Bind<WnpAsyncDbContext>().ToSelf().WithConstructorArgument("connectionStringName", "WNPDatabase");

            this.Kernel.Bind<WNPUnitOfWork>()
                .ToSelf()
                .InRequestScope();

            // -------------------------
            // OData service bindings
            // -------------------------
            this.Kernel.Bind<ListenerODataContext>().ToSelf().InRequestScope();
            this.Kernel.Bind<ODataServiceConfigurator>().ToSelf().InSingletonScope();
            this.Kernel.Bind<IEdmModelGenerator>().To<EdmModelGenerator>();
            this.Kernel.Bind<IFilterTransformer>().To<FilterTransformer>();
            this.Kernel.Bind<IODataFunctionToSqlConvertor>().To<ODataFunctionToSqlConvertorSqlServer>();
            this.Kernel.Bind<ApplicationServiceConfigurator>().ToSelf().InSingletonScope();
            this.Kernel.Bind<IODataQueryHandlerFactory>().To<ODataQueryHandlerFactory>().InRequestScope();

            this.Kernel.Bind<IDependencyInjectionModule>().To<ApplicationScopeDIInitializer>().InSingletonScope().Named("ApplicationScopeModule");
        }
    }
}