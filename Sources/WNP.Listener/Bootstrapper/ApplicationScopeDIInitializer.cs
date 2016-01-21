// <copyright file="ApplicationScopeDIInitializer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using ApplicationService;
    using Core;
    using Ninject;
    using Persistence.Listener;
    using Persistence.WNPAsync;
    using Repository;
    using Repository.Listener;

    /// <summary>
    /// Initializing dependency injection for application scope
    /// </summary>
    public class ApplicationScopeDIInitializer : IDependencyInjectionModule
    {
        /// <summary>
        /// Initializes the specified container.
        /// </summary>
        /// <param name="container">The container.</param>
        public void Initialize(object container)
        {
            var kernel = (StandardKernel)container;
            kernel.Bind<IPersistenceAdapter>().To<ListenerPersistenceAdapter>().WhenClassHas<WithinListenerContextAttribute>();
            kernel.Bind<IPersistenceAdapter>().To<WnpPersistenceAdapter>().WhenClassHas<WithinWnpContextAttribute>();
            kernel.Bind<IRepositoryManager>().To<RepositoryManager>();
            kernel.Bind<ITransactionRepository>().To<TransactionRepository>();
        }
    }
}