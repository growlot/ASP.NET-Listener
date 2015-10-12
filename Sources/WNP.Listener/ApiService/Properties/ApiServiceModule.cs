// //-----------------------------------------------------------------------
// // <copyright file="ApiServiceModule.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService.Properties
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

    public class ApiServiceModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ListenerDbContext>().ToSelf().InRequestScope().WithConstructorArgument("connectionStringName", "ListenerDB");
            //Kernel.Bind<ListenerODataContext>().ToSelf().InRequestScope();

            Kernel.Bind<IPersistenceAdapter>().To<PocoCachedAdapter>().InRequestScope();

            this.Kernel.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();
            this.Kernel.Bind<ApiServiceConfigurator>().ToSelf().InSingletonScope();

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