using System.Web.Http;
using AMSLLC.Listener.Bootstrapper.Owin.Middleware;
using AMSLLC.Listener.MetadataService.Properties;
using AMSLLC.Listener.ODataService;
using AMSLLC.Listener.ODataService.Actions.Properties;
using AMSLLC.Listener.ODataService.Properties;
using Microsoft.Owin.Diagnostics;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using StackExchange.Profiling;

namespace AMSLLC.Listener.Bootstrapper
{

    using ApplicationService;
    using Core;
    using Core.Ninject;
    using Serilog;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var diAdapter = new NinjectDependencyInjectionAdapter();
            diAdapter.Initialize(container =>
            {
                container.Load<ODataServiceModule>();
                // TODO: is there a better way to initialize ODataService.Actions assembly?
                container.Load<ODataServiceActionsModule>();
                container.Load<MetadataServiceModule>();
            });

            ApplicationIntegration.SetDependencyInjectionResolver(diAdapter);

            InitOwinHost(app, diAdapter.Kernel);
            InitProfiler();

            var log =
                new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            Log.Logger = log;
        }

        private void InitProfiler()
        {
            MiniProfiler.Settings.ProfilerProvider = new OwinRequestProfilerProvider();
            MiniProfiler.Settings.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter(true);
        }

        private void InitOwinHost(IAppBuilder app, StandardKernel kernel)
        {
            var config = kernel.Get<HttpConfiguration>();

            var odataServiceConfigurator = kernel.Get<ODataServiceConfigurator>();
            odataServiceConfigurator.Configure(config);

            var appConfigurator = kernel.Get<ApplicationServiceConfigurator>();
            appConfigurator.Configure();

            app.UseRequestScopeContext();
            app.UseErrorPage(ErrorPageOptions.ShowAll);
            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);

            config.EnsureInitialized();
        }
    }
}