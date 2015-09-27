using System.Web.Http;
using AMSLLC.Listener.Bootstrapper.Owin.Middleware;
using AMSLLC.Listener.MetadataService.Properties;
using AMSLLC.Listener.ODataService;
using AMSLLC.Listener.ODataService.Properties;
using Microsoft.Owin.Diagnostics;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using StackExchange.Profiling;

namespace AMSLLC.Listener.Bootstrapper
{
    using ApiService.Properties;
    using ApplicationService;
    using Core;
    using Core.Ninject;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var diAdapter = new NinjectDependencyInjectionAdapter();
            diAdapter.Initialize(container =>
            {
                container.Load<ODataServiceModule>();
                container.Load<MetadataServiceModule>();
                container.Load<ApiServiceModule>();
            });

            ApplicationIntegration.SetDependencyInjectionResolver(diAdapter);

            InitOwinHost(app, diAdapter.Kernel);
            InitProfiler();
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

            app.UseRequestScopeContext();
            app.UseErrorPage(ErrorPageOptions.ShowAll);
            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(config);

            config.EnsureInitialized();
        }
    }
}