// <copyright file="Startup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System.Web.Cors;
    using System.Web.Http;

    using AMSLLC.Listener.Bootstrapper.Owin;
    using Core;
    using Core.Ninject;

    using global::Owin;
    using Microsoft.Owin.Cors;
    using Ninject;
    using Ninject.Web.Common.OwinHost;

    using ODataService;
    using Owin.Middleware;
    using Serilog;
    using StackExchange.Profiling;

    /// <summary>
    /// Manages application/service startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configuration(IAppBuilder app)
        {
            var diAdapterExist = ApplicationIntegration.DependencyResolver != null;
            var diAdapter = (diAdapterExist ? ApplicationIntegration.DependencyResolver : new NinjectDependencyInjectionAdapter()) as INinjectDependencyInjectionAdapter;
            if (diAdapter == null)
            {
                throw new ActivationException("Expecting Ninject as dependency injection container");
            }

            diAdapter.Initialize(container =>
            {
                if (!container.HasModule("DependencyWiring"))
                {
                    container.Load<DependencyWiring>();
                }
            });

            if (!diAdapterExist)
            {
                ApplicationIntegration.SetDependencyInjectionResolver(diAdapter);
            }

            this.InitOwinHost(app, diAdapter);
            this.InitProfiler();

            var log = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            Log.Logger = log;
        }

        private void InitProfiler()
        {
            MiniProfiler.Settings.ProfilerProvider = new OwinRequestProfilerProvider();
            MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter(true);
        }

        private void InitOwinHost(IAppBuilder app, INinjectDependencyInjectionAdapter diAdapter)
        {
            // var policy = new CorsPolicy {AllowAnyHeader = true, AllowAnyMethod = true, AllowAnyOrigin = true, SupportsCredentials = };
            app.UseCors(CorsOptions.AllowAll);
            app.UseRequestScopeContext();

            diAdapter.Initialize(container => app.UseNinjectMiddleware(() => container));

            var config = diAdapter.ResolveType<HttpConfiguration>();
            app.UseNinjectWebApi(config);

            var oDataServiceConfigurator = diAdapter.ResolveType<ODataServiceConfigurator>();
            oDataServiceConfigurator.Configure(config);

            var applicationServerConfigurator = diAdapter.ResolveType<ApplicationServiceConfigurator>();
            applicationServerConfigurator.RegisterCommandHandlers();
            applicationServerConfigurator.RegisterSagaHandlers();
            applicationServerConfigurator.RegisterPersistenceHandlers();

            config.EnsureInitialized();
        }
    }
}