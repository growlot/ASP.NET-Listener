﻿// <copyright file="ApplicationScopeDIInitializer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System;
    using ApplicationService;
    using ApplicationService.Implementations;
    using Core;
    using Ninject;
    using Persistence.Listener;
    using Persistence.WNPAsync;
    using Repository;
    using Repository.WNP;

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
            kernel.Bind<IPersistenceAdapter>().To<ListenerPersistenceAdapter>().WhenClassHas<WithinListenerContextAttribute>().InSingletonScope();
            kernel.Bind<IPersistenceAdapter>().To<WnpPersistenceAdapter>().WhenClassHas<WithinWnpContextAttribute>().InSingletonScope();
            kernel.Bind<IRepositoryManager>().To<RepositoryManager>().InSingletonScope();
            kernel.Bind<ITransactionRepository>().To<TransactionRepository>().InSingletonScope();
            kernel.Bind<IWnpBatchRepository>().To<WnpRepository>().InSingletonScope();
            kernel.Bind<ITransactionDataRepository>().To<TransactionDataRepository>().InSingletonScope();
        }
    }
}