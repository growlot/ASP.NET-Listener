// <copyright file="Startup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Bootstrapper
{
    using System.Web.Http;
    using ApplicationService;
    using Core;
    using Core.Ninject;
    using global::Owin;
    using Microsoft.Owin.Diagnostics;
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
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
            var diAdapter = new NinjectDependencyInjectionAdapter();
            diAdapter.Initialize(container =>
            {
                container.Load<DependencyWiring>();
            });

            ApplicationIntegration.SetDependencyInjectionResolver(diAdapter);

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

        private void InitOwinHost(IAppBuilder app, NinjectDependencyInjectionAdapter diAdapter)
        {
            var config = diAdapter.ResolveType<HttpConfiguration>();

            var odataServiceConfigurator = diAdapter.ResolveType<ODataServiceConfigurator>();
            odataServiceConfigurator.Configure(config);

            diAdapter.ResolveType<ApplicationServiceConfigurator>().Configure();

            app.UseRequestScopeContext();
            app.UseErrorPage(ErrorPageOptions.ShowAll);
            diAdapter.Initialize(container => app.UseNinjectMiddleware(() => container).UseNinjectWebApi(config));

            config.EnsureInitialized();
        }
    }
}