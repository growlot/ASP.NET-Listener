// //-----------------------------------------------------------------------
// // <copyright file="ApiServiceModule.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService.Properties
{
    using ApplicationService;
    using ApplicationService.Impl;
    using Ninject.Modules;

    public class ApiServiceModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind<ITransactionService>().To<TransactionService>().InSingletonScope();
            this.Kernel.Bind<ApiServiceConfigurator>().ToSelf().InSingletonScope();
        }
    }
}