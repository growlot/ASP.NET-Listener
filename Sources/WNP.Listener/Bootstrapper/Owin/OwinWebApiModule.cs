// <copyright file="OwinWebApiModule.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper.Owin
{
    using System;
    using System.Web.Http;

    using Ninject.Modules;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi;

    /// <summary>
    /// The OWIN web API module.
    /// </summary>
    internal class OwinWebApiModule : NinjectModule
    {
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly HttpConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinWebApiModule"/> class.
        /// </summary>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        public OwinWebApiModule(HttpConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Loads the module.
        /// </summary>
        public override void Load()
        {
            this.Kernel.Bind<HttpConfiguration>().ToConstant(this.configuration);
        }

        /// <summary>
        /// Called after loading the modules. A module can verify here if all other required modules are loaded.
        /// </summary>
        public override void VerifyRequiredModulesAreLoaded()
        {
            if (!this.Kernel.HasModule(typeof(WebApiModule).FullName))
            {
                throw new InvalidOperationException("This module requires Ninject.Web.WebAPI extension");
            }

            // no need in OwinNinjectDependencyResolver, because we get scope from Owin
            // this.Rebind<IDependencyResolver>().To<OwinNinjectDependencyResolver>();
            this.Kernel.Components.RemoveAll<IWebApiRequestScopeProvider>();
            this.Kernel.Components.Add<IWebApiRequestScopeProvider, OwinRequestScopeProvider>();

            // need to reload INinjectHttpApplicationPlugin, otherwise it will use old implementation of
            // IWebApiRequestScopeProvider
            this.Kernel.Components.RemoveAll<INinjectHttpApplicationPlugin>();
            this.Kernel.Components.Add<INinjectHttpApplicationPlugin, NinjectWebApiHttpApplicationPlugin>();
        }
    }
}